using System;
using System.Linq;
using Infrastructure.DataContracts;
using Model;

namespace DataContracts
{
    public interface IAttendanceRepository : 
        IRepository<Attendance>
    {
        IQueryable<Attendance> GetBySessionId(int id);
        Attendance GetByIds(Guid userId, int sessionId);
        void Delete(Guid userId, int sessionId);
    }
}