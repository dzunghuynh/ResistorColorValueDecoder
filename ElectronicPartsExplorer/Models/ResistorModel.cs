using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

// C# 6 
using static ResistorColorDecoderLib.ResistanceColorCode;

namespace ElectronicPartsExplorer.Models
{
    public class ResistorModel : INotifyPropertyChanged
    {       
        public ResistorModel()
        {
            ColorBandA = Enum.GetName(typeof(ColorValueCode), ColorValueCode.Brown);
            ColorBandB = Enum.GetName(typeof(ColorValueCode), ColorValueCode.Red);
            ColorBandC = Enum.GetName(typeof(ColorValueCode), ColorValueCode.Orange);

            ColorBandD = Enum.GetName(typeof(ColorPercentToleranceCode), ColorPercentToleranceCode.Brown);

            Legend = String.Format("1 M\u2126 = 1000000 \u2126   |   1 K\u2126 = 1000 \u2126");
        }

        public SelectList ColorValueList
        {
            get
            {
                var colors = Enum.GetValues(typeof(ColorValueCode)).Cast<ColorValueCode>().ToList();
                var selections = colors.Select( x => new { Id = x, Value = ValueToString(x) });                
                return (new SelectList(selections, "Id", "Value"));
            }
        }
        public SelectList ColorValueListWithoutBlack
        {
            get
            {
                var colors = Enum.GetValues(typeof(ColorValueCode)).Cast<ColorValueCode>().ToList();
                var selections = colors.Where(item => item != ColorValueCode.Black).Select(x => new { Id = x, Value = ValueToString(x) });
                return (new SelectList(selections, "Id", "Value"));
            }
        }
        public SelectList ToleranceValueList
        {
            get
            {
                var colors = Enum.GetValues(typeof(ColorPercentToleranceCode)).Cast<ColorPercentToleranceCode>().ToList();
                var selections = colors.Select(x => new { Id = x, Value = ToleranceValueToString(x) });
                return (new SelectList(selections, "Id", "Value"));
            }
        }

        [Display(Name = "Color Band A (Tens)")]
        public string ColorBandA
        {
            get { return _ColorBandA; }
            set
            {
                _ColorBandA = value;
                OnPropertyChanged();
            }
        }
        string _ColorBandA;

        [Display(Name = "Color Band B (Ones)")]
        public string ColorBandB
        {
            get { return _ColorBandB; }
            set
            {
                _ColorBandB = value;
                OnPropertyChanged();
            }
        }
        string _ColorBandB;

        [Display(Name = "Color Band C (Multiplier)")]
        public string ColorBandC
        {
            get { return _ColorBandC; }
            set
            {
                _ColorBandC = value;
                OnPropertyChanged();
            }
        }
        string _ColorBandC;

        [Display(Name = "Color Band D (Tolerance)")]
        public string ColorBandD
        {
            get { return _ColorBandD; }
            set
            {
                _ColorBandD = value;
                OnPropertyChanged();
            }
        }
        string _ColorBandD;

        [Display(Name = "Resistance")]
        public string Resistance
        {
            get { return _Resistance; }
            set
            {
                _Resistance = value;
                OnPropertyChanged();
            }
        }
        string _Resistance;
        double _ResistanaceValue;

        [Display(Name = "Tolerance")]
        public string Tolerance
        {
            get { return _Tolerance; }
            set
            {
                _Tolerance = value;
                OnPropertyChanged();
            }
        }
        string _Tolerance;

        [Display(Name = "Acceptable Value Range")]
        public string AcceptableValueRange
        {
            get { return _AcceptableValueRange; }
            set
            {
                _AcceptableValueRange = value;
                OnPropertyChanged();
            }
        }
        string _AcceptableValueRange;

        public void SetResistanceValue(double resistance)
        { 
            _ResistanaceValue = resistance;
            if (resistance >= 1000000)
                Resistance = String.Format("{0:0.00} M\u2126", (resistance / 1000000));
            else if (resistance >= 1000)
                Resistance = String.Format("{0:0.00} K\u2126", (resistance / 1000));
            else
                Resistance = String.Format("{0} \u2126", (int)(resistance));
        }
        public void SetToleranceValue(double tolerance)
        {
            if (tolerance < 1)
                Tolerance = String.Format("{0:0.00} %", tolerance);
            else
                Tolerance = String.Format("{0} %", (int)tolerance);
            
            // Update Acceptable Range          
            var variation = (_ResistanaceValue * tolerance / 100);
            var lower = _ResistanaceValue - variation;
            var upper = _ResistanaceValue + variation;

            // 
            AcceptableValueRange = String.Format("{0} \u2126  -  {1} \u2126", FormatValue(lower), FormatValue(upper));
        }

        [Display(Name = "Note:")]
        public string Legend
        {
            get { return _Legend; }
            set
            {
                _Legend = value;
                OnPropertyChanged();
            }
        }
        string _Legend;


        private string FormatValue(double val)
        {
            string valueAsString;
            if (val >= 1000000)
                valueAsString = String.Format("{0:0.00} M", (val / 1000000));
            else if (val >= 1000)
                valueAsString = String.Format("{0:0.00} K", (val / 1000));
            else
                valueAsString = val.ToString("0.000");

            return valueAsString;
        }

        private string ValueToString(ColorValueCode code)
        {
            return Enum.GetName(typeof(ColorValueCode), code);
        }

        private ColorValueCode StringToValue(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                ColorValueCode value;
                if (Enum.TryParse<ColorValueCode>(code, out value))
                    return value;
            }
            return ColorValueCode.Black;
        }

        private string ToleranceValueToString(ColorPercentToleranceCode code)
        {
            return Enum.GetName(typeof(ColorPercentToleranceCode), code);
        }

        private ColorPercentToleranceCode StringToToleranceValue(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                ColorPercentToleranceCode value;
                if (Enum.TryParse<ColorPercentToleranceCode>(code, out value))
                    return value;
            }
            return ColorPercentToleranceCode.Transparent; 
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Required C# 6 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
