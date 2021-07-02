using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeClone.Application.Profiles.Queries;
using YoutubeClone.Application.Subscriptions.Queries;

namespace YoutubeClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return Ok(await _mediator.Send(new GetProfileQuery(username)));
        }

        [HttpGet("{username}/{predicate}")]
        public async Task<IActionResult> GetSubscriptions(string username, string predicate)
        {
            return Ok(await _mediator.Send(new GetSubscriptionsQuery(username, predicate)));
        }
    }
}
