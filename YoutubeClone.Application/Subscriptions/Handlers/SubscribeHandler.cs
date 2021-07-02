using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Errors;
using YoutubeClone.Application.Subscriptions.Commands;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.Subscriptions.Handlers
{
    public class SubscribeHandler : IRequestHandler<SubscribeCommand>
    {
        private readonly IRepository<UserSubscription> _subRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IUserAccessor _userAccessor;

        public SubscribeHandler(IRepository<UserSubscription> subRepository, IUserAccessor userAccessor, IRepository<AppUser> userRepository)
        {
             _subRepository = subRepository;
            _userRepository = userRepository;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(SubscribeCommand request, CancellationToken cancellationToken)
        {
            var observer = await _userRepository.GetSingleOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());

            var target = await _userRepository.GetSingleOrDefaultAsync(x => x.NormalizedUserName == request.Username.ToUpper());

            if (target == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

            var subscription = await _subRepository.GetSingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id);

            if (subscription != null)
                throw new RestException(HttpStatusCode.BadRequest, new { User = "You are already subscribed to this user" });

            subscription = new UserSubscription
            {
                Observer = observer,
                Target = target
            };

            _subRepository.Add(subscription);
            var success = await _subRepository.SaveAsync();

            if (!success)
                throw new Exception("Problem saving changes");

            return Unit.Value;
        }
    }
}
