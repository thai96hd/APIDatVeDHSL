
using APIDatVe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL
{
	public class KhachHangDAL
	{
		public bool checkLogin(string phonenumber, string password)
		{
			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@sodienthoai",phonenumber),
				new SqlParameter("@matkhau",password)
			};
			int check = DataProvider.Instance.DangNhap("sp_checkLogin", parameters);
			if (check > 0)
				return true;
			return false;
		}
		public KhachHang AddCustomer(KhachHang kh)
		{
			string makhachhang = "KH" + Guid.NewGuid().ToString();
			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@makhachhang",makhachhang),
				new SqlParameter("@hoten",kh.hoten),
				new SqlParameter("@diachi",kh.diachi),
				new SqlParameter("@sodienthoai",kh.sodienthoai),
				new SqlParameter ("@email",kh.email),
				new SqlParameter("@matkhau",kh.matkhau),
				new SqlParameter("@gioitinh",kh.gioitinh)
			};
			if (DataProvider.Instance.ExecuteNonQuery("sp_themkhachhang", parameters) > 0)
				return kh;
			return null;
		}
		public KhachHang FindCustomerByUserName(string username)
		{
			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@username",username)
			};
			DataTable dt = new DataTable();
			dt = DataProvider.Instance.GetData("sp_search_customerbyusername", parameters);
			if (dt.Rows.Count > 0)
			{
				KhachHang kh = new KhachHang();
				DataRow dr = dt.Rows[0];
				kh.makhachhang = dr["khachhangid"].ToString();
				kh.hoten = dr["hoten"].ToString();
				kh.gioitinh = dr["gioitinh"].ToString();
				kh.sodienthoai = dr["sodienthoai"].ToString();
				kh.matkhau = dr["matkhau"].ToString();
				kh.madoituong = dr["madoituong"].ToString();
				kh.email = dr["email"].ToString();
				kh.diemtichluy = (int)float.Parse(dr["diemtichluy"].ToString());
				kh.diachi = dr["diachi"].ToString();
			
				return kh;
			}
			else return null;
		}
		public bool UpdatePassWord(string username, string password)
		{

			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@username",username),
				new SqlParameter("@password",password)
			};
			if (DataProvider.Instance.ExecuteNonQuery("sp_update_password_customer", parameters) > 0)
				return true;
			return false;
		}
	}
}