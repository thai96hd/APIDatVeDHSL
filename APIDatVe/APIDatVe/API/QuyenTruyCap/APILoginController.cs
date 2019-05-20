using APIDatVe.Database;
using APIDatVe.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
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
            public string _token { get; set; }
            public string emailresetpasswork { get; set; }
        }
        [Route("check-account")]
        [HttpPost]
        public HttpResponseMessage CheckAccount([FromBody]AccountLogin account)
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
                        if (taiKhoan.trangthai == (int)Constant.KHOA)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tài khoản đã bị khóa");
                        }
                        taiKhoan.solandangnhapsai = 0;
                        db.SaveChanges();
                        string danhsachmanhinh = JsonConvert.SerializeObject(db.QuyenManHinhQuanLies
                            .Where(x => x.maquyen == taiKhoan.maquyen && x.chon)
                            .Select(x => x.ManHinhQuanLy.tenmanhinh)
                            .ToList());
                        TaiKhoanCauHinh taiKhoanCauHinh = taiKhoan.TaiKhoanCauHinhs.FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, new
                        {
                            token = Encode.Encrypt(account._userName + ":" + account._password),
                            danhsachmanhinh = Encode.Encrypt(danhsachmanhinh),
                            avatar = taiKhoan.avatar ?? " ",
                            hoten = taiKhoan.hoten ?? "N/A",
                            setingstyle = taiKhoanCauHinh == null ? "0" : taiKhoanCauHinh.cauhinh
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
                                
                            }
							db.SaveChanges();
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

        [Route("reset-account")]
        [HttpPost]
        public HttpResponseMessage ResetAccount([FromBody]AccountLogin account)
        {
            try
            {
                using (var db = new DB())
                {
                    TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == account.emailresetpasswork);
                    if (taiKhoan == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Gửi email thất bại! Thông tin email/tên tài khoản không tồn tại");
                    }
                    string tokenEncode = Encode.MD5(account.emailresetpasswork);
                    string urlResetPasswork = "http://localhost:54328/Login/ResetPasswork?email=" + account.emailresetpasswork + "&token=" + tokenEncode;
                    taiKhoan.linklaylaitaikhoan = tokenEncode;
                    taiKhoan.thoigianyeucaulaylaitk = DateTime.Now.AddDays(1);
                    db.SaveChanges();
                    MailHelper.SendMailGuest(account.emailresetpasswork, "Lấy lại mật khẩu", "Đường dẫn lấy lại mật khẩu: " + urlResetPasswork);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Gửi email thất bại! Vui lòng nhập lại thông tin email");
            }
        }

        [Route("check-token")]
        [HttpPost]
        public HttpResponseMessage CheckToken([FromBody]AccountLogin account)
        {
            try
            {
                using (var db = new DB())
                {
                    TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == account.emailresetpasswork);
                    if (taiKhoan == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token không hợp lệ");
                    if (taiKhoan.thoigianyeucaulaylaitk < DateTime.Now)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Đường link hết hiệu lực");
                    if (taiKhoan.linklaylaitaikhoan != account._token)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token không còn hiệu lực");
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        _userName = taiKhoan.tentaikhoan,
                        _token = account._token
                    });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Không thể lấy lại mật khẩu: " + ex.Message);
            }
        }

        [Route("reset")]
        [HttpPost]
        public HttpResponseMessage Reset([FromBody]AccountLogin account)
        {
            try
            {
                using (var db = new DB())
                {
                    TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == account._userName);
                    if (taiKhoan.thoigianyeucaulaylaitk < DateTime.Now) // xóa nhầm
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Đường link hết hiệu lực");
                    if (taiKhoan.linklaylaitaikhoan != account._token)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token không còn hiệu lực");
                    if (taiKhoan.matkhau == Encode.MD5(account._password))
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mật khẩu không thể trùng với mật khẩu cũ");
                    taiKhoan.matkhau = Encode.MD5(account._password);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cập nhật thất bại: " + ex.Message);
            }
        }


    }
}
