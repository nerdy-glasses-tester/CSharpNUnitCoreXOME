using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class MoreFiltersPage : BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));

        private IWebElement MoreFilterLink => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("#ddbtn-label-filters>.ddbtn-label-arrow.fa.fa-angle-down")));

        private IWebElement MoreFiltersSubmit => Wait.Until(ExpectedConditions.ElementToBeClickable(
            By.Id("filters-submit")));

        private IList<IWebElement> FilterResults => Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
            By.CssSelector(".photo-gallery-has-photo>img")));


        public MoreFilterBySqFt MoreFilterBySqFt { get; }
        public MoreFilterByYear MoreFilterByYear { get; }
        public MoreFilterByListingStatus MoreFilterByListingStatus { get; }
        public MoreFilterByKeyword MoreFilterByKeyword { get; }
        public MoreFilterByPropertyType MoreFilterByPropertyType { get; }

        public MoreFiltersPage(IWebDriver Driver) : base(Driver)
        {
            MoreFilterBySqFt = new MoreFilterBySqFt(Driver);
            MoreFilterByYear = new MoreFilterByYear(Driver);
            MoreFilterByListingStatus = new MoreFilterByListingStatus(Driver);
            MoreFilterByKeyword = new MoreFilterByKeyword(Driver);
            MoreFilterByPropertyType = new MoreFilterByPropertyType(Driver);
        }

        public void ClickMoreFilters()
        {
            //IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            //js.ExecuteScript("arguments[0].click();", MoreFilterLink);
            //Actions move = new Actions(Driver);
            //move.MoveToElement(MoreFilterLink).Build().Perform();
            MoreFilterLink.Click();
            Thread.Sleep(2000);
        }

        public void ClickMoreFilterSubmit()
        {
            MoreFiltersSubmit.Click();
            Thread.Sleep(4000);
        }

        public void FilterByPropertyStatus(string status)
        {
            ClickMoreFilters();
            MoreFilterByListingStatus.FilterByListingStatus(status);
            ClickMoreFilterSubmit();
        }

        public void FilterBySquareFt(string minsqft, string maxsqft)
        {
            ClickMoreFilters();
            MoreFilterBySqFt.FilterBySqFt(minsqft, maxsqft);
            ClickMoreFilterSubmit();
        }

        public PropertyDetailsPage FilterByPropertyType(string property_type)
        {
            ClickMoreFilters();
            MoreFilterByPropertyType.UncheckAllFilters();
            if (property_type == "Land")
            {
                MoreFilterByPropertyType.FilterByLand();
            }
            else if (property_type == "House")
            {
                MoreFilterByPropertyType.FilterByHouse();
            }
            ClickMoreFilterSubmit();

            FilterResults[0].Click();
            Thread.Sleep(3000); //Wait for results to load
            return new PropertyDetailsPage(driver);
        }

        public PropertyDetailsPage FilterByYear(string minyear, string maxyear)
        {
            ClickMoreFilters();
            MoreFilterByYear.FilterByYear(minyear, maxyear);
            ClickMoreFilterSubmit();
            FilterResults[0].Click();
            Thread.Sleep(3000); //Wait for results to load
            return new PropertyDetailsPage(driver);
        }


        public PropertyDetailsPage FilterByKeyword(string filterkeyword)
        {
            ClickMoreFilters();
            MoreFilterByKeyword.FilterByKeyword(filterkeyword);
            ClickMoreFilterSubmit();
            FilterResults[0].Click();
            Thread.Sleep(3000); //Wait for results to load
            return new PropertyDetailsPage(driver);
        }

        
    }
}
