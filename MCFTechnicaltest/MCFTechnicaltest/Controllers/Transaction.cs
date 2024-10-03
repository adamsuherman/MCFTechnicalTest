using MCFTechnicaltest.Context;
using MCFTechnicaltest.Models.CodingTest;
using Microsoft.EntityFrameworkCore;

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
    }
}
