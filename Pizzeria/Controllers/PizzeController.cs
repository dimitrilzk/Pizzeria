﻿using System;
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

        public ActionResult AreaRiservata()
        {
            return View();
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
        public ActionResult Details(int id, int Quantita)
        {
            ListaPizze PizzeOrdinate = new ListaPizze();
            PizzeOrdinate.IdPizza = id;
            PizzeOrdinate.Quantita = Quantita;
            Carrello.ListaCompleta.Add(PizzeOrdinate);
            //foreach( var pizza in Carrello.ListaCompleta)
            //{
            //    DettagliOrdini dettaglio = new DettagliOrdini();
            //    dettaglio.Quantita = pizza.Quantita;
            //    dettaglio.IdPizza = pizza.IdPizza;
            //    db.DettagliOrdini.Add(dettaglio);
            //    //db.SaveChanges();
            //}
            return RedirectToAction("Index", "Pizze");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizze/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "IdPizza,Nome,Prezzo,Ingredienti")] Pizze pizze, HttpPostedFileBase FotoPizza)
        {
            if (ModelState.IsValid == true && FotoPizza != null)
            {
                pizze.Foto = FotoPizza.FileName;
                FotoPizza.SaveAs(Server.MapPath("/Content/img/" + pizze.Foto));
                db.Pizze.Add(pizze);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "IdPizza,Nome,Prezzo,Ingredienti,Foto")] Pizze pizze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
