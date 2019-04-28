using APIDatVe.DTO.PhuXe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.PhuXe
{
    public class GiaVeDAL
    {
        public GiaVeDTO LayThongTinGiaVe(string madiemtrungchuyendon, string madiemtrungchuyentra)
        {
            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("@madiemtrungchuyendon",madiemtrungchuyendon),
                new SqlParameter("@madiemtrungchuyentra",madiemtrungchuyentra)
            };
            DataTable dt = DataProvider.Instance.GetData("sp_laythongtingiave", sqlParameters);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                GiaVeDTO bangGiaDTO = new GiaVeDTO();
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
                return new GiaVeDTO();
        }
    }
}