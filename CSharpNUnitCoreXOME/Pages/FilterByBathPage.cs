using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class FilterByBathPage: BasePage
    {

        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(45));

        private IWebElement BathFilter => driver.FindElement(By.CssSelector("div#ddbtn-label-baths>span.dd-info"));

        private IList<IWebElement> BathFilterSelection => Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("ul#dd-criteria-baths>li.dd-menu-item")));

        private IList<IWebElement> BathResults => driver.FindElements(By.CssSelector(".attrib-number.attrib-number-bathrooms"));
        private IWebElement BathResults1 => BathResults[0];

        private IWebElement BathResults2 => BathResults[1];

        //new readonly HomeController logger = new HomeController();

        private static new readonly Logger logger = LogManager.GetCurrentClassLogger();

        public FilterByBathPage(IWebDriver Driver) : base(Driver)
        {
  
        }

        public void FilterByBath(string bath)
        {
            BathFilter.Click();
            BathFilterSelection[int.Parse(bath)].Click();
            Thread.Sleep(3000); //Wait for page to load
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Filter by " + $"{bath} baths.");
            logger.Info("Filter by " + $"{bath} baths.");
        }

        public bool VerifyIsFilterByBath(string bath)
        {
            bool isFiltered = false;

            int numofbaths = Int32.Parse(bath);
            int result1 = Int32.Parse(BathResults1.GetAttribute("innerText"));
            int result2 = Int32.Parse(BathResults2.GetAttribute("innerText"));

            if((result1 >= numofbaths) && (result2 >= numofbaths))
            {
                isFiltered = true;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Verified it filtered by baths.");
                logger.Info("Verified it filtered by baths.");
            }
            else
            {
                isFiltered = false;
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Failed to filter by baths.");
                logger.Info("Failed to filter by baths.");
            }

  

            return isFiltered;
        }
    }
}
