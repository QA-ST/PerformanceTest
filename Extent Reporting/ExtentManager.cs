using AventStack.ExtentReports;
using NUnit.Framework;
using System.IO;

namespace Forms.Reporting
{
    public class ExtentManager
    {
        public static ExtentReports extent;

        public static ExtentReports GetExtent()
        {
            if (extent == null)
            {
                string PathToDirectory = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory);
                string reportPath = PathToDirectory + @"/../../Result/";
                AventStack.ExtentReports.Reporter.ExtentHtmlReporter htmlReport = new(reportPath);
                extent = new AventStack.ExtentReports.ExtentReports();
                extent.AttachReporter(htmlReport);
            }
            return extent;
        }
    }
}
