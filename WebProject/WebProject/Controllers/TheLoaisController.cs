using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class TheLoaisController : Controller
    {
        private QuanLiSach1Entities db = new QuanLiSach1Entities();
        private authenticationClass authenticateFilter = new authenticationClass();

        // GET: TheLoais
        public ActionResult Index()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            return View(db.TheLoais.ToList());
        }

        // GET: TheLoais/Details/5
        public ActionResult Details(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        // GET: TheLoais/Create
        public ActionResult Create()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            return View();
        }

        // POST: TheLoais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTheLoai,TheLoai1")] TheLoai theLoai)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (ModelState.IsValid)
            {
                int categoryID;
                try
                {
                    categoryID = db.TheLoais.Max(category => category.MaTheLoai) + 1;
                }
                catch
                {
                    categoryID = 0;
                }
                theLoai.MaTheLoai = categoryID;
                db.TheLoais.Add(theLoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theLoai);
        }

        // GET: TheLoais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        // POST: TheLoais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTheLoai,TheLoai1")] TheLoai theLoai)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (ModelState.IsValid)
            {
                db.Entry(theLoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theLoai);
        }

        // GET: TheLoais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        // POST: TheLoais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            TheLoai theLoai = db.TheLoais.Find(id);
            db.TheLoais.Remove(theLoai);
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
