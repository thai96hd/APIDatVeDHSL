using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using APIDatVe.DTO.PhuXe;

namespace APIDatVe.DAL.PhuXe
{
	public class TaiKhoanDAL
	{
		public bool checkLogin(TaiKhoanDTO taiKhoanDTO, Int32 maChucVu)
		{

			String sql = "SELECT 1" +
			 " FROM taikhoan tk" +
			" JOIN NhanVien nv ON tk.hoten = nv.hoten" +
			" JOIN ChucVu cv ON cv.machucvu = nv.machucvu" +
			" WHERE nv.machucvu = @machucvu" +
			" AND tk.tentaikhoan = @taikhoan" +
			" AND tk.matkhau = @matkhau ";

			SqlCommand command = DataProvider.Instance.getCommand(sql);

			command.Parameters.AddWithValue("@taikhoan", taiKhoanDTO.tentaikhoan);

			command.Parameters.AddWithValue("@matkhau", taiKhoanDTO.matkhau);

			command.Parameters.AddWithValue("@machucvu", 3);

			SqlDataReader rowsAffected = command.ExecuteReader();

			if (rowsAffected.HasRows)
				return true;
			return false;
		}

		public bool updatePass(TaiKhoanDTO taiKhoanDTO)
		{

			String sql = "UPDATE TaiKhoan" +
						" SET matkhau = @matkhau" +
						" WHERE tentaikhoan = @taikhoan ";

			SqlCommand command = DataProvider.Instance.getCommand(sql);

			command.Parameters.AddWithValue("@taikhoan", taiKhoanDTO.tentaikhoan);

			command.Parameters.AddWithValue("@matkhau", taiKhoanDTO.matkhau);

			if (command.ExecuteNonQuery() == 1)
				return true;
			return false;
		}

        public DataTable GetThongTinPhuXe(string tentaikhoan, Int32 machucvu)
        {

            String sql = "SELECT *" +
                      "  FROM NhanVien nv" +
                     "   JOIN ChucVu cv ON nv.machucvu = cv.machucvu" +
                     "   WHERE cv.machucvu = @machucvu" +
                      "    AND nv.tentaikhoan = @tentaikhoan";

            SqlCommand command = DataProvider.Instance.getCommand(sql);


            command.Parameters.AddWithValue("@tentaikhoan", tentaikhoan);

            command.Parameters.AddWithValue("@machucvu", 3);

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