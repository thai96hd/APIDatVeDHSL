using APIDatVe.DAL;
using APIDatVe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.Controllers
{
	[RoutePrefix("api/diemtrungchuyen1")]
	public class DiemTrungChuyenController : ApiController
	{
		[Route("danhsachdiemchuyenchuyen")]
		public List<DiemTrungChuyenDTO> getListDiemTrungChuyen(string malotrinh)
		{
			DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
			return diemTrungChuyenDAL.getListDiemTrungChuyen(malotrinh);
		}
	}
}
