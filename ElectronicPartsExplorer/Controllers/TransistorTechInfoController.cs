using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicPartsExplorer.Controllers
{
    public class TransistorTechInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}