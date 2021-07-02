using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Posts.Query;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.Posts.Handler
{
    public class GetPostsHandler : IRequestHandler<GetPostsQuery, IEnumerable<PostDto>>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public GetPostsHandler(IRepository<Post> postRepository, IUserAccessor userAccessor, IRepository<AppUser> userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userAccessor = userAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAsync();

            var postDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            var currentUserId = _userAccessor.GetCurrentUserId();

            /*

            postDto.LikesCount = post.PostReactions.Where(x => x.Reaction == "like").Count();

            postDto.LikesCount = post.PostReactions.Where(x => x.Reaction == "dislike").Count();

            if (post.PostReactions.Any(x => x.LikerId == currentUserId))
            {
                postDto.IsLiked = true;
                var reaction = post.PostReactions.Single(x => x.LikerId == currentUserId);
                postDto.Reaction = reaction.Reaction;
            }
            */

            return postDto;
        }

    }
}
