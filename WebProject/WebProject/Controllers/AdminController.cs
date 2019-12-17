using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;
namespace WebProject.Controllers
{

    public class AdminController : Controller
    {
        QuanLiSach1Entities db = new QuanLiSach1Entities();
        private authenticationClass authenticateFilter = new authenticationClass();

        // GET: Admin
        public ActionResult Index()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");

            return View(db.Saches.ToList());
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            if (authenticateFilter.authenticate(Request, Session) != 2) return RedirectToRoute("ToIndexHome");

            return View();
        }
    }
}