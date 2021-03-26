namespace Store.Shared.Options
{
    public class JwtOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
        public byte Lifetime { get; set; }
        public byte Length { get; set; }
    }
}
