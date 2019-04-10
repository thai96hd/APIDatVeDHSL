using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyLoTrinhController : BaseController
    {
        // GET: QuanLyLoTrinh
        public ActionResult DanhSachLoTrinh()
        {
            if (!CheckAcceptAction("DanhSachLoTrinh"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}