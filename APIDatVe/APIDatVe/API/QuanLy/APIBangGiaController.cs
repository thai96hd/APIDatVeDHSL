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
    [RoutePrefix("api/banggia")]
    [BaseAuthenticationAttribute]
    public class APIBangGiaController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIBangGiaController")]
        public IHttpActionResult Get()
        {
            try
            {
                using (var db = new DB())
                {
                    BangGia bangGia = db.BangGias.FirstOrDefault();
                    if (bangGia == null)
                    {
                        bangGia = new BangGia()
                        {
                            giave = 0,
                            madiemtrungchuyendon = "",
                            madiemtrungchuyentra = "",
                            thoigianhanhtrinh = 0
                        };
                    }
                    return Ok(new
                    {
                        bangGia.thoigianhanhtrinh,
                        bangGia.madiemtrungchuyentra,
                        bangGia.madiemtrungchuyendon,
                        bangGia.giave
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIBangGiaController")]
        public IHttpActionResult Detail(int _banggiaId)
        {
            try
            {
                using (var db = new DB())
                {
                    if (db.BangGias.Any(x => x.banggiaId == _banggiaId))
                        return BadRequest("Bảng giá không tồn tại");
                    BangGia bangGia = db.BangGias.FirstOrDefault(x => x.banggiaId == _banggiaId);
                    return Ok(new
                    {
                        bangGia.madiemtrungchuyendon,
                        bangGia.madiemtrungchuyentra,
                        bangGia.thoigianhanhtrinh,
                        tendiemtrungchuyendon = bangGia.DiemChungChuyen.tendiemtrungchuyen,
                        tendiemtrungchuyentra = bangGia.DiemChungChuyen1.tendiemtrungchuyen,
                        bangGia.giave
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("put")]
        [HttpPut]
        [AcceptAction(ActionName = "Put", ControllerName = "APIBangGiaController")]
        public IHttpActionResult Put(BangGia _bangGia)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        BangGia bangGia = db.BangGias.FirstOrDefault();
                        if (bangGia == null)
                            db.BangGias.Add(_bangGia);
                        else
                        {
                            bangGia.giave = _bangGia.giave;
                            bangGia.madiemtrungchuyendon = _bangGia.madiemtrungchuyendon;
                            bangGia.madiemtrungchuyentra = _bangGia.madiemtrungchuyentra;
                            bangGia.thoigianhanhtrinh = _bangGia.thoigianhanhtrinh;
                        }
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIBangGiaController")]
        public IHttpActionResult Delete(int _banggiaId)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.BangGias.Any(x => x.banggiaId == _banggiaId))
                            return BadRequest("Bảng giá không tồn tại");
                        BangGia bangGia = db.BangGias.FirstOrDefault(x => x.banggiaId == _banggiaId);
                        db.BangGias.Remove(bangGia);
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
