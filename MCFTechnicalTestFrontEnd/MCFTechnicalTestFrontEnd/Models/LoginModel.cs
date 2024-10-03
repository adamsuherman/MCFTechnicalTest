namespace MCFTechnicalTestFrontEnd.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string password { get; set; }
    }

    public class LoginResultModel 
    {
        public string Username { get; set; }
        public string password { get; set; }
        public string Token { get; set; }
    }
}
