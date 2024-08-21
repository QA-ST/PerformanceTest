using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SmartBuildAutomation.Helper;
using SmartBuildProductionAutomation.Helper;
using System;
using System.Diagnostics;
using System.IO;

namespace SmartBuildAutomation.pageObjectModel
{
     public class Performance : BaseClass
    {
        #region File Name
        public static string path = FolderPath.PerformanceReport();
        public string smokeTestEnvironment;
        public string smokeBetaEnvironment;
        public string smokeProductionEnvironment;
        public string version;
        public static string ClickOnTheJob = "//span[contains(text(),'{0}')]";
        #endregion

        public static WebDriverWait GetWebDriverWaitForLoadJob() => new(Driver, TimeSpan.FromSeconds(1));

        public static class NameProvider
        {
            public static string StaticName { get; } = "PT" + DateTime.Now.ToString("(dd-MM-yyyy) (hh-mm tt)");
        }

        public static void RefreshPage(string jobName, string environmentName, int a, int b, int c, int d, int e, string version)
        {
            int w = d;
            string[] testing = new string[3] { "smokeTestEnvironment", "smokeBetaEnvironment", "smokeProductionEnvironment" };
            for (int i = 0; i < testing.Length; i++)
            {
                var stopwatch = new Stopwatch();
               
                if (i == 1 | i == 2)
                {
                    ClickSyncButton();
                    stopwatch.Start();
                    CommonMethod.PageLoaderForApplyElementOnCanvasBuilding();
                }
                else
                {
                    stopwatch.Start();
                    CommonMethod.PageLoader();
                }
                Func<IWebDriver, IWebElement> findElement = new Func<IWebDriver, IWebElement>((IWebDriver Web) =>
                {
                    try
                    {
                        IWebElement element = Web.FindElement(By.XPath("//div[@id='layout_baseLayout_panel_main']/div[4]"));
                        return element;
                    }
                    catch (NoSuchElementException)
                    {
                        // Handle the case when the element is not found
                        return null;
                    }
                });

                IWebElement foundElement = GetWebDriverWait().Until(findElement);

                if (foundElement != null)
                {
                    string smoke = jobName + " Job load in" + " - ";
                    stopwatch.Stop();
                    var totalTime = stopwatch.Elapsed;


                    testing[i] = totalTime.ToString(@"m\:ss");
                    File.AppendAllText(@$"{path}/JobData.txt", $@"{smoke}  {testing[i]} {environmentName}" + Environment.NewLine);
                    Console.WriteLine($@"{smoke}  {testing[i]} {environmentName}");

                    DateTime dateTime = DateTime.Now;
                    string name;
                    name = dateTime.ToString("dd-MM-yyyy");
                    string excelFileData = $@"{path}\Performance Report.xlsx";
                    int latestSheetIndex = -1;
                    DateTime latestSheetDate = DateTime.MinValue;

                    // Open Excel File   
                    XSSFWorkbook workbook = new(File.Open(excelFileData, FileMode.Open));

                    for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
                    {
                        ISheet sheet = workbook.GetSheetAt(sheetIndex);

                        if (latestSheetIndex == -1 || sheetIndex > latestSheetIndex)
                        {
                            latestSheetIndex = sheetIndex;
                            latestSheetDate = DateTime.Now;
                        }
                    }

                    // Access the latest sheet by index
                    ISheet latestSheet = workbook.GetSheetAt(latestSheetIndex);

                    try
                    {
                        // Write data to the sheet
                        latestSheet.CreateRow(w).CreateCell(0).SetCellValue(jobName);
                        latestSheet.GetRow(w).CreateCell(a).SetCellValue(testing[i]);
                        latestSheet.GetRow(w).CreateCell(b).SetCellValue(name);
                        latestSheet.GetRow(e).CreateCell(c).SetCellValue(version);
                    }
                    catch
                    {
                        // Write data to the sheet
                        latestSheet.GetRow(w).CreateCell(0).SetCellValue(jobName);
                        latestSheet.GetRow(w).CreateCell(a).SetCellValue(testing[i]);
                        latestSheet.GetRow(w).CreateCell(b).SetCellValue(name);
                        latestSheet.GetRow(e).CreateCell(c).SetCellValue(version);
                    }

                    // Save the workbook to a file
                    using (FileStream fileStream = new(excelFileData, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fileStream);
                    }
                    w++;
                }
                else
                {
                    Console.WriteLine("ERROR: Missing 3D view");
                }
            }
        }

