using APIDatVe.API.Quyen;
using APIDatVe.Database;
using APIDatVe.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/xe")]
    [BaseAuthenticationAttribute]
    public class APIXeController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIXeController")]
        public IHttpActionResult Get(string _tukhoa, int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var xes = db.Xes
                                .Where(x => (x.maxe.Contains(_tukhoa) || x.biensoxe.Contains(_tukhoa)) && x.trangthai != (int)Constant.KHOA)
                                .ToList();
                    int sobanghi = xes.Count;
                    return Ok(new
                    {
                        xes = xes.Select(x => new
                                {
                                    x.biensoxe,
                                    x.ghichu,
                                    x.maxe,
                                    x.soghe,
                                    x.trangthai
                                }).Skip(_trang).Take(_sobanghi),
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIXeController")]
        public IHttpActionResult Detail(string _maxe)
        {
            try
            {
                using (var db = new DB())
                {
                    if (db.Xes.Any(x => x.maxe == _maxe))
                        return BadRequest("Xe không tồn tại");
                    Xe xe = db.Xes.FirstOrDefault(x => x.maxe == _maxe);
                    return Ok(new
                    {
                        xe.ghichu,
                        xe.maxe,
                        xe.soghe,
                        xe.trangthai,
                        xe.biensoxe
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIXeController")]
        public IHttpActionResult Post(Xe xe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.Xes.Any(x => x.maxe == xe.maxe))
                            return BadRequest("Mã xe đã tồn tại");
                        db.Xes.Add(xe);
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIXeController")]
        public IHttpActionResult Put(Xe xe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (!db.Xes.Any(x => x.maxe == xe.maxe))
                            return BadRequest("Mã xe không tồn tại");
                        Xe oldxe = db.Xes.FirstOrDefault(x => x.maxe == xe.maxe);
                        oldxe.soghe = xe.soghe;
                        oldxe.ghichu = xe.ghichu;
                        oldxe.biensoxe = xe.biensoxe;
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIXeController")]
        public IHttpActionResult Delete(string _maxe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.Xes.Any(x => x.maxe == _maxe))
                            return BadRequest("Xe không tồn tại");
                        Xe xe = db.Xes.FirstOrDefault(x => x.maxe == _maxe);
                        xe.trangthai = (int)Constant.KHOA;
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
