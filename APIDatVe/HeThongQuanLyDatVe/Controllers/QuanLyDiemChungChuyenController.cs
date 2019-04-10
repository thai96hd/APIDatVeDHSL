using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyDiemChungChuyenController : BaseController
    {
        // GET: QuanLyDiemChungChuyen
        public ActionResult DanhSachDiemCC()
        {
            if (!CheckAcceptAction("DanhSachDiemCC"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}