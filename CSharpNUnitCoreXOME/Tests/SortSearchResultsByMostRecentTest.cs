using System.Threading;
using CSharpNUnitCoreXOME.Common;
using CSharpNUnitCoreXOME.Pages;
using NUnit.Framework;

namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class SortSearchResultsByMostRecentTest : BaseTest
    {

        private string keyword = "Irvine, CA";

        public SortSearchResultsByMostRecentTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("2XOME")]
        [Author("Angela Tong")]
        public void SortSearchResultsByMostRecent_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            SearchResultsSortPage sortpg = new SearchResultsSortPage(Driver);
            bool sorted = sortpg.SortByDescPrice();
            Assert.IsTrue(sorted, "Search results are not sorted by price descending.");
            bool sortmostrecent = sortpg.SortByMostRecent();
            Assert.IsTrue(sortmostrecent, "Search results are not sorted by most recent.");

        }


    }
}
