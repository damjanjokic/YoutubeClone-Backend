using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Interfaces;
using YoutubeClone.Application.Subscriptions.Queries;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;

namespace YoutubeClone.Application.Subscriptions.Handlers
{
    public class GetSubscriptionsHandler : IRequestHandler<GetSubscriptionsQuery, IEnumerable<ProfileDto>>
    {
        private readonly IRepository<UserSubscription> _repository;
        private readonly IMapper _mapper;
        private readonly IProfileReader _profileReader;

        public GetSubscriptionsHandler(IRepository<UserSubscription> repository, IMapper mapper, IProfileReader profileReader)
        {
            _repository = repository;
            _profileReader = profileReader;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfileDto>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = new List<UserSubscription>();
            var profiles = new List<ProfileDto>();

            switch (request.Predicate)
            {
                case "subscribers":
                    {
                        subscriptions = await _repository.Query(x => x.Target.UserName == request.Username, includes: (y => y.Observer)).ToListAsync();
                        foreach(var sub in subscriptions)
                        {
                            profiles.Add(await _profileReader.ReadProfile(sub.Observer.UserName));
                        }
                        break;
                    }
                case "subscriptions":
                    {
                        subscriptions = await _repository.Query(x => x.Observer.UserName == request.Username, includes: (y => y.Target)).ToListAsync();
                        foreach (var sub in subscriptions)
                        {
                            profiles.Add(await _profileReader.ReadProfile(sub.Target.UserName));
                        }
                        break;
                    }
            }

            return profiles;
        }
    }
}
