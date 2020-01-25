using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EventReservation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace EventReservation.Controllers
{
    public class LocalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locals
        public ActionResult Index(int? page, int? pageSize)
        {
            if (User.IsInRole("Manager"))
            {
                string user = User.Identity.Name;
                Local local = db.Locals.Where(l => l.Manager.Equals(user)).First();
                return View("~/Views/Locals/Details.cshtml", local);
            }
            else
                return View(db.Locals.OrderByDescending(l => l.Id).ToPagedList(page ?? 1, pageSize ?? 2));
        }

        /*[HttpPost]
         public ActionResult AddImage([Bind(Include = "Id,Name,Description,City,StreetName,StreetNo,OpeningHour,ClosingHour,Parking")] Local local,HttpPostedFileBase photo)
          {
              var db = new Entities();
              if (photo != null) {
                  local.LocalsImage = new byte[photo.ContentLength];
                  photo.InputStream.Read(local.LocalsImage, 0, photo.ContentLength);
              }
              //  db.Locals.Add(local);
              db.Entry(local).State = EntityState.Modified;
              db.SaveChanges();
              return View(local);
          }
          */

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

            int stars = 0;
            var result = db.Locals.Include(l => l.Reviews).FirstOrDefault(l => l.Id == local.Id).Reviews;
            if (result.Any())
                stars = (int)System.Math.Round(db.Locals.Include(l => l.Reviews).FirstOrDefault(l => l.Id == local.Id).Reviews.Average(review => review.Stars));

            int yourReview = 0;
            var yourReviewResult = db.Reviews.FirstOrDefault(r => r.Local.Id == local.Id);
            // && r.User == User.Identity.Name
            if (yourReviewResult != null)
                yourReview = db.Reviews.FirstOrDefault(r => r.Local.Id == local.Id).Stars;
            // && r.User == User.Identity.Name
            ViewBag.average = stars;
            ViewBag.yourReview = yourReview;
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
            //P@ssw0rdPassword
            MailMessage mm = new MailMessage("eventreservationit@gmail.com", user.Email);
            mm.Subject = "Local accepted";
            mm.Body = "Dear " + user.Email + ", your local has been added to our webside. Thank you for choosing us. You can now loging to" +
                "your account";
            mm.Body += "Username: " + user.Email + "\n Password: " + password;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            // smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("eventreservationit@gmail.com", "P@ssw0rdPassword");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mm);

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
        public ActionResult Edit([Bind(Include = "Id,Name,Description,City,StreetName,StreetNo,OpeningHour,Manager,ClosingHour,Parking")] Local local, HttpPostedFileBase LocalsImage)
        {

            if (ModelState.IsValid)
            {
                using (var ms = new MemoryStream())
                {
                    LocalsImage.InputStream.CopyTo(ms);
                    local.LocalsImage = ms.ToArray();
                }
                db.Locals.Add(local);

                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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

        [HttpPost]
        public ActionResult Rating(FormCollection data)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            int numberStars = int.Parse(data["Stars"]);
            int LocalId = int.Parse(data["Local"]);
            var tableRow = db.Reviews.FirstOrDefault(r => r.Local.Id == LocalId);
            int oldReview = 0;
            // && r.User == User.Identity.Name
            if (tableRow != null)
            {
                oldReview = db.Reviews.FirstOrDefault(r => r.Local.Id == LocalId).Stars;
                // && r.User == User.Identity.Name
                db.Reviews.Remove(tableRow);
            }
            Local local = db.Locals.Include(l => l.Reviews).FirstOrDefault(l => l.Id == LocalId);
            Review review = new Review { Stars = numberStars, Local = local };
            // , User = User.Identity.Name
            db.Reviews.Add(review);
            local.Reviews.Add(review);
            if (oldReview != 0)
            {
                var removeReview = db.Reviews.FirstOrDefault(r => r.Local.Id == LocalId);
                // && r.User == User.Identity.Name
                local.Reviews.Remove(removeReview);
            }
            db.SaveChanges();
            double av = local.Reviews.Average(r => r.Stars);
            //}
            //return Json(new { success = true});
            return Json(new { average = av });
        }
    }
}
