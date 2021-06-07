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
    public class PropertyDetailsPage: BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        public PropertyDetailsPageDescription PropertyDetailsPageDescription { get; }
        public PropertyDetailsPageListingDetails PropertyDetailsPageListingDetails { get; }
        public PropertyDetailsPage(IWebDriver Driver): base(Driver)
        {
            PropertyDetailsPageDescription = new PropertyDetailsPageDescription(Driver);
            PropertyDetailsPageListingDetails = new PropertyDetailsPageListingDetails(Driver);
        }

        public void CloseList(IWebDriver Driver)
        {
            IWebElement closelist = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button#top-navigation-v3-closer.close-listing")));
            closelist.Click();
            //IJavaScriptExecutor je = (IJavaScriptExecutor)Driver;
            //je.ExecuteScript("arguments[0].scrollIntoView(true);", closelist);
            Thread.Sleep(3000);
        }

        public List<string> Validate3PropertyType()
        {
            List<string> arrlist = new List<string>();
            string propertytype = PropertyDetailsPageListingDetails.GetPropertyType();
            arrlist.Add(propertytype);
            CloseList(driver);
            IList<IWebElement> properties1 = driver.FindElements(By.CssSelector(".photo-gallery-has-photo>img"));
            properties1[1].Click();
            Thread.Sleep(3000);
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Scroll to Next Listing and Property Type.");
            propertytype = PropertyDetailsPageListingDetails.GetPropertyType();
            arrlist.Add(propertytype);
            CloseList(driver);
            IList<IWebElement> properties2 = driver.FindElements(By.CssSelector(".photo-gallery-has-photo>img"));
            properties2[2].Click();
            Thread.Sleep(3000);
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Scroll to Next Listing and Property Type.");
            propertytype = PropertyDetailsPageListingDetails.GetPropertyType();
            arrlist.Add(propertytype);
            CloseList(driver);
            return arrlist;
        }

 

        public List<string> Validate3Year()
        {
            List<string> propertyyears_arr = new List<string>();
            string year = PropertyDetailsPageListingDetails.GetPropertyYear();
            propertyyears_arr.Add(year);
            CloseList(driver);
            IList<IWebElement> properties1 = driver.FindElements(By.CssSelector(".photo-gallery-has-photo>img"));
            properties1[1].Click();
            Thread.Sleep(3000);
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Scroll to Next Listing and Check Year.");
            year = PropertyDetailsPageListingDetails.GetPropertyYear();
            propertyyears_arr.Add(year);
            CloseList(driver);
            IList<IWebElement> properties2 = driver.FindElements(By.CssSelector(".photo-gallery-has-photo>img"));
            properties2[2].Click();
            Thread.Sleep(3000);
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Scroll to Next Listing and Check Year.");
            year = PropertyDetailsPageListingDetails.GetPropertyYear();
            propertyyears_arr.Add(year);
            return propertyyears_arr;
        }

        public List<Boolean> Validate3Keyword(string filterkeyword)
        {
            List<Boolean> arrlist = new List<Boolean>();
            Boolean match = PropertyDetailsPageDescription.GetKeywordInDescription(filterkeyword);
            arrlist.Add(match);
            CloseList(driver);
            IList<IWebElement> properties1 = driver.FindElements(By.CssSelector(".photo-gallery-has-photo>img"));
            properties1[1].Click();
            Thread.Sleep(3000);
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Scroll to next listing and check for keyword in description.");
            match = PropertyDetailsPageDescription.GetKeywordInDescription(filterkeyword);
            arrlist.Add(match);
            CloseList(driver);
            IList<IWebElement> properties2 = driver.FindElements(By.CssSelector(".photo-gallery-has-photo>img"));
            Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Scroll to next listing and check for keyword in description.");
            properties2[2].Click();
            Thread.Sleep(3000);
            match = PropertyDetailsPageDescription.GetKeywordInDescription(filterkeyword);
            arrlist.Add(match);
            return arrlist;
        }
    }
}