        public static void RefreshPages(string jobName, string environmentName, int a, int b, int c, int d, int e, string version)
        {
            int w = d;
            string[] testing = new string[3] { "smokeTestEnvironment", "smokeBetaEnvironment", "smokeProductionEnvironment" };
            for (int i = 0; i < testing.Length; i++)
            {
                var stopwatch = new Stopwatch();

                if(i==1 | i==2)
                {
                    ClickSyncButton();
                    stopwatch.Start();
                    CommonMethod.PageLoaderForApplyElementOnCanvasBuilding();
                }
                else
                {
                    stopwatch.Start();
                    CommonMethod.PageLoader();
                }

                Func<IWebDriver, IWebElement> findElement = new((IWebDriver Web) =>
                {
                    try
                    {
                        IWebElement element = Web.FindElement(By.XPath("//div[@id='layout_baseLayout_panel_main']/div[4]"));
                        return element;
                    }
                    catch (NoSuchElementException)
                    {
                        // Handle the case when the element is not found
                        return null;
                    }
                });

                IWebElement foundElement = GetWebDriverWait().Until(findElement);

                if (foundElement != null)
                {
                    string smoke = jobName + " Job load in" + " - ";
                    var totalTime = stopwatch.Elapsed;
                    stopwatch.Stop();

                    testing[i] = totalTime.ToString(@"m\:ss");
                    File.AppendAllText(@$"{path}/JobData.txt", smoke + " " + testing[i] + " " + environmentName + Environment.NewLine);
                    Console.WriteLine(smoke + " " + testing[i] + " " + environmentName);

                    DateTime dateTime = DateTime.Now;
                    string name;
                    name = dateTime.ToString("dd-MM-yyyy");
                    string staticName = NameProvider.StaticName;
                    string excelFileData = $@"{path}\Performance Report.xlsx";
                    int latestSheetIndex = -1;
                    DateTime latestSheetDate = DateTime.MinValue;

                    // Open Excel File   
                    XSSFWorkbook workbook = new XSSFWorkbook(File.Open(excelFileData, FileMode.Open));

                    for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
                    {
                        ISheet sheet = workbook.GetSheetAt(sheetIndex);

                        if (latestSheetIndex == -1 || sheetIndex > latestSheetIndex)
                        {
                            latestSheetIndex = sheetIndex;
                            latestSheetDate = DateTime.Now;
                        }
                    }

                    // Access the latest sheet by index
                    ISheet latestSheet = workbook.GetSheetAt(latestSheetIndex);

                    // Write data to the sheet
                    latestSheet.GetRow(w).CreateCell(a).SetCellValue(testing[i]);
                    latestSheet.GetRow(w).CreateCell(b).SetCellValue(name);
                    latestSheet.GetRow(e).CreateCell(c).SetCellValue(version);

                    // Save the workbook to a file
                    using (FileStream fileStream = new FileStream(excelFileData, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fileStream);
                    }
                    w++;
                }
                else
                {
                    Console.WriteLine("ERROR: Missing 3D view");
                }
            }
        }

