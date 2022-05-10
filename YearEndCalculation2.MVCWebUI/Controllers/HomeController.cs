using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YearEndCalculation2.MvcWebUI.Models;

namespace YearEndCalculation2.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            int a = 5;
            int b = 10;
            int result = a * b;
            
            return View(result);
        }
    }
}