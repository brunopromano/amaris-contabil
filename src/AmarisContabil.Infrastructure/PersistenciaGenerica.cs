using AmarisContabil.Infrastructure.Interfaces;

namespace AmarisContabil.Infrastructure;

public class PersistenciaGenerica : IPersistenciaGenerica
{
    private readonly DataContext _dataContext;

    public PersistenciaGenerica(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void Adicionar<T>(T entity) where T : class
    {
        _dataContext.Add(entity);
    }

    public void Atualizar<T>(T entity) where T : class
    {
        _dataContext.Update(entity);
    }

    public void Remover<T>(T entity) where T : class
    {
        _dataContext.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }
}
