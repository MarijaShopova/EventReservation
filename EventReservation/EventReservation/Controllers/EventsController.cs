﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EventReservation.Models;
using PagedList;

namespace EventReservation.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        [AllowAnonymous]
        public ActionResult Index(EventFiltersViewModel @filters)
        {
            
                EventWithFiltersViewModel model = new EventWithFiltersViewModel();
                model.locals = db.Locals.ToList();
                model.cities = model.locals.Select(m => m.City).Distinct().ToList();
                var result = db.Events.Include(@event => @event.local).ToList();
                if (filters.city != null)
                {
                    result = result.Where(e => e.local.City == filters.city).ToList();
                }
                if (filters.genre != null)
                {
                    result = result.Where(e => e.Genre == filters.genre).ToList();
                }
                if (filters.localId != 0)
                {
                    result = result.Where(e => e.local.Id == filters.localId).ToList();
                }
                if (filters.dateFrom.Year != 0001)
                {
                    result = result.Where(e => e.DateStart.CompareTo(filters.dateFrom) >= 0).ToList();
                }
                if (filters.dateTo.Year != 0001)
                {
                    result = result.Where(e => e.DateStart.CompareTo(filters.dateTo) <= 0).ToList();
                }
                model.events = result.Where(m => m.DateStart.CompareTo(DateTime.Now) >= 0).OrderBy(m => m.DateStart).ToList();
                model.filters = filters;
                return View(model);
          
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
        public ActionResult Reserve(Reservation @reservation)
        {
            if (User.IsInRole("User"))
            {
                //this need to be in ajax call so there wont be any changes to the page and the modal should close at end
                Event e = db.Events.Find(reservation.eventId);
                e.ReservedTables += reservation.NoTables;
                reservation.userEmail = User.Identity.Name;
                db.Reservations.Add(reservation);
                db.SaveChanges();

            MailMessage mm = new MailMessage("eventreservationit@gmail.com", reservation.userEmail);
            mm.Subject = "Confirmation for reservation";
            mm.Body = "Thank you for your reservation for the event " + e.Title + ". The event starts at " +
              e.DateStart + ". Please arrive 15 minutes earlier or your reservation will be canceled.";
            mm.Body += "<br/>Have fun! :)";
            mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential("eventreservationit@gmail.com", "P@ssw0rdPassword");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);

                return RedirectToAction("Index", "Locals");
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            if (User.IsInRole("Manager"))
            {
                var LocalId = db.Locals.FirstOrDefault(local => local.Manager == User.Identity.Name).Id;
                ViewBag.LocalId = LocalId;
                ViewBag.genres = new List<string> { "Rock", "Pop", "Techno", "Balkan", "Hip Hop", "XY Hits" };
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,DateStart,TimeStart,TimeEnd,HasTicket,TicketPrice,NoTables,Performer,Genre,LocalId")] Event @event, HttpPostedFileBase EventImage)
        {
            if (User.IsInRole("Manager"))
            {
                if (ModelState.IsValid)
                {
                    if (EventImage != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            EventImage.InputStream.CopyTo(ms);
                            @event.EventImage = ms.ToArray();
                        }
                    }
                    db.Events.Add(@event);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.genres = new List<string> { "Rock", "Pop", "Techno", "Balkan", "Hip Hop", "XY Hits" };
                var LocalId = db.Locals.FirstOrDefault(local => local.Manager == User.Identity.Name).Id;
                ViewBag.LocalId = LocalId;
                return View(@event);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.IsInRole("Manager"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var events = db.Locals
                    .Include(l => l.Events)
                    .Where(l => l.Manager == User.Identity.Name)
                    .SelectMany(l => l.Events);

                var exists = events.Where(e => e.Id == id);

                if (exists != null)
                {

                    Event @event = db.Events.Find(id);
                    ViewBag.EventImage = @event.EventImage;

                    ViewBag.genres = new List<string> { "Rock", "Pop", "Techno", "Balkan", "Hip Hop", "XY Hits" };
                    var LocalId = db.Locals.FirstOrDefault(local => local.Manager == User.Identity.Name).Id;
                    ViewBag.LocalId = LocalId;
                    if (@event == null)
                    {
                        return HttpNotFound();
                    }

                    return View(@event);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,DateStart,Timestart,TimeEnd,HasTicket,TicketPrice,NoTables,Performer,Genre,LocalId")] Event @event, HttpPostedFileBase EventImage)
        {
            if (User.IsInRole("Manager"))
            {
                if (ModelState.IsValid)
                {
                    if (EventImage != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            EventImage.InputStream.CopyTo(ms);
                            @event.EventImage = ms.ToArray();
                        }
                    }
                    db.Entry(@event).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.genres = new List<string> { "Rock", "Pop", "Techno", "Balkan", "Hip Hop", "XY Hits" };
                    var LocalId = db.Locals.FirstOrDefault(local => local.Manager == User.Identity.Name).Id;
                    ViewBag.LocalId = LocalId;
                    return RedirectToAction("ListEvents", "Locals");
                }
                return View(@event);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
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

        [HttpGet]
        public ActionResult List()
        {
            if (User.IsInRole("User"))
            {
                var currentUser = User.Identity.Name;
                var events = db.Reservations
                    .Include(r => r.Event)
                    .Where(r => r.userEmail == currentUser)
                    .Select(r => r.Event);
                return View(events);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // GET
        public ActionResult ListReservations() {
            var currentUser = User.Identity.Name;

            if(User.IsInRole("Manager"))
            {
                var eventIds = db.Reservations
                    .Select(r => r.eventId)
                    .Distinct();

                var events = db.Locals
                    .Include(l => l.Events)
                    .Where(l => l.Manager == currentUser)
                    .SelectMany(l => l.Events)
                    .Where(e => eventIds.Contains(e.Id));

                return View(events);
            }

            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }
    }
}
