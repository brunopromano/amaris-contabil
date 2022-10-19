using AmarisContabil.Domain;

namespace AmarisContabil.Application
{
    public interface IRelatorioService
    {
        Task<List<SaldoDiario>> GerarSaldoConsolidadoPorDia();
    }
}
