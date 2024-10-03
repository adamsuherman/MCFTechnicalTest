using MCFTechnicaltest.Context;
using MCFTechnicaltest.Model;
using MCFTechnicaltest.Models.CodingTest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCFTechnicaltest.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Authorize]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly Transaction _transaction;
        List<ms_storage_location> _msStorageLocation;
        private readonly CodingTestContext _context;
        ResultObject result;
        public TransactionController(IConfiguration configuration, CodingTestContext context) 
        {
            _configuration = configuration;
            _context= context;
            _transaction = new Transaction(_configuration, _context);
        }

        [HttpGet]
        public async Task<IActionResult> GetLocation()
        {
            _msStorageLocation = await _transaction.GetLocationData();
            return new OkObjectResult(_msStorageLocation);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTransaction(tr_bpkb request)
        {
            result = await _transaction.SaveData(request);
            return new OkObjectResult(result);
        }
    }
}
