using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repository
{
    public class SalesRepository : RepositoryBase<MemoryContext, Sales>, ISalesRepository
    {
        public SalesRepository(MemoryContext memoryContext)
            : base(memoryContext)
        {

        }

        public async Task<object> GetBest(bool trackChanges) =>
            await FindAll(trackChanges)
                    .GroupBy(d => d.Vehicle)
                    .Select(s => new
                    {
                        Vehicle = s.Key,
                        Count = s.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .FirstOrDefaultAsync();





    }
}