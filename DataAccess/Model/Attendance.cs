using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public IList<Session> Session { get; set; }
    }
}
