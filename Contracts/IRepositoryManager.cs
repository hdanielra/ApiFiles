using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {

        ISalesRepository Sales { get; }


        Task Save();
    }
}