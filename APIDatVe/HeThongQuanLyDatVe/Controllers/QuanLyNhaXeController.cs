using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyNhaXeController : BaseController
    {
        // GET: QuanLyNhaXe
        public ActionResult ThongTinNhaXe()
        {
            if (!CheckAcceptAction("ThongTinNhaXe"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}