using AmarisContabil.Application;
using AmarisContabil.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AmarisContabil.WebApi.Controllers
{
    [ApiController]
    public class LancamentoController : Controller
    {
        private readonly LancamentoService _lancamentoService;

        public LancamentoController(LancamentoService lancamentoService)
        {
            _lancamentoService = lancamentoService;
        }

        [HttpPost]
        public void AdicionarLancamento([FromBody] LancamentoDto lancamentoDto)
        {
            _lancamentoService.AdicionarLancamento(lancamentoDto);
        }
    }
}
