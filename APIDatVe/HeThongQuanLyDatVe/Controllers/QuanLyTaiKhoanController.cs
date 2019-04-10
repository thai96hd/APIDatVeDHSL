using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyTaiKhoanController : BaseController
    {
        // GET: QuanLyTaiKhoan
        public ActionResult ThongTinTaiKhoan()
        {
            return View();
        }
        public ActionResult DanhSachTaiKhoan()
        {
            if (!CheckAcceptAction("DanhSachTaiKhoan"))
                return Redirect("/Login/DangNhap");
            return View();
        }
        public ActionResult PhanQuyen()
        {
            if (!CheckAcceptAction("PhanQuyen"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}