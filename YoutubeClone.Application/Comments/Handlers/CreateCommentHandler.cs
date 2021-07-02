using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Comments.Commands;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Errors;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.Comments.Handlers
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public CreateCommentHandler(IRepository<Comment> commentRepository, IRepository<AppUser> userRepository, IUserAccessor userAccessor, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }
        public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request);

            var parentComment = await _commentRepository.GetSingleOrDefaultAsync(x => x.ParentId == request.ParentId);

            var currentUser = await _userRepository.GetSingleOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());

            comment.Author = currentUser;
            comment.Created = DateTime.Now;


            _commentRepository.Add(comment);
            var success = await _commentRepository.SaveAsync();

            if (!success)
                    throw new Exception("Problem saving changes");
            return _mapper.Map<CommentDto>(comment);

        }
    }
}
