using CSharpNUnitCoreXOME.Pages;
using CSharpNUnitCoreXOME.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class SearchForSpecificPlaceTest : BaseTest
    {
        private string place = "12512 Brighton Pl Tustin, CA 92780";

        private string pageTitle =
            "12512 Brighton Pl Tustin CA 92780 Property Record & Valuation | Real Estate & Homes For Sale";

        public SearchForSpecificPlaceTest(string browser) : base(browser)
        {

        }

        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void SearchForSpecificPlace_Test()
        {
            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.SearchSpecificPlace(place);
            Assert.IsTrue(searchresultspg.IsLoaded(pageTitle), "Specific search results page was not loaded.");
            Assert.IsTrue(searchresultspg.CheckSpecificSearchResultMatchKeyword(place), "Specific search results did not match keyword address.");

        }

    }
}
