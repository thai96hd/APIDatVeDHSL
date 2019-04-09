using APIDatVe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL
{
	public class DiemTrungChuyenDAL
	{
		public List<DiemTrungChuyenDTO> getListDiemTrungChuyen(string malotrinh) {
			List<DiemTrungChuyenDTO> list = new List<DiemTrungChuyenDTO>();
			DataTable dt = DataProvider.Instance.GetData("sp_listdiemtrungchuyen", null);
			foreach (DataRow dr in dt.Rows) {
				DiemTrungChuyenDTO diemTrungChuyenDTO = new DiemTrungChuyenDTO();
				diemTrungChuyenDTO.madiemtrungchuyen = dr["madiemtrungchuyen"].ToString();
				diemTrungChuyenDTO.tendiemtrungchuyen = dr["tendiemtrungchuyen"].ToString();
				diemTrungChuyenDTO.diachi = dr["diachi"].ToString();
				diemTrungChuyenDTO.matinh = dr["matinh"].ToString();
				diemTrungChuyenDTO.tentinh = dr["tentinh"].ToString();
				list.Add(diemTrungChuyenDTO);
			}
			return list;
		}
	}
}