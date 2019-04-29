
using APIDatVe.DAL.DatVe;
using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace APIDatVe.API.DatVe
{
	/// <summary>
	/// API for KhachHang 
	/// </summary>
	[RoutePrefix("api/customer")]
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
		public KhachHangDTO Login(string username, string password)
		{
			KhachHangDAL khachHangDAL = new KhachHangDAL();
			bool check = khachHangDAL.checkLogin(username, password);
			KhachHangDTO khach = new KhachHangDTO();
			if (check)
			{
				khach = khachHangDAL.FindCustomerByUserName(username);
				return khach;
			}
			else
				return null;
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
		/// <summary>
		///  Cập nhật thông tin khách hàng
		/// </summary>
		/// <param name="customerid"></param>
		/// <param name="khachHangDTO">body requet khachhang</param>
		/// <returns></returns>
		[Route("update_customer/{customerid}")]
		[HttpPost]
		public IHttpActionResult UpdateCustomerInfo(string customerid, [FromBody]KhachHangDTO khachHangDTO)
		{
			KhachHangDAL khachHangDAL = new KhachHangDAL();
			bool check = khachHangDAL.UpdateCustomerInfo(customerid, khachHangDTO);
			if (check)
			{
				KhachHangDTO khachHangDTO1 = khachHangDAL.FindCustomerByUserName(khachHangDTO.sodienthoai);
				return Ok(khachHangDTO1);
			}
			else
				return BadRequest("update not success");

		}


		[HttpGet]
		[Route("getListBookingTicketByCustomerID")]
		public List<KhachHangDatVeDTO> getListBookingTicketByCustomerID(string customerID)
		{
			KhachHangDAL khachHangDAL = new KhachHangDAL();
			return khachHangDAL.getListBookingTicketByCustomerID(customerID);
		}

		[HttpPost]
		[Route("changePasswordCustomer")]
		public bool changePasswordCutomer(string phoneNumber, string password)
		{
			KhachHangDAL khachHangDAL = new KhachHangDAL();
			return khachHangDAL.changePasswordCustomer(phoneNumber, password);
		}

		[HttpPost]
		[Route("addFeedbackCustomer")]
		public bool addFeedbackCustomer(KhachHangDanhGiaDTO khachHangDanhGiaDTO)
		{
			KhachHangDAL khachHangDAL = new KhachHangDAL();
			return khachHangDAL.addFeedbackCustomer(khachHangDanhGiaDTO);
		}
	}
}