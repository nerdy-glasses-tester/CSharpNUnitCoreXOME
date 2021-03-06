using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class FilterByPricePage : BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IWebElement FilterPriceMenu => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#ddbtn-criteria-pricerange>#ddbtn-label-pricerange>.ddbtn-label-arrow.fa.fa-angle-down")));

        private IWebElement FilterMinPrice => driver.FindElement(By.Id("mapsearch-criteria-minprice"));

        private IWebElement FilterMaxPrice => driver.FindElement(By.Id("mapsearch-criteria-maxprice"));

        private IList<IWebElement> PriceResults => driver.FindElements(By.CssSelector(".price>span"));

        public FilterByPricePage(IWebDriver Driver) : base(Driver)
        {

        }

        public void FilterByPrice(string minprice, string maxprice)
        {
            FilterPriceMenu.Click();
            FilterMinPrice.SendKeys(minprice);
            FilterMaxPrice.SendKeys(maxprice);
            Thread.Sleep(1000); //Need wait before click to process correctly
            FilterPriceMenu.Click();
            Thread.Sleep(3000); //Need wait to load page

            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                "Filter by price range " + $"{minprice} - " + $"{maxprice}.");

        }

        public bool VerifyIsFilterByPrice(string minprice, string maxprice)
        {
            
            bool isFiltered = false;
            String filteredresultsprice = Regex.Replace(PriceResults[0].Text, "[$,]", "");
            //Console.WriteLine(filteredresultsprice);
            int result = Int32.Parse(filteredresultsprice);
            int min = int.Parse(minprice);
            int max = int.Parse(maxprice);

            if(result>=min && result<=max)
            {
                isFiltered = true;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,"Verified it filtered by price range.");
            }
            else
            {
                isFiltered = false;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,"Failed to filter by price range.");
            }

            return isFiltered;
        }
    }
}
