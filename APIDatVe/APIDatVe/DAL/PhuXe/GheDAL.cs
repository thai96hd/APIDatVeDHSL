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
        public DataTable GetGhes(String machuyenxe, String maghe)
        {
            String sql = " SELECT ttghe.trangthai," +
                         "  g.*," +
                          " i.vexeId," +
                          " i.ghichu," +
                          " i.matrangthaive," +
                     " (SELECT DISTINCT tendiemtrungchuyen" +
                      " FROM DiemTrungChuyen dtc" +
                     "  WHERE dtc.madiemtrungchuyen = i.madiemtrungchuyendon )AS diemtrungchuyendon," +
                     " (SELECT DISTINCT tendiemtrungchuyen" +

                      "  FROM DiemTrungChuyen dtc" +

                       " WHERE dtc.madiemtrungchuyen = i.madiemtrungchuyentra)AS diemtrungchuyentra," +
                        "     i.hoten," +
                         "  i.sodienthoai," +
                          " i.tenhanhkhach," +
                          " i.sodienthoaikhach," +
                          " i.diachi," +
                          " i.doituong," +
                          " i.email," +
                          " i.gioitinh," +
                          " i.madoituong," +
                          " i.tongtien" +
                   " FROM" +
                   "   (SELECT vx.vexeId," +
                    "          vx.matrangthaive," +
                     "         vx.ghichu," +
                      "        vx.tongtien," +
                       "       vx.madiemtrungchuyendon," +
                        "      vx.madiemtrungchuyentra," +
                         "     ct.maghe," +
                          "    ct.tenhanhkhach," +
                           "   ct.doituong," +
                            "  ct.sodienthoaikhach," +
                             " kh.*" +
                    "   FROM VeXe vx" +
                     "  JOIN ChuyenXe cx ON cx.machuyenxe = vx.machuyenxe" +
                      " JOIN Xe xe ON xe.maxe = cx.maxe" +
                      " JOIN KhachHang kh ON kh.khachhangId = vx.khachhangId" +
                      " JOIN ChiTietVeXe ct ON ct.vexeId = vx.vexeId" +
                      " WHERE cx.machuyenxe = @machuyenxe) AS i" +
                   " RIGHT JOIN Ghe g ON g.maghe = i.maghe" +
                   " JOIN TrangThaiGhe ttghe ON ttghe.maghe = g.maghe" +
                   " WHERE g.active = 1" +
                   "   AND ttghe.machuyenxe = @machuyenxe";

            if (!String.IsNullOrWhiteSpace(maghe))
            {
                sql += " AND g.maghe=@maghe";
            }

            DateTime dateTime = DateTime.Now;

            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@machuyenxe", machuyenxe);

            //   command.Parameters.AddWithValue("@ngayhoatdong", dateTime.ToString("dd-MM-yyyy"));

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