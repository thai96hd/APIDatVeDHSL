using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyNhaXe.Controllers
{
    public class QuanLyTaiKhoanController : Controller
    {
        // GET: QuanLyTaiKhoan
        public ActionResult ThongTinTaiKhoan()
        {
            return View();
        }
        public ActionResult DanhSachTaiKhoan()
        {
            return View();
        }
        public ActionResult DanhSachQuyen()
        {
            return View();
        }
        public ActionResult PhanQuyen()
        {
            return View();
        }
    }
}