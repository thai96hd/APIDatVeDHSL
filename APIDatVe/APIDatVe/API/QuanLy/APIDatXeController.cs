using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using APIDatVe.Helper;
using APIDatVe.Models;
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
                    List<string> maTinhThanhDiQua = db.ChiTietLoTrinhs.Where(x => x.malotrinh == _malotrinh).Select(x => x.TinhThanh.matinh).ToList() ;
                    if (maTinhThanhDiQua == null)
                        maTinhThanhDiQua = new List<string>();
                    List<DiemTrungChuyen> allDiemTrungChuyenTinhThanhDiQua = new List<DiemTrungChuyen>();
                    maTinhThanhDiQua.ForEach(x =>
                    {
                        allDiemTrungChuyenTinhThanhDiQua.AddRange(db.DiemTrungChuyens.Where(y => y.matinh == x).ToList());
                    });
                    allDiemTrungChuyenTinhThanhDiQua.ForEach(x =>
                    {
                        if (diemTrungChuyensDon.FirstOrDefault(y => y.madiemtrungchuyen == x.madiemtrungchuyen) == null)
                            diemTrungChuyensDon.Add(x);
                        if (diemTrungChuyensTra.FirstOrDefault(y => y.madiemtrungchuyen == x.madiemtrungchuyen) == null)
                            diemTrungChuyensTra.Add(x);
                    });
                    return Ok(new
                    {
                        diemTrungChuyensDon = diemTrungChuyensDon.Select(x => new
                        {
                            x.madiemtrungchuyen,
                            x.tendiemtrungchuyen,
                            x.TinhThanh.tentinh
                        }).ToList(),
                        diemTrungChuyensTra = diemTrungChuyensTra.Select(x => new
                        {
                            x.madiemtrungchuyen,
                            x.tendiemtrungchuyen,
                            x.TinhThanh.tentinh
                        }).ToList()
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
                        List<Database.TrangThaiGhe> trangThaiGhes = db.TrangThaiGhes.Where(y => y.Ghe.maxe == x.maxe && y.ngay.Value == ngaydi).ToList();
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
                            soghetrong = db.TrangThaiGhes.Where(y => y.Ghe.maxe == x.maxe && y.ngay.Value == ngaydi && y.trangthai == 0).Count()
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
        public IHttpActionResult GetGheXe(string machuyenxe, DateTime ngaydi)
        {
            try
            {
                using (var db = new DB())
                {
                    ngaydi = ngaydi.Date;
                    List<Database.TrangThaiGhe> trangThaiGhes = db.TrangThaiGhes
                            .Where(x => x.machuyenxe == machuyenxe)
                            .ToList();
                    List<GheHang> ghesAllTang1 = new List<GheHang>();
                    List<GheHang> ghesAllTang2 = new List<GheHang>();
                    for (int i = 0; i < 7; i++)
                    {
                        GheHang gheHangTang1 = new GheHang()
                        {
                            Hang = (i + 1),
                            Tang = 1,
                            Ghes = new List<EGhe>()
                        };
                        GheHang gheHangTang2 = new GheHang()
                        {
                            Hang = (i + 1),
                            Tang = 2,
                            Ghes = new List<EGhe>()
                        };
                        for (int j = 0; j < 5; j++)
                        {
                            gheHangTang1.Ghes.Add(new EGhe()
                            {
                                active = false,
                                maghe = "",
                                tang = 1,
                                vitriX = (j + 1),
                                vitriY = (i + 1),
                                trangthai = 0
                            });
                            gheHangTang2.Ghes.Add(new EGhe()
                            {
                                active = false,
                                maghe = "",
                                tang = 2,
                                vitriX = (j + 1),
                                vitriY = (i + 1),
                                trangthai = 0
                            });
                        }
                        ghesAllTang1.Add(gheHangTang1);
                        ghesAllTang2.Add(gheHangTang2);
                    }
                    trangThaiGhes.ForEach(x =>
                    {
                        EGhe ghe = ghesAllTang1.FirstOrDefault(y => y.Tang == x.Ghe.tang && y.Hang == x.Ghe.vitriY)?.Ghes.FirstOrDefault(y => y.vitriX == x.Ghe.vitriX);
                        if (ghe != null)
                        {
                            ghe.maghe = x.maghe.Split('_')[1];
                            ghe.active = x.Ghe.active;
                            ghe.trangthai = x.trangthai;
                        }
                        else
                        {
                            ghe = ghesAllTang2.FirstOrDefault(y => y.Tang == x.Ghe.tang && y.Hang == x.Ghe.vitriY)
                                    .Ghes
                                    .FirstOrDefault(y => y.vitriX == x.Ghe.vitriX);
                            if (ghe != null)
                            {
                                ghe.maghe = x.maghe.Split('_')[1];
                                ghe.active = x.Ghe.active;
                                ghe.trangthai = x.trangthai;
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
                                y.vitriX,
                                y.vitriY,
                                y.active,
                                y.tang,
                                y.trangthai
                            })
                        }),
                        ghetang2 = ghesAllTang2.Select(x => new
                        {
                            x.Hang,
                            x.Tang,
                            Ghes = x.Ghes.Select(y => new
                            {
                                y.maghe,
                                y.vitriX,
                                y.vitriY,
                                y.active,
                                y.tang,
                                y.trangthai
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
        public IHttpActionResult Post([FromBody] DatXe datXe)
        {
            try
            {
                using (var db = new DB())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DateTime dateTimeNow = DateTime.Now;
                        KhachHang khachHang = db.KhachHangs.FirstOrDefault(x => x.sodienthoai == datXe.khachhang.sodienthoai);
                        string khachhangid = DataHelper.RandomString(20);
                        if (khachHang != null)
                        {
                            khachHang.hoten = datXe.khachhang.hoten;
                            khachHang.email = datXe.khachhang.email;
                            khachHang.gioitinh = datXe.khachhang.gioitinh;
                            khachHang.diachi = datXe.khachhang.diachi;
                            khachhangid = khachHang.khachhangId;
                        }
                        else
                        {
                            datXe.khachhang.madoituong = "DTDEFAULT";
                            datXe.khachhang.diemtichluy = 0;
                            while (db.KhachHangs.FirstOrDefault(x => x.khachhangId == khachhangid) != null)
                            {
                                khachhangid = DataHelper.RandomString(20);
                            }
                            datXe.khachhang.khachhangId = khachhangid;
                            datXe.khachhang.matkhau = Encode.MD5(datXe.khachhang.sodienthoai);
                            db.KhachHangs.Add(datXe.khachhang);
                        }
                        db.SaveChanges();
                        string vexeid = DataHelper.RandomString(20);
                        while (db.VeXes.FirstOrDefault(x => x.vexeId == vexeid) != null)
                        {
                            vexeid = DataHelper.RandomString(20);
                        }
                        VeXe veXe = new VeXe()
                        {
                            vexeId = vexeid,
                            machuyenxe = datXe.machuyenxe,
                            khachhangId = khachhangid,
                            matrangthaive = 0,
                            sokhach = datXe.ghes.Count,
                            madiemtrungchuyendon = datXe.madiemtrungchuyendon,
                            madiemtrungchuyentra = datXe.madiemtrungchuyentra,
                            ngaydat = dateTimeNow,
                            tongtien = datXe.tongtien.ToString()
                        };
                        db.VeXes.Add(veXe);
                        db.SaveChanges();
                        string maxe = db.ChuyenXes.FirstOrDefault(x => x.machuyenxe == datXe.machuyenxe).maxe;
                        datXe.ghes.ForEach(x =>
                        {
                            Database.TrangThaiGhe trangThaiGhe = db.TrangThaiGhes.FirstOrDefault(y => (y.maghe == maxe + "_" + x) && y.machuyenxe == datXe.machuyenxe);
                            if (trangThaiGhe != null)
                                trangThaiGhe.trangthai = 1;

                            string chitietvexeid = DataHelper.RandomString(20);
                            while (db.ChiTietVeXes.FirstOrDefault(y => y.chitietvexeId == chitietvexeid) != null)
                            {
                                chitietvexeid = DataHelper.RandomString(20);
                            }
                            db.ChiTietVeXes.Add(new ChiTietVeXe()
                            {
                                chitietvexeId = chitietvexeid,
                                vexeId = veXe.vexeId,
                                tenhanhkhach = datXe.khachhang.hoten,
                                sodienthoaikhach = datXe.khachhang.sodienthoai,
                                maghe = maxe + "_" + x,
                                doituong = "DTDEFAULT"
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

        [Route("getdanhsachdatxe")]
        [HttpGet]
        public IHttpActionResult GetDanhSachDatXe(DateTime _tungay, DateTime _denngay, int _trang = 1, int _sobanghi = 100)
        {
            try
            {
                using (var db = new DB())
                {
                    _tungay = _tungay.Date;
                    _denngay = _denngay.Date;
                    List<VeXe> veXes = db.VeXes.Where(x => x.ngaydat.Value >= _tungay && x.ngaydat.Value <= _denngay).ToList();
                    int sobanghi = veXes.Count;
                    return Ok(new
                    {
                        vexes = veXes.Select(x => new
                        {
                            x.vexeId,
                            x.khachhangId,
                            x.KhachHang.hoten,
                            x.ChuyenXe.machuyenxe,
                            x.TrangThaiVeXe.tentrangthai,
                            x.sokhach,
                            diemdon = db.DiemTrungChuyens.FirstOrDefault(y => y.madiemtrungchuyen == x.madiemtrungchuyendon).tendiemtrungchuyen,
                            diemtra = db.DiemTrungChuyens.FirstOrDefault(y => y.madiemtrungchuyen == x.madiemtrungchuyentra).tendiemtrungchuyen,
                            ngaydat = x.ngaydat.Value.ToString("dd/MM/yyyy HH:mm"),
                            x.ChiTietVeXes.FirstOrDefault().sodienthoaikhach,
                            x.tongtien
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

        [Route("detailvexe")]
        [HttpGet]
        public IHttpActionResult DetailVeXe(string _vexeId)
        {
            try
            {
                using (var db = new DB())
                {
                    VeXe veXe = db.VeXes.FirstOrDefault(x => x.vexeId == _vexeId);
                    return Ok(new
                    {
                        ve = new
                        {
                            veXe.vexeId,
                            veXe.khachhangId,
                            veXe.KhachHang.hoten,
                            veXe.ChuyenXe.machuyenxe,
                            veXe.TrangThaiVeXe.tentrangthai,
                            veXe.TrangThaiVeXe.matrangthaive,
                            veXe.sokhach,
                            diemdon = db.DiemTrungChuyens.FirstOrDefault(y => y.madiemtrungchuyen == veXe.madiemtrungchuyendon).tendiemtrungchuyen,
                            diemtra = db.DiemTrungChuyens.FirstOrDefault(y => y.madiemtrungchuyen == veXe.madiemtrungchuyentra).tendiemtrungchuyen,
                            ngaydat = veXe.ngaydat.Value.ToString("dd/MM/yyyy HH:mm"),
                            veXe.ChiTietVeXes.FirstOrDefault().sodienthoaikhach,
                            veXe.tongtien
                        },
                        chitietves = veXe.ChiTietVeXes.Select(y => new
                        {
                            y.maghe
                        }),
                        khachhang = new
                        {
                            veXe.KhachHang.hoten,
                            veXe.KhachHang.DoiTuong.tendoituong,
                            veXe.KhachHang.diachi,
                            veXe.KhachHang.sodienthoai,
                            veXe.KhachHang.email,
                            veXe.KhachHang.gioitinh,
                            veXe.KhachHang.diemtichluy
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("thanhtoanvexe")]
        [HttpGet]
        public IHttpActionResult ThanhToanVeXe(string _vexeId)
        {
            try
            {
                using (var db = new DB())
                {
                    VeXe veXe = db.VeXes.FirstOrDefault(x => x.vexeId == _vexeId);
                    if (veXe.matrangthaive == 0)
                        veXe.matrangthaive = 1;
                    veXe.KhachHang.diemtichluy += int.Parse(veXe.tongtien);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("huyvexe")]
        [HttpGet]
        public IHttpActionResult HuyVeXe(string _vexeId)
        {
            try
            {
				using (var db = new DB())
				{
					VeXe veXe = db.VeXes.FirstOrDefault(x => x.vexeId == _vexeId);
					if (veXe.matrangthaive == 0)
						veXe.matrangthaive = 3;
					string machuyenxe = veXe.machuyenxe;
					IEnumerable<ChiTietVeXe> list = db.ChiTietVeXes.Where<ChiTietVeXe>(p => p.vexeId == veXe.vexeId);
					List<string> maghes = new List<string>();
					foreach (ChiTietVeXe ct in list) {
						Database.TrangThaiGhe trangthaighe = db.TrangThaiGhes.SingleOrDefault(p =>( p.maghe == ct.maghe) && (p.machuyenxe == machuyenxe));
						trangthaighe.trangthai = 0;
					}
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class DatXe
    {
        public string machuyenxe { get; set; }
        public string madiemtrungchuyendon { get; set; }
        public string madiemtrungchuyentra { get; set; }
        public string malotrinh { get; set; }
        public DateTime ngaydi { get; set; }
        public List<string> ghes { get; set; }
        public KhachHang khachhang { get; set; }
        public float tongtien { get; set; }
    }
}
