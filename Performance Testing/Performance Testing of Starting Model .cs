using NUnit.Framework;
using SmartBuildAutomation.pageObjectModel;

namespace SmartBuildAutomation
{
    [TestFixture, Category("Performance")]
    public class AllJob : BaseClass
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

        [Test, Order(5)]
        public void GrandchildOpening()
        {
            Performance.GrandChildTest();
            Performance.GrandChildBeta();
            Performance.GrandChildProd();
        }

        [Test, Order(6)]
        public void Job30x40x16()
        {
            Performance.Job30x40x16Test();
            Performance.Job30x40x16Beta();
            Performance.Job30x40x16Prod();
        }

        [Test, Order(7)]
        public void GambrelRoof()
        {
            Performance.GambrelRoofTest();
            Performance.GambrelRoofBeta();
            Performance.GambrelRoofProd();
        }

        [Test, Order(8)]
        public void Template2()
        {
            Performance.Template2Test();
            Performance.Template2Beta();
            Performance.Template2Prod();
        }

        [Test, Order(9)]
        public void Template3()
        {
            Performance.Template3Test();
            Performance.Template3Beta();
            Performance.Template3Prod();
        }

        [Test, Order(10)]
        public void Template4()
        {
            Performance.Template4Test();
            Performance.Template4Beta();
            Performance.Template4Prod();
        }

        [Test, Order(11)]
        public void WoodFlrPeakOut()
        {
            Performance.WoodFlrPeakOutTest();
            Performance.WoodFlrPeakOutBeta();
            Performance.WoodFlrPeakOutProd();
        }

        [Test, Order(12)]
        public void InlineBuilding20x40()
        {
            Performance.InlineBuildingTest();
            Performance.InlineBuildingBeta();
            Performance.InlineBuildingProd();
        }

        [Test, Order(13)]
        public void GirtOutSideCorners()
        {
            Performance.GirtOutSideCornersTest();
            Performance.GirtOutSideCornersBeta();
            Performance.GirtOutSideCornersProd();
        }

        [Test, Order(14)]
        public void GirtOutSidePost()
        {
            Performance.GirtOutSidePostTest();
            Performance.GirtOutSidePostBeta();
            Performance.GirtOutSidePostProd();
        }

        [Test, Order(15)]
        public void StudFrame30x60()
        {
            Performance.StudFrameTest();
            Performance.StudFrameBeta();
            Performance.StudFrameProd();
        }

        [Test, Order(16)]
        public void CantPorchJob()
        {
            Performance.CPABuildingTest();
            Performance.CPABuildingBeta();
            Performance.CPABuildingProd();
        }

        [Test, Order(17)]
        public void AdvancedEdit()
        {
            Performance.AdvancedEditTest();
            Performance.AdvancedEditBeta();
            Performance.AdvancedEditProd();
        }

        [Test, Order(18)]
        public void ParallelSteelTrusses()
        {
            Performance.ParallelSteelTrussesTest();
            Performance.ParallelSteelTrussesBeta();
            Performance.ParallelSteelTrussesProd();
        }

        [Test, Order(19)]
        public void Barndominium()
        {
            Performance.BarndominiumTest();
            Performance.BarndominiumBeta();
            Performance.BarndominiumProd();
        }
    }
}



