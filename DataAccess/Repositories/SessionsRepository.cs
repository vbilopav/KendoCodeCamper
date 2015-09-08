using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DataContracts;

namespace DataAccess
{   
    public class SessionsRepository :
        EntityFrameworkRepository<Session, int>
    {
        public SessionsRepository(DbContext dbContext) :
            base(dbContext, entity => entity.Id) { }

        public virtual async Task<IList<Session>> FindByPersonAsync(Person person)
        {            
            await 
                DbContext
                .Entry(person)
                .Collection(s => s.Sessions)
                .Query()              
                .Include(s => s.TimeSlot)                 
                .OrderBy(s => s.TimeSlot.Start)
                .LoadAsync();
            return person.Sessions;
        }

        public virtual async Task<IList<Session>> SearchAsync(
            string expression, 
            IEnumerable<int> tracks, 
            IEnumerable<string> tags)
        {
            var q = 
                DbContext                
                .Set<Session>()
                .Include(s => s.Speaker)
                .Include(s => s.Track)
                .Include(s => s.TimeSlot)  
                .Include(s => s.Room);

            if (!string.IsNullOrEmpty(expression))
            {
                q = q.Where(s =>
                    s.Title.Contains(expression) ||
                    s.Description.Contains(expression) ||
                    s.Code.Contains(expression));
            }

            IEnumerable<int> t = tracks as int[] ?? tracks.ToArray();
            if (tracks != null && t.Any())
            {
                q = q.Join(t, session => session.TrackId, track => track, (session, track) => session);
            }

            if (tags != null && tags.Any())
            {
                IEnumerable<int> sessionIds =
                    GetTagGroups()
                    .SelectMany(sg => sg.SessionIds )
                    .Distinct();

                IEnumerable<int> s = sessionIds as int[] ?? sessionIds.ToArray();
                if (s.Any())
                {
                    q = q.Join(s, session => session.Id, sid => sid, (session, sid) => session);
                }
            }

            await q.LoadAsync();
            return await q.ToListAsync();
        }

        public IEnumerable<TagGroup> GetTagGroups()
        {
            return 
                DbSet
                .Select(s => new { s.Tags, s.Id })
                .ToArray()              
                .SelectMany(s => 
                    s.Tags.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(t => new { Tag = t, s.Id }))               
                .GroupBy(g => g.Tag, data => data.Id)
                .Select(tg => new TagGroup
                {
                    Tag = tg.Key,
                    SessionIds = tg.Distinct().ToArray(),
                })
                .OrderBy(tg => tg.Tag);            
        }

        public IEnumerable<TagCount> GetTagCounts()
        {
            return
                DbSet
                .Select(s => new { s.Tags })
                .ToArray()
                .SelectMany(s => s.Tags.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                .GroupBy(g => g)
                .Select(group => new TagCount
                {
                    Tag = group.Key,
                    Count = group.Count(),
                })
                .OrderByDescending(tg => tg.Count);
        }   
     
    }
}
