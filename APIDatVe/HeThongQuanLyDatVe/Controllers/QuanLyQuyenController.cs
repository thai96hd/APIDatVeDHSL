using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyQuyenController : BaseController
    {
        // GET: QuanLyQuyen
        public ActionResult DanhSachQuyen()
        {
            if (!CheckAcceptAction("DanhSachQuyen"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}