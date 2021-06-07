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
    public class FilterByPropertyTypeHouseTest: BaseTest
    {
        private string keyword = "Irvine, CA";
        private string property_type = "House";

        public FilterByPropertyTypeHouseTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByPropertyTypeHouse_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            MoreFiltersPage morefilterspg = new MoreFiltersPage(Driver);
            PropertyDetailsPage propertydetailspg = morefilterspg.FilterByPropertyType(property_type);

            List<string> arrlist = propertydetailspg.Validate3PropertyType();
            bool isFiltered = morefilterspg.MoreFilterByPropertyType.VerifyFilterByPropertyTypeHouse(arrlist);
            Assert.IsTrue(isFiltered, "Failed to filter by property type house.");
        }
    }
}
