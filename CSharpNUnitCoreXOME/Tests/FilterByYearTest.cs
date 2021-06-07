using CSharpNUnitCoreXOME.Common;
using CSharpNUnitCoreXOME.Pages;
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
    public class FilterByYearTest: BaseTest
    {
        private string keyword = "Irvine, CA";
        private string minyear = "2000";
        private string maxyear = "2019";

        public FilterByYearTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByYear_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Thread.Sleep(1000); //Let search results page load
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            MoreFiltersPage morefilterspg = new MoreFiltersPage(Driver);
            PropertyDetailsPage propertydetailspg = morefilterspg.FilterByYear(minyear, maxyear);
            List<String> propertyyears_arr = propertydetailspg.Validate3Year();
            bool isFiltered = morefilterspg.MoreFilterByYear.VerifyIsFilterByYear(propertyyears_arr, minyear, maxyear);
            Assert.IsTrue(isFiltered, "Search results are not filtered by year range correctly."); 
        }
    }
}
