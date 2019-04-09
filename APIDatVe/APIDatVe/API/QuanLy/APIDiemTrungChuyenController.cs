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
    [RoutePrefix("api/diemtrungchuyen")]
    [BaseAuthenticationAttribute]
    public class APIDiemTrungChuyenController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIDiemTrungChuyenController")]
        public IHttpActionResult Get(string _tukhoa = "", string _matinh = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    List<DiemTrungChuyen> diemTrungChuyens = new List<DiemTrungChuyen>();
                    if (string.IsNullOrEmpty(_matinh))
                        diemTrungChuyens = db.DiemTrungChuyens
                                .Where(x => string.IsNullOrEmpty(_tukhoa) || x.tendiemtrungchuyen.Contains(_tukhoa) || x.matinh.Contains(_tukhoa) || x.diachi.Contains(_tukhoa))
                                .ToList();
                    else
                        diemTrungChuyens = db.DiemTrungChuyens
                                .Where(x => (string.IsNullOrEmpty(_tukhoa) || x.tendiemtrungchuyen.Contains(_tukhoa) || x.matinh.Contains(_tukhoa) || x.diachi.Contains(_tukhoa)) && x.matinh == _matinh).ToList();
                    int sobanghi = diemTrungChuyens.Count;

                    return Ok(new
                    {
                        diemTrungChuyens = diemTrungChuyens
                                .Select(x => new
                                {
                                    x.trangthai,
                                    x.tendiemtrungchuyen,
                                    x.matinh,
                                    x.madiemtrungchuyen,
                                    x.diachi,
                                    tentinh = x.TinhThanh.tentinh
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIDiemTrungChuyenController")]
        public IHttpActionResult Detail(string _madiemtrungchuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    DiemTrungChuyen DiemTrungChuyen = db.DiemTrungChuyens.FirstOrDefault(x => x.madiemtrungchuyen == _madiemtrungchuyen);
                    if (DiemTrungChuyen == null)
                        return BadRequest("Điểm chung chuyển không tồn tại");
                    return Ok(new
                    {
                        DiemTrungChuyen.trangthai,
                        DiemTrungChuyen.tendiemtrungchuyen,
                        DiemTrungChuyen.matinh,
                        DiemTrungChuyen.madiemtrungchuyen,
                        DiemTrungChuyen.diachi
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIDiemTrungChuyenController")]
        public IHttpActionResult Post(DiemTrungChuyen _DiemTrungChuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DiemTrungChuyen DiemTrungChuyen = db.DiemTrungChuyens.FirstOrDefault(x => x.madiemtrungchuyen == _DiemTrungChuyen.madiemtrungchuyen);
                        if (DiemTrungChuyen != null)
                            return BadRequest("Mã điểm chung chuyển đã tồn tại");
                        db.DiemTrungChuyens.Add(_DiemTrungChuyen);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _DiemTrungChuyen.trangthai,
                            _DiemTrungChuyen.tendiemtrungchuyen,
                            _DiemTrungChuyen.matinh,
                            _DiemTrungChuyen.madiemtrungchuyen,
                            _DiemTrungChuyen.diachi
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIDiemTrungChuyenController")]
        public IHttpActionResult Put(DiemTrungChuyen _DiemTrungChuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DiemTrungChuyen oldDiemTrungChuyen = db.DiemTrungChuyens.FirstOrDefault(x => x.madiemtrungchuyen == _DiemTrungChuyen.madiemtrungchuyen);
                        if (oldDiemTrungChuyen == null)
                            return BadRequest("Mã tỉnh thành không tồn tại");
                        oldDiemTrungChuyen.matinh = _DiemTrungChuyen.matinh;
                        oldDiemTrungChuyen.tendiemtrungchuyen = _DiemTrungChuyen.tendiemtrungchuyen;
                        oldDiemTrungChuyen.diachi = _DiemTrungChuyen.diachi;
                        oldDiemTrungChuyen.trangthai = _DiemTrungChuyen.trangthai;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(new
                        {
                            _DiemTrungChuyen.trangthai,
                            _DiemTrungChuyen.tendiemtrungchuyen,
                            _DiemTrungChuyen.matinh,
                            _DiemTrungChuyen.madiemtrungchuyen,
                            _DiemTrungChuyen.diachi
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
        [AcceptAction(ActionName = "Delete", ControllerName = "APIDiemTrungChuyenController")]
        public IHttpActionResult Delete(string _madiemtrungchuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DiemTrungChuyen DiemTrungChuyen = db.DiemTrungChuyens.FirstOrDefault(x => x.madiemtrungchuyen == _madiemtrungchuyen);
                        if (DiemTrungChuyen == null)
                            return BadRequest("Điểm chung chuyển không tồn tại");
                        DiemTrungChuyen.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_madiemtrungchuyen);
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
        [AcceptAction(ActionName = "ChangeStatus", ControllerName = "APIDiemTrungChuyenController")]
        public IHttpActionResult ChangeStatus(string _madiemtrungchuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DiemTrungChuyen DiemTrungChuyen = db.DiemTrungChuyens.FirstOrDefault(x => x.madiemtrungchuyen == _madiemtrungchuyen);
                        if (DiemTrungChuyen == null)
                            return BadRequest("Điểm chung chuyển không tồn tại");
                        if (DiemTrungChuyen.trangthai == (int)Constant.KHOA)
                            DiemTrungChuyen.trangthai = (int)Constant.HOATDONG;
                        else
                            DiemTrungChuyen.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_madiemtrungchuyen);
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
        [AcceptAction(ActionName = "Ready", ControllerName = "APIDiemTrungChuyenController")]
        public IHttpActionResult Ready()
        {
            try
            {
                using (var db = new DB())
                {
                    List<DiemTrungChuyen> diemTrungChuyens = db.DiemTrungChuyens
                                .Where(x => x.trangthai == (int)Constant.HOATDONG)
                                .OrderBy(x => x.matinh)
                                .ToList();

                    return Ok(diemTrungChuyens.Select(x => new
                    {
                        x.madiemtrungchuyen,
                        tendiemtrungchuyen = x.TinhThanh.tentinh + " - " + x.tendiemtrungchuyen,

                    }).ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
