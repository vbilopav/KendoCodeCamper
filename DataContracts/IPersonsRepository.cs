using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.DataContracts;
using Model;

namespace DataContracts
{
    public interface IPersonsRepository : IRepository<Person>
    {
        Task<IEnumerable<PersonDto>> GetSpeakersAsync();
    }
}
