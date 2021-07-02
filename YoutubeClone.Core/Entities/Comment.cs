using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoutubeClone.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public int? ParentId { get; set; }
        public int AuthorId { get; set; }
        public AppUser Author { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}