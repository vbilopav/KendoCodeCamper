using System;
using Infrastructure.DataContracts;

namespace DataAccess
{
    public class CodeCamperUnitOfWork : 
        EntityFrameworkUnitOfWork        
    {
        public override string GetSql()
        {
            return (DbContext as CodeCamperDbContext).GetSql();
        }

        public CodeCamperUnitOfWork(string nameOrConnectionString = "CodeCamper")
            : base(new CodeCamperDbContext(nameOrConnectionString))
        {
            RoomsRepository = new EntityFrameworkReadRepository<Room>(DbContext);
            TimeSlotsRepository = new EntityFrameworkReadRepository<TimeSlot>(DbContext);
            TracksRepository = new EntityFrameworkReadRepository<Track>(DbContext);
            PersonsRepository = new PersonsRepository(DbContext);
            SessionsRepository = new SessionsRepository(DbContext);
            AttendanceRepository = new AttendanceRepository(DbContext);
        }

        public IReadRepository<Room> RoomsRepository { get; private set; }
        public IReadRepository<TimeSlot> TimeSlotsRepository { get; private set; }
        public IReadRepository<Track> TracksRepository { get; private set; }
        public PersonsRepository PersonsRepository { get; private set; }
        public SessionsRepository SessionsRepository { get; private set; }
        public AttendanceRepository AttendanceRepository { get; private set; }       
    }
}
