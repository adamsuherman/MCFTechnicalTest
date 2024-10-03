using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net;
using System;
using MCFTechnicalTestFrontEnd.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;

namespace MCFTechnicalTestFrontEnd.Helper
{
    public class API
    {
        IConfiguration _config;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public API(IConfiguration config, IHttpContextAccessor httpContextAccessor) 
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        public LoginResultModel RequestPostLogin(LoginModel request)
        {
            string url = _config["AppSettings:BaseUrl"] + "Login";
            string jsonobj = JsonConvert.SerializeObject(request);
            LoginResultModel objRes = new LoginResultModel();
            try
            {
                HttpClient createClient = new HttpClient();
                HttpContent content = new StringContent(jsonobj, Encoding.UTF8, "application/json");

                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url);

                req.Content = content;
                HttpResponseMessage response = createClient.SendAsync(req).GetAwaiter().GetResult();
                string strResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    objRes = JsonConvert.DeserializeObject<LoginResultModel>(strResponse);
                    objRes.Username = request.Username;
                    objRes.password = request.password;
                }
            }
            catch(Exception ex)
            {
                objRes.Username = null;
                objRes.password = null;
                objRes.Token = null;
            }
            return objRes;
        }
        public List<LocationModel> RequestGetLocation()
        {
            string url = _config["AppSettings:BaseUrl"] + "Transaction/GetLocation";
            LocationModel objRes = new LocationModel();
            List<LocationModel> objResList = new List<LocationModel>();
            var token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            try
            {
                HttpClient createClient = new HttpClient();
                createClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                createClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = createClient.GetAsync(url).GetAwaiter().GetResult();
                string strResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    objResList = JsonConvert.DeserializeObject<List<LocationModel>>(strResponse);
                }
            }
            catch (Exception ex)
            {
                objRes.location_id = null;
                objRes.location_name = null;
                objResList.Add(objRes);
            }
            return objResList;
        }

        public SaveResult SaveTransaction(IFormCollection col)
        {
            string url = _config["AppSettings:BaseUrl"] + "Transaction/SaveTransaction";
            SaveResult result = new SaveResult();
            var token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            var Username = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            SaveRequest request = new SaveRequest();
            request.agreement_number = col["InputAgreementTenor"];
            request.branch_id = col["InputBranchId"];
            request.bpkb_no = col["InputNoBPKB"];
            request.bpkb_date_in = Convert.ToDateTime(col["InputTanggalBPKBIn"]);
            request.bpkb_date = Convert.ToDateTime(col["InputTanggalBPKB"]);
            request.faktur_no = col["InputNoFaktur"];
            request.faktur_date = Convert.ToDateTime(col["InputTanggalFaktur"]);
            request.policy_no = col["InputNoPolisi"];
            request.location_id = col["rb-lokasi"];
            request.created_by = Username;
            request.created_on = DateTime.Now;

            string strObj = JsonConvert.SerializeObject(request);
            try
            {
                HttpClient createClient = new HttpClient();
                createClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpContent content = new StringContent(strObj, Encoding.UTF8, "application/json");
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url);

                req.Content = content;
                HttpResponseMessage response = createClient.SendAsync(req).GetAwaiter().GetResult();
                string strResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<SaveResult>(strResponse);
                }
            }
            catch(Exception ex)
            {
                result.ResultCode = "0";
                result.ResultMessage= ex.Message;
            }
            return result;
        }
    }
}
