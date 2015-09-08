using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DataContracts;

namespace DataAccess
{
    public class PersonsRepository :
        EntityFrameworkRepository<Person, int>
    {
        public PersonsRepository(DbContext dbContext)
            : base(dbContext, speaker => speaker.Id)
        {
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await DbSet
                .Distinct()
                .ToListAsync();
        }

        public async Task<int> GetAvgRatingAsync(int id)
        {
            var ratings = await DbContext.Set<SpeakerRating>().Where(r => r.PersonId == id).ToListAsync();
            if (!ratings.Any()) return 0;
            int sum = ratings.Aggregate(0, (current, rating) => current + rating.Rating);
            return sum/ratings.Count;
        }

        public async Task RateAsync(Guid userId, int id, int rating)
        {
            var set = await DbContext.Set<SpeakerRating>().FindAsync(userId, id);
            if (set == null)
            {
                DbContext.Set<SpeakerRating>().Add(new SpeakerRating {PersonId = id, UserId = userId, Rating = rating});
            }
            else
            {
                set.Rating = rating;
                set.TimeStamp = DateTime.Now;
            }            
        }
    }
}
