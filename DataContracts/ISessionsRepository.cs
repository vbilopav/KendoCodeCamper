using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.DataContracts;
using Model;

namespace DataContracts
{
    public interface ISessionsRepository : IRepository<Session>
    {       
        IQueryable<SessionBrief> GetSessionBriefs();
        IEnumerable<TagGroup> GetTagGroups();
    }
}
