namespace Domain.Entities
{

    public class UserFollow
    {
        public string FollowerId { get; set; }
        public TwitterUser Follower { get; set; }

        public string FolloweeId { get; set; }
        public TwitterUser Followee { get; set; }
    }
}