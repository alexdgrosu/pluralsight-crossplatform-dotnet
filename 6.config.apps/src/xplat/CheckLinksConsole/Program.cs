namespace CheckLinksConsole
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new Config(args);

            Directory.CreateDirectory(config.Output.ReportDirectory);

            Console.WriteLine($"Saving report to {config.Output.ReportFilePath}");

            var client = new HttpClient();
            var body = client.GetStringAsync(config.Site);
            Console.WriteLine(body.Result);

            Console.WriteLine();
            Console.WriteLine("Links");
            var links = LinkChecker.GetLinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);

            var checkedLinks = LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(config.Output.ReportFilePath))
            {
                foreach (var link in checkedLinks.OrderByDescending(l => l.IsMissing))
                {
                    var status = link.IsMissing ? "MISSING" : "OK";
                    file.WriteLine($"{status} - {link.Link}");
                }
            }
        }
    }
}