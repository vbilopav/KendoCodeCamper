namespace Infrastructure.DataAccess
{
    public interface IUpdateRepository<in TEntity, out TReturn>
    {
        TReturn Save(TEntity entity);
    }

    public interface IUpdateRepositoryByRef<TEntity, out TReturn>
    {
        TReturn Save(ref TEntity entity);
    }
}
