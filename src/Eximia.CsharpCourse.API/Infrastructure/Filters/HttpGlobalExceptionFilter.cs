using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Eximia.CsharpCourse.API.Infrastructure.Filters;

public class HttpGlobalExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<HttpGlobalExceptionFilter> _logger;

    public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        _env = env;
        _logger = loggerFactory.CreateLogger<HttpGlobalExceptionFilter>();
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogCritical(context.Exception, context.Exception.Message);

        var json = new JsonErrorResponse
        {
            Messages = ["An error occur. Try it again."]
        };

        if (!_env.IsProduction())
            json.DeveloperMessage = context.Exception.ToString();

        context.Result = new InternalServerErrorObjectResult(json);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.ExceptionHandled = true;
    }

    private record JsonErrorResponse
    {
        public string[]? Messages { get; set; }
        public string? DeveloperMessage { get; set; }
    }
}
