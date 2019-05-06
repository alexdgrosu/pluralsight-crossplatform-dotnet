namespace CheckLinksConsole
{
    using HtmlAgilityPack;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

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

        public static IEnumerable<LinkCheckResult> CheckLinks(IEnumerable<string> links)
        {
            var all = Task.WhenAll(links.Select(CheckLink));
            return all.Result;
        }

        private async static Task<LinkCheckResult> CheckLink(string link)
        {
            var result = new LinkCheckResult();
            result.Link = link;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, link);
                try
                {
                    var response = await client.SendAsync(request);
                    result.Problem = response.IsSuccessStatusCode
                        ? null
                        : response.StatusCode.ToString();
                }
                catch (HttpRequestException x)
                {
                    result.Problem = x.Message;
                }
            }

            return result;
        }

        public class LinkCheckResult
        {
            public bool Exists => string.IsNullOrWhiteSpace(Problem);
            public bool IsMissing => !Exists;
            public string Problem { get; set; }
            public string Link { get; set; }
        }
    }
}