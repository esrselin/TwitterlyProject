using Common.DTO;
using Domain.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class UserFollowService : IUserFollowService
    {
        private readonly TwitterDbContext _context;
      

      

        public UserFollowService(TwitterDbContext context)
        {
            _context = context;
          
        }

        // Takip ettiğimiz kişilerin gönderilerini göstermek
        public async Task<List<string>> GetFollowingIdsByUserIdAsync(string userId)
        {
            var followIds = await _context.UserFollows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FolloweeId)
                .ToListAsync();
            return followIds;
        }

        // Yeni bir takip ekler
        public async Task<bool> AddFollowAsync(UserFollow follow)
        {
            try
            {
                
                var existingFollow = await _context.UserFollows
                                                   .AsNoTracking()
                                                   .FirstOrDefaultAsync(x => x.FollowerId == follow.FollowerId && x.FolloweeId == follow.FolloweeId);

                
                //if (existingFollow != null)
                //{
                //    return true; 
                //}

                var entity = new UserFollow
                {
                    FolloweeId = follow.FolloweeId,
                    FollowerId = follow.FollowerId,
                };

                await _context.UserFollows.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Hata loglama
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        // Takibi kaldırır
        public async Task RemoveFollowAsync(UserFollowDTO followDto)
        {
            var followEntity = new UserFollow
            {
                FollowerId = followDto.FollowerId,
                FolloweeId = followDto.FolloweeId
            };

            _context.UserFollows.Remove(followEntity);
            await _context.SaveChangesAsync();
        }

        // Belirli bir takip kaydını getirir
        public async Task<UserFollowDTO> GetUserFollowAsync(string followerId, string followeeId)
        {
            var follow = await _context.UserFollows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);

            if (follow == null)
                return null;

            var followDto = new UserFollowDTO
            {
                FollowerId = follow.FollowerId,
                FolloweeId = follow.FolloweeId
                // FollowerUserName ve FolloweeUserName bilgileri gerekiyorsa, bunları da eklemelisiniz.
            };

            return followDto;
        }

        public async Task<UserFollow> FindUserByIdAsync(string userId)
        {
            // Kullanıcının takip edilip edilmediği kontrolü sağlanır.
            var existingFollow = await _context.UserFollows
                .AsNoTracking()
                .Include(f => f.Followee) // Followee bilgilerini de yüklüyoruz
                .ThenInclude(f=>f.Followers)
                .FirstOrDefaultAsync(f => f.FolloweeId == userId);

            if (existingFollow != null)
            {
                //var followEntity = new UserFollowDTO
                //{
                //    FolloweeId = existingFollow.FolloweeId,
                //    FolloweeUserName = existingFollow.Followee.UserName
                //};
                return existingFollow;
            }
            return null;
        }


    }
}
