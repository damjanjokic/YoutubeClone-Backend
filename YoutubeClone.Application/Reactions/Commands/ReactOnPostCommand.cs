using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeClone.Application.Reactions.Commands
{
    public class ReactOnPostCommand : IRequest
    {
        public int PostId { get; set; }
        public string Predicate { get; set; }

        public ReactOnPostCommand(int postId, string predicate)
        {
            PostId = postId;
            Predicate = predicate;
        }
    }
}
