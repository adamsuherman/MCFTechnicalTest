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

        public async Task<ResultObject> UpdateData(tr_bpkb request)
        {
            ResultObject result = new ResultObject();
            try
            {
                tr_bpkb param = new tr_bpkb();
                param.agreement_number = request.agreement_number;
                param.branch_id = request.branch_id;
                param.bpkb_no = request.bpkb_no;
                param.bpkb_date_in = request.bpkb_date_in;
                param.bpkb_date = request.bpkb_date;
                param.faktur_no = request.faktur_no;
                param.faktur_date = request.faktur_date;
                param.policy_no = request.policy_no;
                param.location_id = request.location_id;
                param.last_updated_by = request.created_by;
                param.last_updated_on = request.created_on;
                _context.Update(param);
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
        public async Task<tr_bpkb> GetTransactionData(string agreementNumber)
        {
            tr_bpkb data = new tr_bpkb();
            try
            {
                var result = await _context.tr_bpkb.Where(t => t.agreement_number == agreementNumber).ToListAsync();
                if(result.Count> 0)
                {
                    data.agreement_number = result[0].agreement_number;
                    data.bpkb_no = result[0].bpkb_no;
                    data.branch_id = result[0].branch_id;
                    data.bpkb_date = result[0].bpkb_date;
                    data.bpkb_date_in = result[0].bpkb_date_in;
                    data.faktur_no = result[0].faktur_no;
                    data.faktur_date = result[0].faktur_date;
                    data.location_id = result[0].location_id;
                    data.created_by = result[0].created_by;
                    data.created_on = result[0].created_on;
                    data.last_updated_by = result[0].last_updated_by;
                    data.last_updated_on = result[0].last_updated_on;
                }
            }
            catch (Exception ex)
            {

            }

            return data;
        }
    }
}
