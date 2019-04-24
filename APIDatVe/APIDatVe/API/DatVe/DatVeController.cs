using APIDatVe.DAL.DatVe;
using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.DatVe
{
	[RoutePrefix("api/datve")]
	public class DatVeController : ApiController
	{
		[HttpPost]
		[Route("themve")]
		public bool AddTicket([FromBody] VeXeDTO veXeDTO) {
			VeXeDAL veXeDAL = new VeXeDAL();
			return veXeDAL.AddTicket(veXeDTO);
		}

		[HttpPost]
		[Route("themchitietve")]
		public bool AddTicketDeTail([FromBody] ChiTietVeXeDTO chiTietVeXeDTO) {
			VeXeDAL veXeDAL = new VeXeDAL();
			return veXeDAL.AddTicketDeTail(chiTietVeXeDTO);
		}
    }
}
