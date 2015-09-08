using System.Collections.Generic;

namespace DataAccess
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageSource { get; set; }

        public string Email { get; set; }
        public string Blog { get; set; }
        public string Twitter { get; set; }
        public string Bio { get; set; }

        public virtual IList<Session> Sessions { get; set; }
        public virtual IList<Attendance> AttendanceList { get; set; }
    }
}
