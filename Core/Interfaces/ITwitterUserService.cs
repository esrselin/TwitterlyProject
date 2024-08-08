using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITwitterUserService
    {
        Task<TwitterUserDTO> GetUserByIdAsync(int userId);
        Task<List<TwitterUserDTO>> GetRecentUsersAsync(int excludeUserId, int count);
    }
}
