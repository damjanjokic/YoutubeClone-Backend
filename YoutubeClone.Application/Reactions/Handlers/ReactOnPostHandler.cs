using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Errors;
using YoutubeClone.Application.Reactions.Commands;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.Reactions.Handlers
{
    public class ReactOnPostHandler : IRequestHandler<ReactOnPostCommand>
    {
        private readonly IRepository<PostReaction> _reactionRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IUserAccessor _userAccessor;

        public ReactOnPostHandler(IRepository<PostReaction> reactionRepository, IRepository<Post> postRepository, IRepository<AppUser> userRepository, IUserAccessor userAccessor)
        {
            _reactionRepository = reactionRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(ReactOnPostCommand request, CancellationToken cancellationToken)
        {
            if (request.Predicate != "like" && request.Predicate != "dislike")
                throw new RestException(HttpStatusCode.BadRequest, new { Reaction = "There is no such reaction" });

            var currentUser = await _userRepository.GetSingleOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());
            var post = await _postRepository.GetSingleOrDefaultAsync(x => x.Id == request.PostId);

            if (post == null)
                throw new RestException(HttpStatusCode.NotFound, new { Post = "Not found" });

            var reaction = await _reactionRepository.GetSingleOrDefaultAsync(x => x.LikerId == currentUser.Id && x.PostId == post.Id);

            if (reaction != null)
                throw new RestException(HttpStatusCode.BadRequest, new { Reqction = "You have already reacted to this post" });

            reaction = new PostReaction
            {
                Liker = currentUser,
                Post = post,
                Reaction = request.Predicate
            };

            _reactionRepository.Add(reaction);
            var success = await _reactionRepository.SaveAsync();

            if (!success)
                throw new Exception("Problem saving changes");

            return Unit.Value;
        }
    }
}
