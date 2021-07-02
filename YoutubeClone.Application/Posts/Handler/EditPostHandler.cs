using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Posts.Command;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;

namespace YoutubeClone.Application.Posts.Handler
{
    public class EditPostHandler : IRequestHandler<EditPostCommand>
    {
        private readonly IRepository<Post> _repository;
        private readonly IMapper _mapper;

        public EditPostHandler(IRepository<Post> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<Unit> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetByIdAsync(request.Id);

            _mapper.Map<EditPostCommand, Post>(request, post);

            var success = await _repository.SaveAsync();

            if (!success)
                throw new Exception("Problem saving changes");

            return Unit.Value;
        }
    }
}
