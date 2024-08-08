using Common.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        public async Task<List<int>> GetFollowingIdsByUserIdAsync(int userId)
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
            //try
            //{

            //    var existingFollow = await _context.UserFollows
            //                                       .AsNoTracking()
            //                                       .FirstOrDefaultAsync(x => x.FollowerId == follow.FollowerId && x.FolloweeId == follow.FolloweeId);


            //    //if (existingFollow != null)
            //    //{
            //    //    return true; 
            //    //}

            //    var entity = new UserFollow
            //    {
            //        FolloweeId = follow.FolloweeId,
            //        FollowerId = follow.FollowerId,
            //    };

                await _context.UserFollows.AddAsync(follow);
                await _context.SaveChangesAsync();
                return true;
            }
        //catch (Exception ex)
        //{
        //    // Hata loglama
        //    Console.WriteLine(ex.Message);
        //    return false;
        //}




        // Takibi kaldırır
        public async Task<bool> RemoveFollowAsync(int followerId, int followeeId)
        {
            try
            {
                var followEntity = await _context.UserFollows
                    .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);

                if (followEntity == null)
                {
                    return false; // Takip kaydı bulunamadıysa false döner
                }

                _context.UserFollows.Remove(followEntity);
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


        // Belirli bir takip kaydını getirir
        public async Task<UserFollowDTO> GetUserFollowAsync(int followerId, int followeeId)
        {
            var follow = await _context.UserFollows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);

            if (follow == null)
                return null;

            var followDto = new UserFollowDTO
            {
                FollowerId = follow.FollowerId,
                FolloweeId = follow.FolloweeId,
                // İsteğe bağlı olarak: FollowerUserName ve FolloweeUserName bilgileri gerekiyorsa ekleyebilirsiniz.
            };

            return followDto;
        }

        //public async Task<UserFollow> FindUserByIdAsync(int userId)
        //{
        //    // Kullanıcının takip edilip edilmediği kontrolü sağlanır.
        //    try
        //    {
        //        //var existingFollow = await _context.UserFollows
        //        //    .AsNoTracking()
        //        //    //.Include(f => f.Followee) // Followee bilgilerini de yüklüyoruz
        //        //    //.ThenInclude(f => f.Followers)
        //        //    .FirstOrDefaultAsync(f => f.FolloweeId == userId);

        //        var entity = await _context.Users.SingleOrDefaultAsync(f=>f.Id==userId);


        //    if (entity != null)
        //    {
        //        //var followEntity = new UserFollowDTO
        //        //{
        //        //    FolloweeId = existingFollow.FolloweeId,
        //        //    FolloweeUserName = existingFollow.Followee.UserName
        //        //};
        //        return entity;
        //    }
        //    }
        //    catch (Exception ex) {

        //        var exx = ex;
        //    }
        //    return null;
        //}


    }
}
