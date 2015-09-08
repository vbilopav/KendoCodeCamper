using System;

namespace Model
{
    public class Attendance
    {
        public Guid UserId { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int Rating { get; set; }     
        public string Text { get; set; }
    }
}
