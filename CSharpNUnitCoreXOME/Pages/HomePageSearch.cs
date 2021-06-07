using NLog;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System.Threading;

namespace CSharpNUnitCoreXOME.Pages
{
    public class HomePageSearch : BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(45));

        private IWebElement SearchField => driver.FindElement(By.Id("homepage-search-field"));

        private IWebElement SearchBtn => driver.FindElement(By.CssSelector("button.call-to-action.search-field-button"));

        private IWebElement SpecificPlaceLoaded => Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".property-address")));

        public HomePageSearch(IWebDriver Driver) : base(Driver)
        {

        }

        public SearchResultsPage Search(String keyword)
        {
            SearchField.SendKeys(keyword);
            SearchBtn.Click();
            Thread.Sleep(4000);
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Search for " + $"{keyword}.");
            return new SearchResultsPage(driver);
        }

        public SearchResultsPage SearchSpecificPlace (String place)
        {
            SearchField.SendKeys(place);
            SearchBtn.Click();
            Thread.Sleep(3000);
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Search for " + $"{place}.");
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
            if (SpecificPlaceLoaded.GetAttribute("innerHTML").Contains("12512 Brighton Pl"))
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Search Results Page Loaded.");
            }

            return new SearchResultsPage(driver);
        }
    }
}
