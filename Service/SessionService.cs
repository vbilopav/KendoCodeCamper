using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Service;
using Service.Model;
using DataAccess;

namespace Service
{
    public class SessionService
    {                
        private readonly IRequestResponseFactory _factory;

        public SessionService(IRequestResponseFactory factory = null)
        {
            _factory = factory ?? new RequestResponseFactory();
        }

        public async Task<Response<IEnumerable<SessionRetreiveAllResponse>>> RetreiveSessionsAsync(RetreiveAllSessionRequest request)
        {
            return await _factory.ProcessRequestAsync(async () =>
            {
                using (var uow = new CodeCamperUnitOfWork())
                {
                    /*
                    var sessions = uow.SessionsRepository.FindAll();

                    if (!string.IsNullOrEmpty(request.FilterExpression))                    
                        sessions = sessions.Where(s => s.Title.Contains(request.FilterExpression))                    
                     * */

                    return new List<SessionRetreiveAllResponse>() as IEnumerable<SessionRetreiveAllResponse>;
                }               
            });
        }
    }
}
