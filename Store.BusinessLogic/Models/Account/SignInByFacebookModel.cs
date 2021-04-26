using Store.BusinessLogic.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Account
{
    public class SignInByFacebookModel : BaseModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
