using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Presentation.Controllers.Base;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task CreateUserAsync(UserModel model)
        {
            await _userService.CreateUserAsync(model);
        }

        [AllowAnonymous]
        [HttpGet("GetUser")]
        public Task<UserModel> GetUserAsync(string id)
        {
            var result = _userService.GetUserAsync(id);
            return result;
        }

        [HttpPost("DeleteUser")]
        public Task<IdentityResult> DeleteUserAsync(UserModel userModel)
        {
            var result = _userService.DeleteUserAsync(userModel);
            return result;
        }

        [HttpPut("UpdateUser")]
        public Task<IdentityResult> UpdateUserAsync(UserModel userModel)
        {
            var result = _userService.UpdateUserAsync(userModel);
            return result;
        }

        [HttpGet("BlockUser")]
        public async Task BlockUserAsync(BlockModel model)
        {
            await _userService.BlockUserAsync(model);
        }

        [HttpPost("FilterUsers")]
        public Task<PageModel<UserModel>> FilterUsersAsync([FromBody] UserFilter filter)
        {
            var userModels = _userService.FilterUsersAsync(filter);
            return userModels;
        }
    }
}
