using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Infrastructure.Service;
using Service;
using Service.Model;

namespace Web.Controllers
{
    [RoutePrefix("Sessions")]
    public class SessionsController : ApiController
    {
        private static readonly SessionService _service;

        static SessionsController()
        {
            _service = new SessionService();
        }

        [HttpGet]
        [Route("")]
        public async Task<Response<IEnumerable<SessionRetreiveAllResponse>>> Search(RetreiveAllSessionRequest request)
        {
            return await _service.RetreiveSessionsAsync(request);            
        }
    }
}