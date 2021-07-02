using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Interfaces;
using YoutubeClone.Application.Profiles.Queries;

namespace YoutubeClone.Application.Profiles.Handlers
{
    public class GetProfileHandler : IRequestHandler<GetProfileQuery, ProfileDto>
    {
        private readonly IProfileReader _profileReader;

        public GetProfileHandler(IProfileReader profileReader)
        {
            _profileReader = profileReader;
        }

        public Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            return _profileReader.ReadProfile(request.Username);
        }
    }
}
