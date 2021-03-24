namespace Store.Shared.Constants
{
    public partial class Constants
    {
        public class AccountServiceOptions
        {
            public const string REFRESH_TOKEN = "RefreshToken";
            public const byte PASSWORD_LENGTH = 9;
            public const string CALLBACK_URL = "http://localhost:4200/confirm-email?mail={0}&name={1}&lName={2}&token={3}";
            public const string MESSAGE = "Confirm registration using this link: <a href='{0}'>confirm registration</a>";
        }
    }
}
