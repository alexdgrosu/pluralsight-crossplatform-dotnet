namespace CheckLinksConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;

    public class LinkChecker
    {
        public static IEnumerable<string> GetLinks(string page)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);

            var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                .Select(n => n.GetAttributeValue("href", string.Empty))
                .Where(l => !string.IsNullOrEmpty(l))
                .Where(l => l.StartsWith("http"));

            return links;
        }
    }
}