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
    [RoutePrefix("api/tinhthanh")]
    [BaseAuthenticationAttribute]
    public class APITinhThanhController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APITinhThanhController")]
        public IHttpActionResult Get(string _tukhoa = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var tinhThanhs = db.TinhThanhs
                            .Where(x => (x.tentinh.Contains(_tukhoa) || x.matinh.Contains(_tukhoa)))
                            .ToList();
                    int sobanghi = tinhThanhs.Count;
                    return Ok(new
                    {
                        tinhThanhs = tinhThanhs
                            .Select(x => new
                            {
                                x.matinh,
                                x.tentinh,
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APITinhThanhController")]
        public IHttpActionResult Detail(string _matinh)
        {
            try
            {
                using (var db = new DB())
                {
                    if (db.TinhThanhs.Any(x => x.matinh == _matinh))
                        return BadRequest("Tỉnh thành không tồn tại");
                    TinhThanh tinhThanh = db.TinhThanhs.FirstOrDefault(x => x.matinh == _matinh);
                    return Ok(new
                    {
                        tinhThanh.matinh,
                        tinhThanh.tentinh,
                        tinhThanh.trangthai
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
        public IHttpActionResult Post(TinhThanh _tinhThanh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.TinhThanhs.Any(x => x.matinh == _tinhThanh.matinh))
                            return BadRequest("Mã tỉnh thành đã tồn tại");
                        db.TinhThanhs.Add(_tinhThanh);
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
        public IHttpActionResult Put(TinhThanh _tinhThanh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (!db.TinhThanhs.Any(x => x.matinh == _tinhThanh.matinh))
                            return BadRequest("Mã tỉnh thành không tồn tại");
                        TinhThanh oldtinhThanh = db.TinhThanhs.FirstOrDefault(x => x.matinh == _tinhThanh.matinh);
                        oldtinhThanh.tentinh = _tinhThanh.tentinh;
                        oldtinhThanh.trangthai = _tinhThanh.trangthai;
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
        public IHttpActionResult Delete(string _matinh)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.TinhThanhs.Any(x => x.matinh == _matinh))
                            return BadRequest("Tỉnh thành không tồn tại");
                        TinhThanh tinhThanh = db.TinhThanhs.FirstOrDefault(x => x.matinh == _matinh);
                        tinhThanh.trangthai = (int)Constant.KHOA;
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
