using System;
using System.Data.Entity;
using System.Linq;
using Infrastructure.DataContracts;

namespace DataAccess
{   
    public class AttendanceRepository :              
        EntityFrameworkRepository<Attendance>
    {                
        public AttendanceRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /*
        public IQueryable<Attendance> GetBySessionId(int id)
        {
            return DbSet.Where(ps => ps.SessionId == id);
        }

        public Attendance GetByIds(Guid userId, int sessionId)
        {
            return DbSet.FirstOrDefault(ps => ps.UserId == userId && ps.SessionId == sessionId);
        }

        public void Delete(Guid userId, int sessionId)
        {            
            Remove(new Attendance { UserId = userId, SessionId = sessionId });
        }
        */
    }
}
