using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyChuyenXeController : BaseController
    {
        // GET: QuanLyChuyenXe
        public ActionResult DanhSachChuyenXe()
        {
            if (!CheckAcceptAction("DanhSachChuyenXe"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}