using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> SaveAllAsync();
        void AddUserAsync(User user);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UserExistsAsync(string username);
    }
}