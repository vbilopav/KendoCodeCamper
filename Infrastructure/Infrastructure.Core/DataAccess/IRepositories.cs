namespace Infrastructure.DataAccess
{
    public interface IRepository<TEntity> :
        ICreateRepository<TEntity, int>,
        IReadRepository<TEntity>,
        IReadRepository<TEntity, int>,
        IUpdateRepository<TEntity, int>,
        IDeleteRepository<TEntity, int> { }

    public interface IRepository<TEntity, TId> :
        ICreateRepository<TEntity, TId>,
        IReadRepository<TEntity>,
        IReadRepository<TEntity, TId>,
        IUpdateRepository<TEntity, TId>,
        IDeleteRepository<TEntity, TId> { }

    public interface ICrudRepository<TEntity, out TReturn> :
       ICreateRepository<TEntity, TReturn>,
       IReadRepository<TEntity>,
       IUpdateRepository<TEntity, TReturn>,
       IDeleteRepository<TEntity, TReturn> { }

    public interface ICrudRepositoryByRef<TEntity, out TReturn> :
       ICreateRepositoryByRef<TEntity, TReturn>,
       IReadRepository<TEntity>,
       IUpdateRepositoryByRef<TEntity, TReturn>,
       IDeleteRepositoryByRef<TEntity, TReturn> { }
}
