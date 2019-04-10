using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyKipController : BaseController
    {
        // GET: QuanLyKip
        public ActionResult DanhSachKip()
        {
            if (!CheckAcceptAction("DanhSachKip"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}