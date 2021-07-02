using System.ComponentModel.DataAnnotations;

namespace YoutubeClone.Core.Entities
{
    public class PostReaction
    {
        public int LikerId { get; set; }
        public AppUser Liker { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Required]
        public string Reaction { get; set; }
    }
}