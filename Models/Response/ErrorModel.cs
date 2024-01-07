using QlhsServer.Models.Response;

namespace QlhsServer.Models.Response;
public class ErrorModel : RequestResponse
{

    public int Status { get; set; }
    public List<ErrorItem> Errors { get; set; }


    public class ErrorItem
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
