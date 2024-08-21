using NUnit.Framework;
using SmartBuildAutomation.pageObjectModel;

namespace SmartBuildAutomation
{
    [TestFixture, Category("Smoke_test")]
    public class FourJobs : BaseClass
    {
        /// <summary>
        /// Performance testing on the starting models
        /// </summary>
        [Test, Order(1)]
        public void SmokeJob()
        {
            Performance.CreateExcelSheet();
            Performance.SmokeTest();
            Performance.SmokeBeta();
            Performance.Smoke20x20x10Production();
        }

        [Test, Order(2)]
        public void MediumPerformance()
        {
            Performance.MediumTest();
            Performance.MediumBeta();
            Performance.MediumProd();
        }

        [Test, Order(3)]
        public void LargePerformance()
        {
            Performance.LargeTest();
            Performance.LargeBeta();
            Performance.LargeProd();
        }

        [Test, Order(4)]
        public void LargeCross()
        {
            Performance.LargeCrossTest();
            Performance.LargeCrossBeta();
            Performance.LargeCrossProd();
        }
    }
}