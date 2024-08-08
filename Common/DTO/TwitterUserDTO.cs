using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Common.DTO
{
    public class TwitterUserDTO 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public ICollection<TweetDTO> Tweets { get; set; } = new List<TweetDTO>();


        public ICollection<string> FollowingIds { get; set; } = new List<string>();
        public ICollection<string> FollowerIds { get; set; } = new List<string>();
    }
}
