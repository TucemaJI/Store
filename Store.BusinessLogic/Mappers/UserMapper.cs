﻿using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mappers
{
    public class UserMapper : BaseMapper<User, UserModel>
    {
        public override User Map(UserModel element)
        {
            return new User
            {
                Id = element.Id,
                FirstName = element.FirstName,
                LastName = element.LastName,
                Email = element.Email,
            };
        }

        public override UserModel Map(User element)
        {
            return new UserModel
            {
                Id = element.Id,
                FirstName = element.FirstName,
                LastName = element.LastName,
                Email = element.Email,
            };
        }
    }
}
