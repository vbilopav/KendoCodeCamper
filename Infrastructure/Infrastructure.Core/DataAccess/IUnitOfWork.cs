using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public interface IUnitOfWork
    {        
        int Commit();
        Task<int> CommitAsync();
        void Rollback();
    }    
}
