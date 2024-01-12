// authorizing the user
// Date: 2020-1-20

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QlhsServer.Helpers;

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
            context.Result = new UnauthorizedObjectResult(AppErrors.AuthenticatedError);
        }
    }
}