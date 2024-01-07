using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using QlhsServer.Models.Response;

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
            ErrorModel errorResponse = new ErrorModel();
            System.Console.WriteLine(ex);
            switch (ex)
            {
                case SqlException sqlEx:
                    errorResponse.Status = StatusCodes.Status500InternalServerError;
                    errorResponse.Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = sqlEx.Source,
                            Message = sqlEx.Message
                        }
                    };
                    break;

                case ValidationException validationEx:
                    errorResponse.Status = StatusCodes.Status400BadRequest;
                    errorResponse.Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = validationEx.Source,
                            Message = validationEx.Message
                        }
                    };
                    break;

                case KeyNotFoundException keyNotFoundEx:
                    errorResponse.Status = StatusCodes.Status404NotFound;
                    errorResponse.Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = keyNotFoundEx.Source,
                            Message = keyNotFoundEx.Message
                        }
                    };
                    break;

                case UnauthorizedAccessException unauthorizedEx:
                    errorResponse.Status = StatusCodes.Status401Unauthorized;
                    errorResponse.Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = unauthorizedEx.Source,
                            Message = unauthorizedEx.Message
                        }
                    };
                    break;

                default:
                    errorResponse.Status = StatusCodes.Status500InternalServerError;
                    errorResponse.Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = ex.Source,
                            Message = ex.Message
                        }
                    };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResponse.Status;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }

}
