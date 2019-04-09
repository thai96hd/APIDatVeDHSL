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
    [RoutePrefix("api/banggia")]
    [BaseAuthenticationAttribute]
    public class APIBangGiaController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIBangGiaController")]
        public IHttpActionResult Get(string _madiemtrungchuyendon = "", string _madiemtrungchuyentra = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    List<BangGia> bangGias = db.BangGias.ToList();
                    if (!string.IsNullOrEmpty(_madiemtrungchuyendon))
                        bangGias = bangGias.Where(x => x.madiemtrungchuyendon == _madiemtrungchuyendon).ToList();
                    if (!string.IsNullOrEmpty(_madiemtrungchuyentra))
                        bangGias = bangGias.Where(x => x.madiemtrungchuyentra == _madiemtrungchuyentra).ToList();

                    int sobanghi = bangGias.Count;
                    return Ok(new
                    {
                        bangGias = bangGias.Select(x => new
                        {
                            x.banggiaId,
                            x.madiemtrungchuyentra,
                            x.madiemtrungchuyendon,
                            x.giave,
                            x.thoigiandukien,
                            tendiemtrungchuyendon = x.DiemTrungChuyen.tendiemtrungchuyen,
                            tendiemtrungchuyentra = x.DiemTrungChuyen1.tendiemtrungchuyen,
                            tentinhdon = x.DiemTrungChuyen.TinhThanh.tentinh,
                            tentinhtra = x.DiemTrungChuyen1.TinhThanh.tentinh
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIBangGiaController")]
        public IHttpActionResult Detail(int _banggiaId)
        {
            try
            {
                using (var db = new DB())
                {
                    BangGia bangGia = db.BangGias.FirstOrDefault(x => x.banggiaId == _banggiaId);
                    if (bangGia == null)
                        return BadRequest("Bảng giá không tồn tại");
                    return Ok(new
                    {
                        bangGia.madiemtrungchuyendon,
                        bangGia.madiemtrungchuyentra,
                        bangGia.thoigiandukien,
                        tendiemtrungchuyendon = bangGia.DiemTrungChuyen.tendiemtrungchuyen,
                        tendiemtrungchuyentra = bangGia.DiemTrungChuyen1.tendiemtrungchuyen,
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
                        BangGia bangGia = db.BangGias.FirstOrDefault(x => x.madiemtrungchuyendon == _bangGia.madiemtrungchuyendon && x.madiemtrungchuyentra == _bangGia.madiemtrungchuyentra);
                        if (bangGia == null)
                            db.BangGias.Add(_bangGia);
                        else
                        {
                            bangGia.giave = _bangGia.giave;
                            bangGia.thoigiandukien = _bangGia.thoigiandukien;
                        }
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _bangGia.madiemtrungchuyendon,
                            _bangGia.madiemtrungchuyentra,
                            _bangGia.thoigiandukien,
                            _bangGia.giave
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIBangGiaController")]
        public IHttpActionResult Delete(int _banggiaId)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        BangGia bangGia = db.BangGias.FirstOrDefault(x => x.banggiaId == _banggiaId);
                        if (bangGia == null)
                            return BadRequest("Bảng giá không tồn tại");
                        db.BangGias.Remove(bangGia);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_banggiaId);
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
