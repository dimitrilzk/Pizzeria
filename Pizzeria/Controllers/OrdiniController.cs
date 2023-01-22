﻿using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    [Authorize]
    public class OrdiniController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        public ActionResult PartialDettagliOrdine()
        {

            return PartialView("_PartialDettagliOrdine", Carrello.ListaCompleta);
        }
        // GET: Ordini
        public ActionResult ConcludiOrdine()
        {
            decimal TotaleComplessivo;
            decimal costo = 0;
            foreach(var item in Carrello.ListaCompleta)
            {
                var totalePizza = item.Prezzo * item.Quantita;
                TotaleComplessivo = costo += totalePizza;
                TempData["Totale"] = TotaleComplessivo.ToString("c2");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ConcludiOrdine(Ordini ordine)
        {
            Users utente = db.Users.Where(x => x.Username == User.Identity.Name).First();
            ordine.IdUser = utente.IdUser;
            ordine.Confermato = true;
            ordine.Evaso = false;
            if(ordine.Note == null)
            {
                ordine.Note = "Nessuna nota";
            }
            db.Ordini.Add(ordine);
            foreach (var item in Carrello.ListaCompleta)
            {
                DettagliOrdini dettaglio = new DettagliOrdini();
                dettaglio.Quantita = item.Quantita;
                dettaglio.IdPizza = item.IdPizza;
                dettaglio.IdOrdine = ordine.IdOrdine;
                db.DettagliOrdini.Add(dettaglio);
            }
            Carrello.ListaCompleta.Clear();

            db.SaveChanges();
            TempData["messaggio"] = "Ordine effettuato, stiamo arrivando!";
            return RedirectToAction("ConcludiOrdine");
        }

    }
}