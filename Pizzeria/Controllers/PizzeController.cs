using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Authorize]
    public class PizzeController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        public ActionResult EvadiOrdini()
        {
            var ordini = db.DettagliOrdini.Include("Ordini").ToList();
            return View(ordini);
        }
        public ActionResult DeleteOrdine(int id)
        {
            Ordini ordine = db.Ordini.Find(id);
            db.Ordini.Remove(ordine);
            db.SaveChanges();
            return RedirectToAction("EvadiOrdini");
        }
        public ActionResult PartialProductList()
        {
            return PartialView("_PartialProductList", db.Pizze.ToList());
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Pizze.ToList());
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }
        [HttpPost]
        public ActionResult Details(int id, int quantita)
        {
            try
            {
                ListaPizze PizzeOrdinate = new ListaPizze();
                PizzeOrdinate.IdPizza = id;
                PizzeOrdinate.Nome = db.Pizze.Find(id).Nome;
                PizzeOrdinate.Prezzo = Convert.ToDecimal(db.Pizze.Find(id).Prezzo);
                PizzeOrdinate.Quantita = quantita;
                PizzeOrdinate.Totale = Convert.ToDecimal(db.Pizze.Find(id).Prezzo) * quantita;
                Carrello.ListaCompleta.Add(PizzeOrdinate);
                return RedirectToAction("Index", "Pizze");
            }catch (Exception ex)
            {
                TempData["errore"] = ex + "Devi inserire una quantità.";
                return RedirectToAction("Details", "Pizze");
            }

        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizze/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IdPizza,Nome,Prezzo,Ingredienti")] Pizze pizze, HttpPostedFileBase FotoPizza)
        {
            if (ModelState.IsValid == true && FotoPizza != null)
            {
                pizze.Foto = FotoPizza.FileName;
                FotoPizza.SaveAs(Server.MapPath("/Content/img/" + pizze.Foto));
                db.Pizze.Add(pizze);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View();
        }

        // GET: Pizze/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }

        // POST: Pizze/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPizza,Nome,Prezzo,Ingredienti,Foto")] Pizze pizze, HttpPostedFileBase FotoPizza)
        {
            if (ModelState.IsValid == true && FotoPizza != null)
            {
                pizze.Foto = FotoPizza.FileName;
                FotoPizza.SaveAs(Server.MapPath("/Content/img/" + pizze.Foto));
                db.Entry(pizze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }else if (ModelState.IsValid)
            {
                Pizze pizzaInDb = db.Pizze.Find(pizze.IdPizza);
                pizzaInDb.Nome = pizze.Nome;
                pizzaInDb.Prezzo= pizze.Prezzo;
                pizzaInDb.Ingredienti = pizze.Ingredienti;
                pizzaInDb.Foto = db.Pizze.Find(pizze.IdPizza).Foto;
                db.Entry(pizzaInDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(pizze);
        }

        // GET: Pizze/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }

        // POST: Pizze/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizze pizze = db.Pizze.Find(id);
            db.Pizze.Remove(pizze);
            db.SaveChanges();
            return RedirectToAction("Create");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
