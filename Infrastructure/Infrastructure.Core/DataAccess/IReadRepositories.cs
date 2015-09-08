using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public interface IReadRepository<TEntity, in TId>
    {
        TEntity FindBy(TId id);
        Task<TEntity> FindByAsync(TId id);
    }

    public interface IReadRepository<TEntity>
    {
        IEnumerable<TEntity> FindAll();
        Task<IEnumerable<TEntity>> FindAllAsync();
    }
}
