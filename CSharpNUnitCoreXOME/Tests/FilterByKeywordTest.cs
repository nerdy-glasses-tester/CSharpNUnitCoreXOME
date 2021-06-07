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
    public class FilterByKeywordTest: BaseTest
    {
        private string keyword = "Irvine, CA";
        private string filterkeyword = "pool";

        public FilterByKeywordTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByKeyword_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Thread.Sleep(1000); //Let search results page load
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            MoreFiltersPage morefilterspg = new MoreFiltersPage(Driver);
            PropertyDetailsPage propertydetailspg = morefilterspg.FilterByKeyword(filterkeyword);
            List<Boolean> arrlist = propertydetailspg.Validate3Keyword(filterkeyword);
            bool isFiltered = morefilterspg.MoreFilterByKeyword.VerifyFilteredKeyword(arrlist, filterkeyword);
            Assert.IsTrue(isFiltered, "Failed to filter by keyword in more filters.");
           
        }
    }
}
