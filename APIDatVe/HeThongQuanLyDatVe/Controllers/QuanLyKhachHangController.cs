using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyKhachHangController : BaseController
    {
        // GET: QuanLyKhachHang
        public ActionResult DanhSachKhachHang()
        {
            if (!CheckAcceptAction("DanhSachKhachHang"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}