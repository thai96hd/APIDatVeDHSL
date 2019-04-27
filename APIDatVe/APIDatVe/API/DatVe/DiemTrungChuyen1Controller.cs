using APIDatVe.DAL.DatVe;
using APIDatVe.DTO.DatVe;
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
		[Route("laythongtingiave")]
		[HttpGet]
		public IHttpActionResult getGiaVeGiuaHaiDiem(string _madiemdon,string _madiemden) {
			if (_madiemdon == "" || _madiemden == "")
			{
				return BadRequest("Mã điểm đón và mã điểm đến không được để trống");
			}

			else {
				DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
				BangGiaDTO bangia=diemTrungChuyenDAL.LayThongTinGiaVe(_madiemdon, _madiemden);
				return Ok(bangia);
			}
		}
		public IHttpActionResult getInfoPointDepartByID(string pointStartID) {
			if (pointStartID == "")
			{
				return BadRequest("pointStartID not empty");
			}
			else {
				DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
				return Ok(diemTrungChuyenDAL.getInforPointStartByID(pointStartID));
			}
		}
	}
}