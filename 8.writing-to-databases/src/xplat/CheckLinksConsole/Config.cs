namespace CheckLinksConsole
{
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.IO;

    public class Config
    {
        private static readonly Dictionary<string, string> InMemory = new Dictionary<string, string>
            {
                { "site", "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps" },
                { "output:folder", "reports" },
                { "output:file", "report.txt" }
            };

        public Config(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(InMemory)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("checksettings.json", optional: true)
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            Site = configuration["site"];
            Output = configuration.GetSection("output").Get<OutputSettings>();
            ConfigurationRoot = configuration;
        }

        public string Site { get; set; }
        public OutputSettings Output { get; set; }

        public IConfigurationRoot ConfigurationRoot { get; set; }
    }
}