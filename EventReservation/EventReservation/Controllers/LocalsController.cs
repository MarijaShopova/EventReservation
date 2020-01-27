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
        public ActionResult Index(int? page)
        {
            return View(db.Locals.Include(l => l.LocalImages).OrderByDescending(l => l.Id).ToPagedList(page ?? 1, 9));
        }



        // GET: Locals/Details/5
        public ActionResult Details(int? id)
        {
            if (!User.IsInRole("Manager"))
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
                var yourReviewResult = db.Reviews.FirstOrDefault(r => r.Local.Id == local.Id && r.User == User.Identity.Name);
                if (yourReviewResult != null)
                    yourReview = db.Reviews.FirstOrDefault(r => r.Local.Id == local.Id && r.User == User.Identity.Name).Stars;
                ViewBag.average = stars;
                ViewBag.yourReview = yourReview;
                return View(local);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // GET: Locals/Requests
        public ActionResult Requests()
        {
            if (User.IsInRole("Admin"))
            {
                return View("~/Views/Admin/LocalRequest.cshtml", db.LocalRequests.ToList());
            }
            return HttpNotFound();
        }

        //GET: Locals/Confirm/5
        public ActionResult Confirm(int id)
        {
            if (User.IsInRole("Admin"))
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
                mm.Subject = "Confirmation for account";
                mm.Body = "Your local account has been accepted and added to our webside. Thank you for choosing us. You can now log in to " +
                    "your account." + "\n";
                mm.Body += "Username: " + user.Email + "\nPassword: " + password;
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
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        //POST: Locals/DeleteRequest
        [HttpPost]
        public ActionResult DeleteRequest(FormCollection data)
        {
            if (User.IsInRole("Manager"))
            {
                int id = int.Parse(data["id"]);
                //find local request
                LocalRequest request = db.LocalRequests.Find(id);

                if (request == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    db.LocalRequests.Remove(request);
                    db.SaveChanges();
                    return Json("{}");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // GET: Locals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.IsInRole("Manager"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Local local = db.Locals.Find(id);
                var currentUser = User.Identity.Name;
                if (local == null)
                {
                    return HttpNotFound();
                }
                if (local.Manager != currentUser)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
                return View(local);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // POST: Locals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,City,StreetName,StreetNo,OpeningHour,Manager,ClosingHour,Parking")] Local local)
        {
            if (User.IsInRole("Manager"))
            {
                var files = Request.Files;

                if (ModelState.IsValid)
                {
                    if (files != null)
                    {

                        foreach (HttpPostedFileBase file in files)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.InputStream.CopyTo(ms);
                                var localImage = new LocalImage
                                {
                                    Local = local,
                                    Image = ms.ToArray()
                                };

                                local.LocalImages.Add(localImage);
                            }
                        }
                    }
                    db.Locals.Add(local);

                    db.Entry(local).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("MyLocal");
                }
                return View(local);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
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

        [HttpGet]
        public ActionResult ListEvents()
        {

            var currentUser = User.Identity.Name;
            var events = db.Locals
                    .Include(l => l.Events)
                    .Where(l => l.Manager == currentUser)
                    .SelectMany(l => l.Events);
            return View(events);

            ///  return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        [HttpGet]
        public ActionResult MyLocal()
        {
            if (User.IsInRole("Manager"))
            {
                var currentUser = User.Identity.Name;
                var local = db.Locals.
                            Where(l => l.Manager == currentUser).
                            FirstOrDefault();

                return View(local);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }


        [HttpPost]
        public ActionResult Rating(FormCollection data)
        {
            double av = 0;
            if (User.Identity.IsAuthenticated)
            {
                int numberStars = int.Parse(data["Stars"]);
                int LocalId = int.Parse(data["Local"]);
                var tableRow = db.Reviews.FirstOrDefault(r => r.Local.Id == LocalId && r.User == User.Identity.Name);
                int oldReview = 0;

                if (tableRow != null)
                {
                    oldReview = db.Reviews.FirstOrDefault(r => r.Local.Id == LocalId && r.User == User.Identity.Name).Stars;
                    db.Reviews.Remove(tableRow);
                }
                Local local = db.Locals.Include(l => l.Reviews).FirstOrDefault(l => l.Id == LocalId);
                Review review = new Review { Stars = numberStars, localId = local.Id, Local = local, User = User.Identity.Name };

                db.Reviews.Add(review);
                local.Reviews.Add(review);
                if (oldReview != 0)
                {
                    var removeReview = db.Reviews.FirstOrDefault(r => r.Local.Id == LocalId && r.User == User.Identity.Name);
                    local.Reviews.Remove(removeReview);
                }
                db.SaveChanges();
                av = local.Reviews.Average(r => r.Stars);
            }
            return Json(new { average = av });
        }

        //POST: Locals/DeleteEvent
        [HttpPost]
        public ActionResult DeleteEvent(FormCollection data)
        {
            if (User.IsInRole("Manager"))
            {
                int id = int.Parse(data["id"]);

                Event myEvent = db.Events.Find(id);

                if (myEvent == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    db.Events.Remove(myEvent);
                    db.SaveChanges();
                    return Json("{}");
                }
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }
    }
}
