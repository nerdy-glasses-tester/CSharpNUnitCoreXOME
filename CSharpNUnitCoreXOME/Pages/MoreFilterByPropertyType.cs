using System;
using System.Collections.Generic;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class MoreFilterByPropertyType : BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IWebElement CheckedHouse => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "label.checkbox.label-proptype-residential>span.proptype-toggler.js-proptype-toggler.active")));

        private IWebElement CheckedCondo => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-hometype-condo>span.hometype-toggler.js-hometype-toggler.active")));

        private IWebElement CheckedMultiFamily => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(
                "label.checkbox.label-proptype-multifamily>span.proptype-toggler.js-proptype-toggler.active")));

        private IWebElement CheckedLand => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-proptype-land>span.proptype-toggler.js-proptype-toggler.active")));

        private IWebElement UncheckedLand => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-proptype-land>span.proptype-toggler.js-proptype-toggler")));

        private IWebElement UncheckedHouse => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-proptype-residential>span.proptype-toggler.js-proptype-toggler")));

        public MoreFilterByPropertyType(IWebDriver Driver): base(Driver)
        {

        }

        public void UncheckAllFilters()
        {
            CheckedHouse.Click();
            CheckedCondo.Click();
            CheckedMultiFamily.Click();
            CheckedLand.Click();
        }

        public void FilterByLand()
        {
            UncheckedLand.Click();
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Check filter by property type land.");
        }

        public void FilterByHouse()
        {
            UncheckedHouse.Click();
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Check filter by property type house.");
        }

        public bool VerifyFilterByPropertyTypeHouse(List<string> arrlist)
        {
            bool isFiltered = false;
            for (int i = 0; i < arrlist.Count; i++)
            {
                if (!arrlist[i].Contains("Single Family"))
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
                    "Verified it filtered by property type house.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Failed to filter by property type house.");
            }

            return isFiltered;
        }


        public bool VerifyFilterByPropertyTypeLand(List<string> arrlist)
        {
            bool isFiltered = false;
            for (int i = 0; i < arrlist.Count; i++)
            {
                if (!arrlist[i].Contains("Land"))
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
                    "Verified it filtered by property type land.");
            }
            else
            {
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Failed to filter by property type land.");
            }

            return isFiltered;
        }
    }
}