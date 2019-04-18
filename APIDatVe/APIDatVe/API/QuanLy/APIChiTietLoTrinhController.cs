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
    [RoutePrefix("api/chitietlotrinh")]
    [BaseAuthenticationAttribute]
    public class APIChiTietLoTrinhController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIChiTietLoTrinhController")]
        public IHttpActionResult Get(string _malotrinh = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    List<ChiTietLoTrinh> chiTietLoTrinhs = db.ChiTietLoTrinhs
                            .Where(x => string.IsNullOrEmpty(_malotrinh) || x.malotrinh == _malotrinh)
                            .ToList();
                    int sobanghi = chiTietLoTrinhs.Count;
                    return Ok(new
                    {
                        chiTietLoTrinhs = chiTietLoTrinhs
                                    .Select(x => new
                                    {
                                        x.machitietlotrinh,
                                        x.LoTrinh.tenlotrinh,
                                        x.TinhThanh.tentinh
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIChiTietLoTrinhController")]
        public IHttpActionResult Detail(int _machitietlotrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    ChiTietLoTrinh chiTietLoTrinh = db.ChiTietLoTrinhs.FirstOrDefault(x => x.machitietlotrinh == _machitietlotrinh);
                    if (chiTietLoTrinh == null)
                        return BadRequest("Chi tiết lộ trình không tồn tại");
                    return Ok(new
                    {
                        chiTietLoTrinh.machitietlotrinh,
                        chiTietLoTrinh.idtinhthanh,
                        chiTietLoTrinh.malotrinh
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIChiTietLoTrinhController")]
        public IHttpActionResult Post(ChiTietLoTrinh _chiTietLoTrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (_chiTietLoTrinh.machitietlotrinh > 0)
                        {
                            db.ChiTietLoTrinhs.Remove(db.ChiTietLoTrinhs.FirstOrDefault(x => x.machitietlotrinh == _chiTietLoTrinh.machitietlotrinh));
                            db.SaveChanges();
                            transaction.Commit();
                        }
                        ChiTietLoTrinh chiTietLoTrinh = db.ChiTietLoTrinhs.FirstOrDefault(x => x.malotrinh == _chiTietLoTrinh.malotrinh && x.idtinhthanh == _chiTietLoTrinh.idtinhthanh);
                        if (chiTietLoTrinh == null)
                        {
                            db.ChiTietLoTrinhs.Add(new ChiTietLoTrinh
                            {
                                malotrinh = _chiTietLoTrinh.malotrinh,
                                idtinhthanh = _chiTietLoTrinh.idtinhthanh
                            });
                            db.SaveChanges();
                            transaction.Commit();
                        }
                        return Ok(new
                        {
                            _chiTietLoTrinh.idtinhthanh,
                            _chiTietLoTrinh.malotrinh
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIChiTietLoTrinhController")]
        public IHttpActionResult Delete(int _machitietlotrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        ChiTietLoTrinh chiTietLoTrinh = db.ChiTietLoTrinhs.FirstOrDefault(x => x.machitietlotrinh == _machitietlotrinh);
                        if (chiTietLoTrinh == null)
                            return BadRequest("Chi tiết lộ trình không tồn tại");
                        db.ChiTietLoTrinhs.Remove(chiTietLoTrinh);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_machitietlotrinh);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
