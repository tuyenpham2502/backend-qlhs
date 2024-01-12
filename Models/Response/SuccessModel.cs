using Microsoft.OpenApi.Any;

namespace QlhsServer.Models.Response
{
    public class SuccessModel : RequestResponse
    {
        public string Message { get; set; }

        public object Data { get; set; }
    }
}