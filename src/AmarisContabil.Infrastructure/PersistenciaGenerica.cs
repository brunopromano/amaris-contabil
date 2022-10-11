using AmarisContabil.Infrastructure.Interfaces;

namespace AmarisContabil.Infrastructure;

public class PersistenciaGenerica : IPersistenciaGenerica
{
    private readonly DataContext _dataContext;

    public void Adicionar<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public void Atualizar<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public void Remover<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
