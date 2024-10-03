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
                tr_bpkb param = new tr_bpkb();
                param.agreement_number= request.agreement_number;
                param.branch_id = request.branch_id;
                param.bpkb_no = request.bpkb_no;
                param.bpkb_date_in = request.bpkb_date_in;
                param.bpkb_date = request.bpkb_date;
                param.faktur_no = request.faktur_no;
                param.faktur_date = request.faktur_date;
                param.policy_no = request.policy_no;
                param.location_id = request.location_id;
                param.created_by = request.created_by;
                param.created_on = request.created_on;

                _context.Add(param);
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
