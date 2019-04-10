using APIDatVe.Database;
using APIDatVe.Helper;
using Newtonsoft.Json;
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
                        if(taiKhoan.trangthai == (int)Constant.KHOA)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tài khoản đã bị khóa");
                        }
                        taiKhoan.solandangnhapsai = 0;
                        db.SaveChanges();
                        string danhsachmanhinh = JsonConvert.SerializeObject(db.QuyenManHinhQuanLies
                            .Where(x => x.maquyen == taiKhoan.maquyen && x.chon)
                            .Select(x => x.ManHinhQuanLy.tenmanhinh)
                            .ToList());
                        return Request.CreateResponse(HttpStatusCode.OK, new
                        {
                            token = Encode.Encrypt(account._userName + ":" + account._password),
                            danhsachmanhinh = Encode.Encrypt(danhsachmanhinh)
                        });
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

    }
}
