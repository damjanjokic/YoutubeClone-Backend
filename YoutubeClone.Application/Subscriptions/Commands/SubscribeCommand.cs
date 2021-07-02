using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeClone.Application.Subscriptions.Commands
{
    public class SubscribeCommand : IRequest
    {
        public string Username { get; set; }

        public SubscribeCommand(string username)
        {
            Username = username;
        }
    }
}
