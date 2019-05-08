namespace CheckLinksConsole
{
    using Microsoft.Extensions.Logging;
    using System.IO;
    using System.Linq;
    using System.Net.Http;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new Config(args);
            Logs.Init(config.ConfigurationRoot);
            var logger = Logs.Factory.CreateLogger<Program>();

            Directory.CreateDirectory(config.Output.ReportDirectory);
            logger.LogInformation(200, $"Saving report to {config.Output.ReportFilePath}");

            var client = new HttpClient();
            var body = client.GetStringAsync(config.Site);
            logger.LogDebug(body.Result);

            var links = LinkChecker.GetLinks(config.Site, body.Result);

            var checkedLinks = LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(config.Output.ReportFilePath))
            using (var linksDb = new LinksDb())
            {
                foreach (var link in checkedLinks.OrderByDescending(l => l.IsMissing))
                {
                    var status = link.IsMissing ? "MISSING" : "OK";
                    file.WriteLine($"{status} - {link.Link}");
                    linksDb.Links.Add(link);
                }

                linksDb.SaveChanges();
            }
        }
    }
}