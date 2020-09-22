using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using System.Collections.Generic;

namespace Store.BusinessLogic.Services
{
    public class UserService : BaseService<UserModel>, IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override void Create(UserModel item)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public override UserModel GetItem(long id)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<UserModel> GetList()
        {
            throw new System.NotImplementedException();
        }

        public override void Save()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(UserModel item)
        {
            throw new System.NotImplementedException();
        }

    }
}
