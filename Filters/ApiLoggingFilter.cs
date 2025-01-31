using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters;

public class ApiLoggingFilter: IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("### Executing action -> OnActionExecuting ###");
        _logger.LogInformation(DateTime.Now.ToLongTimeString());
        _logger.LogInformation($"Model State: {context.ModelState.IsValid}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("### Executing action -> OnActionExecuted ###");
        _logger.LogInformation(DateTime.Now.ToLongTimeString());
        _logger.LogInformation($"Status Code: {context.HttpContext.Response.StatusCode}");
    }
}