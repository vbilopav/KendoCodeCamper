using System;
using System.Collections.Generic;

namespace Service.Model
{
    public class RetreiveAllSpeakerRequest
    {                
    }

    public class RetreivePersonSpeakerRequest
    {
        public int Id { get; set; }
    }

    public class SpeakersRetreiveAllResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageSource { get; set; }       
    }

    public class SpeakersRetreiveResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageSource { get; set; }
        public string Email { get; set; }
        public string Blog { get; set; }
        public string Twitter { get; set; }
        public string Bio { get; set; }
        public int AvgRating { get; set; }

        public IEnumerable<SpeakersSession> Sessions { get; set; }
    }

    public class SpeakersSession
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string From { get; set; }
    }

    public class RateSpeakerRequest
    {
        public Guid UserId { get; set; }
        public int Id { get; set; }
        public int Rating { get; set; }
    }
}
