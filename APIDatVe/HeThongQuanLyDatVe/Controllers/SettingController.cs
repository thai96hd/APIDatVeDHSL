using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class SettingController : BaseController
    {
        // GET: Setting
        public ActionResult SettingStyle()
        {
            if (!CheckAcceptAction("SettingStyle"))
                return Redirect("/Login/DangNhap");
            string setingstyle = Request.Cookies["setingstyle"].Value;
            Session["setingstyle"] = setingstyle;
            return View();
        }
    }
}