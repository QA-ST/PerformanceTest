using AventStack.ExtentReports;
using Forms.Reporting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SmartBuildAutomation.Helper;
using SmartBuildProductionAutomation.Helper;
using System;

namespace SmartBuildAutomation
{
    public class BaseClass
    {
        public static IWebDriver Driver;
        public static WebDriverWait GetWebDriverWait() => new WebDriverWait(Driver, TimeSpan.FromSeconds(90));
        public static string downloadFolder = FolderPath.Download();

        [SetUp]
        public void EnvironmentSetup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--headless");
            options.AddArgument("--disable-gpu");
            options.AddUserProfilePreference("download.default_directory", FolderPath.Download());
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
            options.AddArguments("--window-size=1920,1080");
            Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(5));
            Driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromMinutes(5));
            Driver.Url = TestContext.Parameters.Get("TestURL");
            FolderPath.CreateFolder(downloadFolder);
            CommonMethod.DeleteFolderFile(downloadFolder);
        }

        public static void TakeScreenShot()
        {
            string msg = "";
            Screenshot file = ((ITakesScreenshot)Driver).GetScreenshot();
            string image = file.AsBase64EncodedString;
            ExtentTestManager.test.Fail(msg, MediaEntityBuilder.CreateScreenCaptureFromBase64String(image).Build());
        }

        [TearDown]
        public static void BrowserClose()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (ExtentTestManager.test != null)
            {
                if (status == TestStatus.Passed)
                {
                    ExtentTestManager.test.Log(Status.Pass, "Test Pass");
                    Driver.Quit();
                }

                else if (status == TestStatus.Failed)
                {
                    ExtentTestManager.test.Log(Status.Fail, "Test Fail" + errorMessage);
                    TakeScreenShot();
                    Driver.Quit();
                }
                else
                {
                    ExtentTestManager.test.Log(Status.Skip, "Test skip");
                }
            }
            else
            {
                Driver.Quit();
            }
        }
    }
}
