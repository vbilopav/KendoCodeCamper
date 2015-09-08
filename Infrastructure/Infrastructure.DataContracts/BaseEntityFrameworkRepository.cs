using System.Data.Entity;

namespace Infrastructure.DataContracts
{
    public abstract class BaseEntityFrameworkRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseEntityFrameworkRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }
    }
}