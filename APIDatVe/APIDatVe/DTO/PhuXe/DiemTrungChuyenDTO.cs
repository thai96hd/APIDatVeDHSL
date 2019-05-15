using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDatVe.DTO.PhuXe
{
	public class DiemTrungChuyenDTO
	{
		public String madiemtrungchuyen { get; set; }
		public String matinh { get; set; }
		public String tendiemtrungchuyen { get; set; }
		public String diachi { get; set; }
		public String lat { get; set; }
		public String longt { get; set; }
        public Int32 thutu { get; set; }
	}
}