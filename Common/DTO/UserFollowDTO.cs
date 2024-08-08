using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class UserFollowDTO
    {
        public int FollowerId { get; set; }
        //public string FollowerUserName { get; set; }

        public int FolloweeId { get; set; }
        //public string FolloweeUserName { get; set; }

        //// Yeni özellikler
        //public TwitterUserDTO Follower { get; set; }
        //public TwitterUserDTO Followee { get; set; }
    }

}

