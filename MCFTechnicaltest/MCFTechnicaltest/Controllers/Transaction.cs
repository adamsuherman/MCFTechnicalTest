using MCFTechnicaltest.Context;
using MCFTechnicaltest.Model;
using MCFTechnicaltest.Models.CodingTest;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MCFTechnicaltest.Controllers
{
    public class Transaction
    {
        private IConfiguration _config;
        private readonly CodingTestContext _context;
        public Transaction(IConfiguration config, CodingTestContext context) 
        {
            _config = config;
            _context= context;
        }

        public async Task<List<ms_storage_location>> GetLocationData()
        {
            List<ms_storage_location> data = new List<ms_storage_location>();
            try
            {
                 data = await _context.ms_storage_location.ToListAsync();
            }
            catch(Exception ex)
            {
                
            }

            return data;
        } 

        public async Task<ResultObject> SaveData(tr_bpkb request)
        {
            ResultObject result = new ResultObject();
            try
            {
                _context.Add(request);
                _context.SaveChanges();
                result.ResultCode = "1";
                result.ResultMessage = "SUCCESS";
            }
            catch(Exception ex)
            {
                result.ResultCode = "0";
                result.ResultMessage = ex.Message;
            }
            return result;
        }
    }
}
