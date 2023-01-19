using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    public class OrdiniController : Controller
    {
        // GET: Ordini
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConcludiOrdine(int idUser)
        {

            return View();
        }
    }
}