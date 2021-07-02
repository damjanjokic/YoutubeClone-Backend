using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeClone.Application.Subscriptions.Commands;

namespace YoutubeClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{username}/subscribe")]
        public async Task<IActionResult> Subscribe(string username)
        {
            return Ok(await _mediator.Send(new SubscribeCommand(username)));
        }

        [HttpDelete("{username}/unsubscribe")]
        public async Task<IActionResult> Unfollow(string username)
        {
            return Ok(await _mediator.Send(new UnsubscribeCommand(username)));
        }

    }
}
