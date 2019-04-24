using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDatVe.DTO.PhuXe
{
	public class TaiKhoanDTO
	{
		public string tentaikhoan { get; set; }
		public string maquyen { get; set; }
		public string matkhau { get; set; }
		public string hoten { get; set; }
		public string email { get; set; }
		public int? solandangnhapsai { get; set; }
		public int? trangthai { get; set; }
		public string linklaylaitaikhoan { get; set; }
		public DateTime? thoigianyeucaulaylaitk { get; set; }
        public string matkhaumoi { get; set; }
    }
}