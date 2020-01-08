using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EventReservation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventReservation.Controllers
{
    public class LocalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locals
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                string user = User.Identity.Name;
                Local local = db.Locals.Where(l => l.Manager.Equals(user)).First();
                return View("~/Views/Locals/Details.cshtml", local);
            }
            else
                return View(db.Locals.ToList());
        }

        // GET: Locals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // GET: Locals/Requests
        public ActionResult Requests()
        {
            return View("~/Views/Admin/LocalRequest.cshtml", db.LocalRequests.ToList());
        }

        //GET: Locals/Confirm/5
        public ActionResult Confirm(int id)
        {
            //find local request
            LocalRequest request = db.LocalRequests.Find(id);

            //create and save local
            Local local = new Local();
            local.City = request.LocalCity;
            local.Name = request.LocalName;
            local.Manager = request.Email;
            db.Locals.Add(local);

            // create new user
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            string password = System.Web.Security.Membership.GeneratePassword(12, 4);
            var user = new ApplicationUser();
            user.Email = request.Email;
            user.UserName = request.Email;
            var result = UserManager.Create(user, password);

            //add role 'Menager' to user
            UserManager.AddToRole(user.Id, "Manager");

            //remove request
            db.LocalRequests.Remove(request);
            db.SaveChanges();
            //this needs to be made with ajax call so that it will only be deleted from the table
            return RedirectToAction("Index", "Home");
        }

        // GET: Locals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // POST: Locals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,City,StreetName,StreetNo,OpeningHour,ClosingHour,Parking")] Local local)
        {
            if (ModelState.IsValid)
            {
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(local);
        }

        // GET: Locals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // POST: Locals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Local local = db.Locals.Find(id);
            db.Locals.Remove(local);
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
