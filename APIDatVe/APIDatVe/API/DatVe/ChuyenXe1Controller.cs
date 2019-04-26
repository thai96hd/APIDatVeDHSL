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
		public IHttpActionResult getTrip([FromUri]string malotrinh, [FromUri] string pointStartID, string pointEndID, int day, int month, int year)
		{
			if (malotrinh == "")
				return BadRequest("malotrinh khong de trong");
			string date = "" + month + "/" + day + "/" + year;
			DateTime startDate = DateTime.Parse(date);
			List<ChuyenXeDTO> list = new ChuyenXeDAL().getListChuyenXe(malotrinh, startDate, pointStartID, pointEndID);
			return Ok(list);
		}

		[Route("getSchemaCarByCarIDAndTripID")]
		[HttpGet]
		public XeDTO getSchemaCarByCarIDAndTripID(string carID, string tripID)
		{
			ChuyenXeDAL chuyenXeDAL = new ChuyenXeDAL();
			return chuyenXeDAL.getSchemaCar(carID, tripID);

		}
	}
}