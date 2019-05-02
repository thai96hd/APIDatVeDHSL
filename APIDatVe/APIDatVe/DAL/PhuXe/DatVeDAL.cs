using APIDatVe.DTO.PhuXe;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.PhuXe
{
    public class DatVeDAL
    {
        public bool datve(DatVeDTO datVeDTO)
        {
            SqlParameter[] parameters = new SqlParameter[] {

                new SqlParameter("@maghe",datVeDTO.maghe),
                new SqlParameter("@machuyenxe",datVeDTO.machuyenxe),
                new SqlParameter("@khachhangId", datVeDTO.khachhangId),
                new SqlParameter("@matrangthaive",datVeDTO.matrangthaive),
                new SqlParameter("@tongtien",datVeDTO.tongtien),
                new SqlParameter("@sokhach",datVeDTO.sokhach),
                new SqlParameter("@ghichu",datVeDTO.ghichu),
                new SqlParameter("@madiemtrungchuyendon",datVeDTO.madiemtrungchuyendon),
                new SqlParameter("@madiemtrungchuyentra",datVeDTO.madiemtrungchuyentra),
                new SqlParameter("@tenhanhkhach",datVeDTO.tenhanhkhach),
                new SqlParameter("@sodienthoaikhach",datVeDTO.sodienthoaikhach),
                new SqlParameter("@doituong",datVeDTO.doituong),
                new SqlParameter("@trangthai",datVeDTO.trangthai),
                new SqlParameter("@vexeId", Guid.NewGuid().ToString()),
                new SqlParameter ("@chitietvexeId", Guid.NewGuid().ToString()),

             };
            return DataProvider.Instance.ExecuteNonQuery("sp_journey_book", parameters) > 0;
        }
    }
}