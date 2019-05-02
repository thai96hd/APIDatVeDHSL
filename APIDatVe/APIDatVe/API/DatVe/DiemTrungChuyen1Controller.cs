using APIDatVe.DAL.DatVe;
using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace APIDatVe.API.DatVe
{
	[RoutePrefix("api/transshipmentpoint")]
	public class DiemTrungChuyen1Controller : ApiController
	{
		[Route("getListTransshipmentPoint")]
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
		/// <summary>
		/// Lấy thông tin giá vé
		/// </summary>
		/// <param name="_madiemdon"></param>
		/// <param name="_madiemden"></param>
		/// <returns></returns>
		[Route("getFeeCar")]
		[HttpGet]
		public IHttpActionResult getGiaVeGiuaHaiDiem(string _madiemdon, string _madiemden)
		{
			if (_madiemdon == "" || _madiemden == "")
			{
				return BadRequest("Mã điểm đón và mã điểm đến không được để trống");
			}

			else
			{
				DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
				BangGiaDTO bangia = diemTrungChuyenDAL.LayThongTinGiaVe(_madiemdon, _madiemden);
				return Ok(bangia);
			}
		}

		[HttpGet]
		[Route("getInfoPointDepartByID")]
		public IHttpActionResult getInfoPointDepartByID(string pointStartID)
		{
			if (pointStartID == "")
			{
				return BadRequest("pointStartID not empty");
			}
			else
			{
				DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
				return Ok(diemTrungChuyenDAL.getInforPointStartByID(pointStartID));
			}
		}
	}
}