using System.Threading;
using CSharpNUnitCoreXOME.Common;
using CSharpNUnitCoreXOME.Pages;
using NUnit.Framework;

namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class SortSearchResultsByBathsTest : BaseTest
    {
        private string keyword = "Irvine, CA";

        public SortSearchResultsByBathsTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("2XOME")]
        [Author("Angela Tong")]
        public void SortSearchResultsByBathsDesc_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            SearchResultsSortPage sortpg = new SearchResultsSortPage(Driver);
            bool sorted = sortpg.SortByDescBaths();
            Assert.IsTrue(sorted, "Search results are not sorted by baths descending.");
        }

        [Test]
        [Category("2XOME")]
        [Author("Angela Tong")]
        public void SortSearchResultsByBathsAsc_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            SearchResultsSortPage sortpg = new SearchResultsSortPage(Driver);
            bool sorted = sortpg.SortByAscBaths();
            Assert.IsTrue(sorted, "Search results are not sorted by baths ascending.");
        }

    }
}
