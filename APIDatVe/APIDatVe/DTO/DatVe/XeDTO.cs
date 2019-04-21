using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDatVe.DTO.DatVe
{
	public class XeDTO
	{
		public List<GheDTO> floor1 { get; set; }
		public List<GheDTO> floor2 { get; set; }
		public string maxe { get; set; }
		public int sohang { get; set; }
		public int socot { get; set; }
		public int soghe { get; set; }
	}
}