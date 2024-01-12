using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models.Response;

namespace QlhsServer.DependencyInjection
{
    public static class ControllerConfig
    {
        public static IServiceCollection AddControllerConfig(this IServiceCollection services)
        {
            _ = services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new ErrorModel();
                        result.Status = StatusCodes.Status400BadRequest;
                        result.Errors = context.ModelState
                            .SelectMany(entry => entry.Value.Errors.Select(error => new ErrorModel.ErrorItem
                            {
                                FieldName = entry.Key, // Use entry.Key to get the field name
                                Message = error.ErrorMessage
                            }))
                            .ToList();
                        return new BadRequestObjectResult(result);
                    };
                };
            });
            return services;
        }
    }
}