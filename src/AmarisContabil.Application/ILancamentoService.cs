using AmarisContabil.Domain;
using AmarisContabil.Domain.Dtos;

namespace AmarisContabil.Application
{
    public interface ILancamentoService
    {
        Task<Lancamento> AdicionarLancamento(LancamentoDto lancamentoDto);
    }
}
