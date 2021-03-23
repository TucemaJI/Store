using Store.BusinessLogic.Models.Account;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mappers
{
    public class RegisterMapper : BaseMapper<User, RegistrationModel>
    {
        public override User Map(RegistrationModel element)
        {
            return new User
            {
                FirstName = element.FirstName,
                LastName = element.LastName,
                Email = element.Email,
                UserName = $"{element.FirstName}{element.LastName}",
            };
        }

        public override RegistrationModel Map(User element)
        {
            return new RegistrationModel
            {
                FirstName = element.FirstName,
                LastName = element.LastName,
                Email = element.Email,
            };
        }
    }
}
