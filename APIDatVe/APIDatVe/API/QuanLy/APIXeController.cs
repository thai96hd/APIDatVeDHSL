﻿using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using APIDatVe.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/xe")]
    [BaseAuthenticationAttribute]
    public class APIXeController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIXeController")]
        public IHttpActionResult Get(string _tukhoa = "", int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    var xes = db.Xes
                                .Where(x => (string.IsNullOrEmpty(_tukhoa) || x.maxe.Contains(_tukhoa) || x.biensoxe.Contains(_tukhoa)))
                                .ToList();
                    int sobanghi = xes.Count;
                    return Ok(new
                    {
                        xes = xes.Select(x => new
                        {
                            x.biensoxe,
                            x.ghichu,
                            x.maxe,
                            x.soghe,
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
        [AcceptAction(ActionName = "Detail", ControllerName = "APIXeController")]
        public IHttpActionResult Detail(string _maxe)
        {
            try
            {
                using (var db = new DB())
                {
                    Xe xe = db.Xes.FirstOrDefault(x => x.maxe == _maxe);
                    if (xe == null)
                        return BadRequest("Xe không tồn tại");
                    return Ok(new
                    {
                        xe.ghichu,
                        xe.maxe,
                        xe.soghe,
                        xe.trangthai,
                        xe.biensoxe
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
        [AcceptAction(ActionName = "Post", ControllerName = "APIXeController")]
        public IHttpActionResult Post(Xe xe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (string.IsNullOrEmpty(xe.maxe))
                            return BadRequest("Mã xe không hợp lệ");
                        Xe _xe = db.Xes.FirstOrDefault(x => x.maxe == xe.maxe);
                        if (_xe != null)
                            return BadRequest("Mã xe đã tồn tại");
                        db.Xes.Add(xe);
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(xe);
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIXeController")]
        public IHttpActionResult Put(Xe xe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Xe oldxe = db.Xes.FirstOrDefault(x => x.maxe == xe.maxe);
                        if (oldxe == null)
                            return BadRequest("Mã xe không tồn tại");
                        oldxe.soghe = xe.soghe;
                        oldxe.ghichu = xe.ghichu;
                        oldxe.biensoxe = xe.biensoxe;
                        oldxe.trangthai = xe.trangthai;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(xe);
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
        [AcceptAction(ActionName = "ChangeStatus", ControllerName = "APIXeController")]
        public IHttpActionResult ChangeStatus(string _maxe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Xe xe = db.Xes.FirstOrDefault(x => x.maxe == _maxe);
                        if (xe == null)
                            return BadRequest("Xe không tồn tại");
                        if (xe.trangthai == (int)Constant.KHOA)
                            xe.trangthai = (int)Constant.HOATDONG;
                        else
                            xe.trangthai = (int)Constant.KHOA;
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_maxe);
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
        public IHttpActionResult Ready()
        {
            try
            {
                using (var db = new DB())
                {
                    var xes = db.Xes
                                .Where(x => x.trangthai == (int)Constant.HOATDONG)
                                .ToList();
                    int sobanghi = xes.Count;
                    return Ok(xes.Select(x => new
                    {
                        x.biensoxe,
                        x.ghichu,
                        x.maxe,
                        x.soghe
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
