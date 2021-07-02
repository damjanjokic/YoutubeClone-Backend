using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Core.Entities;

namespace YoutubeClone.Application.Dtos
{
    public class ProfileDto
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public bool Subscription { get; set; }
        public int SubscriptionCount { get; set; }
        public int SubscribersCount { get; set; }
    }
}
