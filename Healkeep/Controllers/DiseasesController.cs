using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Healkeep.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;

namespace Healkeep.Controllers
{
    //[Authorize]
    public class DiseasesController : Controller
    {
        private Healkeep_DBEntities db = new Healkeep_DBEntities();

         //GET: Diseases
        public ActionResult Index(string search, int? page, string sortBy)
        {

            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";

            var diseases = db.Diseases.AsQueryable();

            diseases = diseases.Where(x => x.Name.Contains(search) || search == null);

            switch (sortBy)
            {
                case "Name desc":
                    diseases = diseases.OrderByDescending(x => x.Name);
                    break;
                default:
                    diseases = diseases.OrderBy(x => x.Name);
                    break;
            }

            return View(diseases.ToPagedList(page ?? 1, 5));
        }


        // POST: Diseases
        [HttpPost]
        public ActionResult Index(string searchTerm, int? page)
        {
            Healkeep_DBEntities db = new Healkeep_DBEntities();
            List<Diseases> diseases;
            if (string.IsNullOrEmpty(searchTerm))
            {
                diseases = db.Diseases.OrderBy(x => x.Name).ToList();
            }
            else
            {
                diseases = db.Diseases.Where(x => x.Name.Contains(searchTerm)).OrderBy(x => x.Name).ToList();
            }
            return View(diseases.ToPagedList(page ?? 1, 5));
        }


        // JSON: GetDiseases
        [HttpPost]
        public JsonResult GetDiseases (string term)
        {
            List<string> diseases;
            diseases = db.Diseases.Where(x => x.Name.Contains(term))
                .Select(y => y.Name).ToList();

            return Json(diseases, JsonRequestBehavior.AllowGet);
        }



        // GET: Diseases/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diseases disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }



        [Authorize]
        // GET: Diseases/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Diseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiseaseID,Name,Description,URLwiki")] Diseases disease)
        {
            if (ModelState.IsValid)
            {
                db.Diseases.Add(disease);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disease);
        }



        // GET: Diseases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diseases disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiseaseID,Name,Description,URLwiki")] Diseases disease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disease);
        }

        // GET: Diseases/Delete/5
        [Authorize(Roles="Admin")]
        public ActionResult Delete(int? id)
        {
            //var userId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diseases disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            //return View("Delete", disease);
            return View(disease);
        }

        // POST: Diseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diseases disease = db.Diseases.Find(id);
            db.Diseases.Remove(disease);
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
