namespace AmarisContabil.Infrastructure.Interfaces
{
    internal interface IPersistenciaGenerica
    {
        void Adicionar<T>(T entity) where T : class;
        void Atualizar<T>(T entity) where T : class;
        void Remover<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
