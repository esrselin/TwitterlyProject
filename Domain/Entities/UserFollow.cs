namespace Domain.Entities
{

    public class UserFollow
    {
        public int FollowerId { get; set; }
        public virtual TwitterUser Follower { get; set; }

        public int FolloweeId { get; set; }
        public virtual TwitterUser Followee { get; set; }
    }
}