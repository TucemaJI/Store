namespace Store.Shared.Constants
{
    public partial class Constants
    {
        public class JwtOptions
        {
            public const string AUDIENCE = "MyAuthClient";
            public const string ISSUER = "MyAuthServer";
            public const string KEY = "mysupersecret_secretkey!123";
            public const byte LIFETIME = byte.MaxValue;
            public const byte LENGTH = 32;
        }
    }
}
