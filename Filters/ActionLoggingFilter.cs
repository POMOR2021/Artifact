using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class ActionLoggingFilter : IActionFilter
{
    private readonly ILogger<ActionLoggingFilter> _logger;
    public ActionLoggingFilter(ILogger<ActionLoggingFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.ActionDescriptor.DisplayName;
        var user = context.HttpContext.User.Identity?.Name ?? "Anonymous";
        _logger.LogInformation($"User: {user}, Action: {action}");
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}
