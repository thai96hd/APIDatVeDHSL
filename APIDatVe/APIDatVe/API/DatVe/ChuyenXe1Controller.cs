using APIDatVe.DAL.DatVe;
using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace APIDatVe.API.DatVe
{
	/// <summary>
	///  api trip code by ThaiTran
	/// </summary>
	[RoutePrefix("api/trip")]
	public class ChuyenXe1Controller : ApiController
	{
		[Route("getRoute")]
		public IHttpActionResult getRoutes()
		{
			ChuyenXeDAL ChuyenXeDAL = new ChuyenXeDAL();
			//List<LoTrinhDTO> loTrinhDTOs = tripDAL.getListRoute();
			return Ok(ChuyenXeDAL.getListRoute());
		}
		[Route("getTripByIdAndDateStart/{day}/{month}/{year}")]
		public IHttpActionResult getTrip([FromUri]string malotrinh, int day, int month, int year)
		{
			if (malotrinh == "")
				return BadRequest("lotrinhid khong de trong");
			string date = "" + month + "/" + day + "/" + year;
			DateTime start = DateTime.Parse(date);
			List<ChuyenXeDTO> list = new ChuyenXeDAL().getListChuyenXe(malotrinh, start);
			return Ok(list);
		}
	}
}