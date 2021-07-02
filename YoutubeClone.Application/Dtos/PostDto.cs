using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeClone.Application.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public string VideoFileName { get; set; }
        public ProfileDto Author { get; set; }
        public bool IsLiked { get; set; }
        public string Reaction { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public DateTime Created { get; set; }

    }
}
