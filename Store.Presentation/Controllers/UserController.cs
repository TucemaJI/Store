using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
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
        public Task CreateUserAsync(UserModel model)
        {
            return _userService.CreateUserAsync(model);
        }

        [AllowAnonymous]
        [HttpGet("GetUser")]
        public Task<UserModel> GetUserAsync(string email)
        {
            return _userService.GetUserAsync(email);
        }

        //[HttpGet("GetAllUsers")]
        //public Task<List<UserModel>> GetUsersAsync()
        //{
        //    return _userService.GetUsersAsync();
        //}

        [HttpGet("GetRole")]
        public Task<string> GetRoleAsync(string email)
        {
            return _userService.GetRoleAsync(email);
        }

        [HttpGet("CreateRole")]
        public Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return _userService.CreateRoleAsync(roleName);
        }

        [HttpGet("GetAllRoles")]
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _userService.GetAllRoles();
        }

        [HttpPost("DeleteUser")]
        public Task<IdentityResult> DeleteUserAsync(UserModel userModel)
        {
            return _userService.DeleteUserAsync(userModel);
        }

        [HttpPut("UpdateUser")]
        public Task<IdentityResult> UpdateUserAsync(UserModel userModel)
        {
            return _userService.UpdateUserAsync(userModel);
        }

        [HttpGet("BlockUser")]
        public Task BlockUserAsync(string email)
        {
            return _userService.BlockUserAsync(email);
        }

        [HttpPost("FilterUsers")]
        public Task<PageModel<UserModel>> FilterUsersAsync([FromBody] UserFilter filter)
        {
            var userModels = _userService.FilterUsersAsync(filter);

            return userModels;
        }
    }
}
