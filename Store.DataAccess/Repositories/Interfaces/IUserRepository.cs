using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> FilterUsersAsync(UserFilter filter);
        public Task CountAsync(UserFilter filter);
    }
}
