using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;

namespace Core.Interfaces
{
    public interface ITweetService
    {
        Task<List<TweetDTO>> GetTweetsByUserIdsAsync(IEnumerable<int> userIds);
        Task AddTweetAsync(TweetDTO tweet);

        IQueryable<UserTweetCountDTO> GetUserTweetCountsAsync();
    }
}
