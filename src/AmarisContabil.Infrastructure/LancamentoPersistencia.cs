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

        public List<Lancamento> ObterTodosLancamentos()
        {
            IQueryable<Lancamento> query = _dataContext.Lancamentos;

            return query.ToList();
        }
    }
}
