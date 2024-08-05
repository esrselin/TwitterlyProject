
using Common.DTO;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

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
        public async Task<List<TweetDTO>> GetTweetsByUserIdsAsync(IEnumerable<string> userIds)
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
                    TwitterUser = t.TwitterUser
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




    }
}
