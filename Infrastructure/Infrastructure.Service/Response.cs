using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Infrastructure.Service
{
    [DataContract]
    public class Response
    {
        [DataMember]
        public IList<ResponseError> Errors { get; set; }

        [DataMember]
        public bool Success { get; set; }

        public Response()
        {
            Success = true;
        }

        public Response(params string[] errors)
        {
            Success = false;
            Errors = new List<ResponseError>();
            foreach (string s in errors) { Errors.Add(new ResponseError{ ErrorMessage = s}); }
        }

        public Response<string> ToStringResponse(string value = null)
        {
            return new Response<string>(value)
            {
                Errors = this.Errors,
                Success = this.Success
            };
        }
    }

    [DataContract]
    public class Response<T> : Response
    {
        [DataMember]
        public T Model { get; set; }

        public Response()
        {
            Success = true;
        }

        public Response(T model)
            : base()
        {
            Model = model;
        }
    }
}
