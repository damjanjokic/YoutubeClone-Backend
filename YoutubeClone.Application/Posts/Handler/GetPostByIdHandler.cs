using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Errors;
using YoutubeClone.Application.Posts.Query;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.Posts.Handler
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostDto>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public GetPostByIdHandler(IRepository<Post> postRepository, IUserAccessor userAccessor, IRepository<AppUser> userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userAccessor = userAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetSingleOrDefaultAsync(x => x.Id == request.Id, (x => x.Author), (x => x.PostReactions), (x => x.Comments));

            var currentUserId = _userAccessor.GetCurrentUserId();

            if (post == null)
                throw new RestException(HttpStatusCode.NotFound, new { Post = "Not found" });

            var postDto = _mapper.Map<PostDto>(post);
            postDto.LikesCount = post.PostReactions.Where(x => x.Reaction == "like").Count();

            postDto.LikesCount = post.PostReactions.Where(x => x.Reaction == "dislike").Count();

            if (post.PostReactions.Any(x => x.LikerId == currentUserId))
            {
                postDto.IsLiked = true;
                var reaction = post.PostReactions.Single(x => x.LikerId == currentUserId);
                postDto.Reaction = reaction.Reaction;
            }

            return postDto;

        }

        private Exception RestException()
        {
            throw new NotImplementedException();
        }
    }
}
