using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Account
{
    public class SignInByGoogleModel : BaseModel
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
