using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Presentation.Controllers.Base;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

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
        public async Task BlockUserAsync(string id)
        {
            await _userService.BlockUserAsync(id);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("FilterUsers")]
        public Task<PageModel<UserModel>> FilterUsersAsync([FromBody] UserFilter filter)
        {
            var userModels = _userService.FilterUsersAsync(filter);
            return userModels;
        }

        [Authorize]
        [HttpPost("UploadPhoto")]
        public async Task UploadPhotoAsync([FromForm] IFormFile file)
        {
            var test = file;
            await _userService.UploadPhotoAsync();
        }
    }
}
