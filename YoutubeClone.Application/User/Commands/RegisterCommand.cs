using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeClone.Application.User.Commands
{
    public class RegisterCommand : IRequest
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Origin { get; set; }
    }
}
