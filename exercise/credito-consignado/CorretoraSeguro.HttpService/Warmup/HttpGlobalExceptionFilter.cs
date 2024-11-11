using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CorretoraSeguro.HttpService.Warmup;

public class HttpGlobalExceptionFilter(IWebHostEnvironment env, ILoggerFactory loggerFactory) : IExceptionFilter
{
    private readonly ILogger<HttpGlobalExceptionFilter> _logger = loggerFactory.CreateLogger<HttpGlobalExceptionFilter>();

    public void OnException(ExceptionContext context)
    {
        _logger.LogCritical(context.Exception, context.Exception.Message);

        var json = new JsonErrorResponse
        {
            Messages = new[] { "An error occur.Try it again." }
        };

        if (env.IsDevelopment())
        {
            json.DeveloperMessage = context.Exception.ToString();
        }
           
        context.Result = new InternalServerErrorObjectResult(json);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.ExceptionHandled = true;
    }

    private class JsonErrorResponse
    {
        public string[]? Messages { get; set; }

        public object? DeveloperMessage { get; set; }
    }

    private class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}