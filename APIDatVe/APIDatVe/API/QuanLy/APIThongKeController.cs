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
    [RoutePrefix("api/thongke")]
    [BaseAuthenticationAttribute]
    public class APIThongKeController : ApiController
    {
        [Route("getdanhsachdatxe")]
        [HttpGet]
        public IHttpActionResult GetDanhSachDatXe(DateTime _tungay, DateTime _denngay, int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    _tungay = _tungay.Date;
                    _denngay = _denngay.Date;
                    List<VeXe> veXes = db.VeXes.Where(x => x.ngaydat.Value >= _tungay && x.ngaydat.Value <= _denngay).ToList();
                    int sobanghi = veXes.Count;
                    double tongtien = veXes.Skip((_trang - 1) * _sobanghi).Take(_sobanghi).ToList().Sum(x => double.Parse(x.tongtien));
                    return Ok(new
                    {
                        vexes = veXes.Select(x => new
                        {
                            x.vexeId,
                            x.khachhangId,
                            x.KhachHang.hoten,
                            x.ChuyenXe.machuyenxe,
                            x.TrangThaiVeXe.tentrangthai,
                            x.sokhach,
                            diemdon = db.DiemTrungChuyens.FirstOrDefault(y => y.madiemtrungchuyen == x.madiemtrungchuyendon).tendiemtrungchuyen,
                            diemtra = db.DiemTrungChuyens.FirstOrDefault(y => y.madiemtrungchuyen == x.madiemtrungchuyentra).tendiemtrungchuyen,
                            ngaydat = x.ngaydat.Value.ToString("dd/MM/yyyy HH:mm"),
                            x.ChiTietVeXes.FirstOrDefault().sodienthoaikhach,
                            x.tongtien
                        }).Skip((_trang - 1) * _sobanghi).Take(_sobanghi).ToList(),
                        sobanghi = sobanghi,
                        tongtien = tongtien
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
