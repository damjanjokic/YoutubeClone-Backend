using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Application.Comments.Commands;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Posts.Command;
using YoutubeClone.Application.User.Commands;
using YoutubeClone.Core.Entities;

namespace YoutubeClone.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>();

            CreateMap<CreatePostCommand, Post>();
            CreateMap<EditPostCommand, Post>();

            CreateMap<RegisterCommand, AppUser>();

            CreateMap<AppUser, ProfileDto>();

            CreateMap<CreateCommentCommand, Comment>();
            CreateMap<Comment, CommentDto>()
                .ForMember(cd => cd.AuthorUsername, opt => opt.MapFrom(c => c.Author.UserName));

        }
    }
}
