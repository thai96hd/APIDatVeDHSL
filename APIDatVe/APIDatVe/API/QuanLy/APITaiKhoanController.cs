using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using APIDatVe.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/taikhoan")]
    [BaseAuthenticationAttribute]
    public class APITaiKhoanController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APITaiKhoanController")]
        public IHttpActionResult Get(string _tukhoa = "", string _maquyen = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    List<TaiKhoan> taiKhoans = db.TaiKhoans
                            .Where(x => (string.IsNullOrEmpty(_tukhoa) || x.tentaikhoan.Contains(_tukhoa) || x.hoten.Contains(_tukhoa)))
                            .ToList();
                    if (!string.IsNullOrEmpty(_maquyen))
                        taiKhoans = db.TaiKhoans
                            .Where(x => x.maquyen == _maquyen)
                            .ToList();
                    int sobanghi = taiKhoans.Count;
                    return Ok(new
                    {
                        taiKhoans = taiKhoans
                            .Select(x => new
                            {
                                x.Quyen.tenquyen,
                                x.tentaikhoan,
                                x.hoten,
                                x.email,
                                x.trangthai
                            }).Skip((_trang - 1) * _sobanghi).Take(_sobanghi).ToList(),
                        sobanghi = sobanghi
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("detail")]
        [HttpGet]
        [AcceptAction(ActionName = "Detail", ControllerName = "APITaiKhoanController")]
        public IHttpActionResult Detail(string _tentaikhoan)
        {
            try
            {
                using (var db = new DB())
                {
                    TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == _tentaikhoan);
                    if (taiKhoan == null)
                        return BadRequest("Tài khoản không tồn tại");
                    return Ok(new
                    {
                        taiKhoan.tentaikhoan,
                        taiKhoan.email,
                        taiKhoan.hoten,
                        taiKhoan.maquyen,
                        taiKhoan.trangthai,
                        matkhau = "",
                        taiKhoan.gioitinh
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("post")]
        [HttpPost]
        [AcceptAction(ActionName = "Post", ControllerName = "APITaiKhoanController")]
        public IHttpActionResult Post(TaiKhoan _taikhoan)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == _taikhoan.tentaikhoan);
                        if (taiKhoan != null)
                            return BadRequest("Tài khoản đã tồn tại");
                        _taikhoan.linklaylaitaikhoan = "";
                        _taikhoan.solandangnhapsai = 0;
                        _taikhoan.matkhau = Encode.MD5(_taikhoan.matkhau);

                        db.TaiKhoans.Add(_taikhoan);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _taikhoan.tentaikhoan,
                            _taikhoan.email,
                            _taikhoan.hoten,
                            _taikhoan.maquyen,
                            _taikhoan.trangthai
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("put")]
        [HttpPut]
        [AcceptAction(ActionName = "Put", ControllerName = "APITaiKhoanController")]
        public IHttpActionResult Put(TaiKhoan _taikhoan)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == _taikhoan.tentaikhoan);
                        if (taiKhoan == null)
                            return BadRequest("Tài khoản không tồn tại");
                        taiKhoan.email = _taikhoan.email;
                        taiKhoan.hoten = _taikhoan.hoten;
                        taiKhoan.maquyen = _taikhoan.maquyen;
                        taiKhoan.gioitinh = _taikhoan.gioitinh;
                        taiKhoan.ngaysinh = _taikhoan.ngaysinh;
                        taiKhoan.diachi = _taikhoan.diachi;
                        taiKhoan.trangthai = _taikhoan.trangthai;
                        if (!string.IsNullOrEmpty(_taikhoan.matkhau))
                            taiKhoan.matkhau = Encode.MD5(_taikhoan.matkhau);

                        NhanVien nhanVien = db.NhanViens.FirstOrDefault(x => x.tentaikhoan == _taikhoan.tentaikhoan);
                        if (nhanVien != null)
                        {
                            nhanVien.ngaysinh = _taikhoan.ngaysinh;
                            nhanVien.diachi = _taikhoan.diachi;
                        }
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _taikhoan.tentaikhoan,
                            _taikhoan.email,
                            _taikhoan.hoten,
                            _taikhoan.maquyen,
                            _taikhoan.trangthai
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("change-status")]
        [HttpGet]
        [AcceptAction(ActionName = "ChangeStatus", ControllerName = "APITaiKhoanController")]
        public IHttpActionResult ChangeStatus(string _tentaikhoan)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == _tentaikhoan);
                        if (taiKhoan == null)
                            return BadRequest("Tài khoản không tồn tại");
                        if (taiKhoan.trangthai == (int)Constant.KHOA)
                            taiKhoan.trangthai = (int)Constant.HOATDONG;
                        else
                            taiKhoan.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_tentaikhoan);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("thongtintaikhoan")]
        [HttpGet]
        [AcceptAction(ActionName = "ThongTinTaiKhoan", ControllerName = "APITaiKhoanController")]
        public IHttpActionResult ThongTinTaiKhoan(string _tentaikhoan)
        {
            try
            {
                using (var db = new DB())
                {
                    TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == _tentaikhoan);
                    return Ok(new
                    {
                        taiKhoan.tentaikhoan,
                        taiKhoan.email,
                        taiKhoan.hoten,
                        taiKhoan.gioitinh,
                        taiKhoan.diachi,
                        ngaysinh = taiKhoan.ngaysinh == null ? "" : taiKhoan.ngaysinh.Value.ToString("yyyy-MM-dd"),
                        taiKhoan.avatar
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("capnhatthongtintaikhoan")]
        [HttpPut]
        [AcceptAction(ActionName = "CapNhatThongTinTaiKhoan", ControllerName = "APITaiKhoanController")]
        public IHttpActionResult CapNhatThongTinTaiKhoan(TaiKhoan _taikhoan)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == _taikhoan.tentaikhoan);
                        if (taiKhoan == null)
                            return BadRequest("Tài khoản không tồn tại");
                        taiKhoan.avatar = _taikhoan.avatar;
                        taiKhoan.email = _taikhoan.email;
                        taiKhoan.hoten = _taikhoan.hoten;
                        taiKhoan.gioitinh = _taikhoan.gioitinh;
                        taiKhoan.ngaysinh = _taikhoan.ngaysinh;
                        taiKhoan.diachi = _taikhoan.diachi;
                        if (!string.IsNullOrEmpty(_taikhoan.matkhau))
                            taiKhoan.matkhau = Encode.MD5(_taikhoan.matkhau);
                        NhanVien nhanVien = db.NhanViens.FirstOrDefault(x => x.tentaikhoan == _taikhoan.tentaikhoan);
                        if (nhanVien != null)
                        {
                            nhanVien.ngaysinh = _taikhoan.ngaysinh;
                            nhanVien.diachi = _taikhoan.diachi;
                        }
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("gui-thongtin-taikhoan")]
        [HttpGet]
        public IHttpActionResult GuiMailTaiKhoan(string _tentaikhoan)
        {
            try
            {
                using (var db = new DB())
                {
                    TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == _tentaikhoan);
                    string tokenReset = DataHelper.RandomString(24);
                    string tokenEncode = HttpUtility.HtmlEncode(Encode.Encrypt(taiKhoan.email + "|" + tokenReset));
                    string urlResetPasswork = "http://localhost:54328/Login/ResetPasswork?token=" + tokenEncode;
                    taiKhoan.linklaylaitaikhoan = tokenEncode;
                    taiKhoan.thoigianyeucaulaylaitk = DateTime.Now.AddDays(1);
                    db.SaveChanges();
                    MailHelper.SendMailGuest(taiKhoan.email, "Thông tin tài khoản", "Tài khoản của bạn : " + taiKhoan.tentaikhoan
                        + ". Vui lòng cập nhật lại mật khẩu theo đường dẫn sau: " + urlResetPasswork);
                    return Ok(_tentaikhoan);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
