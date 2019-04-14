using APIDatVe.DAL.DatVe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace APIDatVe.API.DatVe
{
	[RoutePrefix("api/diemtrungchuyen1")]
	public  class DiemTrungChuyen1Controller : ApiController
	{
		[Route("danhsachdiemchuyenchuyen")]
		public IHttpActionResult getListDiemTrungChuyen([FromUri] string malotrinh)
		{
			if (malotrinh == "" || malotrinh == null)
			{
				return BadRequest("Mã lộ trình không được để chống!");
			}
			else
			{
				DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
				return Ok(diemTrungChuyenDAL.getListDiemTrungChuyen(malotrinh));
			}
		}
	}
}