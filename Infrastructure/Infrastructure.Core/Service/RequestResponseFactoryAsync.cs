using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public partial class RequestResponseFactory
    {
        public async Task<TResponse> ProcessRequestAsync<TResponse>(Func<TResponse, Task<TResponse>> handler)    
            where TResponse : Response, new()
        {
            try
            {
                return await handler(new TResponse { Success = true });
            }
            catch (Exception ex)
            {
                return new TResponse
                {
                    Success = false,
                    Errors = new List<ResponseError>
                    {
                        new ResponseError { ErrorMessage = Log.ErrorRefMessage(ex)}
                    }
                };
            }
        }

        public async Task<TResponse> ProcessRequestAsync<TResponse>(Func<Task> handler)
            where TResponse : Response, new()
        {
            try
            {
                await handler();
                return new TResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new TResponse
                {
                    Success = false,
                    Errors = new List<ResponseError>
                    {
                        new ResponseError { ErrorMessage = Log.ErrorRefMessage(ex)}
                    }
                };
            }
        }

        public async Task<Response<TModel>> ProcessRequestAsync<TModel>(Func<Task<TModel>> handler)
        {
            try
            {
                return new Response<TModel>(await handler()) { Success = true };
            }
            catch (Exception ex)
            {
                return new Response<TModel>
                {
                    Success = false,
                    Errors = new List<ResponseError>
                    {
                        new ResponseError {ErrorMessage = Log.ErrorRefMessage(ex)}
                    }
                };
            }
        }

        public async Task<Response<TModel>> ProcessRequestAsync<TModel>(IUnitOfWork uow, Func<IUnitOfWork, Task<TModel>> handler)
        {
            try
            {
                var response = new Response<TModel>(await handler(uow)) { Success = true };
                await uow.CommitAsync();
                return response;
            }
            catch (Exception ex)
            {
                //uow.Rollback();
                return new Response<TModel>
                {
                    Success = false,
                    Errors = new List<ResponseError>
                    {
                        new ResponseError {ErrorMessage = Log.ErrorRefMessage(ex)}
                    }
                };
            }
        }
 
    }
}
