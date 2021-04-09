using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.Extentions;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> FilterUsersAsync(UserFilter filter)
        {
            var users = await _userManager.Users.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"))
                .Where(u => EF.Functions.Like(u.UserName, $"%{filter.Name}%"))
                .Where(u => filter.IsBlocked.Equals(null) || u.IsBlocked.Equals(filter.IsBlocked))
                .OrderBy(filter.OrderByField, filter.IsDescending)
                .ToSortedListAsync(pageNumber: filter.PageOptions.CurrentPage, pageSize: filter.PageOptions.ItemsPerPage);
            return users;
        }

        public async Task CountAsync(UserFilter filter)
        {
            filter.PageOptions.TotalItems = await _userManager.Users.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"))
                .Where(u => EF.Functions.Like(u.UserName, $"%{filter.Name}%"))
                .Where(u => filter.IsBlocked.Equals(null) || u.IsBlocked.Equals(filter.IsBlocked))
                .CountAsync();
        }
    }
}
