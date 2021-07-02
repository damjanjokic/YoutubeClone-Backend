using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeClone.Application.Subscriptions.Commands
{
    public class UnsubscribeCommand : IRequest
    {
        public string Username { get; set; }

        public UnsubscribeCommand(string username)
        {
            Username = username;
        }
    }
}
