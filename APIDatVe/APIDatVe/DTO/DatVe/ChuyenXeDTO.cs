using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDatVe.DTO.DatVe
{
	public class ChuyenXeDTO
	{
		public string tenlotrinh;
		public string machuyenxe;
		public string malotrinh;
		public string makip;
		public string maxe;
		public string mataixe;
		public string maphuxe;
		public DateTime ngayhoatdong;
		public DateTime thoigiandungxe;
		public string tenkip;
		public int gioxuatphat;
		public int phutxuatphat;
		public int tongsoghe;
		public int soghetrong;
		public BangGiaDTO banggia { get; set; }
	}
}