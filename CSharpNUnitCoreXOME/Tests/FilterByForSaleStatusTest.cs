using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CSharpNUnitCoreXOME.Pages;
using CSharpNUnitCoreXOME.Common;
using NUnit.Framework;

namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class FilterByForSaleStatusTest : BaseTest
    {
        private string keyword = "Irvine, CA";

        public FilterByForSaleStatusTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByForSaleStatus_Test()
        {
            string status = "for sale";
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            MoreFiltersPage morefilterspg = new MoreFiltersPage(Driver);
            morefilterspg.FilterByPropertyStatus(status);
            bool isFiltered = morefilterspg.MoreFilterByListingStatus.VerifyFilteredStatus(status);
            Assert.IsTrue(isFiltered, "Failed to filter by for sale status.");
        }

     
    }
}
