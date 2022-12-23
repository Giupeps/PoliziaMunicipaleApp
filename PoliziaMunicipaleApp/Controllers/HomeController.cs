using PoliziaMunicipaleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipaleApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Trasgressore.ListaTrasgressori.Clear();
            Trasgressore.GetTrasgressore();
            return View(Trasgressore.ListaTrasgressori);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Trasgressore t)
        {

            Trasgressore.Create(t);
            return RedirectToAction("Index");
        }

        public ActionResult Violazioni()
        {
            Violazione.ListaViolazioni.Clear();
            Violazione.GetViolazione();
            return View(Violazione.ListaViolazioni);
        }

        public ActionResult NuovaViolazione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuovaViolazione(Violazione v)
        {
            Violazione.CreaViolazione(v);
            return RedirectToAction("Violazioni");
        }

        public ActionResult NuovoVerbale (int id) 
        {
            Violazione.DropdownViolazioni.Clear();
            Violazione.GetViolazioneDropdown();
            ViewBag.DropdownViolazioni = Violazione.DropdownViolazioni;


            return View();
        }


    }
}