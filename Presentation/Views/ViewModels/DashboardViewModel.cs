using System.Collections.Generic;
using Domain.Entities;

namespace Presentation.Views.ViewModels
{
    public class DashboardViewModel
    {
        public List<Tweet> Tweets { get; set; }
        public List<TwitterUser> RecentUsers { get; set; }
        public List<int> FollowingUsers { get; set; } // Takip edilen kullanıcıların id'leri

    }
}
