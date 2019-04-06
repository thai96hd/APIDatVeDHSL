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
    [RoutePrefix("api/chucvu")]
    [BaseAuthenticationAttribute]
    public class APIChucVuController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIChucVuController")]
        public IHttpActionResult Get(string _tukhoa = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var chucvus = db.ChucVus
                            .Where(x => (x.machucvu.Contains(_tukhoa) || x.tenchucvu.Contains(_tukhoa)))
                            .ToList();
                    int sobanghi = chucvus.Count;
                    return Ok(new
                    {
                        chucvus = chucvus
                            .Select(x => new
                            {
                                x.machucvu,
                                x.tenchucvu,
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIChucVuController")]
        public IHttpActionResult Detail(string _machucvu)
        {
            try
            {
                using (var db = new DB())
                {
                    if (db.ChucVus.Any(x => x.machucvu == _machucvu))
                        return BadRequest("Chức vụ không tồn tại");
                    ChucVu chucVu = db.ChucVus.FirstOrDefault(x => x.machucvu == _machucvu);
                    return Ok(new
                    {
                        chucVu.machucvu,
                        chucVu.tenchucvu,
                        chucVu.trangthai
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIChucVuController")]
        public IHttpActionResult Post(ChucVu _chucVu)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.ChucVus.Any(x => x.machucvu == _chucVu.machucvu))
                            return BadRequest("Mã chức vụ đã tồn tại");
                        db.ChucVus.Add(_chucVu);
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIChucVuController")]
        public IHttpActionResult Put(ChucVu _chucVu)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.ChucVus.Any(x => x.machucvu == _chucVu.machucvu))
                            return BadRequest("Mã chức vụ không tồn tại");
                        ChucVu oldChucVu = db.ChucVus.FirstOrDefault(x => x.machucvu == _chucVu.machucvu);
                        oldChucVu.tenchucvu = _chucVu.tenchucvu;
                        oldChucVu.trangthai = _chucVu.trangthai;
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIChucVuController")]
        public IHttpActionResult Delete(string _machucvu)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.ChucVus.Any(x => x.machucvu == _machucvu))
                            return BadRequest("Chức vụ không tồn tại");
                        ChucVu chucVu = db.ChucVus.FirstOrDefault(x => x.machucvu == _machucvu);
                        chucVu.trangthai = (int)Constant.KHOA;
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
