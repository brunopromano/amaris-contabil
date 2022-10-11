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
    }
}
