using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Healkeep.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using SendGrid;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;



namespace Healkeep.Controllers
{
    public class NaturalTreatmentsController : Controller
    {
        private Healkeep_DBEntities db = new Healkeep_DBEntities();

        // GET: NaturalTreatments
        public ActionResult Index()
        {
            var naturalTreatments = db.NaturalTreatments.Include(n => n.Diseases).Include(n => n.Diseases);
            return View(naturalTreatments.ToList());
        }



        // GET: NaturalTreatments/Approval/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Approval(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NaturalTreatments naturalTreatment = db.NaturalTreatments.Find(id);

            if (naturalTreatment == null)
            {
                return HttpNotFound();
            }
            else
            {
                naturalTreatment.Approved = true;
                db.SaveChanges();

                // Create the email object first, then add the properties.
                var myMessage = new SendGridMessage();
                // Add the message properties.
                myMessage.From = new MailAddress("Healkeep@healkeep.com");
                // Add multiple addresses to the To field.
                //var hEmail = naturalTreatment.AspNetUserID;

                //var hEmail = (from nt in db.NaturalTreatments
                //            join us in db.AspNetUsers on nt.AspNetUserID equals us.Id
                //            where us.Id == naturalTreatment.AspNetUserID
                //            select us.Email).Single();

                var hEmail = db.AspNetUsers.Where(m => m.Id == naturalTreatment.AspNetUserID).Single().Email;


                List<String> recipients = new List<String>
                {
                    //@"@hEmail",
                    hEmail
                    //@"Jairo Calderon <jairo.a.calderon@gmail.com>"
                    //,@"Anna Lidman <anna@example.com>",
                };

                myMessage.AddTo(recipients);
                myMessage.Subject = "Felicitaciones, su tratamiento ha sido aprobado!";
                //Add the HTML and Text bodies
                var pageUrl = @Url.Action("Details", "NaturalTreatments", new { id = naturalTreatment.NaturalTreatmentID, id2 = naturalTreatment.DiseaseID });
                myMessage.Html = "<p>Felicitaciones, su tratamiento ha sido publicado, muchas otras personas se podran benificiar de su aporte. Eche un vistazo: </p>" + naturalTreatment.Name + "<p></p>" + Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + @pageUrl;
                myMessage.Text = "Felicitaciones, su tratamiento ha sido publicado, muchas otras personas se podran benificiar de su aporte." + naturalTreatment.Name + ".";

                var credentials = new NetworkCredential(
                           ConfigurationManager.AppSettings["mailAccount"],
                           ConfigurationManager.AppSettings["mailPassword"]
                           );

                // Create a Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                if (transportWeb != null)
                {
                    await transportWeb.DeliverAsync(myMessage);
                }
                else
                {
                    Trace.TraceError("Failed to create Web transport.");
                    await Task.FromResult(0);
                }

            }
           
            return RedirectToAction("Details", "Diseases", new { id = naturalTreatment.DiseaseID });
        }



