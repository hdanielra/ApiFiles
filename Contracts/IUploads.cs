using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUploads
    {
       public Task<string> UploadFile(IFormFile file);
    }
}
