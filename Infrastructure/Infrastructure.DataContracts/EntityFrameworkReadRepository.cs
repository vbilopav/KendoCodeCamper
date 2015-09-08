using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataContracts
{
    public class EntityFrameworkReadRepository<TEntity> :
        BaseEntityFrameworkRepository<TEntity>,
        IReadRepository<TEntity>
        where TEntity : class
    {
        public virtual IEnumerable<TEntity> FindAll()
        {
            return DbSet.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public EntityFrameworkReadRepository(DbContext dbContext)
            : base(dbContext) { }
    }

    public class EntityFrameworkReadRepository<TEntity, TId> :
        BaseEntityFrameworkRepository<TEntity>,
        IReadRepository<TEntity, TId>
        where TEntity : class
    {
        public virtual TEntity FindBy(TId id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<TEntity> FindByAsync(TId id)
        {
            return await DbSet.FindAsync(id);
        }

        public EntityFrameworkReadRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}