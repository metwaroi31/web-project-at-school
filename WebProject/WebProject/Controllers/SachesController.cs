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
    public class SachesController : Controller
    {
        private QuanLiSach1Entities db = new QuanLiSach1Entities();
        private authenticationClass authenticateFilter = new authenticationClass();

        // GET: Saches
        public ActionResult Index()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            var saches = db.Saches.Include(s => s.TacGia).Include(s => s.TheLoai);
            return View(saches.ToList());
        }

        // GET: Saches/Details/5
        public ActionResult Details(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Saches/Create
        public ActionResult Create()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia");
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TheLoai1");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,GiaBan,Mota,MaTheLoai,Soluong,MaTacGia")] Sach sach)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (ModelState.IsValid)
            {
                int bookID;
                try
                {
                    bookID = db.Saches.Max(book => book.MaSach) + 1;
                }catch
                {
                    bookID = 0;
                }
                sach.MaSach = bookID;
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", sach.MaTacGia);
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TheLoai1", sach.MaTheLoai);
            return View(sach);
        }

        // GET: Saches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", sach.MaTacGia);
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TheLoai1", sach.MaTheLoai);
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,GiaBan,Mota,MaTheLoai,Soluong,MaTacGia")] Sach sach)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", sach.MaTacGia);
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TheLoai1", sach.MaTheLoai);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
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
