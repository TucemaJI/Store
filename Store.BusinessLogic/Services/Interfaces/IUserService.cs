using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserModel> GetUserAsync(string email);
        public Task CreateUserAsync(UserModel model);

        public Task<IEnumerable<UserModel>> GetUsersAsync();
        public Task<string> GetRoleAsync(string email);
        public Task<IdentityResult> CreateRoleAsync(string roleName);
        public IEnumerable<IdentityRole> GetAllRoles();
        public Task<IdentityResult> DeleteUserAsync(UserModel userModel);
        public Task<IdentityResult> UpdateUserAsync(UserModel userModel);
    }
}