        // GET: NaturalTreatments/Details/5
        public ActionResult Details(int? id)
        {
            NaturalTreatments naturalTreatment = db.NaturalTreatments.Find(id);

            if (id == null || (naturalTreatment.Approved == false && !User.IsInRole("Admin")))
                // && !User.IsInRole("admin")) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //NaturalTreatments naturalTreatment = db.NaturalTreatments.Find(id);
            if (naturalTreatment == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.DiseaseName = db.Diseases.Single(n => n.DiseaseID == naturalTreatment.Diseases.DiseaseID).Name;
            ViewBag.DiseaseID = db.Diseases.Single(n => n.DiseaseID == naturalTreatment.Diseases.DiseaseID).DiseaseID;

            ViewBag.currentUserId = User.Identity.GetUserId();
            var userid = User.Identity.GetUserId();
            if (ViewBag.currentUserId != null)
            {
                ViewBag.User = HttpContext.User.Identity;
                List<AspNetUserComments> userCommentsCheckedUp;
                userCommentsCheckedUp = db.AspNetUserComments.Where(x => x.AspNetUserID == userid).Where(x => x.CheckedUp == true).ToList();
                if (userCommentsCheckedUp.Count() != 0)
                {
                    ViewBag.UserCommentsCheckedUp = userCommentsCheckedUp;
                }
                List<AspNetUserComments> userCommentsCheckedDown;
                userCommentsCheckedDown = db.AspNetUserComments.Where(x => x.AspNetUserID == userid).Where(x => x.CheckedDown == true).ToList();
                if (userCommentsCheckedUp.Count() != 0)
                {
                    ViewBag.UserCommentsCheckedDown = userCommentsCheckedDown;
                }
                
            }
            
            return View(naturalTreatment);

        }

        // POST: NaturalTreatments/Comments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[AcceptVerbs(HttpVerbs.Post), Authorize]
        [Authorize]
        [HttpPost]
        public ActionResult Details( Comments comment, Int64 naturalTreatmentId, string commentDescription, string btnSubmit)
             
        //public ActionResult Details([Bind(Include = "NaturalTreatmentID,AspNetUserID,CommentDescription")] Comments comment)
        {
            switch (btnSubmit)
            {
                case "Submit":

                    comment.Description = commentDescription;
                    comment.AspNetUserID = User.Identity.GetUserId();
                    comment.NaturalTreatmentID = naturalTreatmentId;
                    comment.CreatedTime = DateTime.Now;
                    db.Comments.Add(comment);
                    db.SaveChanges();

                    return RedirectToAction("Details", "NaturalTreatments", new { id = comment.NaturalTreatmentID });

                case "Cancel":
                         return RedirectToAction("Details", "NaturalTreatments", new { id = comment.NaturalTreatmentID });
            }
            return RedirectToAction("Details", "NaturalTreatments", new { id = comment.NaturalTreatmentID });
        }



        [Authorize]
        // GET: NaturalTreatments/Create
        public ActionResult Create()
        {
            //ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name");
            ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name");
            return View();
        }

        // POST: NaturalTreatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NaturalTreatmentID,Name,ScoreUsers,ScoreEntities,Description,Treatment,Prevention,DiseaseID,DiseaseID")] NaturalTreatments naturalTreatment)
        {
            if (ModelState.IsValid)
            {
                db.NaturalTreatments.Add(naturalTreatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name", naturalTreatment.DiseaseID);
            ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name", naturalTreatment.DiseaseID);
            return View(naturalTreatment);
        }

/////////////////////////////////////

        // GET: NaturalTreatments/Create_
        [Authorize]
        public ActionResult Create_(int? id, bool IsTreat, bool IsPrev)
        {

            NaturalTreatments model = new NaturalTreatments();
            ViewBag.DiseaseName = db.Diseases.Single(n => n.DiseaseID == id).Name;
            ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name", id);
            ViewBag.NuiID = id;
            ViewBag.AspNetUserID = User.Identity.GetUserId();
            model.Treatment = IsTreat;
            model.Prevention = IsPrev;
            model.AspNetUserID = User.Identity.GetUserId();
            return View(model);
        }

        // POST: NaturalTreatments/Create_
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create_([Bind(Include = "NaturalTreatmentID,Name,ScoreUsers,ScoreEntities,Description,Treatment,Prevention,DiseaseID,AspNetUserID")] NaturalTreatments naturalTreatment)
        {

            if (ModelState.IsValid)
            {

                db.NaturalTreatments.Add(naturalTreatment);
                db.SaveChanges();

                // Create the email object first, then add the properties.
                var myMessage = new SendGridMessage();
                // Add the message properties.
                myMessage.From = new MailAddress("NoReply@healkeep.com");
                // Add multiple addresses to the To field.
                List<String> recipients = new List<String>
                {
                    @"Jairo Calderon <jairo.a.calderon@gmail.com>"
                    //,@"Anna Lidman <anna@example.com>",
                };
                myMessage.AddTo(recipients);
                myMessage.Subject = "New Treatment for approval";
                //Add the HTML and Text bodies
                    // Access the page's Request object to retrieve the Url.
                    //var pageUrl = System.Web.HttpContext.Current.Request.Url
                //myMessage.Html = "<a href=\"google.com\">Here</a>";
                var pageUrl = @Url.Action("Details", "NaturalTreatments", new { id = naturalTreatment.NaturalTreatmentID, id2 = naturalTreatment.DiseaseID });
                myMessage.Html = "<p>The treatment </p>" + naturalTreatment.Name + "<p> search approval, please verify. </p>" + Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + @pageUrl;
                myMessage.Text = "The treatment " + naturalTreatment.Name + " search approval, please verify. ";
                         
                var credentials = new NetworkCredential(
                           ConfigurationManager.AppSettings["mailAccount"],
                           ConfigurationManager.AppSettings["mailPassword"]
                           );

                // Create a Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                if (transportWeb != null)
                {
                    await transportWeb.DeliverAsync(myMessage);
                }
                else
                {
                    Trace.TraceError("Failed to create Web transport.");
                    await Task.FromResult(0);
                }

                 //@Html.ActionLink("Adicionar un tratamiento", "Create_", "NaturalTreatments", new { id = Model.DiseaseID, IsTreat = true, IsPrev = false }, null)
                return RedirectToAction("Thanks_Update", "AspNetUsers", new { id = naturalTreatment.AspNetUserID, id2 = naturalTreatment.DiseaseID });

                 
            }
           

            ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name", naturalTreatment.DiseaseID);

            return View(naturalTreatment);
        }

        private object ActionLink(string p1, string p2, object p3)
        {
            throw new NotImplementedException();
        }

////////////////////////////////////

        // GET: NaturalTreatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NaturalTreatments naturalTreatment = db.NaturalTreatments.Find(id);
            if (naturalTreatment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name", naturalTreatment.DiseaseID);
            ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name", naturalTreatment.DiseaseID);
            return View(naturalTreatment);
        }

        // POST: NaturalTreatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NaturalTreatmentID,Name,ScoreUsers,ScoreEntities,Description,Treatment,Prevention,DiseaseID,DiseaseID,AspNetUserID,Approved")] NaturalTreatments naturalTreatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(naturalTreatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "NaturalTreatments", new { id = naturalTreatment.NaturalTreatmentID, id2 = naturalTreatment.DiseaseID });
            }
            //ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name", naturalTreatment.DiseaseID);
            ViewBag.DiseaseID = new SelectList(db.Diseases, "DiseaseID", "Name", naturalTreatment.DiseaseID);
            return View(naturalTreatment);
        }

        // GET: NaturalTreatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NaturalTreatments naturalTreatment = db.NaturalTreatments.Find(id);
            if (naturalTreatment == null)
            {
                return HttpNotFound();
            }
            return View(naturalTreatment);
        }

        // POST: NaturalTreatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            NaturalTreatments naturalTreatment = db.NaturalTreatments.Find(id);
            var diseaseID = naturalTreatment.DiseaseID;
            db.NaturalTreatments.Remove(naturalTreatment);
            db.SaveChanges();
            return RedirectToAction("Details", "Diseases", new { id = diseaseID });
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
