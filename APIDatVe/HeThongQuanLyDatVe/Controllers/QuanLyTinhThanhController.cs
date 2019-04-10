using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyTinhThanhController : BaseController
    {
        // GET: QuanLyTinhThanh
        public ActionResult DanhSachTinhThanh()
        {
            if (!CheckAcceptAction("DanhSachTinhThanh"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}