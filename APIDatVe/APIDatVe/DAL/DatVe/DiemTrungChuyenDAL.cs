using APIDatVe.DTO.DatVe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.DatVe
{
	public class DiemTrungChuyenDAL
	{
		public List<DiemTrungChuyenDTO> getListDiemTrungChuyen(string malotrinh)
		{
			SqlParameter[] sqlParameters = new SqlParameter[] {
				new SqlParameter("@malotrinh",malotrinh)
			};
			List<DiemTrungChuyenDTO> list = new List<DiemTrungChuyenDTO>();
			DataTable dt = DataProvider.Instance.GetData("sp_listdiemtrungchuyen", sqlParameters);
			foreach (DataRow dr in dt.Rows)
			{
				DiemTrungChuyenDTO diemTrungChuyenDTO = new DiemTrungChuyenDTO();
				diemTrungChuyenDTO.madiemtrungchuyen = dr["madiemtrungchuyen"].ToString();
				diemTrungChuyenDTO.tendiemtrungchuyen = dr["tendiemtrungchuyen"].ToString();
				diemTrungChuyenDTO.diachi = dr["diachi"].ToString();
				diemTrungChuyenDTO.matinh = dr["matinh"].ToString();
				//	diemTrungChuyenDTO.tentinh = dr["tentinh"].ToString();
				list.Add(diemTrungChuyenDTO);
			}
			return list;
		}
		public BangGiaDTO LayThongTinGiaVe(string madiemtrungchuyendon, string madiemtrungchuyentra)
		{
			SqlParameter[] sqlParameters = new SqlParameter[] {
				new SqlParameter("@madiemtrungchuyendon",madiemtrungchuyendon),
				new SqlParameter("@madiemtrungchuyentra",madiemtrungchuyentra)
			};
			DataTable dt = DataProvider.Instance.GetData("sp_laythongtingiave", sqlParameters);
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				BangGiaDTO bangGiaDTO = new BangGiaDTO();
				if (dr != null)
				{
					bangGiaDTO.banggiaid = dr["banggiaid"].ToString();
					bangGiaDTO.thoigiandukien = float.Parse(dr["thoigiandukien"].ToString());
					bangGiaDTO.madiemtrungchuyendon = dr["madiemtrungchuyendon"].ToString();
					bangGiaDTO.madiemtrungchuyentra = dr["madiemtrungchuyentra"].ToString();
					bangGiaDTO.giave = float.Parse(dr["giave"].ToString());
				}
				return bangGiaDTO;
			}
			else
				return new BangGiaDTO();
		}
		public DiemTrungChuyenDTO getInforPointStartByID(string pointStartID) {
			SqlParameter[] sqlParameters = new SqlParameter[] {
				new SqlParameter("@madiemtrungchuyen",pointStartID)
			};
			DiemTrungChuyenDTO diemTrungChuyenDTO = new DiemTrungChuyenDTO();
			DataTable dt = DataProvider.Instance.GetData("sp_getInfoPointStartById",sqlParameters);
			if (dt.Rows.Count > 0) {
				DataRow dr = dt.Rows[0];
				diemTrungChuyenDTO.madiemtrungchuyen = dr["madiemtrungchuyen"].ToString();
				diemTrungChuyenDTO.tendiemtrungchuyen = dr["tendiemtrungchuyen"].ToString();
				diemTrungChuyenDTO.diachi = dr["diachi"].ToString();
			}
			return diemTrungChuyenDTO;
		}
    }
}