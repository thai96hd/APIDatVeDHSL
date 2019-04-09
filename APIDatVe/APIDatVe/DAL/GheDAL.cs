using APIDatVe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL
{
    public class GheDAL
    {

        public DataTable GetGhes(String maxe, String maghe)
        {
            String sql = "SELECT ttghe.trangthai," +
                        "  g.*," +
                         "  i.*" +
                   " FROM" +
                     " (SELECT vx.*," +
                          "    ct.maghe," +
                          " kh.* " +
                      " FROM VeXe vx" +
                      " JOIN ChuyenXe cx ON cx.machuyenxe = vx.machuyenxe" +
                    "   JOIN Xe xe ON xe.maxe = cx.maxe" +
                     "  JOIN KhachHang kh ON kh.khachhangId = vx.khachhangId" +
                     "  JOIN ChiTietVeXe ct ON ct.vexeId = vx.vexeId" +
                    "   WHERE xe.maxe = @maxe" +
                    "     AND cx.ngayhoatdong = @ngayhoatdong) AS i" +
                  "  RIGHT JOIN Ghe g ON g.maghe = i.maghe" +
                 "   JOIN TrangThaiGhe ttghe ON ttghe.maghe = g.maghe" +
                 "   WHERE g.active = 1" +
                 "     AND ttghe.ngay = @ngay";

            if (!String.IsNullOrWhiteSpace(maghe))
            {
                sql += " AND g.maghe=@maghe";
            }

            DateTime dateTime = DateTime.Now;

            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@maxe", maxe);

            command.Parameters.AddWithValue("@ngayhoatdong", dateTime.ToString("dd-mm-yy"));

            command.Parameters.AddWithValue("@ngay", dateTime.ToString("dd-mm-yy"));

            if (!String.IsNullOrWhiteSpace(maghe))
            {
                command.Parameters.AddWithValue("@maghe", maghe);
            }

            SqlDataReader rowsAffected = command.ExecuteReader();

            var dataTable = new DataTable();
            if (rowsAffected.HasRows)
            {
                dataTable.Load(rowsAffected);
            }



            return dataTable;
        }
    }
}