        public static void ClickSyncButton()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='serverRefresh']"))).Click();
        }

        #region Method Starting Model Job
        public static void CreateExcelSheet()
        {
            string staticName = NameProvider.StaticName;
            string excelFileData = $@"{path}\Performance Report.xlsx";

            // Open Excel File 
            XSSFWorkbook workbook = new(File.Open(excelFileData, FileMode.Open));

            // Create a new sheet
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet(staticName);

            // Write data to the sheet
            sheet.CreateRow(0).CreateCell(0).SetCellValue("Job Name");
            sheet.GetRow(0).CreateCell(1).SetCellValue("Test Environment");
            sheet.GetRow(0).CreateCell(2).SetCellValue("Beta Environment");
            sheet.GetRow(0).CreateCell(3).SetCellValue("Production Environment");
            sheet.GetRow(0).CreateCell(4).SetCellValue("Date");
            sheet.GetRow(0).CreateCell(5).SetCellValue("Environment Version");

            // Save the workbook to a file
            using (FileStream fileStream = new(excelFileData, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
            }
        }

        public static string GetTheVersion(int pageScroll, int PageUp, string environment)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript(string.Format("window.scrollBy(0,{0})", pageScroll));

            CommonMethod.element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//p[contains(text(),'© 2024 - SmartBuild LLC')]//span[1]")));
            string versionTest = CommonMethod.element.Text;
            Console.WriteLine(environment + " " + versionTest);
            js.ExecuteScript(string.Format("window.scrollBy(0,{0})", PageUp));
            return versionTest;

        }

        public static void PageLoaderForPerformance()
        {
            try
            {
                GetWebDriverWait().Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='w2ui-lock-msg']")));
                GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//canvas[@id='drawingArea']")));

                if (IsElementPresent(By.XPath("//div[@class='w2ui-spinner']")))
                {
                    GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='w2ui-spinner']")));
                    GetWebDriverWait().Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='w2ui-spinner']")));
                }

                string errorMessage = GetWebDriverWaitForLoadJob().Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='w2ui-popup']//div[2]"))).Text;
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
                Driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void ClickOnStartingModels(string jobName)
        {
            // open starting models
            CommonMethod.element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format(ClickOnTheJob, jobName))));
            CommonMethod.GetActions().MoveToElement(CommonMethod.element).Click().Perform();
        }

        public static void SmokeTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Test Environment");
            ClickOnStartingModels("Smoke20x20x10");
            RefreshPage("Smoke20x20x10", "Test Environment", 1, 4, 5, 1, 1, version);
        }

        public static void SmokeBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Beta Environment");
            ClickOnStartingModels("Smoke20x20x10");
            RefreshPages("Smoke20x20x10", "Beta Environment", 2, 4, 5, 1, 2, version);
        }

        public static void Smoke20x20x10Production()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Production Environment");
            ClickOnStartingModels("Smoke20x20x10");
            RefreshPages("Smoke20x20x10", "Production Environment", 3, 4, 5, 1, 3, version);
        }

        public static void MediumTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Test Environment");
            ClickOnStartingModels("Med Performance");
            RefreshPage("Med Performance", "Test Environment", 1, 4, 5, 4, 4, version);
        }

        public static void MediumBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Beta Environment");
            ClickOnStartingModels("Med Performance");
            RefreshPages("Med Performance", "Beta Environment", 2, 4, 5, 4, 5, version);
        }

        public static void MediumProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Production Environment");
            ClickOnStartingModels("Med Performance");
            RefreshPages("Med Performance", "Production Environment", 3, 4, 5, 4, 6, version);
        }

        public static void LargeTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Test Environment");
            ClickOnStartingModels("LRG Performance Test");
            RefreshPage("LRG Performance Test", "Test Environment", 1, 4, 5, 7, 7, version);
        }

        public static void LargeBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Beta Environment");
            ClickOnStartingModels("LRG Performance Test");
            RefreshPages("LRG Performance Test", "Beta Environment", 2, 4, 5, 7, 8, version);
        }

        public static void LargeProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Production Environment");
            ClickOnStartingModels("LRG Performance Test");
            RefreshPages("LRG Performance Test", "Production Environment", 3, 4, 5, 7, 9, version);
        }

        public static void LargeCrossTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Test Environment");
            ClickOnStartingModels("Large Cross");
            RefreshPage("Large Cross", "Test Environment", 1, 4, 5, 10, 10, version);
        }

        public static void LargeCrossBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Beta Environment");
            ClickOnStartingModels("Large Cross");
            RefreshPages("Large Cross", "Beta Environment", 2, 4, 5, 10, 11, version);
        }

        public static void LargeCrossProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Production Environment");
            ClickOnStartingModels("Large Cross");
            RefreshPages("Large Cross", "Production Environment", 3, 4, 5, 10, 12, version);
        }

        public static void GrandChildTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Test Environment");
            ClickOnStartingModels("Grandchild Opening Addon");
            RefreshPage("Grandchild Opening Addon", "Test Environment", 1, 4, 5, 13, 13, version);
        }

        public static void GrandChildBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Beta Environment");
            ClickOnStartingModels("Grandchild Opening Addon");
            RefreshPages("Grandchild Opening Addon", "Beta Environment", 2, 4, 5, 13, 14, version);
        }

        public static void GrandChildProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Production Environment");
            ClickOnStartingModels("Grandchild Opening Addon");
            RefreshPages("Grandchild Opening Addon", "Production Environment", 3, 4, 5, 13, 15, version);
        }

        public static void Job30x40x16Test()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Test Environment");
            ClickOnStartingModels("30x40x16");
            RefreshPage("30x40x16", "Test Environment", 1, 4, 5, 16, 16, version);
        }

        public static void Job30x40x16Beta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Beta Environment");
            ClickOnStartingModels("30x40x16");
            RefreshPages("30x40x16", "Beta Environment", 2, 4, 5, 16, 17, version);
        }

        public static void Job30x40x16Prod()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Production Environment");
            ClickOnStartingModels("30x40x16");
            RefreshPages("30x40x16", "Production Environment", 3, 4, 5, 16, 18, version);
        }

        public static void GambrelRoofTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Test Environment");
            ClickOnStartingModels("Gambrel Roof");
            RefreshPage("Gambrel Roof", "Test Environment", 1, 4, 5, 19, 19, version);
        }

        public static void GambrelRoofBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Beta Environment");
            ClickOnStartingModels("Gambrel Roof");
            RefreshPages("Gambrel Roof", "Beta Environment", 2, 4, 5, 19, 20, version);
        }

        public static void GambrelRoofProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Production Environment");
            ClickOnStartingModels("Gambrel Roof");
            RefreshPages("Gambrel Roof", "Production Environment", 3, 4, 5, 19, 21, version);
        }

        public static void Template2Test()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Test Environment");
            ClickOnStartingModels("Template 2");
            RefreshPage("Template 2", "Test Environment", 1, 4, 5, 22, 22, version);
        }

        public static void Template2Beta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Beta Environment");
            ClickOnStartingModels("Template 2");
            RefreshPages("Template 2", "Beta Environment", 2, 4, 5, 22, 23, version);
        }

        public static void Template2Prod()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -1050, "Production Environment");
            ClickOnStartingModels("Template 2");
            RefreshPages("Template 2", "Production Environment", 3, 4, 5, 22, 24, version);
        }

        public static void Template3Test()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Template 3");
            RefreshPage("Template 3", "Test Environment", 1, 4, 5, 25, 25, version);
        }

        public static void Template3Beta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Template 3");
            RefreshPages("Template 3", "Beta Environment", 2, 4, 5, 25, 26, version);
        }

        public static void Template3Prod()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Template 3");
            RefreshPages("Template 3", "Production Environment", 3, 4, 5, 25, 27, version);
        }

        public static void Template4Test()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Template 4");
            RefreshPage("Template 4", "Test Environment", 1, 4, 5, 28, 28, version);
        }

        public static void Template4Beta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Template 4");
            RefreshPages("Template 4", "Beta Environment", 2, 4, 5, 28, 29, version);
        }

        public static void Template4Prod()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Template 4");
            RefreshPages("Template 4", "Production Environment", 3, 4, 5, 28, 30, version);
        }

        public static void WoodFlrPeakOutTest()
        {
            CommonMethod.Login();
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Wood Flr & Peak Out");
            RefreshPage("Wood Flr & Peak Out", "Test Environment", 1, 4, 5, 31, 31, version);
        }

        public static void WoodFlrPeakOutBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Wood Flr & Peak Out");
            RefreshPages("Wood Flr & Peak Out", "Beta Environment", 2, 4, 5, 31, 32, version);
        }

        public static void WoodFlrPeakOutProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Wood Flr & Peak Out");
            RefreshPages("Wood Flr & Peak Out", "Production Environment", 3, 4, 5, 31, 33, version);
        }

        public static void InlineBuildingTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("20x40 Inline Building");
            RefreshPage("20x40 Inline Building", "Test Environment", 1, 4, 5, 34, 34, version);
        }

        public static void InlineBuildingBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("20x40 Inline Building");
            RefreshPages("20x40 Inline Building", "Beta Environment", 2, 4, 5, 34, 35, version);
        }

        public static void InlineBuildingProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("20x40 Inline Building");
            RefreshPages("20x40 Inline Building", "Production Environment", 3, 4, 5, 34, 36, version);
        }

        public static void GirtOutSideCornersTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Girts Outside Corners");
            RefreshPage("Girts Outside Corners", "Test Environment", 1, 4, 5, 37, 37, version);
        }

        public static void GirtOutSideCornersBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Girts Outside Corners");
            RefreshPages("Girts Outside Corners", "Beta Environment", 2, 4, 5, 37, 38, version);
        }

        public static void GirtOutSideCornersProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Girts Outside Corners");
            RefreshPages("Girts Outside Corners", "Production Environment", 3, 4, 5, 37, 39, version);
        }

        public static void GirtOutSidePostTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Girts Outside Post");
            RefreshPage("Girts Outside Post", "Test Environment", 1, 4, 5, 40, 40, version);
        }

        public static void GirtOutSidePostBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Girts Outside Post");
            RefreshPages("Girts Outside Post", "Beta Environment", 2, 4, 5, 40, 41, version);
        }

        public static void GirtOutSidePostProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Girts Outside Post");
            RefreshPages("Girts Outside Post", "Production Environment", 3, 4, 5, 40, 42, version);
        }

        public static void StudFrameTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("30x60 Stud Frame");
            RefreshPage("30x60 Stud Frame", "Test Environment", 1, 4, 5, 43, 43, version);
        }

        public static void StudFrameBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            Console.WriteLine("Beta Environment" + " " + version);
            ClickOnStartingModels("30x60 Stud Frame");
            RefreshPages("30x60 Stud Frame", "Beta Environment", 2, 4, 5, 43, 44, version);
        }

        public static void StudFrameProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("30x60 Stud Frame");
            RefreshPages("30x60 Stud Frame", "Production Environment", 3, 4, 5, 43, 45, version);
        }

        public static void CPABuildingTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Cant Prch Attached Building");
            RefreshPage("Cant Prch Attached Building", "Test Environment", 1, 4, 5, 46, 46, version);
        }

        public static void CPABuildingBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Cant Prch Attached Building");
            RefreshPages("Cant Prch Attached Building", "Beta Environment", 2, 4, 5, 46, 47, version);
        }

        public static void CPABuildingProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Cant Prch Attached Building");
            RefreshPages("Cant Prch Attached Building", "Production Environment", 3, 4, 5, 46, 48, version);
        }

        public static void AdvancedEditTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Advan Edits 1");
            RefreshPage("Advan Edits 1", "Test Environment", 1, 4, 5, 49, 49, version);
        }

        public static void AdvancedEditBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Advan Edits 1");
            RefreshPages("Advan Edits 1", "Beta Environment", 2, 4, 5, 49, 50, version);
        }

        public static void AdvancedEditProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Advan Edits 1");
            RefreshPages("Advan Edits 1", "Production Environment", 3, 4, 5, 49, 51, version);
        }

        public static void ParallelSteelTrussesTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Parallel Steel Trusses");
            RefreshPage("Parallel Steel Trusses", "Test Environment", 1, 4, 5, 52, 52, version);
        }

        public static void ParallelSteelTrussesBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Parallel Steel Trusses");
            RefreshPages("Parallel Steel Trusses", "Beta Environment", 2, 4, 5, 52, 53, version);
        }

        public static void ParallelSteelTrussesProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Parallel Steel Trusses");
            RefreshPages("Parallel Steel Trusses", "Production Environment", 3, 4, 5, 52, 54, version);
        }

        public static void BarndominiumTest()
        {
            CommonMethod.Login();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Test Environment");
            ClickOnStartingModels("Barndominium");
            RefreshPage("Barndominium", "Test Environment", 1, 4, 5, 55, 55, version);
        }

        public static void BarndominiumBeta()
        {
            CommonMethod.GoToBetaEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Beta Environment");
            ClickOnStartingModels("Barndominium");
            RefreshPages("Barndominium", "Beta Environment", 2, 4, 5, 55, 56, version);
        }

        public static void BarndominiumProd()
        {
            CommonMethod.GoToProductionEnvironment();
            CommonMethod.ChangeDistributor("AUTOTEST_PHTEST");
            string version = GetTheVersion(1050, -200, "Production Environment");
            ClickOnStartingModels("Barndominium");
            RefreshPages("Barndominium", "Production Environment", 3, 4, 5, 55, 57, version);
        }
    }
}
#endregion