using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using APIDatVe.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/quyen")]
    [BaseAuthenticationAttribute]
    public class APIQuyenController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIQuyenController")]
        public IHttpActionResult Get(string _tukhoa = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    List<Quyen> quyens = db.Quyens
                            .Where(x => (string.IsNullOrEmpty(_tukhoa) || x.maquyen.Contains(_tukhoa) || x.tenquyen.Contains(_tukhoa)))
                            .ToList();
                    int sobanghi = quyens.Count;
                    return Ok(new
                    {
                        quyens = quyens
                            .Select(x => new
                            {
                                x.maquyen,
                                x.tenquyen
                            }).Skip((_trang - 1) * _sobanghi).Take(_sobanghi),
                        sobanghi = sobanghi
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getall")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                using (var db = new DB())
                {
                    List<Quyen> quyens = db.Quyens.ToList();
                    return Ok(quyens.Select(x => new
                    {
                        x.maquyen,
                        x.tenquyen
                    }));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("detail")]
        [HttpGet]
        [AcceptAction(ActionName = "Detail", ControllerName = "APIQuyenController")]
        public IHttpActionResult Detail(string _maquyen)
        {
            try
            {
                using (var db = new DB())
                {
                    Quyen quyen = db.Quyens.FirstOrDefault(x => x.maquyen == _maquyen);
                    if (quyen == null)
                        return BadRequest("Quyền không tồn tại");
                    return Ok(new
                    {
                        quyen.maquyen,
                        quyen.tenquyen
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIQuyenController")]
        public IHttpActionResult Post(Quyen _quyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Quyen quyen = db.Quyens.FirstOrDefault(x => x.maquyen == _quyen.maquyen);
                        if (quyen != null)
                            return BadRequest("Mã quyền đã tồn tại");
                        db.Quyens.Add(_quyen);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _quyen.maquyen,
                            _quyen.tenquyen
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIQuyenController")]
        public IHttpActionResult Put(Quyen _quyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Quyen quyen = db.Quyens.FirstOrDefault(x => x.maquyen == _quyen.maquyen);
                        if (quyen == null)
                            return BadRequest("Quyền không tồn tại");
                        quyen.tenquyen = _quyen.tenquyen;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _quyen.maquyen,
                            _quyen.tenquyen
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIQuyenController")]
        public IHttpActionResult Delete(string _maquyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Quyen quyen = db.Quyens.FirstOrDefault(x => x.maquyen == _maquyen);
                        if (quyen == null)
                            return BadRequest("Quyền không tồn tại");
                        List<TaiKhoan> taiKhoans = db.TaiKhoans.ToList();
                        taiKhoans.ForEach(x =>
                        {
                            x.maquyen = "Default";
                        });
                        db.Quyens.Remove(quyen);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_maquyen);
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
                    var quyens = db.Quyens.ToList();
                    return Ok(quyens.Select(x => new
                    {
                        x.maquyen,
                        x.tenquyen
                    }));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
