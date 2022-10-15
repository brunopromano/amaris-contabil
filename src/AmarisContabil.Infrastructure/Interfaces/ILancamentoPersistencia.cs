using AmarisContabil.Domain;

namespace AmarisContabil.Infrastructure.Interfaces
{
    public interface ILancamentoPersistencia : IPersistenciaGenerica
    {
        Lancamento ObterLancamentoPorId(int idLancamento);
        List<Lancamento> ObterTodosLancamentos();
        Task<List<SaldoDiario>> GerarSaldoDiario();
    }
}
