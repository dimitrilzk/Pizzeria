using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    public class DettagliController : Controller
    {
        // GET: Dettagli
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PartialCreate()
        {
            return View();
        }
    }
}