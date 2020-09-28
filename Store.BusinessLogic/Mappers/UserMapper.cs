using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mappers
{
    public class UserMapper : BaseMapper<User, UserModel>
    {
        public override User Map(UserModel element)
        {
            return new User
            {
                FirstName = element.FirstName,
                LastName = element.LastName,
                Email = element.Email,
                UserName = element.UserName,
            };
        }

        public override UserModel Map(User element)
        {
            return new UserModel
            {
                FirstName = element.FirstName,
                LastName = element.LastName,
                Email = element.Email,
                UserName = element.UserName,
            };
        }
    }
}
