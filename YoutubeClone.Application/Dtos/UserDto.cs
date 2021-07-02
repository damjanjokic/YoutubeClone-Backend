using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Core.Entities;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.Dtos
{
    public class UserDto
    {
        public UserDto(AppUser user, IJwtGenerator jwtGenerator)
        {
            DisplayName = user.DisplayName;
            Token = jwtGenerator.CreateToken(user);
            Username = user.UserName;
            Id = user.Id;
        }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }

    }
}
