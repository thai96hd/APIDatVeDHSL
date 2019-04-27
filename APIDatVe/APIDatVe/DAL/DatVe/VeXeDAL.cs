using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.DatVe
{
	public class VeXeDAL
	{
		public bool AddTicketDeTail(ChiTietVeXeDTO chiTietVeXeDTO)
		{
			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@chitietvexeid",Guid.NewGuid().ToString()),
				new SqlParameter("@vexeid",chiTietVeXeDTO.vexeid),
				new SqlParameter("@maghe",chiTietVeXeDTO.maghe),
				new SqlParameter("@tenhanhkhach",chiTietVeXeDTO.tenhanhkhach),
				new SqlParameter("@sodienthoai",chiTietVeXeDTO.sodienthoai),
				new SqlParameter("@doituong",chiTietVeXeDTO.doituong),
				new SqlParameter("@machuyenxe",chiTietVeXeDTO.machuyenxe)
			};
			return DataProvider.Instance.ExecuteNonQuery("sp_addTicketDetail", parameters) > 0;
		}
		public bool AddTicket(VeXeDTO veXeDTO)
		{
			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@vexeid",veXeDTO.vexeid),
				new SqlParameter("@machuyenxe",veXeDTO.machuyenxe),
				new SqlParameter("@khachhangid",veXeDTO.khachhangid),
				new SqlParameter("@matrangthaive",veXeDTO.matrangthaive),
				new SqlParameter("@tongtien",veXeDTO.tongtien+""),
				new SqlParameter("@sokhach",veXeDTO.sokhach),
				new SqlParameter("@ghichu",veXeDTO.ghichu),
				new SqlParameter("@madiemtrungchuyendon",veXeDTO.madiemtrungchuyendon),
				new SqlParameter("@madiemtrungchuyentra",veXeDTO.madiemtrungchuyentra)
			};
			return DataProvider.Instance.ExecuteNonQuery("sp_addTicket", parameters) > 0;
		}
		public List<ChiTietVeXeDTO> getTicketDetailByTicketID(string ticketID)
		{
			SqlParameter[] sqlParameters = new SqlParameter[] {
				new SqlParameter("@vexeid",ticketID)
			};
			List<ChiTietVeXeDTO> list = new List<ChiTietVeXeDTO>();
			DataTable dt = DataProvider.Instance.GetData("getTicketDetailByTicketID", sqlParameters);
			foreach (DataRow dr in dt.Rows)
			{
				ChiTietVeXeDTO chiTietVeXeDTO = new ChiTietVeXeDTO();
				chiTietVeXeDTO.sodienthoai = dr["sodienthoaikhach"].ToString();
				chiTietVeXeDTO.tenhanhkhach = dr["tenhanhkhach"].ToString();
				chiTietVeXeDTO.maghe = dr["maghe"].ToString();
				chiTietVeXeDTO.chitietvexeid = dr["chitietvexeid"].ToString();
				chiTietVeXeDTO.doituong = dr["doituong"].ToString();
				chiTietVeXeDTO.vexeid = dr["vexeid"].ToString();
				list.Add(chiTietVeXeDTO);
			}
			return list;
		}
	}
}