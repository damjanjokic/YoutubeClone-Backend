using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace YoutubeClone.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30)]
        public string DisplayName { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostReaction> Likes { get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }
        public ICollection<UserSubscription> Subscribers { get; set; }
             
    }
}