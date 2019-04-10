using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyDatXeController : BaseController
    {
        // GET: QuanLyDatXe
        public ActionResult DanhSachDatXe()
        {
            if (!CheckAcceptAction("DanhSachDatXe"))
                return Redirect("/Login/DangNhap");
            return View();
        }
        public ActionResult DatXe()
        {
            if (!CheckAcceptAction("DatXe"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}