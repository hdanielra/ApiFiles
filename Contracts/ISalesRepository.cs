using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISalesRepository : IRepositoryBase<Sales>
    {
        public Task<object> GetBest(bool trackChanges);
    }
}
