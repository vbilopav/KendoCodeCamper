using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class Session
    {
        public int Id { get; set; }    
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public int SpeakerId { get; set; }
        public virtual Person Speaker { get; set; }

        public int TrackId { get; set; }
        public virtual Track Track { get; set; }

        public int TimeSlotId { get; set; }
        public virtual TimeSlot TimeSlot { get; set; }
        
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public string Level { get; set; }
        public string Tags { get; set; }

        public virtual IList<Attendance> AttendanceList { get; set; }
    }
}
