using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.Posts.Query
{
    public class GetPostByUserIdQuery : IRequest<IEnumerable<PostDto>>
    {
        public string Username { get; set; }
       /* public int Page { get; set; }
        public byte PageSize { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }*/

        public GetPostByUserIdQuery(string username/*, int page, byte pageSize, string sortBy, bool isAscending*/)
        {
            Username = username;
            /*Page = page;
            PageSize = pageSize;
            SortBy = sortBy;
            IsAscending = isAscending;*/
        }
    }
}
