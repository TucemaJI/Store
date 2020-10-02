using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    [Authorize(Roles = "Admin")] // change to enum or use const
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
        }

        public async Task CreateUserAsync(UserModel model)
        {
            await _userService.CreateUserAsync(model);
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            return await _userService.GetUserAsync(email);
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await _userService.GetUsersAsync();
        }
        public async Task<string> GetRoleAsync(string email)
        {
            return await _userService.GetRoleAsync(email);
        }
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await _userService.CreateRoleAsync(roleName);
        }
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _userService.GetAllRoles();
        }
        public async Task<IdentityResult> DeleteUserAsync(UserModel userModel)
        {
            return await _userService.DeleteUserAsync(userModel);
        }
        public async Task<IdentityResult> UpdateUserAsync(UserModel userModel)
        {
            return await _userService.UpdateUserAsync(userModel);
        }
    }
}
