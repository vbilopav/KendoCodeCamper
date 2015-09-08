using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Service;
using Service.Model;
using DataAccess;

namespace Service
{
    public class SpeakerService
    {                
        private readonly IRequestResponseFactory _factory;

        public SpeakerService(IRequestResponseFactory factory = null)
        {
            _factory = factory ?? new RequestResponseFactory();
        }

        public async Task<Response<IEnumerable<SpeakersRetreiveAllResponse>>> RetreiveAllAsync(RetreiveAllSpeakerRequest request)
        {
            return await _factory.ProcessRequestAsync(async () =>
            {
                using (var uow = new CodeCamperUnitOfWork())
                {
                    var speakers = await uow.PersonsRepository.GetPersonsAsync();
                    return speakers.OrderBy(s => s.FirstName).Select(s =>
                        new SpeakersRetreiveAllResponse
                        {
                            Id = s.Id,
                            FirstName = s.FirstName.ToUpper(),
                            LastName = s.LastName.ToUpper(),
                            ImageSource = string.Concat(ServiceConfigManager.Config.ImageUrl, s.ImageSource),
                        });
                }               
            });
        }

        public async Task<Response<SpeakersRetreiveResponse>> RetreivePersonAsync(RetreivePersonSpeakerRequest request)
        {
            return await _factory.ProcessRequestAsync(async () =>
            {
                using (var uow = new CodeCamperUnitOfWork())
                {
                    var person = await uow.PersonsRepository.FindByAsync(request.Id);
                    IList<SpeakersSession> sessions = (from s in await uow.SessionsRepository.FindByPersonAsync(person)
                        select new SpeakersSession
                        {
                            Id = s.Id, 
                            Title = s.Title, 
                            From = s.TimeSlot.Start.ToString("ddd hh:mm tt")
                        }).ToList();
                    return new SpeakersRetreiveResponse
                    {
                        Id = person.Id,
                        FullName = string.Concat(person.FirstName, " ", person.LastName),
                        ImageSource = string.Concat(ServiceConfigManager.Config.ImageUrl, person.ImageSource),
                        Email = person.Email,
                        Blog = person.Blog,
                        Twitter = person.Twitter,
                        Bio = person.Bio,
                        Sessions = sessions,
                        AvgRating = await uow.PersonsRepository.GetAvgRatingAsync(request.Id)
                    };
                }
            });
        }

        public async Task<Response> RateSpeakerAsync(RateSpeakerRequest request)
        {
            return await _factory.ProcessRequestAsync<Response>(async () =>
            {
                request.UserId = Guid.Parse(HttpContext.Current.User.Identity.Name);
                using (var uow = new CodeCamperUnitOfWork())
                {
                    await uow.PersonsRepository.RateAsync(request.UserId, request.Id, request.Rating);
                    await uow.CommitAsync();
                }
            });
        }
    }
}
