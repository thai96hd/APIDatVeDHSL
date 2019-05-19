using APIDatVe.DTO.PhuXe;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.PhuXe
{
	public class TrangThaiGheDAL
	{
		public bool updateGhe(TrangThaiGheDTO trangThaiGhe)
		{

			SqlParameter[] parameters = new SqlParameter[] {
				//new SqlParameter("@maghe",trangThaiGhe.maghe),
				new SqlParameter("@trangthai",trangThaiGhe.trangthai),
				new SqlParameter("@vexeid",trangThaiGhe.vexeId),
				new SqlParameter("@matrangthaive",trangThaiGhe.matrangthaive)

			};
			if (DataProvider.Instance.ExecuteNonQuery("updatetrangthaighe", parameters) > 0)
				return true;
			return false;
		}
	}
}