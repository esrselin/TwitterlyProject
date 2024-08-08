using Common.DTO;
using Core.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class TwitterUserService : ITwitterUserService
    {
        private readonly TwitterDbContext _context;

        public TwitterUserService(TwitterDbContext context)
        {
            _context = context;
        }

        // Belirli bir kullanıcıyı veritabanından almak ve istemcilere DTO formatında dönmek için
        public async Task<TwitterUserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Tweets)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            var userDto = new TwitterUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Tweets = user.Tweets.Select(t => new TweetDTO
                {
                    Id = t.Id,
                    Content = t.Content,
                    UserId = t.UserId,
                    CreatedAt = t.CreatedAt
                }).ToList(),
                
            };

            return userDto;
        }

        // Who To Follow
        public async Task<List<TwitterUserDTO>> GetRecentUsersAsync(int excludeUserId, int count)
        {
            var users = await _context.Users
                .Where(u => u.Id != excludeUserId)
                .OrderByDescending(u => u.Id)
                .Take(count)
                .Include(u => u.Tweets)
                .ToListAsync();

            var userDtos = users.Select(u => new TwitterUserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                
                FirstName = u.FirstName,
                LastName = u.LastName,
                Tweets = u.Tweets.Select(t => new TweetDTO
                {
                    Id = t.Id,
                    Content = t.Content,
                    UserId = t.UserId,
                    CreatedAt = t.CreatedAt
                }).ToList(),
                // FollowingIds ve FollowerIds gibi özellikler varsa manuel eklemelisiniz
            }).ToList();

            return userDtos;
        }
    }
}
