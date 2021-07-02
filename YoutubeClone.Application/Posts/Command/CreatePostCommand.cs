using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.Posts.Command
{
    public class CreatePostCommand : IRequest<PostDto>
    {
        [Required]
        public string Title { get; set; }
        public IFormFile File { get; set; }
    }
}
