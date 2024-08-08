using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TwitterUser : IdentityUser<int>
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }

        // Followings: Users that this user is following
        public virtual ICollection<UserFollow> Followings { get; set; }
        // Followers: Users that are following this user
        public virtual ICollection<UserFollow> Followers { get; set; }
    }
}
