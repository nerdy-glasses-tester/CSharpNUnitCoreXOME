using System;
using System.Collections.Generic;
using System.Threading;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class MoreFilterByListingStatus : BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IWebElement ActiveForSale => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-status-forsale>span.status-chkbox.active.filter-criteria-change")));

        private IWebElement InactiveForSale => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-status-forsale>span.status-chkbox.filter-criteria-change")));

        private IWebElement ActivePending => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-status-pending>span.status-chkbox.filter-criteria-change.active")));

        private IWebElement InactivePending => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-status-pending>span.status-chkbox.filter-criteria-change")));

        private IWebElement ActiveSold => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-status-sold>span.status-chkbox.filter-criteria-change.active")));

        private IWebElement InactiveSold => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("label.checkbox.label-status-sold>span.status-chkbox.filter-criteria-change")));

        private IWebElement ForSaleResults => Wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".ribbon-new.ribbon")));

        private IList<IWebElement> PendingStatus => driver.FindElements(By.CssSelector(".ribbon-forsale.ribbon.Pending>span"));

        private IList<IWebElement> PendingResults => driver.FindElements(By.CssSelector(".photo-gallery-has-photo>img"));

        private IWebElement PendingTag => Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".status.status-for-sale.Pending")));

        private IList<IWebElement> SoldStatus => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".ribbon-sold.ribbon")));

        private IList<IWebElement> SoldResults => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".r-side-list-photo.photo-gallery-no-photo>img")));
        
        private IList<IWebElement> SoldResultsTag => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".status.status-sold")));

        private IWebElement SoldResultTag => SoldResultsTag[0];
        public MoreFilterByListingStatus(IWebDriver Driver) : base(Driver)
        {
            
        }

        public void FilterByListingStatus(string status)
        {
            if (status.Equals("pending"))
            {
                ActiveForSale.Click();
                Thread.Sleep(2000);
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Deactivate search For Sale status.");
                InactivePending.Click();
                Thread.Sleep(2000);
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Activate search Pending status.");
            }
            else if (status.Equals("sold"))
            {
                ActiveForSale.Click();
                Thread.Sleep(2000);
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Deactivate search For Sale status.");
                InactiveSold.Click();
                Thread.Sleep(2000);
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Activate search Sold status.");
            }
            else if (status.Equals("for sale"))
            {
                //Deactivate and reactivate since by default it is turned on.
                ActiveForSale.Click();
                InactiveForSale.Click();
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Activate search For Sale status.");
            }
        }


        public bool VerifyFilteredStatus(string status)
        {
            bool isFiltered = false;

            if (status.Equals("pending"))
            {
                IWebElement pending = PendingStatus[0];
                String pendingtext = pending.GetAttribute("innerText").ToUpper();
                if (pendingtext.Contains("PENDING"))
                {
                    IWebElement pendingresult = PendingResults[0];
                    pendingresult.Click();

                    Thread.Sleep(3000); //Wait for Page to Load

                    if (PendingTag.GetAttribute("innerText").Contains("PENDING"))
                    {
                        isFiltered = true;
                    }
                    else
                    {
                        isFiltered = false;
                    }
                }
            }
            else if (status.Equals("sold"))
            {
                IWebElement soldstatus = SoldStatus[0];
                if(soldstatus.GetAttribute("innerText").Contains("SOLD"))
                {
                    IWebElement soldelement = SoldResults[0];
                    soldelement.Click();
                    Thread.Sleep(1000);

                    if (SoldResultTag.GetAttribute("innerText").Contains("SOLD"))
                    {
                        isFiltered = true;
                    }
                    else
                    {
                        isFiltered = false;
                    }
                }
                
            }
            else if (status.Equals("for sale"))
            {  
                if (!ForSaleResults.Text.Equals("NEW"))
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
                    "Verified it filtered by listing status: "+$"{status}");
            }
            else
            {

                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info,
                    "Failed to filter by listing status: "+$"{status}");
            }

            return isFiltered;
        }




    }
}