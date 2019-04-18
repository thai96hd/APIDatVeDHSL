using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using APIDatVe.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/chuyenxe")]
    [BaseAuthenticationAttribute]
    public class APIChuyenXeController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIChuyenXeController")]
        public IHttpActionResult Get(DateTime _tungay, DateTime _denngay, int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    _tungay = _tungay.AddDays(-1);
                    List<ChuyenXe> chuyenXes = db.ChuyenXes.Where(x => x.ngayhoatdong.Value >= _tungay && x.ngayhoatdong.Value <= _denngay).ToList();
                    int sobanghi = chuyenXes.Count;
                    return Ok(new
                    {
                        chuyenXes = JsonConvert.SerializeObject(
                            chuyenXes
                            .Select(x => new
                            {
                                x.machuyenxe,
                                x.LoTrinh.tenlotrinh,
                                x.Kip.tenkip,
                                x.Xe.maxe,
                                x.Xe.biensoxe,
                                taixe = x.NhanVien.hoten,
                                phuxe = x.NhanVien1.hoten,
                                x.ngayhoatdong
                            }).Skip((_trang - 1) * _sobanghi).Take(_sobanghi).ToList()),
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIChuyenXeController")]
        public IHttpActionResult Detail(string _machuyenxe)
        {
            try
            {
                using (var db = new DB())
                {
                    ChuyenXe chuyenXe = db.ChuyenXes.FirstOrDefault(x => x.machuyenxe == _machuyenxe);
                    if (chuyenXe == null)
                        return BadRequest("Chuyến xe không tồn tại");
                    return Ok(new
                    {
                        chuyenXe.machuyenxe,
                        chuyenXe.malotrinh,
                        chuyenXe.makip,
                        chuyenXe.maxe,
                        chuyenXe.mataixe,
                        chuyenXe.maphuxe,
                        chuyenXe.ngayhoatdong,
                        chuyenXe.thoigiandungxe
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIChuyenXeController")]
        public IHttpActionResult Post(ChuyenXe _chuyenXe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        ChuyenXe chuyenXe = db.ChuyenXes.FirstOrDefault(x => x.machuyenxe == _chuyenXe.machuyenxe);
                        if (chuyenXe != null)
                            return BadRequest("Mã chuyến xe đã tồn tại");
                        db.ChuyenXes.Add(_chuyenXe);
                        // khởi tạo danh sách ghế đặt có trạng thái mặc định trống toàn bộ trong chuyến
                        List<Ghe> ghes = db.Ghes.Where(x => x.maxe == _chuyenXe.maxe).ToList();
                        if (ghes == null)
                            ghes = new List<Ghe>();
                        ghes.ForEach(x =>
                        {
                            if (db.TrangThaiGhes.FirstOrDefault(y => y.maghe == x.maghe && x.ngaycapnhat == _chuyenXe.ngayhoatdong.Value) == null)
                            {
                                db.TrangThaiGhes.Add(new Database.TrangThaiGhe()
                                {
                                    maghe = x.maghe,
                                    ngay = _chuyenXe.ngayhoatdong,
                                    trangthai = Helper.TrangThaiGhe.GHETRONG
                                });
                            }
                        });
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _chuyenXe.machuyenxe
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIChuyenXeController")]
        public IHttpActionResult Put(ChuyenXe _chuyenXe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        ChuyenXe chuyenXe = db.ChuyenXes.FirstOrDefault(x => x.machuyenxe == _chuyenXe.machuyenxe);
                        if (chuyenXe == null)
                            return BadRequest("Mã chuyến xe không tồn tại");
                        chuyenXe.malotrinh = _chuyenXe.malotrinh;
                        chuyenXe.makip = _chuyenXe.makip;
                        chuyenXe.maxe = _chuyenXe.maxe;
                        chuyenXe.mataixe = _chuyenXe.mataixe;
                        chuyenXe.maphuxe = _chuyenXe.maphuxe;
                        chuyenXe.ngayhoatdong = _chuyenXe.ngayhoatdong;

                        // khởi tạo danh sách ghế đặt có trạng thái mặc định trống toàn bộ trong chuyến
                        List<Ghe> ghes = db.Ghes.Where(x => x.maxe == _chuyenXe.maxe).ToList();
                        if (ghes == null)
                            ghes = new List<Ghe>();
                        ghes.ForEach(x =>
                        {
                            if (db.TrangThaiGhes.FirstOrDefault(y => y.maghe == x.maghe && x.ngaycapnhat == _chuyenXe.ngayhoatdong.Value) == null)
                            {
                                db.TrangThaiGhes.Add(new Database.TrangThaiGhe()
                                {
                                    maghe = x.maghe,
                                    ngay = _chuyenXe.ngayhoatdong,
                                    trangthai = Helper.TrangThaiGhe.GHETRONG
                                });
                            }
                        });
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _chuyenXe.machuyenxe
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("delete")]
        [HttpDelete]
        [AcceptAction(ActionName = "Delete", ControllerName = "APIChuyenXeController")]
        public IHttpActionResult Delete(string _machuyenxe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        ChuyenXe chuyenXe = db.ChuyenXes.FirstOrDefault(x => x.machuyenxe == _machuyenxe);
                        if (chuyenXe == null)
                            return BadRequest("Chuyến xe không tồn tại");
                        db.ChuyenXes.Remove(chuyenXe);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_machuyenxe);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Chuyến xe đã được sử dụng nên không thể xóa. " + ex.Message);
            }
        }
    }
}
