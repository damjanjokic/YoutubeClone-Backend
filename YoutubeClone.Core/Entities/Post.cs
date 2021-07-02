using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoutubeClone.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public AppUser Author { get; set; }
        public DateTime Created { get; set; }
        public string VideoFileName { get; set; }
        public ICollection<PostReaction> PostReactions { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}