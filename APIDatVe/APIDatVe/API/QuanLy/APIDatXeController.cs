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
    [RoutePrefix("api/datxe")]
    [BaseAuthenticationAttribute]
    public class APIDatXeController : ApiController
    {
        [Route("getlotrinh")]
        [HttpGet]
        public IHttpActionResult GetLoTrinh()
        {
            try
            {
                using (var db = new DB())
                {
                    var loTrinhs = db.LoTrinhs.ToList();
                    int sobanghi = loTrinhs.Count;
                    return Ok(loTrinhs.Select(x => new
                    {
                        x.malotrinh,
                        x.tenlotrinh
                    }).ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getdiemtrungchuyenlotrinh")]
        [HttpGet]
        public IHttpActionResult GetDiemTrungChuyenLoTrinh(string _malotrinh)
        {
            if (string.IsNullOrEmpty(_malotrinh))
                return Ok(new
                {
                    diemTrungChuyensDon = new List<DiemTrungChuyen>(),
                    diemTrungChuyensTra = new List<DiemTrungChuyen>()
                });
            try
            {
                using (var db = new DB())
                {
                    LoTrinh loTrinh = db.LoTrinhs.FirstOrDefault(x => x.malotrinh == _malotrinh);
                    List<DiemTrungChuyen> diemTrungChuyensDon = db.DiemTrungChuyens.Where(x => x.matinh == loTrinh.matinhdon).ToList();
                    List<DiemTrungChuyen> diemTrungChuyensTra = db.DiemTrungChuyens.Where(x => x.matinh == loTrinh.matinhtra).ToList();
                    return Ok(new
                    {
                        diemTrungChuyensDon = diemTrungChuyensDon.Select(x => new
                        {
                            x.madiemtrungchuyen,
                            x.tendiemtrungchuyen
                        }),
                        diemTrungChuyensTra = diemTrungChuyensTra.Select(x => new
                        {
                            x.madiemtrungchuyen,
                            x.tendiemtrungchuyen
                        })
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("gettuyendi")]
        [HttpGet]
        public IHttpActionResult GetTuyenDi()
        {
            try
            {
                using (var db = new DB())
                {
                    var loTrinhs = db.LoTrinhs.ToList();
                    int sobanghi = loTrinhs.Count;
                    return Ok(loTrinhs.Select(x => new
                    {
                        x.matinhdon,
                        x.matinhtra,
                        x.tenlotrinh,
                        x.khoangthoigiandukien,
                        x.malotrinh,
                        tentinhdon = db.TinhThanhs.FirstOrDefault(y => y.matinh == x.matinhdon).tentinh,
                        tentinhtra = db.TinhThanhs.FirstOrDefault(y => y.matinh == x.matinhtra).tentinh
                    }).ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("post")]
        [HttpPost]
        [AcceptAction(ActionName = "Post", ControllerName = "APIDatXeController")]
        public IHttpActionResult Post()
        {
            try
            {
                using (var db = new DB())
                {
                    var loTrinhs = db.LoTrinhs.ToList();
                    int sobanghi = loTrinhs.Count;
                    return Ok(loTrinhs.Select(x => new
                    {
                        x.matinhdon,
                        x.matinhtra,
                        x.tenlotrinh,
                        x.khoangthoigiandukien,
                        x.malotrinh,
                        tentinhdon = db.TinhThanhs.FirstOrDefault(y => y.matinh == x.matinhdon).tentinh,
                        tentinhtra = db.TinhThanhs.FirstOrDefault(y => y.matinh == x.matinhtra).tentinh
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
