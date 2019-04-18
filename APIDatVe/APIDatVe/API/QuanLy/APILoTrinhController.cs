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
    [RoutePrefix("api/lotrinh")]
    [BaseAuthenticationAttribute]
    public class APILoTrinhController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APILoTrinhController")]
        public IHttpActionResult Get(string _tukhoa = "", string _matinh = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var loTrinhs = db.LoTrinhs.Where(x => (string.IsNullOrEmpty(_tukhoa) || x.tenlotrinh.Contains(_tukhoa))).ToList();
                    if (!string.IsNullOrEmpty(_matinh))
                        loTrinhs = loTrinhs.Where(x => x.matinhdon == _matinh).ToList();
                    int sobanghi = loTrinhs.Count;
                    return Ok(new
                    {
                        loTrinhs = loTrinhs
                                    .Select(x => new
                                    {
                                        x.matinhdon,
                                        x.matinhtra,
                                        x.tenlotrinh,
                                        x.khoangthoigiandukien,
                                        x.malotrinh,
                                        tentinhdon = db.TinhThanhs.FirstOrDefault(y => y.matinh == x.matinhdon).tentinh,
                                        tentinhtra = db.TinhThanhs.FirstOrDefault(y => y.matinh == x.matinhtra).tentinh
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APILoTrinhController")]
        public IHttpActionResult Detail(string _malotrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    LoTrinh loTrinh = db.LoTrinhs.FirstOrDefault(x => x.malotrinh == _malotrinh);
                    if (loTrinh == null)
                        return BadRequest("Lộ trình không tồn tại");
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
        [AcceptAction(ActionName = "Post", ControllerName = "APILoTrinhController")]
        public IHttpActionResult Post(LoTrinh _loTrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        LoTrinh loTrinh = db.LoTrinhs.FirstOrDefault(x => x.malotrinh == _loTrinh.malotrinh);
                        if (loTrinh != null)
                            return BadRequest("Mã lộ trình đã tồn tại");
                        db.LoTrinhs.Add(_loTrinh);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _loTrinh.malotrinh,
                            _loTrinh.tenlotrinh,
                            _loTrinh.matinhdon,
                            _loTrinh.matinhtra,
                            _loTrinh.khoangthoigiandukien,
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
        [AcceptAction(ActionName = "Put", ControllerName = "APILoTrinhController")]
        public IHttpActionResult Put(LoTrinh _loTrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        LoTrinh oldloTrinh = db.LoTrinhs.FirstOrDefault(x => x.malotrinh == _loTrinh.malotrinh);
                        if (oldloTrinh == null)
                            return BadRequest("Mã lộ trình không tồn tại");
                        oldloTrinh.tenlotrinh = _loTrinh.tenlotrinh;
                        oldloTrinh.matinhdon = _loTrinh.matinhdon;
                        oldloTrinh.matinhtra = _loTrinh.matinhtra;
                        oldloTrinh.khoangthoigiandukien = _loTrinh.khoangthoigiandukien;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _loTrinh.malotrinh,
                            _loTrinh.tenlotrinh,
                            _loTrinh.matinhdon,
                            _loTrinh.matinhtra,
                            _loTrinh.khoangthoigiandukien,
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APILoTrinhController")]
        public IHttpActionResult Delete(string _malotrinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        LoTrinh loTrinh = db.LoTrinhs.FirstOrDefault(x => x.malotrinh == _malotrinh);
                        if (loTrinh == null)
                            return BadRequest("Lộ trình không tồn tại");
                        db.ChuyenXes.RemoveRange(db.ChuyenXes.Where(x => x.malotrinh == _malotrinh));
                        db.LoTrinhs.Remove(loTrinh);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_malotrinh);
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
                    List<LoTrinh> loTrinhs = db.LoTrinhs.ToList();
                    return Ok(loTrinhs.Select(x => new
                    {
                        x.malotrinh,
                        x.tenlotrinh
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
