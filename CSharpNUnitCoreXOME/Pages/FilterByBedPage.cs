using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class FilterByBedPage : BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(45));

        private IWebElement FilterBedMenu => driver.FindElement((By.CssSelector("div#ddbtn-label-beds>span.dd-info")));

        private IList<IWebElement> BedsDrpDownSelections =>
            Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy((By.CssSelector("ul#dd-criteria-beds>li.dd-menu-item"))));

        private IList<IWebElement> FilteredBedResults => Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy((By.CssSelector(".attrib-number.attrib-number-bedrooms"))));
        private IWebElement FilteredBedResults1 => FilteredBedResults[0];

        private IWebElement FilteredBedResults2 => FilteredBedResults[1];

        public FilterByBedPage(IWebDriver Driver) : base(Driver)
        {

        }

        public void FilterByBed(string bed)
        {
            FilterBedMenu.Click();
            int bedstofilter = int.Parse(bed);
            BedsDrpDownSelections[bedstofilter].Click();
            Thread.Sleep(3000); //Wait for page to load

            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                "Filter by " + $"{bed} beds.");
        }

        public bool VerifyIsFilterByBed(string bed)
        {
            bool isFiltered = false;

            int numofbed = Int32.Parse(bed);
            int result1 = Int32.Parse(FilteredBedResults1.Text);
            int result2 = Int32.Parse(FilteredBedResults2.Text);

            if ((result1 >= numofbed) && (result2 >= numofbed))
            {
                isFiltered = true;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,"Verified it filtered by beds.");
            }
            else
            {
                isFiltered = false;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,"Failed to filter by beds.");
            }

            return isFiltered;
        }
    }
}
