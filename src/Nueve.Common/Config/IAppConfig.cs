namespace Nueve.Common.Config
{
    public interface IAppConfig
    {
        string AppName { get; set; }
        string Version { get; set; }
        string IdentitySecret { get; set; }
        string IdentityIssuer { get; set; }
        string IdentityAudience { get; set; }
        int IdentityTokenLiftimeInHours { get; set; }
        bool LoggingEnabled { get; set; }
    }
}
