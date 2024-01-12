
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models.Response;

namespace QlhsServer.Helpers
{

    public static class AppResult
    {
        public static IActionResult ActionResult(RequestResponse result)
        {
            if (result is ErrorModel)
            {
                switch (result.Status)
                {
                    case StatusCodes.Status202Accepted:
                        return new AcceptedWithMessageResult(result);
                    case StatusCodes.Status400BadRequest:
                        return new BadRequestObjectResult(result);
                    
                }
            }

            return new OkObjectResult(result);
        }
    }

    public class AcceptedWithMessageResult : ObjectResult
    {
        public AcceptedWithMessageResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status202Accepted;
        }
    }
}

