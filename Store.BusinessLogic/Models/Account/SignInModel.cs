using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Account
{
    public class SignInModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
