using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Account
{
    public class ConfirmModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
