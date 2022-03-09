using MediatR;
using Microsoft.AspNetCore.Mvc;
using Negotiations.Application.Features.Products.Commands.Create;
using Negotiations.Application.Features.Products.Queries.GetAllProducts;

namespace Negotiations.WebApi.Controllers;

[ApiController]
[Route("api/helloWorld")]
public class HelloWorldController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
	    var products = await Mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateProductCommand command)
    {
        return await Mediator.Send(command);
    }
}
