using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class GioHang
    {
        QuanLiSach1Entities db = new QuanLiSach1Entities();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public double dGiaSach { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dGiaSach; }
        }

        // ham khoi tao gio hang
        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            Sach sach = db.Saches.Single(n => n.MaSach == iMaSach);
            sTenSach = sach.TenSach;
            dGiaSach = double.Parse(sach.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}