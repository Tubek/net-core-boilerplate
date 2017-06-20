namespace Nueve.Common.Config
{
    public class AppConfig : IAppConfig
    {
        public string AppName { get; set; } = "default.name";
        public string Version { get; set; } = "1.0";
        public string IdentitySecret { get; set; } = "default.secret.identity.key";
        public string IdentityIssuer { get; set; } = "default.issuer";
        public string IdentityAudience { get; set; } = "default.audience";
        public int IdentityTokenLiftimeInHours { get; set; } = 1;
        public bool LoggingEnabled { get; set; } = false;
    }
}
