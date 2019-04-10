using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyXeController : BaseController
    {
        // GET: QuanLyXe
        public ActionResult DanhSachXe()
        {
            if (!CheckAcceptAction("DanhSachXe"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}