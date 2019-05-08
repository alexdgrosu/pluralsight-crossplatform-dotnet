namespace CheckLinksTest
{
    using CheckLinksConsole;
    using System.Linq;
    using Xunit;

    public class Tests
    {
        [Fact]
        public void WithoutHttpAtStartOfLink_NoLinks()
        {
            var links = LinkChecker.GetLinks(string.Empty, "<a href=\"google.com\" />");

            Assert.Empty(links);
        }

        [Fact]
        public void WithHttpAtStartOfLink_Links()
        {
            var links = LinkChecker.GetLinks(string.Empty, "<a href=\"http://google.com\" />");

            Assert.Single(links);
            Assert.Equal("http://google.com", links.First());
        }
    }
}