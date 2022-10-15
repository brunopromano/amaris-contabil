using AmarisContabil.Domain;
using AmarisContabil.Infrastructure.Interfaces;

namespace AmarisContabil.Application
{
    public class RelatorioService : IRelatorioService
    {
        private readonly ILancamentoPersistencia _lancamentoPersistencia;

        public RelatorioService(ILancamentoPersistencia lancamentoPersistencia)
        {
            _lancamentoPersistencia = lancamentoPersistencia;
        }

        public async Task<List<SaldoDiario>> GerarSaldoConsolidadoPorDia()
        {
            return await _lancamentoPersistencia.GerarSaldoDiario();
        }
    }
}
