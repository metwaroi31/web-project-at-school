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
    public class ChiTietDonsController : Controller
    {
        private QuanLiSach1Entities db = new QuanLiSach1Entities();
        private authenticationClass authenticateFilter = new authenticationClass();

        // GET: ChiTietDons
        public ActionResult Index()
        {
            if (authenticateFilter.authenticate(Request, Session) != 1) return RedirectToRoute("ToIndexHome");
            string userLogin = Request.Cookies["username"].Value.ToString();
            var chiTietDons = db.ChiTietDons.Include(c => c.DonHang).Where(c => c.DonHang.username == userLogin);
            return View(chiTietDons.ToList());
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
