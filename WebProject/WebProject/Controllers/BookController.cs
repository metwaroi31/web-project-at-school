using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;
using PagedList;

namespace WebProject.Controllers
{
    public class BookController : Controller
    {
        private authenticationClass authenticateFilter = new authenticationClass();
        private QuanLiSach1Entities db = new QuanLiSach1Entities();
        public ActionResult Index(int ? page, int ? category, int ? author)
        {
            int userRole = authenticateFilter.authenticate(Request, Session);
            if (userRole == 0) return RedirectToRoute("ToIndexUser");
            if (userRole == 2) return RedirectToRoute("IndexForManager");
            int numberOfBooksInOnePage = 6;
            int displayPage = page ?? 1;
            var displayBooks = db.Saches.ToList();

            if (category != null)
            {
                displayBooks = db.Saches.Where(book => book.MaTheLoai == category).ToList();
                TheLoai displayTheloai = db.TheLoais.Find(category);
                ViewBag.categoryDisplay = displayTheloai.TheLoai1;
                ViewBag.category = category;
            } 
            if (author != null)
            {
                displayBooks = db.Saches.Where(book => book.MaTacGia == author).ToList();
                TacGia displayTacgia = db.TacGias.Find(author);
                ViewBag.authorDisplay = displayTacgia.TenTacGia;
                ViewBag.author = author;
            }
            ViewBag.Categories = db.TheLoais.ToList();
            ViewBag.Authors = db.TacGias.ToList();
            return View(displayBooks.ToPagedList(displayPage, numberOfBooksInOnePage));
        }
        public ActionResult DetailBook(int ? MaSach)
        {
            if (authenticateFilter.authenticate(Request, Session) == 0) return RedirectToRoute("ToIndexUser");
            Sach sach = db.Saches.Find(MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        public List<GioHang> takeCart()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                // gio hang == null thi  khoi tao list gio hang
                // session dung de luu gio hang tren toan trang web
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        private List<int> readCart (string[] cartFromClient)
        {
            int numberOfBooks = cartFromClient.Length;
            List<int> returnedCart = new List<int>();
            for (int i = 0; i < numberOfBooks; i += 2)
            {
                int book = int.Parse(cartFromClient[i]);
                int soluong = int.Parse(cartFromClient[i + 1]);
                int bookChecker = returnedCart.IndexOf(book);
                if (bookChecker % 2 == 0 && returnedCart.Count != 0)
                {
                    returnedCart[bookChecker + 1] += soluong;
                }
                else
                {
                    returnedCart.Add(book);
                    returnedCart.Add(soluong);
                }
            }
            return returnedCart;
        }
        public ActionResult Cart()
        {
            /*
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("About", "Home");
            }
            */

            if (authenticateFilter.authenticate(Request, Session) != 1) return RedirectToAction("Index");
            List<GioHang> lstGioHang = new List<GioHang>();
            List<int> cartFromClient = new List<int>();
            string[] cart;
            try
            {
                cart = Request.Cookies["cart"].Value.ToString().Split(',');
            } catch
            {
                ViewBag.CartError = "Your cart is empty";
                return View();
            }
            int numberOfBooks = cart.Length;
            if (numberOfBooks % 2 != 0 || numberOfBooks == 0)
            {
                ViewBag.CartError = "Your cart is empty";
                return View();
            }
            cartFromClient = readCart(cart);
            for (int i = 0; i < cartFromClient.Count; i += 2)
            {
                int book = cartFromClient[i];
                int soluong = cartFromClient[i + 1];
                GioHang bookToClient = new GioHang(book);
                bookToClient.iSoLuong = soluong;
                lstGioHang.Add(bookToClient);
            }
            return View(lstGioHang);
        }
        public ActionResult Buy ()
        {
            if (authenticateFilter.authenticate(Request, Session) != 1) return RedirectToAction("Index");
            List<GioHang> lstGioHang = new List<GioHang>();
            List<int> cartFromClient = new List<int>();
            string[] cart;
            string username;
            try
            {
                cart = Request.Cookies["cart"].Value.ToString().Split(',');
                username = Request.Cookies["username"].Value.ToString();
            }
            catch
            {
                return Json(new { message = "Nothing in cart" });
            }
            int numberOfBooks = cart.Length;
            if (numberOfBooks % 2 != 0 || numberOfBooks == 0)
            {
                return Json(new { message = "invalid cart" });
            }
            KhachHang customer = db.KhachHangs.Find(username);
            DonHang bill = new DonHang();
            int madonhang;
            try
            {
                madonhang = db.DonHangs.Max(donhang => donhang.MaDonHang) + 1;
            } catch
            {
                madonhang = 0;
            }
            bill.MaDonHang = madonhang;
            bill.KhachHang = customer;
            db.DonHangs.Add(bill);  //add bill before adding bill detail
            cartFromClient = readCart(cart);
            for (int i = 0; i < cartFromClient.Count; i += 2)
            {
                int numberOfBooksSelling = cartFromClient[i + 1];
                int bookID = cartFromClient[i];
                ChiTietDon billDetail = new ChiTietDon();
                Sach bookToSell = db.Saches.Find(bookID);
                bookToSell.Soluong -= numberOfBooksSelling;
                if (bookToSell.Soluong < 0)
                {
                    return Json(new { message = "Some of your  items  you want to buy is out of stock" });
                }
                billDetail.MaDonHang = bill.MaDonHang;
                billDetail.MaSach = bookToSell.MaSach;
                billDetail.SoLuong = numberOfBooksSelling;
                billDetail.DonGia = bookToSell.GiaBan.ToString();
                db.ChiTietDons.Add(billDetail);
            }
            db.SaveChanges();
            return Json(new { message = "Successfully bought" });
        }
        /*
        // add gio hang
        public ActionResult AddCart(int iMaSach, string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404; // tra ve trang bao loi
                return null;
            }

            List<GioHang> lstGioHang = takeCart();
            // kiem tra masach dc mua chua, neu dc mua roi thi ++
            GioHang cart = lstGioHang.Find(n => n.iMaSach == iMaSach); // tim xem ma sach nay ton tai trong session hay khong
            if (cart == null)
            {
                cart = new GioHang(iMaSach);
                // add book into list
                lstGioHang.Add(cart);
                return Redirect(strURL);
            }
            else
            {
                cart.iSoLuong++;
                return Redirect(strURL);
            }
        }

        // update cart
        public ActionResult updateCart(int iMaSach, FormCollection f)
        {
            // kiem tra sach
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // kiem tra sach co trong sesseion khong
            // neu co cho suwa
            List<GioHang> lstGioHang = takeCart();
            GioHang cart = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);
            if (cart != null)
            {
                cart.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return View("GioHang");
        }

        // delete cart
        public ActionResult DeleteCart(int iMaSach)
        {
            // kiem tra sach
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // kiem tra sach co trong sesseion khong
            // neu co cho suwa
            List<GioHang> lstGioHang = takeCart();
            GioHang cart = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);
            if (cart != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSach);
            }

            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("About", "Home");
            }
            return RedirectToAction("GioHang");
        }

        // build cart page

        // calculate total number of book
        private int totalNumber()
        {
            int itotalNumber = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                itotalNumber = lstGioHang.Sum(n => n.iSoLuong);
            }
            return itotalNumber;
        }

        // total money
        private double TotalMoney()
        {
            double dMoney = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dMoney = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dMoney;
        }
        */
    }
}