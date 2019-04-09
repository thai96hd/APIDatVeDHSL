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
    [RoutePrefix("api/doituong")]
    [BaseAuthenticationAttribute]
    public class APIDoiTuongController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIDoiTuongController")]
        public IHttpActionResult Get(string _tukhoa = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var doituongs = db.DoiTuongs
                            .Where(x => (string.IsNullOrEmpty(_tukhoa) || x.madoituong.Contains(_tukhoa) || x.tendoituong.Contains(_tukhoa)))
                            .ToList();
                    int sobanghi = doituongs.Count;
                    return Ok(new
                    {
                        doituongs = doituongs
                            .Select(x => new
                            {
                                x.tendoituong,
                                x.madoituong,
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIDoiTuongController")]
        public IHttpActionResult Detail(string _madoituong)
        {
            try
            {
                using (var db = new DB())
                {
                    DoiTuong doiTuong = db.DoiTuongs.FirstOrDefault(x => x.madoituong == _madoituong);
                    if (doiTuong == null)
                        return BadRequest("Đối tượng không tồn tại");
                    return Ok(new
                    {
                        doiTuong.madoituong,
                        doiTuong.tendoituong,
                        doiTuong.trangthai
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIDoiTuongController")]
        public IHttpActionResult Post(DoiTuong _doiTuong)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DoiTuong doiTuong = db.DoiTuongs.FirstOrDefault(x => x.madoituong == _doiTuong.madoituong);
                        if (doiTuong != null)
                            return BadRequest("Mã đối tượng đã tồn tại");
                        db.DoiTuongs.Add(_doiTuong);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _doiTuong.madoituong,
                            _doiTuong.tendoituong,
                            _doiTuong.trangthai
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIDoiTuongController")]
        public IHttpActionResult Put(DoiTuong _doiTuong)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DoiTuong oldDoiTuong = db.DoiTuongs.FirstOrDefault(x => x.madoituong == _doiTuong.madoituong);
                        if (oldDoiTuong == null)
                            return BadRequest("Mã đối tượng không tồn tại");
                        oldDoiTuong.tendoituong = _doiTuong.tendoituong;
                        oldDoiTuong.trangthai = _doiTuong.trangthai;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _doiTuong.madoituong,
                            _doiTuong.tendoituong,
                            _doiTuong.trangthai
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIDoiTuongController")]
        public IHttpActionResult Delete(string _madoituong)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DoiTuong doiTuong = db.DoiTuongs.FirstOrDefault(x => x.madoituong == _madoituong);
                        if (doiTuong == null)
                            return BadRequest("Đối tượng không tồn tại");
                        doiTuong.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_madoituong);
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
