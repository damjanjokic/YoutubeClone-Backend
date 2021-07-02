namespace YoutubeClone.Core.Entities
{
    public class UserSubscription
    {
        public int ObserverId { get; set; }
        public AppUser Observer { get; set; }
        public int TargetId { get; set; }
        public AppUser Target { get; set; }
    }
}