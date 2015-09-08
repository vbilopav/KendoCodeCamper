namespace Infrastructure.DataAccess
{
    public interface IDeleteRepository<in TEntity, out TReturn>
    {
        TReturn Remove(TEntity entity);
    }

    public interface IDeleteRepositoryByRef<TEntity, out TReturn>
    {
        TReturn Remove(ref TEntity entity);
    }

    public interface IDeleteRepository<in TEntity1, in TEntity2, out TReturn>
    {
        TReturn Remove(TEntity1 entity1, TEntity2 entity2);
    }

    public interface IDeleteRepositoryByRef<TEntity1, in TEntity2, out TReturn>
    {
        TReturn Remove(ref TEntity1 entity1, TEntity2 entity2);
    }
}
