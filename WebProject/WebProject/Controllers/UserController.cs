using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class UserController : Controller
    {
        private QuanLiSach1Entities db = new QuanLiSach1Entities();
        private authenticationClass authenticateFilter = new authenticationClass();
        // GET: User
        public ActionResult Index()
        {
            if (authenticateFilter.authenticate(Request, Session) == 0) return RedirectToAction("Login");

            return RedirectToRoute("ToIndexHome");
        }

        public ActionResult Login()
        {
            if (authenticateFilter.authenticate(Request, Session) != 0) return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string pwd)
        {
            if (authenticateFilter.authenticate(Request, Session) != 0) return Json(new { message = "not authenticated" });

            Account user = db.Accounts.Find(username);
            int customerFlag = db.KhachHangs.Where(cust => cust.username == username).ToList().Count;
            int managerFlag = db.NguoiQuanLies.Where(manager => manager.username == username).ToList().Count;
            bool[] userFlags = { false, false };

            if (user != null)
            {
                if (user.password == pwd)
                {
                    userFlags[0] = true;
                    if (managerFlag == 1)
                    {
                        userFlags[1] = true;
                    }
                    // add session
                    Session[username] = userFlags;
                }
                else
                {
                    return Json(new { message = "wrong password or username" });
                }
            }
            else
            {
                return Json(new { message = "wrong password or username" });
            }
            return Json(new { user = username, userRole = userFlags[1] });
        }
        public ActionResult Register()
        {
            if (authenticateFilter.authenticate(Request, Session) != 0) return RedirectToAction("Index");

            return View();
        }
        [HttpPost]
        public ActionResult Register(KhachHang khachhang)
        {
            if (authenticateFilter.authenticate(Request, Session) != 0) return RedirectToAction("Index");
            KhachHang khachHangChecker = db.KhachHangs.Find(khachhang.username);
            if (khachHangChecker != null)
            {
                ViewBag.message = "the username has already be taken";
                return View();
            }
            khachhang.Account.username = khachhang.username;
            db.KhachHangs.Add(khachhang);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult EditProfile()
        {
            if (authenticateFilter.authenticate(Request, Session) == 0) return RedirectToAction("Login");

            object user = Request.Cookies["username"].Value;
            string loginUser = user.ToString();

            KhachHang userToEdit = db.KhachHangs.Find(loginUser);

            return View(userToEdit);
        }
        public ActionResult Logout ()
        {
            if (authenticateFilter.authenticate(Request, Session) == 0) return RedirectToAction("Login");
            string userLogin = Request.Cookies["username"].Value.ToString();
            Session[userLogin] = null;
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult EditProfile(KhachHang khachhang)
        {
            if (authenticateFilter.authenticate(Request, Session) == 0) return RedirectToAction("Login");
            KhachHang khachHangToEdit = db.KhachHangs.Find(khachhang.username);
            KhachHang displayKhachHang;
            khachHangToEdit.HoTen = khachhang.HoTen;
            khachHangToEdit.NgaySinh = khachhang.NgaySinh;
            khachHangToEdit.GioiTinh = khachhang.GioiTinh;
            khachHangToEdit.DienThoai = khachhang.DienThoai;
            khachHangToEdit.Account.password = khachhang.Account.password;
            db.SaveChanges();
            ViewBag.message = "Succesfully editted";
            displayKhachHang = db.KhachHangs.Find(khachhang.username);
            return RedirectToAction("Index");
        }

    }
}