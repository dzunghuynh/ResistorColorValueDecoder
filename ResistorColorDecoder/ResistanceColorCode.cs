using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ResistorColorDecoderLib
{
    public class ResistanceColorCode
    {
        public enum ColorValueCode
        {
            [Description("Black")]
            Black = 0,
            [Description("Brown")]
            Brown = 1,
            [Description("Red")]
            Red = 2,
            [Description("Orange")]
            Orange = 3,
            [Description("Yellow")]
            Yellow = 4,
            [Description("Green")]
            Green = 5,
            [Description("Blue")]
            Blue = 6,
            [Description("Violet")]
            Violet = 7,
            [Description("Gray")]
            Gray = 8,
            [Description("White")]
            White = 9,
        };

        public enum ColorPercentToleranceCode  // value x 100
        {
            [Description("Brown")]
            Brown = 100,
            [Description("Red")]
            Red = 200,
            [Description("Orange")]
            Orange = 300,
            [Description("Yellow")]
            Yellow = 400,
            [Description("Green")]
            Green = 50,
            [Description("Blue")]
            Blue = 25,
            [Description("Violet")]
            Violet = 10,
            [Description("Gray")]
            Gray = 5,
            [Description("Gold")]
            Gold = 500,
            [Description("Silver")]
            Silver = 1000,
            [Description("Transparent")]
            Transparent = 2000,   // <- Undefined
        };

        public static string ValueToString(ColorValueCode code)
        {
            return Enum.GetName(typeof(ColorValueCode), code);
        }

        public static ColorValueCode StringToValue(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                ColorValueCode value;
                if (Enum.TryParse<ColorValueCode>(code, out value))
                    return value;
            }
            return ColorValueCode.Black;
        }

        public static string ToleranceValueToString(ColorPercentToleranceCode code)
        {
            return Enum.GetName(typeof(ColorPercentToleranceCode), code);
        }

        public static ColorPercentToleranceCode StringToToleranceValue(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                ColorPercentToleranceCode value;
                if (Enum.TryParse<ColorPercentToleranceCode>(code, out value))
                    return value;
            }
            return ColorPercentToleranceCode.Transparent;
        }
    }
}
