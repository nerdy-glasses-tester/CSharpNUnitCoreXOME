using CSharpNUnitCoreXOME.Pages;
using CSharpNUnitCoreXOME.Common;
using NLog;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharpNUnitCoreXOME.Tests
{

    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class FilterByPendingStatusTest: BaseTest
    {
        private string keyword = "Irvine, CA";
        public FilterByPendingStatusTest(string browser) : base(browser)
        {

        }


        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByPendingStatus_Test()
        {
            string status = "pending";
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            MoreFiltersPage morefilterspg = new MoreFiltersPage(Driver);
            morefilterspg.FilterByPropertyStatus(status);
            bool isFiltered = morefilterspg.MoreFilterByListingStatus.VerifyFilteredStatus(status);
            Assert.IsTrue(isFiltered, "Failed to filter by pending status.");
        }
    }
}
