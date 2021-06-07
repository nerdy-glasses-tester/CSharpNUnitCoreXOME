using CSharpNUnitCoreXOME.Common;
using CSharpNUnitCoreXOME.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class FilterByBedTest: BaseTest
    {
        private string keyword = "Irvine, CA";
        private string bed = "3";

        public FilterByBedTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByBed_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            FilterByBedPage filterbybedpg = new FilterByBedPage(Driver);
            filterbybedpg.FilterByBed(bed);
            bool isFiltered = filterbybedpg.VerifyIsFilterByBed(bed);
            Assert.IsTrue(isFiltered, "Search results are not filtered by bed.");
        }
    }
}
