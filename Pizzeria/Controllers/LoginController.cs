using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pizzeria.Controllers
{
    public class LoginController : Controller
    {
        ModelDBContext dBContext= new ModelDBContext();
        // GET: Login
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Users u)
        {
            string username = u.Username;
            string password = u.Pass;
            var user = dBContext.Users.Where(x => x.Username == username && x.Pass == password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                return RedirectToAction("Index","Pizze"); 
            }
            else
            {
                ViewBag.logmessage = "Username e/o password errati!";
            }
            return View();
        }
    }
}