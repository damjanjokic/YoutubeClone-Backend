using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.Posts.Query
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public int Id { get; set; }

        public GetPostByIdQuery(int id)
        {
            Id = id;
        }
    }
}
