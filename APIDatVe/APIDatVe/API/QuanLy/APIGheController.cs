using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using APIDatVe.Models;
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
						x.tenghe,
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

		[Route("get-all")]
		[HttpGet]
		[AcceptAction(ActionName = "GetAll", ControllerName = "APIGheController")]
		public IHttpActionResult GetAll(string _maxe)
		{
			try
			{
				using (var db = new DB())
				{
					List<Ghe> ghes = db.Ghes
							.Where(x => x.maxe == _maxe).OrderByDescending(x => x.ngaycapnhat)
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
								maxe = _maxe,
								tang = 1,
								vitriX = (j + 1),
								vitriY = (i + 1)
							});
							gheHangTang2.Ghes.Add(new EGhe()
							{
								active = false,
								maghe = "",
								maxe = _maxe,
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
						EGhe ghe = ghesAllTang1.FirstOrDefault(y => y.Tang == x.tang && y.Hang == x.vitriY)?.Ghes.FirstOrDefault(y => y.vitriX == x.vitriX);
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
								y.tenghe,
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
								y.tenghe,
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
							return BadRequest("Xe không t?n t?i");
						DateTime ngayCapNhat = DateTime.Now;
						_gheXe._ghes.ForEach(x =>
						{
							Ghe ghe = db.Ghes.FirstOrDefault(y => y.maghe == x.maghe);
							if (ghe == null)
							{
								db.Ghes.Add(new Ghe()
								{
									ngaycapnhat = ngayCapNhat,
									maxe = _gheXe._maxe,
									vitriX = x.vitriX,
									vitriY = x.vitriY,
									tang = x.tang,
									active = x.active,


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

		[Route("put-all")]
		[HttpPut]
		[AcceptAction(ActionName = "PutAll", ControllerName = "APIGheController")]
		public IHttpActionResult PutAll(GheXeAll _gheXe)
		{
			try
			{
				using (var db = new DB())
				{
					using (var transaction = db.Database.BeginTransaction())
					{
						Xe xe = db.Xes.FirstOrDefault(x => x.maxe == _gheXe.maxe);
						if (xe == null)
							return BadRequest("Xe không t?n t?i");
						DateTime ngayCapNhat = DateTime.Now;
						_gheXe.GheTang1.ForEach(x =>
						{
							x.Ghes.ForEach(y =>
							{
								if (!string.IsNullOrEmpty(y.maghe))
								{
									Ghe ghe = db.Ghes.FirstOrDefault(z => z.maghe == (_gheXe.maxe + "_" + y.maghe));
									if (ghe == null)
									{
										db.Ghes.Add(new Ghe()
										{
											active = y.active,

											maghe = (_gheXe.maxe + "_" + y.maghe),
											tenghe = y.maghe,
											maxe = _gheXe.maxe,
											ngaycapnhat = ngayCapNhat,
											tang = 1,
											vitriX = y.vitriX,
											vitriY = y.vitriY
										});
									}
									else
									{
										ghe.active = y.active;
										ghe.ngaycapnhat = ngayCapNhat;
										ghe.tang = 1;
										ghe.vitriX = y.vitriX;
										ghe.vitriY = y.vitriY;
									}
								}
							});
						});
						_gheXe.GheTang2.ForEach(x =>
						{
							x.Ghes.ForEach(y =>
							{
								if (!string.IsNullOrEmpty(y.maghe))
								{
									Ghe ghe = db.Ghes.FirstOrDefault(z => z.maghe == (_gheXe.maxe + "_" + y.maghe));
									if (ghe == null)
									{
										db.Ghes.Add(new Ghe()
										{
											active = y.active,
											maghe = (_gheXe.maxe + "_" + y.maghe),
											tenghe = y.maghe,
											maxe = _gheXe.maxe,
											ngaycapnhat = ngayCapNhat,
											tang = 2,
											vitriX = y.vitriX,
											vitriY = y.vitriY
										});
									}
									else
									{
										ghe.active = y.active;
										ghe.ngaycapnhat = ngayCapNhat;
										ghe.tang = 2;
										ghe.vitriX = y.vitriX;
										ghe.vitriY = y.vitriY;
									}
								}
							});
						});
						db.SaveChanges();
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

	public class GheHang
	{
		public int Hang { get; set; }
		public int Tang { get; set; }
		public List<EGhe> Ghes { get; set; }
	}
	public class GheXeAll
	{
		public string maxe { get; set; }
		public List<GheHang> GheTang1 { get; set; }
		public List<GheHang> GheTang2 { get; set; }
	}
}
