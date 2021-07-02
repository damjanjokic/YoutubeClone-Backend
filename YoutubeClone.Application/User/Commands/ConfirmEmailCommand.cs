using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeClone.Application.User.Commands
{
    public class ConfirmEmailCommand : IRequest<IdentityResult>
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
