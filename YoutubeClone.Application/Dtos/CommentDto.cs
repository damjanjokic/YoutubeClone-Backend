using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YoutubeClone.Core.Entities;

namespace YoutubeClone.Application.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public int ParentId { get; set; }
        public string AuthorUsername { get; set; }
    }
}
