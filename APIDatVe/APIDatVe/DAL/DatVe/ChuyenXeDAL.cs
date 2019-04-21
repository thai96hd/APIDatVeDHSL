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
				DateTime refDate = DateTime.Now;
				//cx.thoigiandungxe = DateTime.TryParse(dr["thoigiandungxe"].ToString(),out refDate);
				cx.tenkip = dr["tenkip"].ToString();
				cx.maxe = dr["maxe"].ToString();
				cx.makip = dr["makip"].ToString();
				cx.mataixe = dr["mataixe"].ToString();
				cx.maphuxe = dr["maphuxe"].ToString();
				cx.machuyenxe = dr["machuyenxe"].ToString();
				cx.gioxuatphat = int.Parse(dr["gio"].ToString());
				cx.phutxuatphat = int.Parse(dr["phut"].ToString());
				cx.tenlotrinh = dr["tenlotrinh"].ToString();
				chuyenXeDTOs.Add(cx);
			}
			return chuyenXeDTOs;
		}

		public XeDTO getSchemaCar(string carID, string tripID)
		{
			XeDTO xeDTO = new XeDTO();
			xeDTO.floor1 = new List<GheDTO>();
			xeDTO.floor2 = new List<GheDTO>();
			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@maxe",carID)
			};
			DataTable dt = new DataTable();
			dt = DataProvider.Instance.GetData("sp_getSechemaCar", parameters);

			//init defalut value
			int sohang = 7;
			int soghe = 35;
			int socot = 5;
			foreach (DataRow dr in dt.Rows)
			{
				GheDTO gheDTO = new GheDTO();
				gheDTO.maghe = dr["maghe"].ToString();
				gheDTO.vitriX = int.Parse(dr["vitriX"].ToString());
				gheDTO.vitriY = int.Parse(dr["vitriY"].ToString());
				gheDTO.tang = int.Parse(dr["tang"].ToString());
				gheDTO.isActive = bool.Parse(dr["active"].ToString());
				gheDTO.tenghe = dr["tenghe"].ToString();
				gheDTO.status = 0;
				////////////////
				///
				
				int.TryParse(dr["sohang"].ToString(), out sohang);
				
				int.TryParse(dr["socot"].ToString(), out socot);
				xeDTO.socot = socot;
				xeDTO.sohang = sohang;
				xeDTO.maxe = carID;
				
				int.TryParse(dr["soghe"].ToString(), out soghe);
				xeDTO.soghe = soghe;
				if (gheDTO.tang == 1)
				{
					xeDTO.floor1.Add(gheDTO);
				}
				else if (gheDTO.tang == 2)
				{
					xeDTO.floor2.Add(gheDTO);
				}
			}
			List<GheDTO> listTrangThaiGhe = getGheStatusByTripID(tripID);
			foreach (GheDTO ghe in listTrangThaiGhe)
			{
				if (ghe.status == 1) {
					GheDTO ghe1 = xeDTO.floor1.SingleOrDefault(p => p.maghe == ghe.maghe);
					GheDTO ghe2 = xeDTO.floor2.SingleOrDefault(p => p.maghe == ghe.maghe);
					if (ghe1 != null)
					{
						xeDTO.floor1.Single(p => p.maghe == ghe.maghe).status = 1;
					}
					else if (ghe2 != null) {
						xeDTO.floor2.Single(p => p.maghe == ghe.maghe).status = 1;
					}
				}
			}
			return xeDTO;
		}
		public List<GheDTO> getGheStatusByTripID(string tripID)
		{
			List<GheDTO> gheDTOs = new List<GheDTO>();

			SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@machuyenxe",tripID)
			};
			DataTable dt = new DataTable();
			dt = DataProvider.Instance.GetData("sp_getStatusSeatByTripId", parameters);
			foreach (DataRow dr in dt.Rows)
			{
				GheDTO gheDTO = new GheDTO();
				gheDTO.maghe = dr["maghe"].ToString();
				gheDTO.status = int.Parse(dr["trangthai"].ToString());
				gheDTO.vitriX = int.Parse(dr["vitriX"].ToString());
				gheDTO.vitriY = int.Parse(dr["vitriY"].ToString());
				gheDTO.tang = int.Parse(dr["tang"].ToString());
				gheDTO.status = int.Parse(dr["trangthai"].ToString());
				gheDTOs.Add(gheDTO);
			}
			return gheDTOs;
		}
	}
}