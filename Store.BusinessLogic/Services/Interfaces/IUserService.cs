using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Models.Filters;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserModel> GetUserAsync(string email);
        public Task CreateUserAsync(UserModel model);
        public Task<IdentityResult> DeleteUserAsync(string id);
        public Task<IdentityResult> UpdateUserAsync(UserModel userModel);
        public Task BlockUserAsync(BlockModel model);
        public Task<PageModel<UserModel>> FilterUsersAsync(UserFilter filter);
    }
}
