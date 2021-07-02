using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Interfaces;
using YoutubeClone.Application.Posts.Command;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.Posts.Handler
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly IRepository<Post> _repository;
        private readonly IUploadVideoService _videoService;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IRepository<AppUser> _userRepository;

        public CreatePostHandler(IRepository<Post> repository, IUploadVideoService videoService, IUserAccessor userAccessor, IRepository<AppUser> userRepository, IMapper mapper)
        {
            _repository = repository;
            _videoService = videoService;
            _userAccessor = userAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request);
            post.Created = DateTime.Now;
            post.Author = await _userRepository.GetSingleOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());
            post.VideoFileName = await _videoService.UploadVideo(request.File);
           

            _repository.Add(post);

            var success = await _repository.SaveAsync();

            if (success)
                return _mapper.Map<PostDto>(post);

            throw new Exception("Problem saving changes");
        }
    }
}
