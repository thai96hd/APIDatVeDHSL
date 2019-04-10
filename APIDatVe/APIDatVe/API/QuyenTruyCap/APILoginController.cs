using APIDatVe.Database;
using APIDatVe.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace APIDatVe.API.QuyenTruyCap
{
    [RoutePrefix("api/login")]
    public class APILoginController : ApiController
    {
        public class AccountLogin
        {
            public string _userName { get; set; }
            public string _password { get; set; }
        }
        [Route("check-account")]
        [HttpPost]
        public HttpResponseMessage CheckAccount(AccountLogin account)
        {
            try
            {
                using (var db = new DB())
                {
                    if (db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == account._userName) == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tài khoản không tồn tại");
                    }
                    if (UserSecurity.CheckLogin(account._userName, account._password))
                    {
                        TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == account._userName);
                        taiKhoan.solandangnhapsai = 0;
                        db.SaveChanges();
                        string token = "";
                        HttpResponseMessage response = getResponseToken(account._userName, account._password, out token);
                        response.Content = new StringContent(token);
                        return response;
                    }
                    else
                    {
                        TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == account._userName);
                        if (taiKhoan != null)
                        {
                            taiKhoan.solandangnhapsai = taiKhoan.solandangnhapsai + 1;
                            if (taiKhoan.solandangnhapsai >= 5)
                            {
                                taiKhoan.trangthai = (int)Constant.KHOA;
                                db.SaveChanges();
                            }
                        }
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tài khoản hoặc mật khẩu không đúng");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private HttpResponseMessage getResponseToken(string _userName, string _password, out string _token)
        {
            _token = Encode.Encrypt(_userName) + ":" + Encode.Encrypt(_password);
            byte[] bytes = new byte[_token.Length];
            for (int i = 0; i < _token.Length; i++)
            {
                bytes[i] = (byte)_token[i];
            }
            string tokenString = Convert.ToBase64String(bytes);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var cookie = new CookieHeaderValue("token", tokenString);
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return response;
        }
    }
}
