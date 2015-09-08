using System;
using System.Collections.Generic;
using Infrastructure.DataContracts;

namespace Infrastructure.Service
{
    public partial class RequestResponseFactory : IRequestResponseFactory
    {      
        public TResponse ProcessRequest<TResponse>(Func<TResponse, TResponse> handler) where TResponse : Response, new()
        {
            try
            {
                return handler(new TResponse { Success = true });
            }
            catch (Exception ex)
            {
                return new TResponse
                {
                    Success = false,
                    Errors = new List<ResponseError>
                    {
                        new ResponseError {ErrorMessage = Log.ErrorRefMessage(ex)}
                    }
                };
            }
        }

        public TResponse ProcessRequest<TResponse>(Action handler) where TResponse : Response, new()
        {
            try
            {
                handler();
                return new TResponse {Success = true};
            }
            catch (Exception ex)
            {
                return new TResponse
                {
                    Success = false,
                    Errors = new List<ResponseError>
                    {
                        new ResponseError {ErrorMessage = Log.ErrorRefMessage(ex)}
                    }
                };
            }
        }

        public Response<TModel> ProcessRequest<TModel>(Func<TModel> handler)
        {
            try
            {
                return new Response<TModel>(handler()) { Success = true };
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

        public Response<TModel> ProcessRequest<TModel>(IUnitOfWork uow, Func<IUnitOfWork, TModel> handler)
        {
            try
            {
                var response = new Response<TModel>(handler(uow)) { Success = true };
                uow.Commit();
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
