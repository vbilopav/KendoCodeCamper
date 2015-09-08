namespace Infrastructure.DataAccess
{
    public interface ICreateRepository<in TEntity, out TReturn>
    {
        TReturn Add(TEntity entity);
    }

    public interface ICreateRepositoryByRef<TEntity, out TReturn>
    {
        TReturn Add(ref TEntity entity);
    }

    public interface ICreateRepository<in TEntity1, in TEntity2, out TReturn>
    {
        TReturn Add(TEntity1 entity1, TEntity2 entity2);
    }

    public interface ICreateRepositoryByRef<TEntity1, in TEntity2, out TReturn>
    {
        TReturn Add(ref TEntity1 entity1, TEntity2 entity2);
    }
}
