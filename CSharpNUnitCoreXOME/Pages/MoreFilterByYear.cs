using System;
using System.Collections.Generic;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class MoreFilterByYear : BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IWebElement MinYearDrpDown => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "button.btn.dropdown-toggle.btn-default[data-id='filters-minyearbuilt']>span.filter-option.pull-left")));

        private IWebElement MaxYearDrpDown => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "button.btn.dropdown-toggle.btn-default[data-id='filters-maxyearbuilt']>span.filter-option.pull-left")));

        private IList<IWebElement> MinYearDrpDownSelection => Wait.Until(
            ExpectedConditions.PresenceOfAllElementsLocatedBy(
                By.XPath(
                    "//button[@data-id='filters-minyearbuilt']/following-sibling::div[@class='dropdown-menu open']/ul/li/a/span[@class='text']")));

        private IList<IWebElement> MaxYearDrpDownSelection => Wait.Until(
            ExpectedConditions.PresenceOfAllElementsLocatedBy(
                By.XPath(
                    "//button[@data-id='filters-maxyearbuilt']/following-sibling::div[@class='dropdown-menu open']/ul/li/a/span[@class='text']")));

        public MoreFilterByYear(IWebDriver Driver): base(Driver)
        {
            
        }

        public void FilterByYear(string minyear, string maxyear)
        {
            MinYearDrpDown.Click();

            for (int i = 0; i < MinYearDrpDownSelection.Count; i++)
            {
                if (minyear.Equals(MinYearDrpDownSelection[i].GetAttribute("innerHTML")))
                {
                    MinYearDrpDownSelection[i].Click();
                    break;
                }
            }


        
            MaxYearDrpDown.Click();
            for (int i = 0; i < MaxYearDrpDownSelection.Count; i++)
            {
                if (maxyear.Equals(MaxYearDrpDownSelection[i].GetAttribute("innerHTML")))
                {
                    MaxYearDrpDownSelection[i].Click();
                    break;
                }
            }
                

            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                $"Filtered by {minyear} - " + $"{maxyear} year.");

        }

        public bool VerifyIsFilterByYear(List<string> propertyyears_arr, string minyear, string maxyear)
        {
            bool isFiltered = false;
            for (int i = 0; i < propertyyears_arr.Count; i++)
            {
               
                if (!((int.Parse(propertyyears_arr[i]) >= int.Parse(minyear)) && (int.Parse(propertyyears_arr[i]) <= int.Parse(maxyear))))
                {
                    isFiltered = false;
                }
                else
                {
                    isFiltered = true;
                }
            }

            if (isFiltered)
            {

                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Verified it filtered by year range.");
            }
            else
            {

                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Failed to filter by year range.");
            }

            return isFiltered;
        }
    }
}