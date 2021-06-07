using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NLog;
using NLog.Time;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class SearchResultsSortPage: BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));

        private IWebElement SortByBtn => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("button.btn.dropdown-toggle.btn-default[data-id='search-results-sorttype']")));

        private IWebElement SortList => Wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.btn-group.bootstrap-select.open>div.dropdown-menu.open")));

        private IWebElement PriceHighToLow => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "div.btn-group.bootstrap-select.open>div.dropdown-menu.open>ul.dropdown-menu.inner>li[data-original-index='1']>a")));

        private IWebElement PriceLowToHigh => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "div.btn-group.bootstrap-select.open>div.dropdown-menu.open>ul.dropdown-menu.inner>li[data-original-index='2']>a")));

        private IWebElement BedHighToLow => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "div.btn-group.bootstrap-select.open>div.dropdown-menu.open>ul.dropdown-menu.inner>li[data-original-index='3']>a")));

        private IWebElement BedLowToHigh => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "div.btn-group.bootstrap-select.open>div.dropdown-menu.open>ul.dropdown-menu.inner>li[data-original-index='4']>a")));

        private IWebElement BathHighToLow => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "div.btn-group.bootstrap-select.open>div.dropdown-menu.open>ul.dropdown-menu.inner>li[data-original-index='5']>a")));

        private IWebElement BathLowToHigh => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "div.btn-group.bootstrap-select.open>div.dropdown-menu.open>ul.dropdown-menu.inner>li[data-original-index='6']>a")));

        private IWebElement SelectMostRecent => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("li[data-original-index='0']>a.asc>span.text")));

        private IList<IWebElement> SearchResultsPrice => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".price")));

        private IWebElement SearchResultsPrice1 => SearchResultsPrice[0];
        private IWebElement SearchResultsPrice2 => SearchResultsPrice[1];
        private IWebElement SearchResultsPrice3 => SearchResultsPrice[2];

        private IList<IWebElement> SearchResultsBeds => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".attrib-number.attrib-number-bedrooms")));
        private IWebElement SearchResultsBeds1 => SearchResultsBeds[0];
        private IWebElement SearchResultsBeds2 => SearchResultsBeds[1];
        private IWebElement SearchResultsBeds3 => SearchResultsBeds[2];

        private IList<IWebElement> SearchResultsBaths => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".attrib-number.attrib-number-bathrooms")));
        private IWebElement SearchResultsBaths1 => SearchResultsBaths[0];
        private IWebElement SearchResultsBaths2 => SearchResultsBaths[1];
        private IWebElement SearchResultsBaths3 => SearchResultsBaths[2];

        private IList<IWebElement> SearchResultsMostRecent => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".ribbon-new.ribbon")));
        private IWebElement SearchResultsMostRecent1 => SearchResultsMostRecent[0];
        private IWebElement SearchResultsMostRecent2 => SearchResultsMostRecent[1];
        private IWebElement SearchResultsMostRecent3 => SearchResultsMostRecent[2];

        public SearchResultsSortPage(IWebDriver Driver) : base(Driver)
        {
  
        }

        public bool SortByMostRecent()
        {
            bool isSorted = true;
            IWebElement result = null;

            SortByBtn.Click();
            SelectMostRecent.Click();
            Thread.Sleep(4000); //Wait for it to load

            for (int i = 1; i < 4; i++)
            {
                switch (i)
                {
                    case 1:
                        result = SearchResultsMostRecent1;
                        break;
                    case 2:
                        result = SearchResultsMostRecent2;
                        break;
                    case 3:
                        result = SearchResultsMostRecent3;
                        break;
                }

                if (result.Text.Equals("NEW"))
                {
                    isSorted = true;
                    break;
                }
                else
                {
                    isSorted = false;
                }
  
            }
            

            if (isSorted)
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results sorted by most recent.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results not sorted by most recent.");
            }
           
            return isSorted;
        }

        public bool SortByDesc(IWebElement result1, IWebElement result2, IWebElement result3)
        {
            bool isSorted = false;
            IWebElement result = null;

            //Get Sorted List
            string[] splitDollarSign;
            List<int> original = new List<int>();
            List<int> arr = new List<int>();
 
            for(int i=1; i<4; i++)
            {
                switch (i)
                {
                    case 1:
                        result = result1;
                        break;
                    case 2:
                        result = result2;
                        break;
                    case 3:
                        result = result3;
                        break;
                }

                if (result.Text.Contains("$"))
                {
                    splitDollarSign = result.Text.Replace(",", "").Split("$");
                    original.Add(Int32.Parse(splitDollarSign[1]));
                    arr.Add(Int32.Parse(splitDollarSign[1]));
                }
                else
                {
                    original.Add(Int32.Parse(result.Text));
                    arr.Add(Int32.Parse(result.Text));
                }

            }


            //Sort and reverse the original list of prices and compare to original sorted list to see if it is equal then it is sorted
            int[] original2 = original.ToArray();
            int[] arr2 = arr.ToArray();
            Array.Sort(original2);
            Array.Reverse(original2);
            Console.WriteLine("**************");
            Console.WriteLine("             ");
            Console.WriteLine("Reversed Sorted Original");
            for (int i = 0; i < original2.Length; i++)
            {
                Console.WriteLine(original2[i]);
            }

            Console.WriteLine("             ");
            Console.WriteLine("  Original Sorted List   ");
            for (int i = 0; i < arr2.Length; i++)
            {
                Console.WriteLine(arr2[i]);
            }

            if (arr2.SequenceEqual(original2))
            {
                isSorted = true;
            }
            else
            {
                isSorted = false;
            }

            return isSorted;

        }


        public bool SortByAsc(IWebElement result1, IWebElement result2, IWebElement result3)
        {
            bool isSorted = false;
            IWebElement result = null;
            

            //Get Sorted List
            string[] splitDollarSign;
            List<int> original = new List<int>();
            List<int> arr = new List<int>();

            for (int i = 1; i < 4; i++)
            {
                switch (i)
                {
                    case 1:
                        result = result1;
                        break;
                    case 2:
                        result = result2;
                        break;
                    case 3:
                        result = result3;
                        break;
                }

                if(!result.Text.Contains("$"))
                {
                    original.Add(Int32.Parse(result.Text));
                    arr.Add(Int32.Parse(result.Text));
                }
                else
                {
                    splitDollarSign = result.Text.Replace(",", "").Split("$");
                    original.Add(Int32.Parse(splitDollarSign[1]));
                    arr.Add(Int32.Parse(splitDollarSign[1]));
                }

            }

            //Sort the original list of prices and if it is equal to the original sorted list then it is sorted.
            int[] original2 = original.ToArray();
            Array.Sort(original2);
            int[] arr2 = arr.ToArray();

            if (arr2.SequenceEqual(original2))
            {
                isSorted = true;
            }
            else
            {
                isSorted = false;
            }

            return isSorted;
        }

        public bool SortByDescPrice()
        {
            bool isSorted = false;
            SortByBtn.Click();
            Actions actions = new Actions(driver);
            actions.MoveToElement(SortList).Perform();
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            Thread.Sleep(1000); //Wait for it to process
            actions.MoveToElement(PriceHighToLow).Click().Perform();
            Thread.Sleep(4000); //Wait for it to process

            isSorted = SortByDesc(SearchResultsPrice1, SearchResultsPrice2, SearchResultsPrice3);

            if (isSorted)
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results sorted by descending price.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, 
                    "Search results not sorted by descending price.");
            }

            return isSorted;

        }

        public bool SortByAscPrice()
        {
            bool isSorted = false;
            SortByBtn.Click();
            Actions actions = new Actions(driver);
            actions.MoveToElement(SortList).Perform();
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            Thread.Sleep(1000); //Wait for it to process
            actions.MoveToElement(PriceLowToHigh).Click().Perform();
            Thread.Sleep(4000); //Wait for it to process

            isSorted = SortByAsc(SearchResultsPrice1, SearchResultsPrice2, SearchResultsPrice3);

            if (isSorted)
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results sorted by ascending price.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results not sorted by ascending price.");
            }

            return isSorted;
        }

        
        public bool SortByDescBeds()
        {
            bool isSorted = false;
            SortByBtn.Click();
            Actions actions = new Actions(driver);
            actions.MoveToElement(SortList).Perform();
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            Thread.Sleep(1000); //Wait for it to process
            actions.MoveToElement(BedHighToLow).Click().Perform();
            Thread.Sleep(4000); //Wait for it to process

            isSorted = SortByDesc(SearchResultsBeds1, SearchResultsBeds2, SearchResultsBeds3);

            if (isSorted)
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results sorted by descending beds.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results not sorted by descending beds.");
            }

            return isSorted;
        }

        public bool SortByAscBeds()
        {
            bool isSorted = false;
            SortByBtn.Click();
            Actions actions = new Actions(driver);
            actions.MoveToElement(SortList).Perform();
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            Thread.Sleep(1000); //Wait for it to process
            actions.MoveToElement(BedLowToHigh).Click().Perform();
            Thread.Sleep(4000); //Wait for it to process

            isSorted = SortByAsc(SearchResultsBeds1, SearchResultsBeds2, SearchResultsBeds3);

            if (isSorted)
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results sorted by ascending beds.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results not sorted by ascending beds.");
            }

            return isSorted;

        }

       
        public bool SortByDescBaths()
        {
            bool isSorted = false;
            SortByBtn.Click();
            Thread.Sleep(2000);
            Actions actions = new Actions(driver);
            actions.MoveToElement(SortList).Perform();
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            Thread.Sleep(1000); //Wait for it to process
            actions.MoveToElement(BathHighToLow).Click().Perform();
            Thread.Sleep(4000); //Wait for it to process

            isSorted = SortByDesc(SearchResultsBaths1, SearchResultsBaths2, SearchResultsBaths3);

            if (isSorted)
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results sorted by descending baths.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results not sorted by descending baths.");
            }

            return isSorted;
        }

        public bool SortByAscBaths()
        {
            bool isSorted = false;
            SortByBtn.Click();
            Thread.Sleep(2000);
            Actions actions = new Actions(driver);
            actions.MoveToElement(SortList).Perform();
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            actions.SendKeys(Keys.ArrowDown);
            Thread.Sleep(1000); //Wait for it to process
            actions.MoveToElement(BathLowToHigh).Click().Perform();
            Thread.Sleep(4000); //Wait for it to process

            isSorted = SortByAsc(SearchResultsBaths1, SearchResultsBaths2, SearchResultsBaths3);

            if (isSorted)
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results sorted by ascending baths.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Search results not sorted by ascending baths.");
            }

            return isSorted;
        }

    }
}
