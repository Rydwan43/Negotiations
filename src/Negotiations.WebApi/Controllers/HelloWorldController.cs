using Microsoft.AspNetCore.Mvc;

namespace Negotiations.WebApi.Controllers;

[ApiController]
[Route("api/helloWorld")]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
	    return "Hello world";
    }
}
