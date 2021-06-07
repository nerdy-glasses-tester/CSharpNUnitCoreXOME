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
    public class FilterBySquareFtTest: BaseTest
    {
        private string keyword = "Irvine, CA";
        private string minsqft = "1000";
        private string maxsqft = "1500";

        public FilterBySquareFtTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterBySquareFt_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Thread.Sleep(1000); //Let search results page load
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            MoreFiltersPage morefilterspg = new MoreFiltersPage(Driver);
            morefilterspg.FilterBySquareFt(minsqft, maxsqft);
            bool isFiltered = morefilterspg.MoreFilterBySqFt.VerifyIsFilterBySqFt(minsqft, maxsqft);
            Assert.IsTrue(isFiltered, "Search results are not filtered by sq feet correctly.");
        }
    }
}
