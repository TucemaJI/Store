using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task CreateUserAsync(UserModel model)
        {
            await _userService.CreateUserAsync(model);
        }

        [HttpGet("GetUser")]
        public async Task<UserModel> GetUserAsync(string email)
        {
            return await _userService.GetUserAsync(email);
        }

        [HttpGet("GetAllUsers")]
        public async Task<List<UserModel>> GetUsersAsync()
        {
            return await _userService.GetUsersAsync();
        }

        [HttpGet("GetRole")]
        public async Task<string> GetRoleAsync(string email)
        {
            return await _userService.GetRoleAsync(email);
        }

        [HttpGet("CreateRole")]
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await _userService.CreateRoleAsync(roleName);
        }

        [HttpGet("GetAllRoles")]
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _userService.GetAllRoles();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IdentityResult> DeleteUserAsync(UserModel userModel)
        {
            return await _userService.DeleteUserAsync(userModel);
        }

        [HttpPut("UpdateUser")]
        public async Task<IdentityResult> UpdateUserAsync(UserModel userModel)
        {
            return await _userService.UpdateUserAsync(userModel);
        }

        [HttpGet("BlockUser")]
        public async Task BlockUserAsync(string email)
        {
            await _userService.BlockUserAsync(email);
        }

        [HttpGet("FilterUsers")]
        public async Task<List<UserModel>> FilterUsersAsync(string filter, string filterBy)
        {
            return await _userService.FilterUsersAsync(filter, filterBy);
        }
    }
}
