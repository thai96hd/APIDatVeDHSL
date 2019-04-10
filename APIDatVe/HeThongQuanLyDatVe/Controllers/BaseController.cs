using HeThongQuanLyDatVe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class BaseController : Controller
    {
        public bool TryGetRole()
        {
            return true;
            //try
            //{
            //    string authenticationToken = Request.Cookies["token"].Value;
            //    if (authenticationToken is null)
            //    {
            //        return false;
            //    }
            //    string userName = GetCurrentUser(authenticationToken);
            //    var db = new DB();
            //    List<string> acceptScreen =
            //    Session["acceptScreen"] = acceptScreen;
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }
        public bool CheckAcceptAction(string _action)
        {
            return true;
            if (Session["acceptScreen"] is null)
                return false;
            List<string> acceptScreen = (List<string>)Session["acceptScreen"];
            return acceptScreen.Any(x => x == _action);
        }
        private string GetCurrentUser(string authenticationToken)
        {
            authenticationToken = System.Web.HttpUtility.UrlDecode(authenticationToken);
            string decodeAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
            string[] account = decodeAuthenticationToken.Split(':');
            return Encode.Decrypt(account[0]);
        }
    }
}