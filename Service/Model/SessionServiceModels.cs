using System;
using System.Collections.Generic;

namespace Service.Model
{
    public class RetreiveAllSessionRequest
    {
        public string FilterExpression { get; set; }
        public IEnumerable<int> Tracks { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public int? Take { get; set; }
        public int? Skip { get; set; }
    }

    public class SessionRetreiveAllResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string SpeakerId { get; set; }
        public string SpeakerFullName { get; set; }
        public string SpeakerImageSource { get; set; }

        public string Track { get; set; }
        public string From { get; set; }
        public string Room { get; set; }
        public string Level { get; set; }
        public string Tags { get; set; }
    }

    public class RetreiveSessionRequest
    {
        public int Id { get; set; }
    }
}
