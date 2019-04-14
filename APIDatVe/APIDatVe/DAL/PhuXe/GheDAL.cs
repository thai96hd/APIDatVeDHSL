using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.PhuXe
{
    public class GheDAL
    {

        public DataTable GetGhes(String maxe, String maghe)
        {
            String sql = " SELECT ttghe.trangthai," +
                       "  g.*," +
                         "  i.*" +
                 "  FROM" +
                 "     (SELECT vx.vexeId, vx.ghichu," +
                   "           ct.maghe," +
                    "      kh.*" +
                    "   FROM VeXe vx" +
                    "   JOIN ChuyenXe cx ON cx.machuyenxe = vx.machuyenxe" +
                   "    JOIN Xe xe ON xe.maxe = cx.maxe" +
                    "   JOIN KhachHang kh ON kh.khachhangId = vx.khachhangId" +
                    "   JOIN ChiTietVeXe ct ON ct.vexeId = vx.vexeId" +
                   "    WHERE xe.maxe = 1" +
                   "      AND cx.ngayhoatdong =  Convert(datetime, @ngayhoatdong, 103)) AS i" +
                  "  RIGHT JOIN Ghe g ON g.maghe = i.maghe" +
                  "  JOIN TrangThaiGhe ttghe ON ttghe.maghe = g.maghe" +
                 "   WHERE g.active = 1" +
                 "    AND ttghe.ngay = Convert(datetime, @ngayhoatdong, 103)";

            if (!String.IsNullOrWhiteSpace(maghe))
            {
                sql += " AND g.maghe=@maghe";
            }

            DateTime dateTime = DateTime.Now;

            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@maxe", maxe);

            command.Parameters.AddWithValue("@ngayhoatdong", dateTime.ToString("dd-MM-yyyy"));

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


        public DataTable GetSoDoXe(string maxe)
        {
            String sql = " SELECT ghe.tang," +
             "  ghe.vitriX," +
            "   ghe.vitriY," +
           "    ghe.active," +
          "     ghe.tenghe" +
   "     FROM Xe xe" +
"         JOIN Ghe ghe ON xe.maxe = ghe.maxe" +
"    where xe.maxe= @maxe ";

            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@maxe", maxe);

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