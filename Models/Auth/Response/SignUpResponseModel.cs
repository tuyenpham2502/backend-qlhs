namespace QlhsServer.Models
{
    public class SignUpResponseSuccessModel {
        public int Status { get; set; }

        public string Message { get; set; }

    }

    public class SignUpResponseFailModel {
        public int Status { get; set; }

        public string ErrorMessage { get; set; }
    }   

}