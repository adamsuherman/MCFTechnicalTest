namespace MCFTechnicalTestFrontEnd.Models
{
    public class APIModel
    {
    }

    public class APIResult
    {
        public string apiName { get; set; }
        public string apiType { get; set; }
        public string apiResponse { get; set; }
        public string jsonParam { get; set; }
        public string Response { get; set; }
        public string jsonResponse { get; set; }
        public HttpResponseMessage responData { get; set; }
        public Object parameter { get; set; }
        public string eventName1 { get; set; }
        public string controller { get; set; }
        public string eventId { get; set; }
    }

    public class APIRequest
    {
        public string Token { get; set; }
        public string APIReq { get; set; }
        public HttpClient Http { get; set; }
        public Object Param { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
    }
}
