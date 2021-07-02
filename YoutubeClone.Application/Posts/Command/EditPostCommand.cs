using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeClone.Application.Posts.Command
{
    public class EditPostCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
