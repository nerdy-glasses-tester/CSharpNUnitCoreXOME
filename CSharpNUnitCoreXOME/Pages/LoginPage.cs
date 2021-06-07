using NUnit.Framework;
using NLog;
using OpenQA.Selenium;
using System;
using System.Security.AccessControl;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpNUnitCoreXOME.Pages
{
    public class LoginPage: BasePage
    {
        public WebDriverWait Wait => new WebDriverWait(driver, System.TimeSpan.FromSeconds(45));

        private IWebElement SigninLink => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.LinkButton.btn.btn-secondary")));

        private IWebElement Emailfield => Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#security_loginname")));

        private IWebElement Pwdfield => Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#security_password")));

        private IWebElement Submitbtn => Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("submit-button")));

        private IWebElement SignedInPageLastElementToLoad => Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//h2[@class='overlay-title' and contains(text(), 'Xome Auction')]")));

        private IWebElement LoggedInUserName => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".NavItem.top-level.user-menu>.user-name")));

        public LoginPage(IWebDriver Driver) : base(Driver)
        {

        }

        public void Login(string email, string pwd)
        {
   
                SigninLink.Click();
                Thread.Sleep(2000);
                driver.SwitchTo().Frame("login-iframe");
                Thread.Sleep(2000);
                Emailfield.SendKeys(email);
                Pwdfield.SendKeys(pwd);
                Submitbtn.Click();
                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Login with email and password. Click submit button.");
                driver.SwitchTo().DefaultContent();
                if (SignedInPageLastElementToLoad.Displayed)
                {
                    Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Logged in page loaded.");
                }
                
        }

        public string GetLoggedInUser()
        {
            string username = "";

                Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Verify logged in user name.");


                if (LoggedInUserName.Displayed)
                {
                    username = LoggedInUserName.GetAttribute("innerHTML");
                    Reporter.LogTestStepForBugLogger(AventStack.ExtentReports.Status.Info, "Logged in user name is " + $"{username}");

                }
   

            return username;
        }
    }   


}
