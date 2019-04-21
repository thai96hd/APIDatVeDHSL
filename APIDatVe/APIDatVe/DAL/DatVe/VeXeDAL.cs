using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.DatVe
{
	public class VeXeDAL
	{
		public bool AddTicketDeTail(ChiTietVeXeDTO chiTietVeXeDTO) {
			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@chitietvexeid",Guid.NewGuid().ToString()),
				new SqlParameter("@vexeid",chiTietVeXeDTO.vexeid),
				new SqlParameter("@maghe",chiTietVeXeDTO.maghe),
				new SqlParameter("@tenhanhkhach",chiTietVeXeDTO.tenhanhkhach),
				new SqlParameter("@sodienthoai",chiTietVeXeDTO.sodienthoai),
				new SqlParameter("@doituong",chiTietVeXeDTO.doituong)
			};
			return DataProvider.Instance.ExecuteNonQuery("sp_addTicketDetail", parameters) > 0;
		}
		public bool AddTicket(VeXeDTO veXeDTO) {
			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@vexeid",Guid.NewGuid().ToString()),
				new SqlParameter("@machuyenxe",veXeDTO.machuyenxe),
				new SqlParameter("@khachhangid",veXeDTO.khachhangid),
				new SqlParameter("@matrangthaive",veXeDTO.matrangthaive),
				new SqlParameter("@tongtien",veXeDTO.tongtien+""),
				new SqlParameter("@sokhach",veXeDTO.sokhach),
				new SqlParameter("@ghichu",veXeDTO.ghichu),
				new SqlParameter("@madiemtrungchuyendon",veXeDTO.madiemtrungchuyendon),
				new SqlParameter("@madiemtrungchuyentra",veXeDTO.madiemtrungchuyentra)
			};
			return DataProvider.Instance.ExecuteNonQuery("sp_addTicket", parameters)>0;
		}
	}
}