namespace QlhsServer.Models
{
    public class SignInResponseSuccessModel
    {
        public string Token { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }

    }

    public class SignInResponseFailModel
    {
        public int Status { get; set; }

        public string ErrorMessage { get; set; }
    }


}