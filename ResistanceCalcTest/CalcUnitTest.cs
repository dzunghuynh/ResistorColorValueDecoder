using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResistorColorDecoderLib;
using System.Drawing;

namespace ResistanceCalcTest
{
    [TestClass]
    public class CalcUnitTest
    {
        IOhmValueCalculator calculator = new OhmValueCalculator();

        [TestMethod]
        public void TestColorValueConversion()
        {
            // Color Value Codes
            string colorBlack = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Black);
            Assert.IsTrue(colorBlack == Color.Black.Name);

            string colorBrown = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Brown);
            Assert.IsTrue(colorBrown == Color.Brown.Name);

            string colorRed = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Red);
            Assert.IsTrue(colorRed == Color.Red.Name);

            string colorOrange = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Orange);
            Assert.IsTrue(colorOrange == Color.Orange.Name);

            string colorYellow = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Yellow);
            Assert.IsTrue(colorYellow == Color.Yellow.Name);

            string colorGreen = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Green);
            Assert.IsTrue(colorGreen == Color.Green.Name);

            string colorBlue = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Blue);
            Assert.IsTrue(colorBlue == Color.Blue.Name);

            string colorViolet = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Violet);
            Assert.IsTrue(colorViolet == Color.Violet.Name);

            string colorGray = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Gray);
            Assert.IsTrue(colorGray == Color.Gray.Name);

            string colorWhite = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.White);
            Assert.IsTrue(colorWhite == Color.White.Name);

            // Color Tolerance Codes
            string pctColorBrown = ResistanceColorCode.ToleranceValueToString(ResistanceColorCode.ColorPercentToleranceCode.Brown);
            Assert.IsTrue(pctColorBrown == Color.Brown.Name);

            string pctColorRed = ResistanceColorCode.ToleranceValueToString(ResistanceColorCode.ColorPercentToleranceCode.Red);
            Assert.IsTrue(pctColorRed == Color.Red.Name);
            
        }
        
        [TestMethod]
        public void TestOhmValueCalculation()
        {
            
            string color_band_A = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Brown);
            string color_band_B = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Green);
            string color_band_C = ResistanceColorCode.ValueToString(ResistanceColorCode.ColorValueCode.Red);
            string color_band_D = ResistanceColorCode.ToleranceValueToString(ResistanceColorCode.ColorPercentToleranceCode.Silver);

            double[] results = calculator.CalculateOhmValue(
                                                color_band_A, color_band_B, color_band_C, color_band_D);

            // Check value = 1500
            double expectedVal = 1500;
            Assert.IsTrue(results[0] == expectedVal);

            // check tolerance = 10
            double expectedTol = 10;
            Assert.IsTrue(results[1] == expectedTol);
        }


        [TestMethod]
        public void TestOhmValueTolerationCalculation()
        {
            double ohms = 100;          // value = 100 ohms
            double tolerance = 0.1;     // tolerance = 10%
            var expected_upper_range = ohms + (ohms * tolerance);   // 110
            var expected_lower_range = ohms - (ohms * tolerance);   //  90

            double[] results = calculator.GetValueRange(ohms, tolerance);
            Assert.IsTrue(results[1] == expected_upper_range && results[0] == expected_lower_range);
        }


    }
}
