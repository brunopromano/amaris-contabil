using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AmarisContabil.WebApi.Controllers
{
    /// <summary>
    /// Endpoint api/v1/logs
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly string _logDirectory;
        private readonly string _logFilePrefix;

        // Log line timestamp format: "yyyy-MM-dd HH:mm:ss.fff zzz" → 30 chars
        // Example: "2026-04-19 10:30:45.123 +00:00 [INF] message"
        private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff zzz";
        private const int TimestampLength = 30;

        public LogsController(IConfiguration configuration)
        {
            // Rolling log path from Serilog config: /app/logs/amaris-contabil-.log
            var configuredPath = configuration["Serilog:WriteTo:1:Args:path"] ?? "/app/logs/amaris-contabil-.log";
            _logDirectory  = Path.GetDirectoryName(configuredPath) ?? "/app/logs";
            _logFilePrefix = Path.GetFileNameWithoutExtension(configuredPath); // "amaris-contabil-"
        }

        /// <summary>
        /// Retorna entradas do arquivo de log com suporte a paginação por tail ou filtro por intervalo de datas.
        /// </summary>
        /// <param name="tail">Número das últimas N entradas de log a retornar.</param>
        /// <param name="from">Data/hora inicial do filtro (UTC). Exemplo: 2026-04-01 00:00:00</param>
        /// <param name="to">Data/hora final do filtro (UTC). Exemplo: 2026-04-01 23:59:59</param>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] int? tail,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            if (tail.HasValue && tail.Value <= 0)
                return BadRequest(new { message = "O parâmetro 'tail' deve ser um inteiro positivo." });

            var logFilePath = ResolveLogFilePath();

            if (logFilePath == null)
                return NotFound(new { message = "Arquivo de log não encontrado.", directory = _logDirectory });

            List<string> lines;
            try
            {
                // FileShare.ReadWrite allows reading while Serilog is writing
                using var fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var reader = new StreamReader(fs);
                lines = new List<string>();
                string? line;
                while ((line = reader.ReadLine()) != null)
                    lines.Add(line);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Falha ao ler o arquivo de log.", detail = ex.Message });
            }

            if (from.HasValue || to.HasValue)
            {
                var fromUtc = from.HasValue ? DateTime.SpecifyKind(from.Value, DateTimeKind.Utc) : (DateTime?)null;
                var toUtc   = to.HasValue   ? DateTime.SpecifyKind(to.Value,   DateTimeKind.Utc) : (DateTime?)null;
                return Ok(FilterByDateRange(lines, fromUtc, toUtc));
            }

            if (tail.HasValue)
            {
                var entries = GroupIntoEntries(lines);
                var offset  = Math.Max(0, entries.Count - tail.Value);
                return Ok(FlattenEntries(entries.GetRange(offset, entries.Count - offset)));
            }

            return Ok(lines);
        }

        /// <summary>
        /// Resolves the most recent log file matching the configured prefix in the log directory.
        /// Serilog rolling files are named: {prefix}{yyyyMMdd}.log
        /// </summary>
        private string? ResolveLogFilePath()
        {
            if (!Directory.Exists(_logDirectory))
                return null;

            var files = Directory.GetFiles(_logDirectory, _logFilePrefix + "*.log");
            return files.OrderByDescending(f => f).FirstOrDefault();
        }

        private static bool TryParseTimestamp(string line, out DateTimeOffset timestamp)
        {
            if (line != null && line.Length >= TimestampLength &&
                DateTimeOffset.TryParseExact(
                    line.Substring(0, TimestampLength),
                    TimestampFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out timestamp))
                return true;

            timestamp = default;
            return false;
        }

        /// <summary>
        /// Groups raw log lines into logical entries. Each entry starts with a timestamped line;
        /// continuation lines (e.g. stack traces) are kept attached to their parent entry.
        /// </summary>
        private static List<List<string>> GroupIntoEntries(List<string> lines)
        {
            var entries = new List<List<string>>();
            List<string>? current = null;

            foreach (var line in lines)
            {
                if (TryParseTimestamp(line, out _))
                {
                    current = new List<string> { line };
                    entries.Add(current);
                }
                else
                {
                    current?.Add(line);
                }
            }

            return entries;
        }

        private static List<string> FlattenEntries(List<List<string>> entries)
        {
            var result = new List<string>();
            foreach (var entry in entries)
                result.AddRange(entry);
            return result;
        }

        private static List<string> FilterByDateRange(List<string> lines, DateTime? fromUtc, DateTime? toUtc)
        {
            var result  = new List<string>();
            bool inRange = false;

            foreach (var line in lines)
            {
                if (TryParseTimestamp(line, out var ts))
                {
                    var lineUtc = ts.UtcDateTime;
                    inRange = (!fromUtc.HasValue || lineUtc >= fromUtc.Value) &&
                              (!toUtc.HasValue   || lineUtc <= toUtc.Value);
                }

                if (inRange)
                    result.Add(line);
            }

            return result;
        }
    }
}
