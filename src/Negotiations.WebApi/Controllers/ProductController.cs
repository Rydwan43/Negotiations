using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negotiations.Application.Features.Products.Commands.Create;
using Negotiations.Application.Features.Products.Commands.Delete;
using Negotiations.Application.Features.Products.Queries.GetAllProducts;
using Negotiations.Application.Features.Products.Queries.GetProductById;

namespace Negotiations.WebApi.Controllers
{
    [Route("/api/product")]
    public class ProductController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var products = await Mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetbyId([FromRoute]int id)
        {
            var product = await Mediator.Send(new GetProductByIdQuerry { Id = id });
            return Ok(product);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> Create(CreateProductCommand command)
        {
            var productId = await Mediator.Send(command);
            return Created($"/api/product/{productId}", null);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute]DeleteProductCommand command)
        {
            var productId = await Mediator.Send(command);
            return NoContent();
        }
    }
}