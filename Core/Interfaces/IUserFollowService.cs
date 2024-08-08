using Common.DTO;
using Domain.Entities;

public interface IUserFollowService
{
    Task<List<int>> GetFollowingIdsByUserIdAsync(int userId);
    Task<bool> AddFollowAsync(UserFollow follow);
    Task<bool> RemoveFollowAsync(int followerId, int followeeId);
    Task<UserFollowDTO> GetUserFollowAsync(int followerId, int followeeId);

   // Task<UserFollow> FindUserByIdAsync(int userId);
}
