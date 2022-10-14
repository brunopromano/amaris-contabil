using AmarisContabil.Domain;
using AmarisContabil.Infrastructure.Interfaces;

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

        //public List<SaldoDiarioConsolidado> GerarSaldoDiario()
        //{

        //}
    }
}
