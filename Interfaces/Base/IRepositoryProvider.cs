namespace Interfaces.Base
{
    public interface IRepositoryProvider
    {
        // get standard repository for type TEntity
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;

        // get custom repository, based on interface
        TRepository GetCustomRepository<TRepository>() where TRepository : class;
    }
}