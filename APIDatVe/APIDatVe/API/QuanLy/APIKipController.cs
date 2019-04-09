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
    [RoutePrefix("api/kip")]
    [BaseAuthenticationAttribute]
    public class APIKipController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIKipController")]
        public IHttpActionResult Get(string _tukhoa = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var kips = db.Kips
                            .Where(x => (string.IsNullOrEmpty(_tukhoa) || x.makip.Contains(_tukhoa) || x.tenkip.Contains(_tukhoa)))
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

        [Route("detail")]
        [HttpGet]
        [AcceptAction(ActionName = "Detail", ControllerName = "APIKipController")]
        public IHttpActionResult Detail(string _makip)
        {
            try
            {
                using (var db = new DB())
                {
                    Kip kip = db.Kips.FirstOrDefault(x => x.makip == _makip);
                    if (kip == null)
                        return BadRequest("Kip không tồn tại");
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
                        Kip kip = db.Kips.FirstOrDefault(x => x.makip == _kip.makip);
                        if (kip != null)
                            return BadRequest("Mã kip đã tồn tại");
                        db.Kips.Add(_kip);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _kip.makip,
                            _kip.tenkip,
                            _kip.trangthai
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIKipController")]
        public IHttpActionResult Put(Kip _kip)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Kip oldKip = db.Kips.FirstOrDefault(x => x.makip == _kip.makip);
                        if (oldKip == null)
                            return BadRequest("Mã kip không tồn tại");
                        oldKip.tenkip = _kip.tenkip;
                        oldKip.trangthai = _kip.trangthai;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _kip.makip,
                            _kip.tenkip,
                            _kip.trangthai
                        });
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
        [AcceptAction(ActionName = "ChangeStatus", ControllerName = "APIKipController")]
        public IHttpActionResult ChangeStatus(string _makip)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Kip kip = db.Kips.FirstOrDefault(x => x.makip == _makip);
                        if (kip == null)
                            return BadRequest("Kip không tồn tại");
                        if (kip.trangthai == (int)Constant.KHOA)
                            kip.trangthai = (int)Constant.HOATDONG;
                        else
                            kip.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_makip);
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
                        Kip kip = db.Kips.FirstOrDefault(x => x.makip == _makip);
                        if (kip == null)
                            return BadRequest("Kip không tồn tại");
                        kip.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_makip);
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
