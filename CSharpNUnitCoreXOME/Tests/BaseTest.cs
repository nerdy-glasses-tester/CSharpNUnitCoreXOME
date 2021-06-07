using NUnit.Framework;
using OpenQA.Selenium;
using CSharpNUnitCoreXOME.Common;
using CSharpNUnitCoreXOME.Pages;
using NLog;
using System;
using System.Threading;
using System.IO;

namespace CSharpNUnitCoreXOME.Tests
{
    [TestFixture]
    public class BaseTest : BasePage
    {
        protected static IWebDriver Driver { get; set; }
        private NUnit.Framework.TestContext TestContext { get; set; }

        private ScreenshotTaker ScreenshotTaker { get; set; }

        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static string baseURL = "https://www.xome.com/";
        private string browser;

        public BaseTest(string browser): base(Driver)
        {
            this.browser = browser;
        }

        public void Initialize()
        {
            if (!Directory.Exists("C:\\CSharpNUnitCoreXOME\\CSharpNUnitCoreXOME\\Logs"))
            {
                Directory.CreateDirectory("C:\\CSharpNUnitCoreXOME\\CSharpNUnitCoreXOME\\Logs");
            }
        }

        [SetUp]
        public void SetupBeforeEveryTestMethod()
        {
            logger.Debug("*************************************** TEST STARTED");
            logger.Debug("*************************************** TEST STARTED");
            Reporter.AddTestCaseMetadataToHtmlReport(TestContext.CurrentContext);
            var factory = new WebDriverFactory();
            if(browser=="Chrome")
            {
                Driver = factory.Create(BrowserType.Chrome);
            }
            else if(browser=="Firefox")
            {
                Driver = factory.Create(BrowserType.Firefox);
            }

            Driver.Navigate().GoToUrl(baseURL);
            Driver.Manage().Window.Maximize();
            Thread.Sleep(2000);
            ScreenshotTaker = new ScreenshotTaker(Driver, TestContext.CurrentContext);

        }


        [TearDown]
        public void TearDownForEverySingleTestMethod()
        {
            logger.Debug(GetType().FullName + " started a method tear down");
            try
            {
                TakeScreenshotForTestFailure();
            }
            catch (Exception e)
            {
                logger.Error(e.Source);
                logger.Error(e.StackTrace);
                logger.Error(e.InnerException);
                logger.Error(e.Message);
            }
            finally
            {
                StopBrowser();
                logger.Debug(TestContext.CurrentContext.Test.Name);
                logger.Debug("*************************************** TEST STOPPED");
                logger.Debug("*************************************** TEST STOPPED");
            }
        }

        private void TakeScreenshotForTestFailure()
        {
            if (ScreenshotTaker != null)
            {
                ScreenshotTaker.CreateScreenshotIfTestFailed();
                Reporter.ReportTestOutcome(ScreenshotTaker.ScreenshotFilePath);
            }
            else
            {
                Reporter.ReportTestOutcome("");
            }
        }

        private void StopBrowser()
        {
            if (Driver == null)
                return;
            Driver.Quit();
            Driver = null;
            logger.Trace("Browser stopped successfully.");
        }
  
    }
}