using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Errors;
using YoutubeClone.Application.Interfaces;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.Profiles
{
    public class ProfileReader : IProfileReader
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IRepository<UserSubscription> _subRepository;
        private readonly IMapper _mapper;

        public ProfileReader(IRepository<AppUser> userRepository, IRepository<UserSubscription> subRepository,  IUserAccessor userAccessor, IMapper mapper)
        {
            _userAccessor = userAccessor;
            _userRepository = userRepository;
            _subRepository = subRepository;
            _mapper = mapper;
        }

        public async Task<ProfileDto> ReadProfile(string username)
        {
            var user = await _userRepository.GetSingleOrDefaultAsync(x => x.UserName == username, (x => x.Posts));

            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

            var currentUser = await _userRepository.GetSingleOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId(), (y => y.Subscriptions));

            var profile = _mapper.Map<ProfileDto>(user);
            
            profile.SubscribersCount = await _subRepository.CountAsync(x => x.TargetId == user.Id);
            profile.SubscriptionCount = await _subRepository.CountAsync(x => x.ObserverId == user.Id);
            
            if (currentUser.Subscriptions.Any(x => x.TargetId == user.Id))
            {
                profile.Subscription = true;
            }

            return profile;
        }
    }
}
