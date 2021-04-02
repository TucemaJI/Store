using Store.BusinessLogic.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Account
{
    public class SignInModel : BaseModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
