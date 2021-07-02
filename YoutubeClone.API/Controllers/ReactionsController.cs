using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeClone.Application.Reactions.Commands;

namespace YoutubeClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{postId}/{predicate}")]
        public async Task<IActionResult> ReactOnPost(int postId, string predicate)
        {
            return Ok(await _mediator.Send(new ReactOnPostCommand(postId, predicate)));
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteReactionOnPost(int postId)
        {
            return Ok(await _mediator.Send(new DeleteReactionOnPostCommand(postId)));
        }
    }
}
