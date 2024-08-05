using Common.DTO;
using Domain.Entities;

public interface IUserFollowService
{
    Task<List<string>> GetFollowingIdsByUserIdAsync(string userId);
    Task<bool> AddFollowAsync(UserFollow follow);
    Task RemoveFollowAsync(UserFollowDTO follow);
    Task<UserFollowDTO> GetUserFollowAsync(string followerId, string followeeId);

    Task<UserFollow> FindUserByIdAsync(string userId);
}
