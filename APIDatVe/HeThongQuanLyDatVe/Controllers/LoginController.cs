using System.Web.Mvc;

namespace HeThongQuanLyNhaXe.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["acceptScreen"] = null;
            return RedirectToAction("DangNhap");
        }
    }
}