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
    [RoutePrefix("api/phanquyen")]
    [BaseAuthenticationAttribute]
    public class APIPhanQuyenController : ApiController
    {
        [Route()]
        [HttpGet]
        [AcceptAction(ActionName = "Get", ControllerName = "APIPhanQuyenController")]
        public IHttpActionResult Get()
        {
            try
            {
                using (var db = new DB())
                {
                    List<Quyen> quyens = db.Quyens.ToList();
                    return Ok(quyens.Select(x => new
                    {
                        x.maquyen,
                        x.tenquyen
                    }));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("detail")]
        [HttpGet]
        [AcceptAction(ActionName = "Detail", ControllerName = "APIPhanQuyenController")]
        public IHttpActionResult Detail(string _maquyen)
        {
            try
            {
                using (var db = new DB())
                {
                    Quyen quyen = db.Quyens.FirstOrDefault(x => x.maquyen == _maquyen);
                    if (quyen == null)
                        return BadRequest("Quyền không tồn tại");
                    List<QuyenManHinhQuanLy> quyenManHinhQuanLies = new List<QuyenManHinhQuanLy>();
                    List<QuyenAPIQuanLy> quyenAPIQuanLies = new List<QuyenAPIQuanLy>();
                    List<ManHinhQuanLy> manHinhQuanLies = db.ManHinhQuanLies.ToList();
                    List<APIQuanLy> aPIQuanLies = db.APIQuanLies.ToList();
                    manHinhQuanLies.ForEach(x =>
                    {
                        QuyenManHinhQuanLy quyenManHinhQuanLy = quyen.QuyenManHinhQuanLies
                                    .FirstOrDefault(y => y.manhinhquanlyId == x.manhinhquanlyId);
                        if (quyenManHinhQuanLy == null)
                        {
                            quyenManHinhQuanLy = new QuyenManHinhQuanLy();
                            quyenManHinhQuanLy.manhinhquanlyId = x.manhinhquanlyId;
                            quyenManHinhQuanLy.maquyen = _maquyen;
                            quyenManHinhQuanLy.chon = false;
                            quyenManHinhQuanLy.ManHinhQuanLy = x;
                        }
                        quyenManHinhQuanLies.Add(quyenManHinhQuanLy);
                    });
                    aPIQuanLies.ForEach(x =>
                    {
                        QuyenAPIQuanLy quyenAPIQuanLy = quyen.QuyenAPIQuanLies
                                    .FirstOrDefault(y => y.APIquanlyid == x.APIquanlyid);
                        if (quyenAPIQuanLy == null)
                        {
                            quyenAPIQuanLy = new QuyenAPIQuanLy();
                            quyenAPIQuanLy.APIquanlyid = x.APIquanlyid;
                            quyenAPIQuanLy.maquyen = _maquyen;
                            quyenAPIQuanLy.chon = false;
                            quyenAPIQuanLy.APIQuanLy = x;
                        }
                        quyenAPIQuanLies.Add(quyenAPIQuanLy);
                    });
                    return Ok(new
                    {
                        quyen = new
                        {
                            quyen.maquyen,
                            quyen.tenquyen
                        },
                        quyenManHinhQuanLies = quyenManHinhQuanLies.Select(x => new
                        {
                            x.manhinhquanlyId,
                            x.maquyen,
                            x.ManHinhQuanLy.mota,
                            x.chon
                        }).ToList(),
                        quyenAPIQuanLies = quyenAPIQuanLies.Select(x => new
                        {
                            x.APIquanlyid,
                            x.maquyen,
                            x.APIQuanLy.mota,
                            x.chon
                        }).ToList()
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
        [AcceptAction(ActionName = "Put", ControllerName = "APIPhanQuyenController")]
        public IHttpActionResult Put(PhanQuyen _phanQuyen)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Quyen quyen = db.Quyens.FirstOrDefault(x => x.maquyen.Trim() == _phanQuyen.maquyen.Trim());
                        if (quyen == null)
                            return BadRequest("Quyền không tồn tại");
                        db.QuyenAPIQuanLies.RemoveRange(db.QuyenAPIQuanLies.Where(x => x.maquyen == quyen.maquyen));
                        db.QuyenManHinhQuanLies.RemoveRange(db.QuyenManHinhQuanLies.Where(x => x.maquyen == quyen.maquyen));
                        _phanQuyen.quyenManHinhQuanLies.ForEach(x =>
                        {
                            db.QuyenManHinhQuanLies.Add(new QuyenManHinhQuanLy()
                            {
                                maquyen = quyen.maquyen,
                                chon = x.chon,
                                manhinhquanlyId = x.manhinhquanlyId
                            });
                        });
                        _phanQuyen.quyenAPIQuanLies.ForEach(x =>
                        {
                            db.QuyenAPIQuanLies.Add(new QuyenAPIQuanLy()
                            {
                                chon = x.chon,
                                maquyen = quyen.maquyen,
                                APIquanlyid = x.APIquanlyid
                            });
                        });
                        db.SaveChanges();
                        transaction.Commit();
                        return Ok(_phanQuyen.maquyen);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    public class PhanQuyen
    {
        public string maquyen { get; set; }
        public List<QuyenManHinhQuanLy> quyenManHinhQuanLies { get; set; }
        public List<QuyenAPIQuanLy> quyenAPIQuanLies { get; set; }
    }
}
