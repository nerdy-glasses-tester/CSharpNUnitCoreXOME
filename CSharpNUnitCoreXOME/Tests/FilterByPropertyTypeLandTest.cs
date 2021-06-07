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
    public  class FilterByPropertyTypeLandTest: BaseTest
    {
        private string keyword = "Malibu, CA";
        private string property_type = "Land";

        public FilterByPropertyTypeLandTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void FilterByPropertyTypeLand_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
            MoreFiltersPage morefilterspg = new MoreFiltersPage(Driver);
            PropertyDetailsPage propertydetailspg = morefilterspg.FilterByPropertyType(property_type);

            List<string> arrlist = propertydetailspg.Validate3PropertyType();
            bool isFiltered = morefilterspg.MoreFilterByPropertyType.VerifyFilterByPropertyTypeLand(arrlist);
            Assert.IsTrue(isFiltered, "Failed to filter by property type land.");
        }

    }
 }
