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
    public class FilterByBathTest: BaseTest
    {

        private string keyword = "Irvine, CA";
        private string bath = "3";
        public FilterByBathTest(string browser) : base(browser)
        {

        }


        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByBath_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            FilterByBathPage filterbybathpg = new FilterByBathPage(Driver);
            filterbybathpg.FilterByBath(bath);
            bool isFiltered = filterbybathpg.VerifyIsFilterByBath(bath);
            Assert.IsTrue(isFiltered, "Search results are not filtered by bath.");
        }
    }
}
