using AmarisContabil.Application;
using AmarisContabil.Domain;
using AmarisContabil.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmarisContabil.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LancamentosController : Controller
    {
        private readonly ILancamentoService _lancamentoService;

        public LancamentosController(ILancamentoService lancamentoService)
        {
            _lancamentoService = lancamentoService;
        }

        [HttpGet]
        public List<Lancamento> ObterTodosLancamentos()
        {
            return _lancamentoService.ObterTodosLancamentos();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarLancamento([FromBody] LancamentoDto lancamentoDto)
        {
            try
            {
                Lancamento lancamentoInserido = await _lancamentoService.AdicionarLancamento(lancamentoDto);

                return Created(string.Empty, lancamentoInserido);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível adicionar o lançamento!");
            }
        }
    }
}
