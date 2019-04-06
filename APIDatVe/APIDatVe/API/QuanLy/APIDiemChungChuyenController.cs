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
    [RoutePrefix("api/diemchungchuyen")]
    [BaseAuthenticationAttribute]
    public class APIDiemChungChuyenController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIDiemChungChuyenController")]
        public IHttpActionResult Get(string _tukhoa = "", string _matinh = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var diemChungChuyens = new List<DiemChungChuyen>();
                    if (_matinh == "")
                        diemChungChuyens = db.DiemChungChuyens
                                .Where(x => x.tendiemtrungchuyen.Contains(_tukhoa) || x.matinh.Contains(_tukhoa) || x.diachi.Contains(_tukhoa))
                                .ToList();
                    else
                        diemChungChuyens = db.DiemChungChuyens
                                .Where(x => (x.tendiemtrungchuyen.Contains(_tukhoa) || x.matinh.Contains(_tukhoa) || x.diachi.Contains(_tukhoa)) && x.matinh == _matinh).ToList();
                    int sobanghi = diemChungChuyens.Count;
                    return Ok(new
                    {
                        diemChungChuyens = diemChungChuyens
                                .Select(x => new
                                {
                                    x.trangthai,
                                    x.tendiemtrungchuyen,
                                    x.matinh,
                                    x.madiemtrungchuyen,
                                    x.diachi
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIDiemChungChuyenController")]
        public IHttpActionResult Detail(string _madiemtrungchuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    if (db.DiemChungChuyens.Any(x => x.madiemtrungchuyen == _madiemtrungchuyen))
                        return BadRequest("Điểm chung chuyển không tồn tại");
                    DiemChungChuyen diemChungChuyen = db.DiemChungChuyens.FirstOrDefault(x => x.madiemtrungchuyen == _madiemtrungchuyen);
                    return Ok(diemChungChuyen);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("post")]
        [HttpPost]
        [AcceptAction(ActionName = "Post", ControllerName = "APIDiemChungChuyenController")]
        public IHttpActionResult Post(DiemChungChuyen _diemChungChuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.DiemChungChuyens.Any(x => x.madiemtrungchuyen == _diemChungChuyen.madiemtrungchuyen))
                            return BadRequest("Mã điểm chung chuyển đã tồn tại");
                        db.DiemChungChuyens.Add(_diemChungChuyen);
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIDiemChungChuyenController")]
        public IHttpActionResult Put(DiemChungChuyen _diemChungChuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (!db.DiemChungChuyens.Any(x => x.madiemtrungchuyen == _diemChungChuyen.madiemtrungchuyen))
                            return BadRequest("Mã tỉnh thành không tồn tại");
                        DiemChungChuyen olddiemChungChuyen = db.DiemChungChuyens.FirstOrDefault(x => x.madiemtrungchuyen == _diemChungChuyen.madiemtrungchuyen);
                        olddiemChungChuyen.matinh = _diemChungChuyen.matinh;
                        olddiemChungChuyen.tendiemtrungchuyen = _diemChungChuyen.tendiemtrungchuyen;
                        olddiemChungChuyen.diachi = _diemChungChuyen.diachi;
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIDiemChungChuyenController")]
        public IHttpActionResult Delete(string _madiemtrungchuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (db.DiemChungChuyens.Any(x => x.madiemtrungchuyen == _madiemtrungchuyen))
                            return BadRequest("Điểm chung chuyển không tồn tại");
                        DiemChungChuyen diemChungChuyen = db.DiemChungChuyens.FirstOrDefault(x => x.madiemtrungchuyen == _madiemtrungchuyen);
                        diemChungChuyen.trangthai = (int)Constant.KHOA;
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
