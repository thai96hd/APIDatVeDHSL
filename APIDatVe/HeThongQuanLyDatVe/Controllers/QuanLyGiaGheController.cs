using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyGiaGheController : BaseController
    {
        // GET: QuanLyGiaGhe
        public ActionResult GiaGhe()
        {
            if (!CheckAcceptAction("GiaGhe"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}