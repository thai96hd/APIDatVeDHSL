using APIDatVe.DTO.PhuXe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.PhuXe
{
	public class DiemTrungChuyenDAL
	{
		public List<DiemTrungChuyenDTO> GetDiemTrungChuyens()
		{
			List<DiemTrungChuyenDTO> diemTrungChuyens = new List<DiemTrungChuyenDTO>();
			String sql = "SELECT *" +
				   " FROM diemtrungchuyen";

			SqlCommand command = DataProvider.Instance.getCommand(sql);

			SqlDataReader rowsAffected = command.ExecuteReader();// lay dlieu -> ktra dlieu co ton tai hay khong

			if (rowsAffected.HasRows)
			{
				int sl = 0;
				while (rowsAffected.Read())
				{
					DiemTrungChuyenDTO diemTrungChuyen = new DiemTrungChuyenDTO();
					diemTrungChuyen.madiemtrungchuyen = rowsAffected.GetString(sl++);
					diemTrungChuyen.matinh = rowsAffected.GetString(sl++);
					diemTrungChuyen.tendiemtrungchuyen = rowsAffected.GetString(sl++);
					diemTrungChuyen.diachi = rowsAffected.IsDBNull(sl) ? "" : rowsAffected.GetString(sl++);
					diemTrungChuyen.lat = rowsAffected.IsDBNull(sl) ? "" : rowsAffected.GetString(sl++);
					diemTrungChuyen.longt = rowsAffected.IsDBNull(sl) ? "" : rowsAffected.GetString(sl++);

					diemTrungChuyens.Add(diemTrungChuyen);
					sl = 0;
				}
			}

			return diemTrungChuyens;
		}

		//lay ttin khach xg tai diem trung chuyen

		public DataTable GetKhachHangByDiemXuong(string madiemtrungchuyen, string machuyenxe)
		{

			String sql = "SELECT *" +
					   " FROM KhachHang kh" +
					  "  JOIN VeXe vx ON kh.khachhangId = vx.khachhangId" +
					   " JOIN DiemTrungChuyen dtc ON vx.madiemtrungchuyentra = dtc.madiemtrungchuyen" +
					   " JOIN ChuyenXe cx ON vx.machuyenxe = cx.machuyenxe" +
					  "  WHERE dtc.madiemtrungchuyen = @madiemtrungchuyen" +
					   "   AND cx.machuyenxe = @machuyenxe";
			SqlCommand command = DataProvider.Instance.getCommand(sql);

			command.Parameters.AddWithValue("@madiemtrungchuyen", madiemtrungchuyen);

			command.Parameters.AddWithValue("@machuyenxe", machuyenxe);

			SqlDataReader rowsAffected = command.ExecuteReader();

			var dataTable = new DataTable();
			if (rowsAffected.HasRows)
			{
				dataTable.Load(rowsAffected);
			}

			return dataTable;

		}

	}
}