using EventReservation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventReservation.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [AllowAnonymous]
        public ActionResult Index()
        {
                return View();
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            var cities = new List<String>() { "Skopje", "Strumica", "Stip", "Ohrid", "Bitola", "Prilep", "Gevgelija",
                "Berovo", "Gostivar", "Kumanovo", "Kocani", "Kicevo", "Krushevo", "Struga", "Negotino", "Veles", "Radovis", "Tetovo" };
            ViewBag.cities = cities;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(LocalRequest model)
        {
            if (ModelState.IsValid)
            {
                db.LocalRequests.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}