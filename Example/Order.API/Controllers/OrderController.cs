using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JieDDDFramework.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Domain.Application.Commands;

namespace Order.API.Controllers
{
    [Authorize]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return result?Success():Fail();
        }
    }
}
