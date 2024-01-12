// authorizing the user
// Date: 2020-1-20

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Protocol;
using QlhsServer.Models.Response;

namespace QlhsServer.Filters
{
    public class Authorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }
            context.Result = new UnauthorizedObjectResult(new ErrorModel
            {
                Status = 401,
                Errors = new List<ErrorModel.ErrorItem>
                {
                    new ErrorModel.ErrorItem
                    {
                        FieldName = "Unauthorized",
                        Message = "You are not authorized to access this resource"
                    }
                }
            });
        }
    }
}