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
        Task<TwitterUserDTO> GetUserByIdAsync(string userId);
        Task<List<TwitterUserDTO>> GetRecentUsersAsync(string excludeUserId, int count);
    }
}
