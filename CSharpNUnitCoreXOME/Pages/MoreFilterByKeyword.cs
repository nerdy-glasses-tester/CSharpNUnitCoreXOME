using System;
using System.Collections.Generic;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class MoreFilterByKeyword: BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IWebElement FilterKeyword => Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("filters-keyword")));


        public MoreFilterByKeyword(IWebDriver Driver): base(Driver)
        {
            
        }

        public void FilterByKeyword(string filterkeyword)
        {
            FilterKeyword.SendKeys(filterkeyword);
        }

        public bool VerifyFilteredKeyword(List<Boolean> arrlist, string filterkeyword)
        {
            bool isFiltered = false;

            for (int i = 0; i < arrlist.Count; i++)
            {
                if (!arrlist[i])
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
                    "Verified it filtered by keyword: " + $"{filterkeyword}");
            }
            else
            {

                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Failed to filter by keyword: " + $"{filterkeyword}");
            }


            return isFiltered;
        }
    }
}