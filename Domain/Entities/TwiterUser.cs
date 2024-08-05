using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class TwitterUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public ICollection<Tweet> Tweets { get; set; } = new List<Tweet>();

        // Followings: Users that this user is following
        public ICollection<UserFollow> Followings { get; set; } = new List<UserFollow>();

        // Followers: Users that are following this user
        public ICollection<UserFollow> Followers { get; set; } = new List<UserFollow>();
    }
}
