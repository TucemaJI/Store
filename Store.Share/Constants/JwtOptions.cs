namespace Store.Shared.Constants
{
    public partial class Constants
    {
        public class JwtOptions
        {
            public const string Audience = "MyAuthClient";
            public const string Issuer = "MyAuthServer";
            public const string Key = "mysupersecret_secretkey!123";
            public const byte Lifetime = 1;
        }
    }
}
