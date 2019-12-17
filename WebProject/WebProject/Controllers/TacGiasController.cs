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
    public class TacGiasController : Controller
    {
        private QuanLiSach1Entities db = new QuanLiSach1Entities();
        private authenticationClass authenticateFilter = new authenticationClass();

        // GET: TacGias
        public ActionResult Index()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            return View(db.TacGias.ToList());
        }

        // GET: TacGias/Details/5
        public ActionResult Details(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TacGia tacGia = db.TacGias.Find(id);
            if (tacGia == null)
            {
                return HttpNotFound();
            }
            return View(tacGia);
        }

        // GET: TacGias/Create
        public ActionResult Create()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            return View();
        }

        // POST: TacGias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTacGia,TenTacGia,DiaChi,DienThoai")] TacGia tacGia)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (ModelState.IsValid)
            {
                int authorID;
                try
                {
                    authorID = db.TacGias.Max(author => author.MaTacGia) + 1;
                } catch
                {
                    authorID = 0;
                }
                tacGia.MaTacGia = authorID;
                db.TacGias.Add(tacGia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tacGia);
        }

        // GET: TacGias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TacGia tacGia = db.TacGias.Find(id);
            if (tacGia == null)
            {
                return HttpNotFound();
            }
            return View(tacGia);
        }

        // POST: TacGias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTacGia,TenTacGia,DiaChi,DienThoai")] TacGia tacGia)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (ModelState.IsValid)
            {
                db.Entry(tacGia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tacGia);
        }

        // GET: TacGias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TacGia tacGia = db.TacGias.Find(id);
            if (tacGia == null)
            {
                return HttpNotFound();
            }
            return View(tacGia);
        }

        // POST: TacGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            TacGia tacGia = db.TacGias.Find(id);
            db.TacGias.Remove(tacGia);
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
