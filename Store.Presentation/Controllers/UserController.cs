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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("CreateUser")]
        public async Task CreateUserAsync(UserModel model)
        {
            await _userService.CreateUserAsync(model);
        }

        [Authorize]
        [HttpGet("GetUser")]
        public Task<UserModel> GetUserAsync(string id)
        {
            var result = _userService.GetUserAsync(id);
            return result;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("DeleteUser")]
        public Task<IdentityResult> DeleteUserAsync(UserModel userModel)
        {
            var result = _userService.DeleteUserAsync(userModel.Id);
            return result;
        }

        [Authorize]
        [HttpPut("UpdateUser")]
        public Task<UserModel> UpdateUserAsync(UserModel userModel)
        {
            var result = _userService.UpdateUserAsync(userModel);
            return result;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet("BlockUser")]
        public async Task BlockUserAsync(BlockModel model)
        {
            await _userService.BlockUserAsync(model.Id);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("FilterUsers")]
        public Task<PageModel<UserModel>> FilterUsersAsync([FromBody] UserFilter filter)
        {
            var userModels = _userService.FilterUsersAsync(filter);
            return userModels;
        }
    }
}
