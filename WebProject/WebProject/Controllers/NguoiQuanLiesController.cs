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
    public class NguoiQuanLiesController : Controller
    {
        private QuanLiSach1Entities db = new QuanLiSach1Entities();
        private authenticationClass authenticateFilter = new authenticationClass();
        // GET: NguoiQuanLies
        public ActionResult Index()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            object user = Request.Cookies["username"].Value;
            string loginUser = user.ToString();
            var nguoiQuanLies = db.NguoiQuanLies.Include(n => n.Account).Include(n => n.NguoiQuanLy2).Where(manager => manager.username != loginUser);
            return View(nguoiQuanLies.ToList());
        }

        // GET: NguoiQuanLies/Details/5
        public ActionResult Details(string id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiQuanLy nguoiQuanLy = db.NguoiQuanLies.Find(id);
            if (nguoiQuanLy == null)
            {
                return HttpNotFound();
            }
            return View(nguoiQuanLy);
        }

        // GET: NguoiQuanLies/Create
        public ActionResult Create()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            ViewBag.username = new SelectList(db.Accounts, "username", "password");
            ViewBag.NguoiThem = new SelectList(db.NguoiQuanLies, "username", "HoTen");
            return View();
        }

        // POST: NguoiQuanLies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HoTen,NgaySinh,GioiTinh,DienThoai,Account,NguoiThem")] NguoiQuanLy nguoiQuanLy)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (ModelState.IsValid)
            {
                string username = nguoiQuanLy.Account.username;
                string managerWhoAdds = Request.Cookies["username"].Value.ToString();
                NguoiQuanLy existManager = db.NguoiQuanLies.Find(username);
                if (existManager != null)
                {
                    ViewBag.message = "please use another username";
                    return View();
                }
                nguoiQuanLy.NguoiThem = managerWhoAdds;
                db.NguoiQuanLies.Add(nguoiQuanLy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.username = new SelectList(db.Accounts, "username", "password", nguoiQuanLy.username);
            ViewBag.NguoiThem = new SelectList(db.NguoiQuanLies, "username", "HoTen", nguoiQuanLy.NguoiThem);
            return View(nguoiQuanLy);
        }

        // GET: NguoiQuanLies/Edit/5
        public ActionResult Edit()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            object user = Request.Cookies["username"].Value;
            string loginUser = user.ToString();
            NguoiQuanLy nguoiQuanLy = db.NguoiQuanLies.Find(loginUser);
            if (nguoiQuanLy == null)
            {
                return HttpNotFound();
            }
            ViewBag.username = new SelectList(db.Accounts, "username", "password", nguoiQuanLy.username);
            ViewBag.NguoiThem = new SelectList(db.NguoiQuanLies, "username", "HoTen", nguoiQuanLy.NguoiThem);
            return View(nguoiQuanLy);
        }

        // POST: NguoiQuanLies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HoTen,NgaySinh,GioiTinh,DienThoai,Account,NguoiThem")] NguoiQuanLy nguoiQuanLy)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (ModelState.IsValid)
            {
                nguoiQuanLy.username = nguoiQuanLy.Account.username;
                NguoiQuanLy managerToEdit = db.NguoiQuanLies.Find(nguoiQuanLy.username);
                Account accountToEdit = db.Accounts.Find(nguoiQuanLy.username);
                managerToEdit.HoTen = nguoiQuanLy.HoTen;
                managerToEdit.NgaySinh = nguoiQuanLy.NgaySinh;
                managerToEdit.GioiTinh = nguoiQuanLy.GioiTinh;
                managerToEdit.DienThoai = nguoiQuanLy.DienThoai;
                accountToEdit.password = nguoiQuanLy.Account.password;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.username = new SelectList(db.Accounts, "username", "password", nguoiQuanLy.username);
            ViewBag.NguoiThem = new SelectList(db.NguoiQuanLies, "username", "HoTen", nguoiQuanLy.NguoiThem);
            return View(nguoiQuanLy);
        }

        // GET: NguoiQuanLies/Delete/5
        public ActionResult Delete(string id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiQuanLy nguoiQuanLy = db.NguoiQuanLies.Find(id);
            if (nguoiQuanLy == null)
            {
                return HttpNotFound();
            }
            return View(nguoiQuanLy);
        }

        // POST: NguoiQuanLies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");
            NguoiQuanLy nguoiQuanLy = db.NguoiQuanLies.Find(id);
            Account accountOfManager = db.Accounts.Find(id);
            db.NguoiQuanLies.Remove(nguoiQuanLy);
            db.Accounts.Remove(accountOfManager);
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
