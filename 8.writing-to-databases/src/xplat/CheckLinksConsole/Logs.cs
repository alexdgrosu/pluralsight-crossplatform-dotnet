namespace CheckLinksConsole
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public static class Logs
    {
        public static LoggerFactory Factory = new LoggerFactory();

        public static void Init(IConfiguration configuration)
        {
#pragma warning disable 0618
            Factory.AddConsole(configuration.GetSection("Logging"));
#pragma warning restore 0618
            Factory.AddFile(
                "logs/checklinks-{Date}.json",
                isJson: true,
                minimumLevel: LogLevel.Trace);
        }
    }
}