using APIDatVe.API.Quyen;
using APIDatVe.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/tinhthanh")]
    [BaseAuthenticationAttribute]
    public class APILoTrinhController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APITinhThanhController")]
        public IHttpActionResult Get(string _tukhoa = "", string _matinh = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var loTrinhs = db.LoTrinhs.Where(x => (x.tenlotrinh.Contains(_tukhoa))).ToList();
                    if (_matinh != "")
                        loTrinhs = loTrinhs.Where(x => x.matinhdon == _matinh).ToList();
                    int sobanghi = loTrinhs.Count;
                    return Ok(new
                    {
                        tinhThanhs = loTrinhs
                                    .Select(x => new
                                    {
                                        x.matinhdon,
                                        x.matinhtra,
                                        x.tenlotrinh,
                                        x.khoangthoigiandukien,
                                        x.malotrinh
                                    })
                                    .Skip(_trang).Take(_sobanghi),
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APITinhThanhController")]
        public IHttpActionResult Detail(string _malotrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    if (db.LoTrinhs.Any(x => x.malotrinh == _malotrinh))
                        return BadRequest("Lộ trình không tồn tại");
                    LoTrinh loTrinh = db.LoTrinhs.FirstOrDefault(x => x.malotrinh == _malotrinh);
                    return Ok(new
                    {
                        loTrinh.malotrinh,
                        loTrinh.tenlotrinh,
                        loTrinh.matinhdon,
                        loTrinh.matinhtra,
                        loTrinh.khoangthoigiandukien,
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
        [AcceptAction(ActionName = "Post", ControllerName = "APITinhThanhController")]
        public IHttpActionResult Post(LoTrinh _loTrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.LoTrinhs.Any(x => x.malotrinh == _loTrinh.malotrinh))
                            return BadRequest("Mã lộ trình đã tồn tại");
                        db.LoTrinhs.Add(_loTrinh);
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

        [Route("put")]
        [HttpPut]
        [AcceptAction(ActionName = "Put", ControllerName = "APITinhThanhController")]
        public IHttpActionResult Put(LoTrinh _loTrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (!db.LoTrinhs.Any(x => x.malotrinh == _loTrinh.malotrinh))
                            return BadRequest("Mã lộ trình không tồn tại");
                        LoTrinh oldloTrinh = db.LoTrinhs.FirstOrDefault(x => x.malotrinh == _loTrinh.malotrinh);
                        oldloTrinh.tenlotrinh = _loTrinh.tenlotrinh;
                        oldloTrinh.matinhdon = _loTrinh.matinhdon;
                        oldloTrinh.matinhtra = _loTrinh.matinhtra;
                        oldloTrinh.khoangthoigiandukien = _loTrinh.khoangthoigiandukien;
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

        [Route("delete")]
        [HttpDelete]
        [AcceptAction(ActionName = "Delete", ControllerName = "APITinhThanhController")]
        public IHttpActionResult Delete(string _malotrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.LoTrinhs.Any(x => x.malotrinh == _malotrinh))
                            return BadRequest("Lộ trình không tồn tại");
                        LoTrinh loTrinh = db.LoTrinhs.FirstOrDefault(x => x.malotrinh == _malotrinh);
                        db.ChuyenXes.RemoveRange(db.ChuyenXes.Where(x => x.malotrinh == _malotrinh));
                        db.LoTrinhs.Remove(loTrinh);
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
    }
}
