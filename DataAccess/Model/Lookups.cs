using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TimeSlot
    {
        public int Id { get; set; }
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

    public class TagCount
    {
        public string Tag { get; set; }
        public int Count { get; set; }
    }    
}
