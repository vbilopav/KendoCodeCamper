using System;

namespace DataAccess
{
    public class SpeakerRating
    {
        public Guid UserId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int Rating { get; set; }     
        public DateTime TimeStamp { get; set; }

        public SpeakerRating()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
