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
    public class FilterByPriceTest: BaseTest
    {
        private string keyword = "Irvine, CA";
        private string minprice = "800000";
        private string maxprice = "1300000";

        public FilterByPriceTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByPrice_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            FilterByPricePage filterbypricepg = new FilterByPricePage(Driver); 
            filterbypricepg.FilterByPrice(minprice, maxprice);
            bool isFiltered = filterbypricepg.VerifyIsFilterByPrice(minprice, maxprice);
            Assert.IsTrue(isFiltered, "Search results are not filtered by price.");
        }
    }
}
