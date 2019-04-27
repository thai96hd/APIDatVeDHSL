using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDatVe.DTO.DatVe
{
	public class KhachHangDatVeDTO
	{
		public string makhachhang { get; set; }
		public string vexeid { get; set; }
		public string machuyenxe { get; set; }
		public DiemTrungChuyenDTO diemdon { get; set; }
		public DiemTrungChuyenDTO diemtra { get; set; }
		public string tongtien { get; set; }
		public int trangthaive { get; set; }
		public int sokhach { get; set; }
		public DateTime ngaydat { get; set; }
		public List<ChiTietVeXeDTO> danhsachve { get; set; }
	}
}