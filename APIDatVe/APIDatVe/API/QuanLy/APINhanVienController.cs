using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using APIDatVe.Helper;
using APIDatVe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/nhanvien")]
    [BaseAuthenticationAttribute]
    public class APINhanVienController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APINhanVienController")]
        public IHttpActionResult Get(string _tukhoa = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    List<NhanVien> nhanViens = db.NhanViens
                            .Where(x => string.IsNullOrEmpty(_tukhoa)
                                            || x.manhanvien.Contains(_tukhoa)
                                            || x.tentaikhoan.Contains(_tukhoa)
                                            || x.hoten.Contains(_tukhoa)
                                            || x.email.Contains(_tukhoa))
                            .ToList();
                    int sobanghi = nhanViens.Count;
                    return Ok(new
                    {
                        nhanViens = nhanViens.Select(x => new
                        {
                            x.tentaikhoan,
                            x.manhanvien,
                            x.hoten,
                            x.sodienthoai,
                            x.ChucVu.tenchucvu,
                            x.email,
                            x.TaiKhoan.trangthai
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APINhanVienController")]
        public IHttpActionResult Detail(string _manhanvien)
        {
            try
            {
                using (var db = new DB())
                {
                    NhanVien nhanVien = db.NhanViens.FirstOrDefault(x => x.manhanvien == _manhanvien);
                    if (nhanVien == null)
                        return BadRequest("Nhân viên không tồn tại");
                    ENhanVien eNhanVien = new ENhanVien()
                    {
                        diachi = nhanVien.diachi,
                        tentaikhoan = nhanVien.tentaikhoan,
                        sodienthoai = nhanVien.sodienthoai,
                        socmt = nhanVien.socmt,
                        noicap = nhanVien.noicap,
                        email = nhanVien.email,
                        hoten = nhanVien.hoten,
                        machucvu = nhanVien.machucvu,
                        manhanvien = nhanVien.manhanvien,
                        ngaycap = nhanVien.ngaycap,
                        ngaysinh = nhanVien.ngaysinh,
                        maquyen = nhanVien.TaiKhoan.maquyen,
                        trangthai = nhanVien.TaiKhoan.trangthai.Value,
                        gioitinh = nhanVien.TaiKhoan.gioitinh
                    };
                    ChiTietLaiXe chiTietLaiXe = db.ChiTietLaiXes.FirstOrDefault(x => x.manhanvien == _manhanvien);
                    if (chiTietLaiXe != null)
                    {
                        eNhanVien.giaypheplaixe = chiTietLaiXe.giaypheplaixe;
                        eNhanVien.hanglai = chiTietLaiXe.hanglai;
                        eNhanVien.ngaycaplaixe = chiTietLaiXe.ngaycap;
                        eNhanVien.noicaplaixe = chiTietLaiXe.noicap;
                        eNhanVien.ghichu = chiTietLaiXe.ghichu;
                    }
                    return Ok(eNhanVien);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("put")]
        [HttpPut]
        [AcceptAction(ActionName = "Put", ControllerName = "APINhanVienController")]
        public IHttpActionResult Put(ENhanVien eNhanVien)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        NhanVien oldNhanVien = db.NhanViens.FirstOrDefault(x => x.manhanvien == eNhanVien.manhanvien && x.tentaikhoan == eNhanVien.tentaikhoan);
                        if (oldNhanVien == null)
                        {
                            if (db.NhanViens.FirstOrDefault(x => x.manhanvien == eNhanVien.manhanvien) != null)
                                return BadRequest("Mã nhân viên đã tồn tại");
                            if (db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == eNhanVien.tentaikhoan) != null)
                                return BadRequest("Tên tài khoản đã tồn tại");

                            NhanVien nhanVien = new NhanVien()
                            {
                                tentaikhoan = eNhanVien.tentaikhoan,
                                diachi = eNhanVien.diachi,
                                email = eNhanVien.email,
                                hoten = eNhanVien.hoten,
                                machucvu = eNhanVien.machucvu,
                                manhanvien = eNhanVien.manhanvien,
                                ngaycap = eNhanVien.ngaycap,
                                noicap = eNhanVien.noicap,
                                ngaysinh = eNhanVien.ngaysinh,
                                socmt = eNhanVien.socmt,
                                sodienthoai = eNhanVien.sodienthoai,
                                TaiKhoan = new TaiKhoan()
                                {
                                    hoten = eNhanVien.hoten,
                                    email = eNhanVien.email,
                                    maquyen = eNhanVien.maquyen,
                                    matkhau = Encode.MD5(eNhanVien.matkhau),
                                    solandangnhapsai = 0,
                                    tentaikhoan = eNhanVien.tentaikhoan,
                                    trangthai = eNhanVien.trangthai,
                                    gioitinh = eNhanVien.gioitinh,
                                    avatar = "",
                                    diachi = eNhanVien.diachi,
                                    ngaysinh = eNhanVien.ngaysinh
                                }
                            };
                            db.NhanViens.Add(nhanVien);
                        }
                        else
                        {
                            oldNhanVien.tentaikhoan = eNhanVien.tentaikhoan;
                            oldNhanVien.diachi = eNhanVien.diachi;
                            oldNhanVien.email = eNhanVien.email;
                            oldNhanVien.hoten = eNhanVien.hoten;
                            oldNhanVien.machucvu = eNhanVien.machucvu;
                            oldNhanVien.manhanvien = eNhanVien.manhanvien;
                            oldNhanVien.ngaycap = eNhanVien.ngaycap;
                            oldNhanVien.noicap = eNhanVien.noicap;
                            oldNhanVien.ngaysinh = eNhanVien.ngaysinh;
                            oldNhanVien.socmt = eNhanVien.socmt;
                            oldNhanVien.sodienthoai = eNhanVien.sodienthoai;
                            oldNhanVien.TaiKhoan.hoten = eNhanVien.hoten;
                            oldNhanVien.TaiKhoan.email = eNhanVien.email;
                            oldNhanVien.TaiKhoan.maquyen = eNhanVien.maquyen;
                            oldNhanVien.TaiKhoan.tentaikhoan = eNhanVien.tentaikhoan;
                            oldNhanVien.TaiKhoan.trangthai = eNhanVien.trangthai;
                            oldNhanVien.TaiKhoan.gioitinh = eNhanVien.gioitinh;
                            oldNhanVien.TaiKhoan.ngaysinh = eNhanVien.ngaysinh;
                            oldNhanVien.TaiKhoan.diachi = eNhanVien.diachi;
                            if (!string.IsNullOrEmpty(eNhanVien.matkhau))
                                oldNhanVien.TaiKhoan.matkhau = Encode.MD5(eNhanVien.matkhau);
                        }

                        ChiTietLaiXe chiTietLaiXe = db.ChiTietLaiXes.FirstOrDefault(x => x.manhanvien == eNhanVien.manhanvien);
                        if (chiTietLaiXe == null)
                        {
                            db.ChiTietLaiXes.Add(new ChiTietLaiXe()
                            {
                                ghichu = eNhanVien.ghichu,
                                giaypheplaixe = eNhanVien.giaypheplaixe,
                                hanglai = eNhanVien.hanglai,
                                manhanvien = eNhanVien.manhanvien,
                                ngaycap = eNhanVien.ngaycaplaixe,
                                noicap = eNhanVien.noicaplaixe
                            });
                        }
                        else
                        {
                            chiTietLaiXe.ghichu = eNhanVien.ghichu;
                            chiTietLaiXe.giaypheplaixe = eNhanVien.giaypheplaixe;
                            chiTietLaiXe.hanglai = eNhanVien.hanglai;
                            chiTietLaiXe.ngaycap = eNhanVien.ngaycaplaixe;
                            chiTietLaiXe.noicap = eNhanVien.noicaplaixe;
                        }
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(eNhanVien);
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
        [AcceptAction(ActionName = "ChangeStatus", ControllerName = "APINhanVienController")]
        public IHttpActionResult ChangeStatus(string _manhanvien)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        NhanVien nhanVien = db.NhanViens.FirstOrDefault(x => x.manhanvien == _manhanvien);
                        if (nhanVien == null)
                            return BadRequest("Nhân viên không tồn tại");
                        if (nhanVien.TaiKhoan.trangthai == (int)Constant.KHOA)
                            nhanVien.TaiKhoan.trangthai = (int)Constant.HOATDONG;
                        else
                            nhanVien.TaiKhoan.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_manhanvien);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ready")]
        [HttpGet]
        public IHttpActionResult Ready()
        {
            try
            {
                using (var db = new DB())
                {
                    List<NhanVien> nhanViens = db.NhanViens
                            .Where(x => x.TaiKhoan.trangthai.Value == (int)Constant.HOATDONG)
                            .ToList();
                    int sobanghi = nhanViens.Count;
                    return Ok(nhanViens.Select(x => new
                    {
                        x.manhanvien,
                        x.hoten,
                    }).ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
