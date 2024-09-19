using Microsoft.AspNetCore.Mvc;

namespace Eximia.CsharpCourse.API.Infrastructure.Filters;

public class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object error) : base(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
