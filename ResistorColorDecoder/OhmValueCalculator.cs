using Microsoft.AspNetCore.Mvc;
using System;

namespace ResistorColorDecoderLib
{
    [ModelMetadataType(typeof(IOhmValueCalculator))]
    public class OhmValueCalculator : IOhmValueCalculator
    {

        public OhmValueCalculator()
        {
        }

        public double[] CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            double valueInOhms = -1;

            // tolerance - Band D
            var tolerance = GetBandToleranceFromString(bandDColor);

            // band C - multiplier Range = [0-9]
            var expVal = GetBandValueFromString(bandCColor);
            if (expVal < 0)
                throw new Exception("Invalid 'Band C' - the multiplier.");

            // multiplier band
            var multiplier = (int)Math.Pow(10, expVal);

            // calc. first band Range = [1..9]
            var band1 = GetBandValueFromString(bandAColor);
            if (band1 < 0)
                throw new Exception("Invalid 'Band A' - tenth digit must be from 0 to 9.");

            // calc. second band
            var band2 = GetBandValueFromString(bandBColor);
            if (band2 < 0)
                throw new Exception("Invalid 'Band B' - tenth digit must be from 0 to 9.");

            valueInOhms = (band2 + (band1 * 10)) * multiplier;

            return new double[] { valueInOhms, tolerance };
        }

        public double[] GetValueRange(double value, double tolerance)
        {
            double lower = value - ((double)value * tolerance);
            double upper = value + ((double)value * tolerance);
            return new double[] { lower, upper };
        }

        private int GetBandValueFromString(string band)
        {
            if (!String.IsNullOrEmpty(band))
            {
                ResistanceColorCode.ColorValueCode value;
                if (Enum.TryParse<ResistanceColorCode.ColorValueCode>(band, out value))
                    return (int)value;
            }
            return -1;
        }
        private double GetBandToleranceFromString(string band)
        {
            if (!String.IsNullOrEmpty(band))
            {
                ResistanceColorCode.ColorPercentToleranceCode value;
                if (Enum.TryParse<ResistanceColorCode.ColorPercentToleranceCode>(band, out value))
                    return ((double)value / 100);
            }
            return 0.2; // default = 20%
        }

    }
}
