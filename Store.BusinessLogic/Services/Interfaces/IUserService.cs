using Store.BusinessLogic.Models.Users;
using System;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        public Task<UserModel> GetUserAsync(string email);
    }
}
