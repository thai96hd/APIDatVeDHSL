using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyNhanVienController : BaseController
    {
        // GET: QuanLyNhanVien
        public ActionResult DanhSachNhanVien()
        {
            if (!CheckAcceptAction("DanhSachNhanVien"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}