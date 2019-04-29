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
	[RoutePrefix("api/bookingTicket")]
	public class DatVeController : ApiController
	{
		[HttpPost]
		[Route("addTicket")]
		public bool AddTicket([FromBody] VeXeDTO veXeDTO) {
			VeXeDAL veXeDAL = new VeXeDAL();
			return veXeDAL.AddTicket(veXeDTO);
		}

		[HttpPost]
		[Route("addTicketDetail")]
		public bool AddTicketDeTail([FromBody] ChiTietVeXeDTO chiTietVeXeDTO) {
			VeXeDAL veXeDAL = new VeXeDAL();
			return veXeDAL.AddTicketDeTail(chiTietVeXeDTO);
		}
		[HttpGet]
		[Route("getTicketDetailByTicketID")]
		public List<ChiTietVeXeDTO> getTicketDetailByTicketID(string ticketID) {
			VeXeDAL veXeDAL = new VeXeDAL();
			return veXeDAL.getTicketDetailByTicketID(ticketID);
		}
    }
}
