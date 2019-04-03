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
	/// <summary>
	/// API for KhachHang 
	/// </summary>
	[RoutePrefix("api/khachhang")]
	public class KhachHangController : ApiController
	{
		/// <summary>
		///    check login custommer
		/// </summary>
		/// <param name="username">email hoac so dien thoai cua khach hang</param>
		/// <param name="password"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("check_login")]
		public bool Login(string username, string password)
		{
			KhachHangDAL khachHangDAL = new KhachHangDAL();
			return khachHangDAL.checkLogin(username, password);
		}
		[HttpPost]
		[Route("add_customer")]
		public KhachHang AddCumstomer([FromBody] KhachHang kh)
		{
			return new KhachHangDAL().AddCustomer(kh);
		}
		/// <summary>
		/// get customer information by username :usrname is phonenumber or email
		/// </summary>
		/// <returns>customer information</returns>
		[HttpGet]
		[Route("get_customer/{username}")]
		public KhachHang FindCustomerByUserName(string username)
		{
			return new KhachHangDAL().FindCustomerByUserName(username);
		}

	}
}
