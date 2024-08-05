using System.Collections.Generic;
using Domain.Entities;

namespace Twitter.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<Tweet> Tweets { get; set; }
        public List<TwitterUser> RecentUsers { get; set; }
    }
}
