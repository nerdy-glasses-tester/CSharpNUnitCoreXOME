using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CSharpNUnitCoreXOME.Common;
using CSharpNUnitCoreXOME.Pages;
using NUnit.Framework;

namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class SortSearchResultsByPriceTest : BaseTest
    {
        private string keyword = "Irvine, CA";

        public SortSearchResultsByPriceTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("2XOME")]
        [Author("Angela Tong")]
        public void SortSearchResultsByPriceDesc_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            SearchResultsSortPage sortpg = new SearchResultsSortPage(Driver);
            bool sorted = sortpg.SortByDescPrice();
            Assert.IsTrue(sorted, "Search results are not sorted by price descending.");

        }

     
        [Test]
        [Category("2XOME")]
        [Author("Angela Tong")]
        public void SortSearchResultsByPriceAsc_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            SearchResultsSortPage sortpg = new SearchResultsSortPage(Driver);
            bool sorted = sortpg.SortByAscPrice();
            Assert.IsTrue(sorted, "Search results are not sorted by price ascending.");
        }
      
    }
}
