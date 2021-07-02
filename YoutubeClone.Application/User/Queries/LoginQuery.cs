using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.User.Queries
{
    public class LoginQuery : IRequest<UserDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
