using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TimerFilter;

public class TimerFilter : IAsyncActionFilter
{
    private readonly ILogger<TimerFilter> _logger;

    public TimerFilter(ILogger<TimerFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();

        try
        {
            await next();
        }
        finally
        {
            sw.Stop();
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation("{0} took {1}ms", actionName, sw.ElapsedMilliseconds.ToString());
        }
    }
}