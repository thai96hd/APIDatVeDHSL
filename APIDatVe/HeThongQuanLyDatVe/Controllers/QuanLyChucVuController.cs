using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class QuanLyChucVuController : BaseController
    {
        // GET: QuanLyChucVu
        public ActionResult DanhSachChucVu()
        {
            if (!CheckAcceptAction("DanhSachChucVu"))
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}