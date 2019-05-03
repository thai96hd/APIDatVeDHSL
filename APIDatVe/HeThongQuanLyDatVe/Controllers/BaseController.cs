using HeThongQuanLyDatVe.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HeThongQuanLyDatVe.Controllers
{
    public class BaseController : Controller
    {
        public bool TryGetRole()
        {
            try
            {
                string token = Request.Cookies["token"].Value;
                string danhsachmanhinh = Request.Cookies["danhsachmanhinh"].Value;
                string setingstyle = Request.Cookies["setingstyle"].Value;
                if (token is null || danhsachmanhinh is null)
                {
                    return false;
                }
                Session["userName"] = Encode.Decrypt(token).Split(':')[0];
                Session["setingstyle"] = setingstyle;
                Session["acceptScreen"] = JsonConvert.DeserializeObject<List<string>>(Encode.Decrypt(danhsachmanhinh));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckAcceptAction(string _action)
        {
            if (Session["acceptScreen"] is null)
                return false;
            List<string> acceptScreen = (List<string>)Session["acceptScreen"];
            return acceptScreen.Any(x => x == _action);
        }
        private string GetCurrentUser(string authenticationToken, out List<string> acceptScreen)
        {
            authenticationToken = System.Web.HttpUtility.UrlDecode(authenticationToken);
            string decodeAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
            decodeAuthenticationToken = Encode.Decrypt(decodeAuthenticationToken);
            string[] account = decodeAuthenticationToken.Split(':');
            acceptScreen = JsonConvert.DeserializeObject<List<string>>(account[2]);
            return account[0];
        }
    }
}