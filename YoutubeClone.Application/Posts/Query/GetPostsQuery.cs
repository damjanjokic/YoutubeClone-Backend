using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.Posts.Query
{
    public class GetPostsQuery : IRequest<IEnumerable<PostDto>>
    {

    }
}
