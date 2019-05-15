using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class ThongKeController : BaseController
    {
        // GET: ThongKe
        public ActionResult ThongKeDatXe()
        {
            if (!CheckAcceptAction("ThongKeDatXe"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}