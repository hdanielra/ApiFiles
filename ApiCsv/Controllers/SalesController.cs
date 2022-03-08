using Contracts;
using Entities.Models;
using Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;





namespace ApiCsv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUploads _uploads;
        private readonly IConvertFile _convertFile;




        // ------------------------------------------------------------------------------------
        public SalesController(IRepositoryManager repositoryManager,
                               IUploads uploads,
                               IConvertFile convertFile)
        {
            _repositoryManager = repositoryManager;
            _uploads = uploads;
            _convertFile = convertFile;
        }
        // ------------------------------------------------------------------------------------





        // ------------------------------------------------------------------------------------
        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<List<Sales>>> Get()
        {
            try
            {
                var sales = await _repositoryManager.Sales.FindAll(false).ToListAsync();
                return Ok(sales);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // ------------------------------------------------------------------------------------






        // ------------------------------------------------------------------------------------
        // GET: api/Sales/GetData
        [Route("GetData")]
        [HttpGet]
        public async Task<ActionResult<DataResponse>> GetData()
        {
            DataResponse dataResponse = new();

            try
            {
                var sales = await _repositoryManager.Sales.FindAll(false).ToListAsync();

                dataResponse.Message = "Successfully! All records has been fetched.";
                dataResponse.Status = "Success";
                dataResponse.Data = sales;
                dataResponse.MostSold = await _repositoryManager.Sales.GetBest(false);

                return Ok(dataResponse);
            }
            catch (System.Exception ex)
            {
                dataResponse.Message = "Error! " + ex.Message;
                dataResponse.Status = "Error";
                return BadRequest(dataResponse);
            }
        }
        // ------------------------------------------------------------------------------------





        // ------------------------------------------------------------------------------------
        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var sale = await _repositoryManager.Sales
                        .FindByCondition(s => s.DealNumber.Equals(Id), false)
                        .FirstOrDefaultAsync();

            if (sale == null)
            {
                return BadRequest();
            }

            return Ok(sale);
        }
        // ------------------------------------------------------------------------------------








        // ------------------------------------------------------------------------------------
        // GET: api/Sales/GetBestSeller
        [Route("GetBestSeller")]
        [HttpGet]
        public async Task<ActionResult> GetBestSeller()
        {

            var bestSeller = await _repositoryManager.Sales.GetBest(false);

            if (bestSeller == null)
            {
                return BadRequest();
            }

            return Ok(bestSeller);
        }
        // ------------------------------------------------------------------------------------






        // ------------------------------------------------------------------------------------
        [Route("UploadFileCsv")]
        [HttpPost]
        public async Task<ActionResult> UploadFileCsv()
        {
            var httpRequest = Request.Form;
            IFormFile file = httpRequest.Files[0];

            var fileName = await _uploads.UploadFile(file);
            var sales = SaveSalesFromFile(fileName);

            return Ok(sales);

        }
        // ------------------------------------------------------------------------------------




        // ------------------------------------------------------------------------------------
        private List<Sales> SaveSalesFromFile(string path)
        {
            List<Sales> sales = _convertFile.ToSales(path);

            // save in memory
            _repositoryManager.Sales.CreateRange(sales);
            _repositoryManager.Save();

            sales = _repositoryManager.Sales.FindAll(false).ToList();

            return sales;
        }
        // ------------------------------------------------------------------------------------

    }
}
