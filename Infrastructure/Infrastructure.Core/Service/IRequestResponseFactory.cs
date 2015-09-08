using Infrastructure.DataAccess;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IRequestResponseFactory
    {
        TResponse ProcessRequest<TResponse>(Func<TResponse, TResponse> handler) where TResponse : Response, new();
        TResponse ProcessRequest<TResponse>(Action handler) where TResponse : Response, new();

        Response<TModel> ProcessRequest<TModel>(Func<TModel> handler);
        Response<TModel> ProcessRequest<TModel>(IUnitOfWork uow, Func<IUnitOfWork, TModel> handler);
        
        Task<TResponse> ProcessRequestAsync<TResponse>(Func<TResponse, Task<TResponse>> handler) where TResponse : Response, new();
        Task<TResponse> ProcessRequestAsync<TResponse>(Func<Task> handler) where TResponse : Response, new();
        
        Task<Response<TModel>> ProcessRequestAsync<TModel>(Func<Task<TModel>> handler);
        Task<Response<TModel>> ProcessRequestAsync<TModel>(IUnitOfWork uow, Func<IUnitOfWork, Task<TModel>> handler);
    }
}