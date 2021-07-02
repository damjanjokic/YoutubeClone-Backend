using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using YoutubeClone.Application.Posts.Command;
using YoutubeClone.Application.Posts.Query;

namespace YoutubeClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _mediator.Send(new GetPostsQuery()));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            return Ok(await _mediator.Send(new GetPostByIdQuery(id)));
        }

        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetPostByUserId(string username/*, int page, byte pageSize, string sortBy, bool isAscending*/ )
        {
            return Ok(await _mediator.Send(new GetPostByUserIdQuery(username/*, page, pageSize, sortBy, isAscending*/)));
        }

        [HttpPost]
        //[ValidateModel]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command)
        {
            var post = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost([FromBody] EditPostCommand command, int id)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

    }
}
