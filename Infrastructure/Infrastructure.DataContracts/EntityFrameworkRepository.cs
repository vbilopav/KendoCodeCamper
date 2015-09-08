using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataContracts
{   
    public class EntityFrameworkRepository<TEntity, TId> :
        EntityFrameworkReadRepository<TEntity, TId>,
        IRepository<TEntity, TId>
        where TEntity : class
    {
        protected readonly Func<TEntity, TId> IdentityFunc;

        public EntityFrameworkRepository(DbContext dbContext, Func<TEntity, TId> identityFunc)
            : base(dbContext)
        {
            IdentityFunc = identityFunc;
        }

        public virtual TId Add(TEntity entity)
        {
            DbEntityEntry<TEntity> dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
            return IdentityFunc(entity);
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return DbSet.ToList<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual TId Save(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            return IdentityFunc(entity);
        }

        public virtual TId Remove(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            return IdentityFunc(entity);
        }
    }


    public class EntityFrameworkRepository<TEntity> :        
        EntityFrameworkReadRepository<TEntity, int>,
        IRepository<TEntity>
    where TEntity : class
    {
     
        public EntityFrameworkRepository(DbContext dbContext)
            : base(dbContext) {}

        public virtual int Add(TEntity entity)
        {
            DbEntityEntry<TEntity> dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
            return 0;
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return DbSet.ToList<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual int Save(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            return 0;
        }

        public virtual int Remove(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            return 0;
        }
    }
}
