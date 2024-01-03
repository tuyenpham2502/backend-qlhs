namespace QlhsServer.Models
{
    public class SignInResponseSuccessModel
    {
        public string Token { get; set; }


        public string Message { get; set; }

    }

    public class SignInResponseFailModel
    {
        public string ErrorMessage { get; set; }
    }


}