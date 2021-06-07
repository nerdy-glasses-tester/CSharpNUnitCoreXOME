using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class SearchResultsPage: BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IList<IWebElement> SearchResultsCity => Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("span[itemprop='addressLocality']")));
        private IList<IWebElement> SearchResultsState => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("span[itemprop='addressRegion']")));

        private IWebElement Address1 => driver.FindElement(By.CssSelector(".address-line-1.js-street-address-line-1"));

        private IWebElement Address2 => driver.FindElement(By.CssSelector(".address-line-2"));

        public SearchResultsPage(IWebDriver Driver): base(Driver)
        {
           
        }

        public bool IsLoaded(string pageTitle)
        {

            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                "Verify search results page title: "+driver.Title.Trim());
            return driver.Title.Contains(pageTitle);

        }

        public bool CheckSpecificSearchResultMatchKeyword(string keyword)
        {
            bool match = false;
            string actualaddress = "";

            string address1 = Address1.Text;
            string address2 = Address2.Text;
            actualaddress = address1 +" "+ address2;
            if (actualaddress.Equals(keyword))
            {
                match=true; 
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Specific property search results does match keyword. "+$"Actual => { actualaddress} "+$"Expected => { keyword}");
            }
            else
            {
                match = false;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Specific search results does not match keyword. " + $"Actual=>{actualaddress} " + $"Expected=>{keyword}");

            }


            return match;
        }

        public bool CheckSearchResultsMatchKeyword(string keyword)
        {
            //Have to trim the extra space in front of state after the comma: Irvine, CA
            bool match = false;
            bool citymatch = false;
            bool statematch = false;
            string[] arr = keyword.Split(",");
            string keywordcity=arr[0];
            string keywordstate=arr[1];
            keywordstate = keywordstate.Trim();
            string searchResultsCity = "";
            string searchResultState = "";

            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Checking through 3 search results if it matches keyword.");

            for (int i = 0; i<3; i++)
            {
                IWebElement element = SearchResultsCity[i];
                if (element.Displayed)
                {
                    searchResultsCity = element.Text;
                    Console.WriteLine(searchResultsCity);
                    Console.WriteLine(keywordcity);
                    if (!searchResultsCity.Equals(keywordcity))
                    {
                        citymatch = false;
                        break;
                    }
                    else
                    {
                        citymatch = true;
                    }
                }
                
            };

            for (int j = 0; j< 3; j++)
            {
                IWebElement element = SearchResultsState[j];
                if(element.Displayed)
                {
                    searchResultState = SearchResultsState[j].Text;
                    Console.WriteLine(searchResultState);
                    Console.WriteLine(keywordstate);

                    if (!searchResultState.Equals(keywordstate))
                    {
                        statematch = false;
                        break;
                    }
                    else
                    {
                        statematch = true;
                    }
                }

            };

            Console.WriteLine("citymatch:"+citymatch);
            Console.WriteLine("statematch:"+statematch);

            if (!citymatch || !statematch)
            {
                match = false;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results does not match keyword.");
            }
            else
            {
                match = true;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results matches keyword.");
            }

            return match;

        }
    }
}