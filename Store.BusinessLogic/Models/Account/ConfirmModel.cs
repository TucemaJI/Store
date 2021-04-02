using Store.BusinessLogic.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Account
{
    public class ConfirmModel : BaseModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
