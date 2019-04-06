using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyNhaXe.Controllers
{
    public class QuanLyDatXeController : Controller
    {
        // GET: QuanLyDatXe
        public ActionResult DanhSachDatXe()
        {
            return View();
        }
        public ActionResult DatXe()
        {
            return View();
        }
    }
}