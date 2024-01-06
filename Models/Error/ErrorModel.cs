namespace QlhsServer.Models.Error;
public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    // Các thuộc tính khác để mô tả lỗi nếu cần thiết
}
