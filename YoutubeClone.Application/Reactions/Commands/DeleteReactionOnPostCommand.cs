using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeClone.Application.Reactions.Commands
{
    public class DeleteReactionOnPostCommand : IRequest
    {
        public int PostId { get; set; }

        public DeleteReactionOnPostCommand(int postId)
        {
            PostId = postId;
        }
    }
}
