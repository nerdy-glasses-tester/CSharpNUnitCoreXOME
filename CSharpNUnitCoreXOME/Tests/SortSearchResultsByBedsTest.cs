using System.Threading;
using CSharpNUnitCoreXOME.Common;
using CSharpNUnitCoreXOME.Pages;
using NUnit.Framework;

namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class SortSearchResultsByBedsTest : BaseTest
    {
        private string keyword = "Irvine, CA";

        public SortSearchResultsByBedsTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("2XOME")]
        [Author("Angela Tong")]
        public void SortSearchResultsByBedsDesc_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            SearchResultsSortPage sortpg = new SearchResultsSortPage(Driver);
            bool sorted = sortpg.SortByDescBeds();
            Assert.IsTrue(sorted, "Search results are not sorted by beds descending.");
        }

        [Test]
        [Category("2XOME")]
        [Author("Angela Tong")]
        public void SortSearchResultsByBedsAsc_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            SearchResultsSortPage sortpg = new SearchResultsSortPage(Driver);
            bool sorted = sortpg.SortByAscBeds();
            Assert.IsTrue(sorted, "Search results are not sorted by beds ascending.");
        }

    }
}
