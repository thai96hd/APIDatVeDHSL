using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/khachhang")]
    [BaseAuthenticationAttribute]
    public class APIKhachHangController : ApiController
    {
        [Route()]
        [HttpGet]
        //[AcceptAction(ActionName = "Get", ControllerName = "APIKhachHangController")]
        public IHttpActionResult Get(string _tukhoa = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    List<KhachHang> khachHangs = db.KhachHangs
                            .Where(x => string.IsNullOrEmpty(_tukhoa)
                                            || x.hoten.Contains(_tukhoa)
                                            || x.sodienthoai.Contains(_tukhoa)
                                            || x.email.Contains(_tukhoa))
                            .ToList();
                    int sobanghi = khachHangs.Count;
                    return Ok(new
                    {
                        khachHangs = khachHangs.Select(x => new
                        {
                            x.khachhangId,
                            x.hoten,
                            x.sodienthoai,
                            x.email,
                            tendoituong = x.DoiTuong == null ? "Thành viên" : x.DoiTuong.tendoituong,
                            x.diemtichluy
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIKhachHangController")]
        public IHttpActionResult Detail(string _manhanvien)
        {
            try
            {
                using (var db = new DB())
                {
                    //NhanVien nhanVien = db.NhanViens.FirstOrDefault(x => x.manhanvien == _manhanvien);
                    //if (nhanVien == null)
                    //    return BadRequest("Nhân viên không tồn tại");
                    //ENhanVien eNhanVien = new ENhanVien()
                    //{
                    //    diachi = nhanVien.diachi,
                    //    tentaikhoan = nhanVien.tentaikhoan,
                    //    sodienthoai = nhanVien.sodienthoai,
                    //    socmt = nhanVien.socmt,
                    //    noicap = nhanVien.noicap,
                    //    email = nhanVien.email,
                    //    hoten = nhanVien.hoten,
                    //    machucvu = nhanVien.machucvu,
                    //    manhanvien = nhanVien.manhanvien,
                    //    ngaycap = nhanVien.ngaycap,
                    //    ngaysinh = nhanVien.ngaysinh,
                    //    maquyen = nhanVien.TaiKhoan.maquyen,
                    //    trangthai = nhanVien.TaiKhoan.trangthai.Value,
                    //    gioitinh = nhanVien.TaiKhoan.gioitinh
                    //};
                    //ChiTietLaiXe chiTietLaiXe = db.ChiTietLaiXes.FirstOrDefault(x => x.manhanvien == _manhanvien);
                    //if (chiTietLaiXe != null)
                    //{
                    //    eNhanVien.giaypheplaixe = chiTietLaiXe.giaypheplaixe;
                    //    eNhanVien.hanglai = chiTietLaiXe.hanglai;
                    //    eNhanVien.ngaycaplaixe = chiTietLaiXe.ngaycap;
                    //    eNhanVien.noicaplaixe = chiTietLaiXe.noicap;
                    //    eNhanVien.ghichu = chiTietLaiXe.ghichu;
                    //}
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
