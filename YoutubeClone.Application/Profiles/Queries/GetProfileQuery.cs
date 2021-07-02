using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.Profiles.Queries
{
    public class GetProfileQuery : IRequest<ProfileDto>
    {
        public string Username { get; set; }

        public GetProfileQuery(string username)
        {
            Username = username;
        }
    }
}
