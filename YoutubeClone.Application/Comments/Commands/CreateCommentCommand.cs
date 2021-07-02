using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.Comments.Commands
{
    public class CreateCommentCommand : IRequest<CommentDto>
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public int? ParentId { get; set; }
    }
}
