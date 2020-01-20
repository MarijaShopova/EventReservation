using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using EventReservation.Models;

namespace EventReservation.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(@event => @event.local);
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(m => m.local).SingleOrDefault(e => e.Id == id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        //POST: Events/Reserve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Reserve(Reservation @reservation)
        {
            //this need to be in ajax call so there wont be any changes to the page and the modal should close at end
            Event e = db.Events.Find(reservation.eventId);
            e.ReservedTables += reservation.NoTables;
            reservation.userEmail = User.Identity.Name;
            db.Reservations.Add(reservation);
            db.SaveChanges();

            MailMessage mm = new MailMessage("karachanakova98@gmail.com", reservation.userEmail);
            mm.Subject = "Confrming for your reservation";
            mm.Body = "Thank you for your reservation for the event " + reservation.Name + ". The event starts at " +
              e.DateStart + ". Please arrive 15 minutes earlier or your reservation will be canceled!";
            mm.Body += " /n Have fun! :)";
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("eventreservationit@gmail.com", "P@ssw0rdPassword");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            var LocalId = db.Locals.FirstOrDefault(local => local.Manager == User.Identity.Name).Id;
            ViewBag.LocalId = LocalId;
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,DateStart,TimeStart,TimeEnd,HasTicket,TicketPrice,NoTables,Performer,Genre,LocalId")] Event @event)
        {

            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var LocalId = db.Locals.FirstOrDefault(local => local.Manager == User.Identity.Name).Id;
            ViewBag.LocalId = LocalId;
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,DateStart,Timestart,TimeEnd,HasTicket,TicketPrice,NoTables,Performer,Genre,LocalId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
