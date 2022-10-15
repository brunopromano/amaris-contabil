using AmarisContabil.Application;
using AmarisContabil.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AmarisContabil.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : Controller
    {
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet]
        public List<SaldoDiario> ObterRelatorioConsolidadoPorDia()
        {
            return _relatorioService.GerarSaldoConsolidadoPorDia().Result;
        }
    }
}
