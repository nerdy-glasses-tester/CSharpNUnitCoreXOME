using System;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class PropertyDetailsPageListingDetails : BasePage
    {

        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IWebElement Year => Wait.Until(ExpectedConditions.ElementExists(By.Id("mls-yr2")));

        private IWebElement PropertyType => Wait.Until(ExpectedConditions.ElementExists(By.Id("mls-propt2")));


        public PropertyDetailsPageListingDetails(IWebDriver driver) : base(driver)
        {
        }

        public string GetPropertyYear()
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)driver;
            je.ExecuteScript("arguments[0].scrollIntoView(true);", Year);
            return Year.Text;
        }

        public string GetPropertyType()
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)driver;
            je.ExecuteScript("arguments[0].scrollIntoView(true);", PropertyType);
            return PropertyType.Text;
        }
    }
}