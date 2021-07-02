using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeClone.Application.User.Commands;
using YoutubeClone.Application.User.Queries;

namespace YoutubeClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterCommand command)
        {
            command.Origin = Request.Headers["origin"];
            await _mediator.Send(command);
            return Ok("Registration successful - please check your email");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("verifyEmail")]
        public async Task<ActionResult> VerifyEmail(ConfirmEmailCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Succeeded) return BadRequest("Problem verifying email address");
            return Ok("Email confirmed - you can now login");
        }

    }
}
