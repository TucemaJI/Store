using Store.BusinessLogic.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Account
{
    public class SignInByGoogleModel : BaseModel
    {
        [Required]
        public string Provider { get; set; }
        [Required]
        public string IdToken { get; set; }
    }
}
