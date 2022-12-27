using PoliziaMunicipaleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease;

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
            TempData["id"] = id;
            Violazione.DropdownViolazioni.Clear();
            Violazione.GetViolazioneDropdown();
            ViewBag.DropdownViolazioni = Violazione.DropdownViolazioni;


            return View();
        }

       [HttpPost]
        public ActionResult NuovoVerbale(Verbale ve)
        {
            int id = Convert.ToInt32(TempData["id"]);
            Verbale.CreaVerbale(ve, id);
            return RedirectToAction("Index");
        }

        public ActionResult MostraVerbali()
        {
            return View();
        }

        public ActionResult PVTotTrasgr()
        {
           
            return PartialView("_PVTotTrasgr", Verbale.ListaVerbTrasgr());

        }

        public ActionResult PVPtTrasgr()
        {
            return PartialView("_PVPtTrasgr", Verbale.ListaPtTrasgr());
        }

        public ActionResult PVOverTenPoints()
        {
            return PartialView("_PVOverTenPoints", Verbale.ListaOverTenPoints());
        }
        public ActionResult PVCostoMaggiore()
        {
            return PartialView("_PVCostoMaggiore", Verbale.ListaCostoMaggiore());
        }
    }
}