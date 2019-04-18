using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using Newtonsoft.Json;
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
        public IHttpActionResult GetTuyenDi(string malotrinh, DateTime ngaydi, string madiemtrungchuyendon, string madiemtrungchuyentra)
        {
            try
            {
                using (var db = new DB())
                {
                    ngaydi = ngaydi.Date;
                    List<ChuyenXe> chuyenXes = db.ChuyenXes.Where(x => x.malotrinh == malotrinh && x.ngayhoatdong.Value == ngaydi).ToList();
                    List<ChuyenXe> chuyenXesFilter = new List<ChuyenXe>();
                    DateTime dateTimeNow = DateTime.Now;
                    chuyenXes.ForEach(x =>
                    {
                        DateTime ngayChuyenXe = new DateTime(x.ngayhoatdong.Value.Year, x.ngayhoatdong.Value.Month, x.ngayhoatdong.Value.Day, x.Kip.gio.Value, x.Kip.phut.Value, 0);
                        if (ngayChuyenXe >= dateTimeNow)
                        {
                            chuyenXesFilter.Add(x);
                        }
                    });
                    chuyenXesFilter.ForEach(x =>
                    {
                        List<TrangThaiGhe> trangThaiGhes = db.TrangThaiGhes.Where(y => y.Ghe.maxe == x.maxe && y.ngay.Value == ngaydi).ToList();
                    });
                    BangGia bangGia = db.BangGias.FirstOrDefault(x => x.madiemtrungchuyendon == madiemtrungchuyendon && x.madiemtrungchuyentra == madiemtrungchuyentra);
                    double giave = 0;
                    if (bangGia != null)
                        giave = bangGia.giave.Value;
                    return Ok(new
                    {
                        chuyenXes = JsonConvert.SerializeObject(chuyenXesFilter.Select(x => new
                        {
                            x.machuyenxe,
                            x.LoTrinh.tenlotrinh,
                            x.Kip.tenkip,
                            thoigian = x.Kip.gio.Value + "h:" + x.Kip.phut.Value + "p",
                            x.Xe.maxe,
                            x.Xe.biensoxe,
                            taixe = x.NhanVien.hoten,
                            phuxe = x.NhanVien1.hoten,
                            x.ngayhoatdong,
                            giave,
                            soghetrong = db.TrangThaiGhes.Where(y => y.Ghe.maxe == x.maxe && y.ngay.Value == ngaydi).Count()
                        }).ToList())
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getghexe")]
        [HttpGet]
        public IHttpActionResult GetGheXe(string maxe, DateTime ngaydi)
        {
            try
            {
                using (var db = new DB())
                {
                    ngaydi = ngaydi.Date;
                    List<Ghe> ghes = db.TrangThaiGhes
                            .Where(x => x.Ghe.maxe == maxe)
                            .Select(x => x.Ghe)
                            .Distinct()
                            .ToList();
                    List<GheHang> ghesAllTang1 = new List<GheHang>();
                    List<GheHang> ghesAllTang2 = new List<GheHang>();
                    for (int i = 0; i < 7; i++)
                    {
                        GheHang gheHangTang1 = new GheHang()
                        {
                            Hang = (i + 1),
                            Tang = 1,
                            Ghes = new List<Ghe>()
                        };
                        GheHang gheHangTang2 = new GheHang()
                        {
                            Hang = (i + 1),
                            Tang = 2,
                            Ghes = new List<Ghe>()
                        };
                        for (int j = 0; j < 5; j++)
                        {
                            gheHangTang1.Ghes.Add(new Ghe()
                            {
                                active = false,
                                maghe = "",
                                maxe = maxe,
                                tang = 1,
                                vitriX = (j + 1),
                                vitriY = (i + 1)
                            });
                            gheHangTang2.Ghes.Add(new Ghe()
                            {
                                active = false,
                                maghe = "",
                                maxe = maxe,
                                tang = 2,
                                vitriX = (j + 1),
                                vitriY = (i + 1)
                            });
                        }
                        ghesAllTang1.Add(gheHangTang1);
                        ghesAllTang2.Add(gheHangTang2);
                    }
                    ghes.ForEach(x =>
                    {
                        Ghe ghe = ghesAllTang1.FirstOrDefault(y => y.Tang == x.tang && y.Hang == x.vitriY)?.Ghes.FirstOrDefault(y => y.vitriX == x.vitriX);
                        if (ghe != null)
                        {
                            ghe.maghe = x.maghe.Split('_')[1];
                            ghe.active = x.active;
                        }
                        else
                        {
                            ghe = ghesAllTang2.FirstOrDefault(y => y.Tang == x.tang && y.Hang == x.vitriY)
                                    .Ghes
                                    .FirstOrDefault(y => y.vitriX == x.vitriX);
                            if (ghe != null)
                            {
                                ghe.maghe = x.maghe.Split('_')[1];
                                ghe.active = x.active;
                            }
                        }
                    });
                    return Ok(new
                    {
                        ghetang1 = ghesAllTang1.Select(x => new
                        {
                            x.Hang,
                            x.Tang,
                            Ghes = x.Ghes.Select(y => new
                            {
                                y.maghe,
                                y.maxe,
                                y.vitriX,
                                y.vitriY,
                                y.active,
                                y.tang
                            })
                        }),
                        ghetang2 = ghesAllTang2.Select(x => new
                        {
                            x.Hang,
                            x.Tang,
                            Ghes = x.Ghes.Select(y => new
                            {
                                y.maghe,
                                y.maxe,
                                y.vitriX,
                                y.vitriY,
                                y.active,
                                y.tang
                            })
                        })
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
