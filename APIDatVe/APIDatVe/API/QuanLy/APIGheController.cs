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
        public IHttpActionResult Put(GheXe _gheXe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Xe xe = db.Xes.FirstOrDefault(x => x.maxe == _gheXe._maxe);
                        if (xe == null)
                            return BadRequest("Xe không tồn tại");
                        DateTime ngayCapNhat = DateTime.Now;
                        _gheXe._ghes.ForEach(x =>
                        {
                            Ghe ghe = db.Ghes.FirstOrDefault(y => y.maghe == x.maghe);
                            if(ghe == null)
                            {
                                db.Ghes.Add(new Ghe()
                                {
                                    ngaycapnhat = ngayCapNhat,
                                    maxe = _gheXe._maxe,
                                    vitriX = x.vitriX,
                                    vitriY = x.vitriY,
                                    tang = x.tang,
                                    active = x.active,
                                    maghe = x.maghe
                                });
                            }
                            else
                            {
                                ghe.vitriX = x.vitriX;
                                ghe.vitriY = x.vitriY;
                                ghe.tang = x.tang;
                                ghe.active = x.active;
                            }
                            db.SaveChanges();
                        });
                        transaction.Commit();
                        return Ok(_gheXe);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class GheXe
    {
        public string _maxe { get; set; }
        public List<Ghe> _ghes { get; set; }
    }
}
