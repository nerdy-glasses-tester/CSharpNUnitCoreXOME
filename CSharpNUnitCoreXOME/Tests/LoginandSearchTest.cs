using System;
using System.Threading;
using CSharpNUnitCoreXOME.Pages;
using CSharpNUnitCoreXOME.Common;
using NUnit.Framework;


namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    public class LoginandSearchTest: BaseTest
    {
        private string username = "Automation Tester";
        private string keyword = "Irvine, CA";
        private string pageTitle = "Listing Search Form - Search for Real Estate Properties | Real Estate & Homes For Sale";
        //private string pageTitle = "Xome Retail | Real Estate & Homes For Sale";

        public LoginandSearchTest(string browser):base(browser)
        {

        }


        [Test]
        [Category("XOME")]
        [Author("Angela Tong")]
        public void LoginandSearch_Test()
        {
            LoginPage loginpg = new LoginPage(Driver);
            loginpg.Login("sqatester2018@gmail.com", "TestPassword2021");
            string loggedInUser = loginpg.GetLoggedInUser();
            Assert.AreEqual(username, loggedInUser, $"Logged in user name doesn't match. Expected => {username}" +
            $" Actual=>{loggedInUser}");

            HomePageSearch search = new HomePageSearch(Driver);
            var searchresultspg = search.Search(keyword);
            Assert.IsTrue(searchresultspg.IsLoaded(pageTitle), "Search results page was not loaded.");
            Assert.IsTrue(searchresultspg.CheckSearchResultsMatchKeyword(keyword), "Search results did not match keyword.");
        }
    }
}
