using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class GetPostByUserIdHandler : IRequestHandler<GetPostByUserIdQuery, IEnumerable<PostDto>>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public GetPostByUserIdHandler(IRepository<Post> postRepository, IUserAccessor userAccessor, IRepository<AppUser> userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userAccessor = userAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetPostByUserIdQuery request, CancellationToken cancellationToken)
      {
            return _mapper.Map<IEnumerable<PostDto>>(await _postRepository.GetAsync(x => x.Author.NormalizedUserName == request.Username.ToUpper(), includes:(x => x.Author)));
            /*var query = _postRepository.Query(p => p.Author.NormalizedUserName == request.Username.ToUpper());

            var columnsMap = new Dictionary<string, Expression<Func<Post, object>>>()
            {
                ["title"] = p => p.Title,
                ["created"] = p => p.Created
               // ["view"] = p => p.Views
            };

            if (String.IsNullOrWhiteSpace(request.SortBy) || !columnsMap.ContainsKey(request.SortBy))
                return _mapper.Map<IEnumerable<PostDto>>(await query.ToListAsync());

            if (request.IsAscending)
                query = query.OrderBy(columnsMap[request.SortBy]);
            else
                query = query.OrderByDescending(columnsMap[request.SortBy]);

            if (request.Page <= 0)
                request.Page = 1;

            if (request.PageSize <= 0)
                request.PageSize = 10;

            return  _mapper.Map<IEnumerable<PostDto>>(await query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync());*/
        }
    }
}
