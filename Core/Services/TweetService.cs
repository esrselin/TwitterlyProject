using Common.DTO;
using Core.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Core.Services
{
    public class TweetService : ITweetService
    {
        private readonly TwitterDbContext _context;

        public TweetService(TwitterDbContext context)
        {
            _context = context;
        }

        // Kullanıcının tweetlerini alır ve DTO'lara dönüştürür.
        public async Task<List<TweetDTO>> GetTweetsByUserIdsAsync(IEnumerable<int> userIds)
        {
            var tweets = await _context.Tweets
                .Include(t => t.TwitterUser)
                .Where(t => userIds.Contains(t.UserId))
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TweetDTO
                {
                    Id = t.Id,
                    Content = t.Content,
                    UserId = t.UserId,
                    CreatedAt = t.CreatedAt,
                    FirstName = t.TwitterUser.FirstName,  // TwitterUser.FirstName
                    LastName = t.TwitterUser.LastName,    // TwitterUser.LastName
                    UserName = t.TwitterUser.UserName     // TwitterUser.UserName
                    //TwitterUser = t.TwitterUser
                })
                .ToListAsync();

            return tweets;
        }


        // Yeni tweet ekler.
        public async Task AddTweetAsync(TweetDTO tweetDto)
        {
            // UserId kullanarak TwitterUser'ı veritabanından alıyoruz
            var twitterUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == tweetDto.UserId);

            if (twitterUser == null)
            {
                throw new Exception("User not found");
            }

            // Tweet entity'sini oluşturup, kullanıcıyla ilişkilendiriyoruz
            var tweetEntity = new Tweet
            {
                Content = tweetDto.Content,
                UserId = tweetDto.UserId,
                CreatedAt = tweetDto.CreatedAt,
                TwitterUser = twitterUser
            };

            await _context.Tweets.AddAsync(tweetEntity);
            await _context.SaveChangesAsync();

            // DTO'ya ID'yi geri döndürmek için
            tweetDto.Id = tweetEntity.Id;
        }

        public IQueryable<UserTweetCountDTO> GetUserTweetCountsAsync()
        {
            var userTweetCounts = _context.Users
                .Select(u => new UserTweetCountDTO
                {
                    UserName = u.UserName,
                    TweetCount = u.Tweets.Count()
                });

            return userTweetCounts.AsQueryable(); // IQueryable olarak döndürüyoruz
        }



    }
}
