using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Infrastructure.DataContracts
{
    public abstract class EntityFrameworkUnitOfWork : IUnitOfWork, IDisposable
    {
        protected DbContext DbContext;

        protected EntityFrameworkUnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract string GetSql();

        public int Commit()
        {
            try
            {
                return DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                e.Data["SQL"] = GetSql();
                throw e;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                e.Data["SQL"] = GetSql();
                throw e;
            }
        }
       
        public void Rollback()
        {
            foreach (var entry in DbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        {
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        }
                    case EntityState.Deleted:
                        {
                            entry.State = EntityState.Unchanged;
                            break;
                        }
                    case EntityState.Added:
                        {
                            entry.State = EntityState.Detached;
                            break;
                        }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                    DbContext = null;
                }
            }
        }
    }
}
