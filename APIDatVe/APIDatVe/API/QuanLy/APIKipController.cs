using APIDatVe.API.Quyen;
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
    [RoutePrefix("api/kip")]
    [BaseAuthenticationAttribute]
    public class APIKipController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIKipController")]
        public IHttpActionResult Get(string _tukhoa, int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var kips = db.Kips
                            .Where(x => (x.makip.Contains(_tukhoa) || x.tenkip.Contains(_tukhoa)))
                            .ToList();
                    int sobanghi = kips.Count;
                    return Ok(new
                    {
                        kips = kips
                            .Select(x => new
                            {
                                x.makip,
                                x.tenkip,
                                x.trangthai
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIKipController")]
        public IHttpActionResult Detail(string _makip)
        {
            try
            {
                using (var db = new DB())
                {
                    if (db.Kips.Any(x => x.makip == _makip))
                        return BadRequest("Kip không tồn tại");
                    Kip kip = db.Kips.FirstOrDefault(x => x.makip == _makip);
                    return Ok(new
                    {
                        kip.makip,
                        kip.tenkip,
                        kip.trangthai
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIKipController")]
        public IHttpActionResult Post(Kip _kip)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.Kips.Any(x => x.makip == _kip.makip))
                            return BadRequest("Mã kip đã tồn tại");
                        db.Kips.Add(_kip);
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIKipController")]
        public IHttpActionResult Put(Kip _kip)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (!db.Kips.Any(x => x.makip == _kip.makip))
                            return BadRequest("Mã kip không tồn tại");
                        Kip oldKip = db.Kips.FirstOrDefault(x => x.makip == _kip.makip);
                        oldKip.tenkip = _kip.tenkip;
                        oldKip.trangthai = _kip.trangthai;
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIKipController")]
        public IHttpActionResult Delete(string _makip)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.Kips.Any(x => x.makip == _makip))
                            return BadRequest("Kip không tồn tại");
                        Kip kip = db.Kips.FirstOrDefault(x => x.makip == _makip);
                        kip.trangthai = (int)Constant.KHOA;
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
