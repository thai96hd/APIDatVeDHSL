using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class TongQuanController : BaseController
    {
        // GET: TongQuan
        public ActionResult ManHinhTongQuan()
        {
            if (!TryGetRole())
                return Redirect("/Login/DangNhap");
            return View();
        }
    }
}