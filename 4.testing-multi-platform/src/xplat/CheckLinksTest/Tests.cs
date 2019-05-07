namespace CheckLinksTest
{
    using System.Linq;
    using CheckLinksConsole;
    using Xunit;

    public class Tests
    {
        [Fact]
        public void WithoutHttpAtStartOfLink_NoLinks()
        {
            var links = LinkChecker.GetLinks("<a href=\"google.com\" />");

            Assert.Empty(links);
        }

        [Fact]
        public void WithHttpAtStartOfLink_Links()
        {
            var links = LinkChecker.GetLinks("<a href=\"http://google.com\" />");

            Assert.Single(links);
            Assert.Equal("http://google.com", links.First());
        }
    }
}
