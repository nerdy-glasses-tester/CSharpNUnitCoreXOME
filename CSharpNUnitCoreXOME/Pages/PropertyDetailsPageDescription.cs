using System;
using System.Collections.Generic;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class PropertyDetailsPageDescription: BasePage
    {
        public WebDriverWait wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));

        private IWebElement ContinueLink => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.show-more-listing-desc.view-toggler")));

        private IWebElement Description => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".more-content>span")));

        public PropertyDetailsPageDescription(IWebDriver Driver): base(Driver)
        {
            
        }

        public Boolean GetKeywordInDescription(string filterkeyword)
        {
            Boolean match = false;
            //Actions move = new Actions(driver);
            //move.MoveToElement((IWebElement)ContinueLink).Build().Perform();
            ContinueLink.Click();

            String keywd = Description.GetAttribute("innerText").ToLower();
            match = keywd.Contains(filterkeyword);

            Console.WriteLine("filterkeyword is " + filterkeyword);
            Console.WriteLine("description is " + keywd);
            Console.WriteLine("match is " + match);

            /***
            if (keywd.Contains(filterkeyword))
            {
                match = true;   
            }
            else
            {
                match = false;
            }
            ***/

            return match;
        }
    }
}