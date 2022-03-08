using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using Contracts;

namespace ApiCsv.Services
{
    public class Uploads : IUploads
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Uploads(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            string fileName = $"{_webHostEnvironment.ContentRootPath}\\FileStorage\\{file.FileName}";


            using (FileStream fileStream = File.Create(fileName))
            {
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }


            //var sales = SaveSalesFromFile(fileName);
            
            return fileName;
        }
    }
}
