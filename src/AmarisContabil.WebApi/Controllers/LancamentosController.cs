using AmarisContabil.Application;
using AmarisContabil.Domain;
using AmarisContabil.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [Route("{idLancamento:int}")]
        public IActionResult ObterLancamentoPorId(int idLancamento)
        {
            try
            {
                Lancamento lancamento = _lancamentoService.ObterLancamentoPorId(idLancamento);

                if (lancamento != null)
                    return Ok(lancamento);

                return NotFound($"Não foi encontrado o Lançamento com o ID {idLancamento}");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult ObterTodosLancamentos()
        {
            try
            {
                return Ok(_lancamentoService.ObterTodosLancamentos());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarLancamento([FromBody] LancamentoDto lancamentoDto)
        {
            try
            {
                Lancamento lancamentoInserido = await _lancamentoService.AdicionarLancamento(lancamentoDto);

                return Created(string.Empty, lancamentoInserido);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível adicionar o lançamento!");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarLancamento([FromBody] LancamentoEditadoDto lancamentoEditadoDto)
        {
            try
            {
                Lancamento lancamentoAtualizado = await _lancamentoService.AtualizarLancamento(lancamentoEditadoDto);

                return Ok(lancamentoAtualizado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{idLancamento:int}")]
        public IActionResult ExcluirLancamento(int idLancamento)
        {
            try
            {
                bool lancamentoExcluido = _lancamentoService.ExcluirLancamento(idLancamento).Result;

                if (lancamentoExcluido)
                    return Ok();

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
