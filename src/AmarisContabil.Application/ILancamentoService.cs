using AmarisContabil.Domain;
using AmarisContabil.Domain.Dtos;

namespace AmarisContabil.Application
{
    public interface ILancamentoService
    {
        Lancamento ObterLancamentoPorId(int idLancamento);
        List<Lancamento> ObterTodosLancamentos();
        Task<Lancamento> AdicionarLancamento(LancamentoDto lancamentoDto);
        Task<Lancamento> AtualizarLancamento(LancamentoEditadoDto lancamentoEditadoDto);
        Task<bool> ExcluirLancamento(int idLancamento);
    }
}
