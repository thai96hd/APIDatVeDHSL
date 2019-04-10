using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyGheNgoiController : BaseController
    {
        // GET: QuanLyGheNgoi
        public ActionResult DanhSachGheNgoi()
        {
            if (!CheckAcceptAction("DanhSachGheNgoi"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}