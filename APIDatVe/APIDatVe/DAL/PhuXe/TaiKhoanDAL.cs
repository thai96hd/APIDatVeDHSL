using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.PhuXe
{
	public class TaiKhoanDAL
	{
		public bool checkLogin(string username, string password, Int32 maChucVu)
		{

			String sql = "SELECT 1" +
			 " FROM taikhoan tk" +
			" JOIN NhanVien nv ON tk.hoten = nv.hoten" +
			" JOIN ChucVu cv ON cv.machucvu = nv.machucvu" +
			" WHERE nv.machucvu = @machucvu" +
			" AND tk.tentaikhoan = @taikhoan" +
			" AND tk.matkhau = @matkhau ";

			SqlCommand command = DataProvider.Instance.getCommand(sql);

			command.Parameters.AddWithValue("@taikhoan", username);

			command.Parameters.AddWithValue("@matkhau", password);

			command.Parameters.AddWithValue("@machucvu", 3);

			SqlDataReader rowsAffected = command.ExecuteReader();

			if (rowsAffected.HasRows)
				return true;
			return false;
		}

		public bool updatePass(string username, string password)
		{

			String sql = "UPDATE TaiKhoan" +
						" SET matkhau = @matkhau" +
						" WHERE tentaikhoan = @taikhoan ";

			SqlCommand command = DataProvider.Instance.getCommand(sql);

			command.Parameters.AddWithValue("@taikhoan", username);

			command.Parameters.AddWithValue("@matkhau", password);

			if (command.ExecuteNonQuery() == 1)
				return true;
			return false;
		}
	}
}