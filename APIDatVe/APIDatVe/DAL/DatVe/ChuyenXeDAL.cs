using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.DatVe
{
	public class ChuyenXeDAL
	{
		public List<LoTrinhDTO> getListRoute()
		{
			List<LoTrinhDTO> loTrinhDTOs = new List<LoTrinhDTO>();
			DataTable dt = DataProvider.Instance.GetDataQuerry("select *from LoTrinh");
			foreach (DataRow dr in dt.Rows)
			{
				LoTrinhDTO lotrinhDTO = new LoTrinhDTO();
				lotrinhDTO.malotrinh = dr["malotrinh"].ToString();
				lotrinhDTO.tenlotrinh = dr["tenlotrinh"].ToString();
				lotrinhDTO.matinhdon = dr["matinhdon"].ToString();
				lotrinhDTO.matinhtra = dr["matinhtra"].ToString();
				lotrinhDTO.khoangthoigiandukien = float.Parse(dr["khoangthoigiandukien"].ToString());
				loTrinhDTOs.Add(lotrinhDTO);
			}
			return loTrinhDTOs;
		}
		public List<ChuyenXeDTO> getListChuyenXe(string malotrinh, DateTime ngayhoatdong)
		{
			List<ChuyenXeDTO> chuyenXeDTOs = new List<ChuyenXeDTO>();
			SqlParameter[] sqlParameters = new SqlParameter[] {
				new SqlParameter("@malotrinh",malotrinh),
				new SqlParameter("@ngayhoatdong",ngayhoatdong)

			};
			DataTable dt = DataProvider.Instance.GetData("sp_gettripbytripId", sqlParameters);

			foreach (DataRow dr in dt.Rows)
			{
				ChuyenXeDTO cx = new ChuyenXeDTO();
			
				cx.malotrinh = dr["malotrinh"].ToString();
				cx.ngayhoatdong = DateTime.Parse(dr["ngayhoatdong"].ToString());
				DateTime  refDate=DateTime.Now;
				//cx.thoigiandungxe = DateTime.TryParse(dr["thoigiandungxe"].ToString(),out refDate);
				cx.tenkip = dr["tenkip"].ToString();
				cx.maxe = dr["maxe"].ToString();
				cx.makip = dr["makip"].ToString();
				cx.mataixe = dr["mataixe"].ToString();
				cx.maphuxe = dr["maphuxe"].ToString();
				cx.machuyenxe = dr["machuyenxe"].ToString();
				cx.gioxuatphat = int.Parse(dr["gio"].ToString());
				cx.phutxuatphat = int.Parse(dr["phut"].ToString());
				chuyenXeDTOs.Add(cx);
			}
			return chuyenXeDTOs;
		}
	}
}