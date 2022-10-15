using AmarisContabil.Domain;
using AmarisContabil.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace AmarisContabil.Infrastructure
{
    public class LancamentoPersistencia : PersistenciaGenerica, ILancamentoPersistencia
    {
        private readonly DataContext _dataContext;

        public LancamentoPersistencia(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public Lancamento ObterLancamentoPorId(int idLancamento)
        {
            IQueryable<Lancamento> query =  _dataContext.Lancamentos;

            query = query.Where(lancamento => lancamento.Id == idLancamento);

            return query.FirstOrDefault();
        }

        public List<Lancamento> ObterTodosLancamentos()
        {
            IQueryable<Lancamento> query = _dataContext.Lancamentos;

            return query.ToList();
        }

        public async Task<List<SaldoDiario>> GerarSaldoDiario()
        {
            var dados = await _dataContext.Lancamentos
                .GroupBy(g => g.DataLancamento)
                .Select(diaAgrupado => new SaldoDiario
                {
                    DiaConsolidado = diaAgrupado.Key,
                    TotalDebito = diaAgrupado.Sum(t => t.TipoLancamento == 'D' ? t.ValorBrl : 0),
                    TotalCredito = diaAgrupado.Sum(t => t.TipoLancamento == 'C' ? t.ValorBrl : 0),
                    SaldoDia = diaAgrupado.Sum(t => t.TipoLancamento == 'C' ? t.ValorBrl : -t.ValorBrl)
                })
                .ToListAsync();

            return dados;
        }
    }
}
