using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negotiations.Application.Features.Negotiations.Commands.Create;
using Negotiations.Application.Features.Negotiations.Commands.Update;
using Negotiations.Application.Features.Negotiations.Queries.GetNegotiationById;
using Negotiations.Application.Features.Negotiations.Queries.GetNegotiationsByProduct;
using Negotiations.Application.Features.Negotiations.Validators;

namespace Negotiations.WebApi.Controllers
{
    [ApiController]
    [Route("api/product/{ProductId}/negotiation")]
    public class NegotiationController : ApiControllerBase
    {
        [HttpPost("/api/product/negotiation")]
        public async Task<ActionResult<int>> Create([FromBody]CreateNegotiationCommand command)
        {
            var negotiationId = await Mediator.Send(command);
            return Created($"/api/{command.ProductId}/negotiation/{negotiationId}", null);
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetAll([FromRoute]GetNegotiationsByProductQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<int>> GetById([FromRoute]GetNegotiationByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPut("{Id}/accept")]
        [Authorize]
        public async Task<ActionResult<int>> Accept([FromRoute]AcceptNegotiationCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{Id}/reject")]
        [Authorize]
        public async Task<ActionResult<int>> Reject([FromRoute]RejectNegotiationCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}