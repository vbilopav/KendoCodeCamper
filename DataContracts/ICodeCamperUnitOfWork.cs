using System;
using Infrastructure.DataContracts;
using Model;

namespace DataContracts
{
    public interface ICodeCamperUnitOfWork : IUnitOfWork
    {
        IReadRepository<Room> Rooms { get; }
        IReadRepository<TimeSlot> TimeSlots { get; }
        IReadRepository<Track> Tracks { get; }

        IPersonsRepository Persons { get; }      
        ISessionsRepository Sessions { get; }
        IAttendanceRepository Attendance { get; }
    }
}
