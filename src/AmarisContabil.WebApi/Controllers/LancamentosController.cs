using AmarisContabil.Application;
using AmarisContabil.Domain;
using AmarisContabil.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AmarisContabil.WebApi.Controllers
{
    [ApiController]
    public class LancamentosController : Controller
    {
        private readonly LancamentoService _lancamentoService;

        public LancamentosController(LancamentoService lancamentoService)
        {
            _lancamentoService = lancamentoService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarLancamento([FromBody] LancamentoDto lancamentoDto)
        {
            try
            {
                bool lancamentoInserido = await _lancamentoService.AdicionarLancamento(lancamentoDto);

                return Created(string.Empty, lancamentoInserido);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível adicionar o lançamento!");
            }
        }
    }
}
