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
                            .Where(x => (string.IsNullOrEmpty(_tukhoa) || x.machucvu.Contains(_tukhoa) || x.tenchucvu.Contains(_tukhoa)))
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIChucVuController")]
        public IHttpActionResult Detail(string _machucvu)
        {
            try
            {
                using (var db = new DB())
                {
                    ChucVu chucVu = db.ChucVus.FirstOrDefault(x => x.machucvu == _machucvu);
                    if (chucVu == null)
                        return BadRequest("Chức vụ không tồn tại");
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
                        ChucVu chucVu = db.ChucVus.FirstOrDefault(x => x.machucvu == _chucVu.machucvu);
                        if (chucVu != null)
                            return BadRequest("Mã chức vụ đã tồn tại");
                        db.ChucVus.Add(_chucVu);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _chucVu.machucvu,
                            _chucVu.tenchucvu,
                            _chucVu.trangthai
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIChucVuController")]
        public IHttpActionResult Put(ChucVu _chucVu)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        ChucVu oldChucVu = db.ChucVus.FirstOrDefault(x => x.machucvu == _chucVu.machucvu);
                        if (oldChucVu == null)
                            return BadRequest("Chức vụ không tồn tại");
                        oldChucVu.tenchucvu = _chucVu.tenchucvu;
                        oldChucVu.trangthai = _chucVu.trangthai;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _chucVu.machucvu,
                            _chucVu.tenchucvu,
                            _chucVu.trangthai
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
        [AcceptAction(ActionName = "ChangeStatus", ControllerName = "APIChucVuController")]
        public IHttpActionResult ChangeStatus(string _machucvu)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        ChucVu chucVu = db.ChucVus.FirstOrDefault(x => x.machucvu == _machucvu);
                        if (chucVu == null)
                            return BadRequest("Chức vụ không tồn tại");
                        if (chucVu.trangthai == (int)Constant.KHOA)
                            chucVu.trangthai = (int)Constant.HOATDONG;
                        else
                            chucVu.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_machucvu);
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
                        ChucVu chucVu = db.ChucVus.FirstOrDefault(x => x.machucvu == _machucvu);
                        if (chucVu == null)
                            return BadRequest("Chức vụ không tồn tại");
                        chucVu.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_machucvu);
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
        [AcceptAction(ActionName = "Ready", ControllerName = "APIChucVuController")]
        public IHttpActionResult Ready()
        {
            try
            {
                using (var db = new DB())
                {
                    var chucvus = db.ChucVus.ToList();
                    return Ok(chucvus.Select(x => new
                    {
                        x.machucvu,
                        x.tenchucvu
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
