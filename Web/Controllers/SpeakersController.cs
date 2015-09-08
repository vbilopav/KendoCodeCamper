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
    [RoutePrefix("Speakers")]
    public class SpeakersController : ApiController
    {
        private static readonly SpeakerService Service = new SpeakerService();

        [HttpGet]
        [Route("")]
        public async Task<Response<IEnumerable<SpeakersRetreiveAllResponse>>> RetreiveAll([FromUri]RetreiveAllSpeakerRequest request)
        {
            //throw new Exception("bumer!");
            //Thread.Sleep(10000);
            return await Service.RetreiveAllAsync(request);
        }

        [HttpGet]
        [Route("Details")]
        public async Task<Response<SpeakersRetreiveResponse>> Retreive([FromUri]RetreivePersonSpeakerRequest request)
        {
            //Thread.Sleep(5000);
            return await Service.RetreivePersonAsync(request);
        }

        [HttpGet]
        [Route("Rate")]
        public async Task<Response> Rate([FromUri]RateSpeakerRequest request)
        {
            //Thread.Sleep(5000);
            return await Service.RateSpeakerAsync(request);
        } 
    }
}