using Forms.Reporting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SmartBuildAutomation;
using SmartBuildAutomation.Helper;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace SmartBuildProductionAutomation.Helper
{
    public class CommonMethod : BaseClass
    {
        public static IWebElement element;
        public static Actions GetActions() => new(Driver);
        public static WebDriverWait GetWebDriverWaitForDefault() => new(Driver, TimeSpan.FromSeconds(2));

        // Performs the login operation using credentials provided in TestContext parameters.
        public static void Login()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.Name("Username"))).SendKeys(TestContext.Parameters.Get("Username"));
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.Name("Password"))).SendKeys(TestContext.Parameters.Get("Password"));
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#RememberMe"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='loginForm']/form/div[4]/div/input"))).Click();
            CreateDownloadFolder();
        }

        /// <summary>
        /// Sends an email with a specified report attached.
        /// </summary>
        /// <param name="reportName">The name of the report.</param>
        public static void SendEmail(string reportName)
        {
            string PathToDirectory = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory);
            var path = PathToDirectory + @"/../../Result/index.html";
            var fromAddress = new MailAddress("dev@keymark.com", "From Name");
            var toAddress = new MailAddress("kdhillon@keymark.com", "To Name");
            string fromPassword = "dev12345678";
            string subject = reportName;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = "Please find the attachment of this Test"
            };

            message.Attachments.Add(new Attachment(path));
            smtp.Send(message);
        }

        // Delete file from folder
        public static void DeleteFolderFile(string fileName)
        {
            DirectoryInfo deleteFiles = new DirectoryInfo(fileName);
            foreach (FileInfo file in deleteFiles.GetFiles())
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        // Checking download folder create
        public static bool CreateDownloadFolder()
        {
            string folderPath = FolderPath.Download();

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Wait for the job page loads
        public static void PageLoader()
        {
            try
            {
                GetWebDriverWait().Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='w2ui-lock-msg']")));
                GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//canvas[@id='drawingArea']")));
                Wait(1);

                if (IsElementPresent(By.XPath("//div[@class='w2ui-spinner']")))
                {
                    GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='w2ui-spinner']")));
                    GetWebDriverWait().Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='w2ui-spinner']")));
                }

                string errorMessage = Driver.FindElement(By.XPath("//div[@id='w2ui-popup']//div[2]")).Text;
                Console.WriteLine(errorMessage);
            }
            catch (Exception)
            {
                GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//canvas[@id='drawingArea']")));
            }
        }

        public static bool IsElementPresent(By locator)
        {
            try
            {
                GetWebDriverWaitForDefault().Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void PageLoaderForApplyElementOnCanvasBuilding()
        {
            try
            {
                GetWebDriverWait().Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='w2ui-spinner']")));
                GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//canvas[@id='drawingArea']")));
                Wait(1);

                if (IsElementPresent(By.XPath("//div[@class='w2ui-spinner']")))
                {
                    GetWebDriverWait().Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='w2ui-spinner']")));
                    GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//canvas[@id='drawingArea']")));
                }

                string errorMessage = Driver.FindElement(By.XPath("//div[@id='w2ui-popup']//div[2]")).Text;
                Console.WriteLine(errorMessage);
            }
            catch (Exception)
            {
                GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//canvas[@id='drawingArea']")));
            }
        }


        /// <summary>
        /// Navigates to the beta environment and performs login.
        /// </summary>
        public static void GoToBetaEnvironment()
        {
            try
            {
                Driver.Navigate().GoToUrl(TestContext.Parameters.Get("BetaURL"));
                Driver.SwitchTo().Alert().Accept();
                Login();
            }
            catch (Exception)
            {
                Login();
            }
        }

        /// <summary>
        /// Navigates to the Production environment and performs login.
        /// </summary>
        public static void GoToProductionEnvironment()
        {
            try
            {
                Driver.Navigate().GoToUrl(TestContext.Parameters.Get("ProductionURL"));
                Driver.SwitchTo().Alert().Accept();
                Login();
            }
            catch (Exception)
            {
                Login();
            }
        }

        public static void Wait(int delay)
        {
            // Causes the WebDriver to wait for at least a fixed delay
            var now = DateTime.Now;
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(delay));
            wait.Until(Driver => (DateTime.Now - now) - TimeSpan.FromSeconds(delay) > TimeSpan.Zero);
        }

        public static void ChangeDistributor(string distributor)
        {
            try
            {
                Driver.FindElement(By.XPath("//h2[text()='Welcome back, Kritika Dhillon at AUTOTEST_PHTEST!']"));
            }
            catch (NoSuchElementException)
            {
                // Click on the user profile link
                element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Hello Kritika Dhillon!')]")));
                GetActions().Click(element).Pause(TimeSpan.FromSeconds(1)).Perform();

                // Wait for the distributor dropdown to be clickable
                IWebElement distributorName = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//select[contains(@class,'baseDistEditor')]")));

                // Get the currently selected distributor
                SelectElement select = new SelectElement(distributorName);
                string currentDistributor = select.SelectedOption.Text;

                // Check if the current distributor is different from the desired distributor
                if (currentDistributor != distributor)
                {
                    // Change the distributor
                    select.SelectByText(distributor);

                    // Click on the submit button to apply the changes
                    GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'form-manage')]//following :: input[7]"))).Click();
                }

                // Click on the "Home" link
                GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Home"))).Click();
            }
        }
    }
}
