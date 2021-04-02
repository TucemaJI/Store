using Store.BusinessLogic.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Account
{
    public class TokenModel : BaseModel
    {
        [Required]
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
