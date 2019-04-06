﻿using APIDatVe.API.Quyen;
using APIDatVe.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/xe")]
    [BaseAuthenticationAttribute]
    public class APIGheController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIGheController")]
        public IHttpActionResult Get(string _maxe)
        {
            try
            {
                using (var db = new DB())
                {
                    List<Ghe> ghes = db.Ghes.Where(x => x.maxe == _maxe).OrderByDescending(x => x.ngaycapnhat).ToList();
                    return Ok(ghes);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("put")]
        [HttpPut]
        [AcceptAction(ActionName = "Put", ControllerName = "APIGheController")]
        public IHttpActionResult Put(string _maxe, List<Ghe> _ghes)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        if (!db.Xes.Any(x => x.maxe == _maxe))
                            return BadRequest("Xe không tồn tại");

                        _ghes.ForEach(x =>
                        {
                            db.Ghes.Add(new Ghe()
                            {
                                ngaycapnhat = DateTime.Now,
                                maxe = _maxe,
                                soghe = x.soghe,
                                vitriX = x.vitriX,
                                vitriY = x.vitriY
                            });
                        });
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
