using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using QlhsServer.Models.Error;

namespace QlhsServer.middleware;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            ErrorResponse errorResponse = new ErrorResponse();

            switch (ex)
            {
                case SqlException sqlEx:
                    errorResponse.StatusCode = StatusCodes.Status500InternalServerError;
                    errorResponse.Message = "Lost connection to the database";
                    break;

                case ValidationException validationEx:
                    errorResponse.StatusCode = StatusCodes.Status400BadRequest;
                    errorResponse.Message = validationEx.Message;
                    break;

                case KeyNotFoundException keyNotFoundEx:
                    errorResponse.StatusCode = StatusCodes.Status404NotFound;
                    errorResponse.Message = keyNotFoundEx.Message;
                    break;

                case UnauthorizedAccessException unauthorizedEx:
                    errorResponse.StatusCode = StatusCodes.Status401Unauthorized;
                    errorResponse.Message = unauthorizedEx.Message;
                    break;

                default:
                    errorResponse.StatusCode = StatusCodes.Status500InternalServerError;
                    errorResponse.Message = "Internal Server Error";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResponse.StatusCode;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }

}
