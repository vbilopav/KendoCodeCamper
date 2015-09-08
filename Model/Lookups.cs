using System;
using System.Collections.Generic;

namespace Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TimeSlot
    {
        public TimeSlot()
        {
            IsSessionSlot = true;
        }

        public int Id { get; set; }
        private bool IsSessionSlot { get; set; }  
        public DateTime Start { get; set; }
        public int DurationMinutes { get; set; }
    }

    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TagGroup
    {
        public string Tag { get; set; }
        public ICollection<int> SessionIds { get; set; }
    }
}
