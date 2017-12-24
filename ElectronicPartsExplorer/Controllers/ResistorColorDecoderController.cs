using ElectronicPartsExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using ResistorColorDecoderLib;

namespace ElectronicPartsExplorer.Controllers
{
    public class ResistorColorDecoderController : Controller
    {
        private readonly IOhmValueCalculator _IOhmValueCalculator = null;
        private ResistorModel _ResistorModel = new ResistorModel();

        public ResistorColorDecoderController(IOhmValueCalculator ohmValueCalculator)
        {
            _IOhmValueCalculator = ohmValueCalculator;
        }

        // GET: ResistanceColorDecoder        
        public ActionResult Index()
        {
            if (null != _IOhmValueCalculator)
            {
                // Calculate the resistance value

                var results = _IOhmValueCalculator.CalculateOhmValue(
                    _ResistorModel.ColorBandA, _ResistorModel.ColorBandB, _ResistorModel.ColorBandC, _ResistorModel.ColorBandD);
                if (results[0] > 0 && results[1] > 0)
                {
                    _ResistorModel.SetResistanceValue(results[0]);
                    _ResistorModel.SetToleranceValue(results[1]);
                }
            }
            return View(_ResistorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ResistorModel model)
        {           
            if ((_ResistorModel.ColorBandA != model.ColorBandA) ||
                (_ResistorModel.ColorBandB != model.ColorBandB) ||
                (_ResistorModel.ColorBandC != model.ColorBandC) ||
                (_ResistorModel.ColorBandD != model.ColorBandD) )

            {
                _ResistorModel.ColorBandA = model.ColorBandA;
                _ResistorModel.ColorBandB = model.ColorBandB;                
                _ResistorModel.ColorBandC = model.ColorBandC;                
                _ResistorModel.ColorBandD = model.ColorBandD;

                if (null != _IOhmValueCalculator)
                {
                    // Calculate the resistance value

                    var results = _IOhmValueCalculator.CalculateOhmValue(
                        _ResistorModel.ColorBandA, _ResistorModel.ColorBandB, _ResistorModel.ColorBandC, _ResistorModel.ColorBandD);
                    if (results[0] > 0 && results[1] > 0)
                    {
                        _ResistorModel.SetResistanceValue(results[0]);
                        _ResistorModel.SetToleranceValue(results[1]);
                    }
                }
            }
            return View(_ResistorModel);
        }
                       

    }
}