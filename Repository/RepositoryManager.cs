using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {

        // --------------------------------------------------------
        public MemoryContext _memoryContext;
        // --------------------------------------------------------



        // --------------------------------------------------------
        internal ISalesRepository _sales;
        // --------------------------------------------------------



        // --------------------------------------------------------
        public RepositoryManager(MemoryContext memoryContext)
        {
            _memoryContext = memoryContext;
        }
        // --------------------------------------------------------





        // --------------------------------------------------------
        public ISalesRepository Sales
        {
            get
            {
                if (_sales == null)
                    _sales = new SalesRepository(_memoryContext);
                return _sales;
            }
        }
        // --------------------------------------------------------





        // --------------------------------------------------------
        public async Task Save()
        {
            try
            {
                await _memoryContext.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
        }
        // --------------------------------------------------------
    }
}