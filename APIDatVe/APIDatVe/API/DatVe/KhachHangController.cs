
using APIDatVe.DAL.DatVe;
using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace APIDatVe.API.DatVe
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
		public KhachHangDTO AddCumstomer([FromBody] KhachHangDTO kh)
		{
			return new KhachHangDAL().AddCustomer(kh);
		}
		/// <summary>
		/// get customer information by username :usrname is phonenumber or email
		/// </summary>
		/// <returns>customer information</returns>
		[HttpGet]
		[Route("get_customer/{username}")]
		public KhachHangDTO FindCustomerByUserName(string username)
		{
			return new KhachHangDAL().FindCustomerByUserName(username);
		}

		/// <summary>
		/// api check phone number co ton tai trong co so du lieu chua hoac emal co ton tai chua
		/// </summary>
		/// <param name="username">usernam co the la so dien thoai hoac email ham nay kiem tra ca so dien thoai hoac email</param>
		/// <returns></returns>
		[Route("checkUsername")]
		[HttpGet]
		public IHttpActionResult CheckPhoneNumberAndEmailExist([FromUri] string username)
		{
			if (username == "")
			{
				return BadRequest("username khong duoc chong");
			}
			string _contentMessage = "username is valid";
			KhachHangDAL khachHangDAL = new KhachHangDAL();
			List<KhachHangDTO> list = khachHangDAL.getKhachHangs();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].sodienthoai.Equals(username))
				{
					_contentMessage = "phonenumber duplicate";
					break;
				}
				if (list[i].email.Equals(username))
				{
					_contentMessage = "email duplicate";
					break;
				}
			}
			return Ok(_contentMessage);
		}
	}
}