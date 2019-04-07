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
    [RoutePrefix("api/ghe")]
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
                    var ghes = db.Ghes
                            .Where(x => x.maxe == _maxe).OrderByDescending(x => x.ngaycapnhat)
                            .ToList();
                    return Ok(ghes.Select(x => new
                    {
                        x.maghe,
                        x.maxe,
                        x.ngaycapnhat,
                        x.vitriX,
                        x.vitriY,
                        x.active,
                        x.tang
                    }));
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
                                vitriX = x.vitriX,
                                vitriY = x.vitriY,
                                tang = x.tang,
                                active = x.active,
                                maghe = x.maghe
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
