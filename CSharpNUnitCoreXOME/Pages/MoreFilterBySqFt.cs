using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class MoreFilterBySqFt: BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IWebElement SqFtMinDrpDown => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("#filters-sqftmin>div>.btn.dropdown-toggle.btn-default>.filter-option.pull-left")));

        private IWebElement SqFtMaxDrpDown => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("#filters-sqftmax>div>.btn.dropdown-toggle.btn-default>.filter-option.pull-left")));

        private IWebElement minsqft1000 => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.btn-group.bootstrap-select.filter-criteria-change.open>div.dropdown-menu.open>ul>li[data-original-index='3']>a>span.text")));

        private IWebElement maxsqft1500 => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.btn-group.bootstrap-select.filter-criteria-change.open>div.dropdown-menu.open>ul>li[data-original-index='5']>a>span.text")));

        private IList<IWebElement> SqFtFilterResults => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
            By.CssSelector(".attrib-number.attrib-number-area")));

        private IWebElement SqFtFilterResults1 => SqFtFilterResults[0];

        private IWebElement SqFtFilterResults2 => SqFtFilterResults[1];

        public MoreFilterBySqFt(IWebDriver Driver): base(Driver)
        {
            
        }

        public void FilterBySqFt(string minsqft, string maxsqft)
        {
            SqFtMinDrpDown.Click();
            Thread.Sleep(1000); //Wait for selection to process

            minsqft1000.Click();
            Thread.Sleep(1000); //Wait for selection to process

            SqFtMaxDrpDown.Click();
            Thread.Sleep(1000); //Wait for selection to process

            maxsqft1500.Click();
            Thread.Sleep(1000); //Wait for selection to process

            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                $"Filtered by {minsqft} - "+$"{maxsqft} sq ft.");
        }

        public bool VerifyIsFilterBySqFt(string minsqft, string maxsqft)
        {
            bool isFiltered = false;
            List<int> arrlist = new List<int>();
           
            arrlist.Add(int.Parse(SqFtFilterResults1.Text.Replace(",", "")));
            arrlist.Add(int.Parse(SqFtFilterResults2.Text.Replace(",", "")));

            for (int j = 0; j < arrlist.Count; j++)
            {
                if(!((arrlist[j] >= int.Parse(minsqft)) && (arrlist[j]<=int.Parse(maxsqft))))
                {
                    isFiltered = false;
                    break;
                }
                else
                {
                    isFiltered = true;
                }
            }

            if (isFiltered)
            {

                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Verified it filtered by sq ft range.");
            }
            else
            {

                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Failed to filter by sq ft range.");
            }

            return isFiltered;
        }


    }
}