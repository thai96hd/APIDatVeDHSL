using APIDatVe.DTO.PhuXe;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.PhuXe
{
    public class ChuyenXeDAL
    {
        public List<LichTrinhPhuXeDTO> GetLichTrinhPhuXes(string tentaikhoan, string manhanvien)
        {
            List<LichTrinhPhuXeDTO> lichTrinhPhuXes = new List<LichTrinhPhuXeDTO>();
            String sql = "SELECT cx.ngayhoatdong," +
                        " lt.tenlotrinh," +
                         "  kip.tenkip," +
                         " xe.biensoxe" +
                        " FROM NhanVien nv" +
                            " JOIN ChuyenXe cx ON nv.manhanvien = cx.maphuxe" +
                           " JOIN Kip kip ON cx.makip = kip.makip" +
                          "  JOIN LoTrinh lt ON cx.malotrinh = lt.malotrinh" +
                          "  JOIN Xe xe ON cx.maxe = xe.maxe" +
                           " WHERE cx.ngayhoatdong<Convert(datetime,@ngayhoatdong,103)";

            if (!String.IsNullOrEmpty(tentaikhoan))
            {
                sql += " AND nv.tentaikhoan = @tentaikhoan";
            }
            if (!String.IsNullOrEmpty(manhanvien))
            {
                sql += " AND nv.manhanvien = @manhanvien";
            }

            sql += " order by cx.ngayhoatdong desc";

            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@ngayhoatdong", DateTime.Now.ToString("dd-MM-yyyy"));

            if (!String.IsNullOrEmpty(tentaikhoan))
            {
                command.Parameters.AddWithValue("@tentaikhoan", tentaikhoan);
            }

            if (!String.IsNullOrEmpty(manhanvien))
            {
                command.Parameters.AddWithValue("@manhanvien", manhanvien);
            }


            SqlDataReader rowsAffected = command.ExecuteReader();

            if (rowsAffected.HasRows)
            {
                int sl = 0;
                while (rowsAffected.Read())
                {
                    LichTrinhPhuXeDTO lichTrinhPhuXeDTO = new LichTrinhPhuXeDTO();
                    lichTrinhPhuXeDTO.ngayhoatdong = rowsAffected.GetDateTime(sl++);
                    lichTrinhPhuXeDTO.tenlotrinh = rowsAffected.GetString(sl++);
                    lichTrinhPhuXeDTO.tenkip = rowsAffected.GetString(sl++);
                    lichTrinhPhuXeDTO.biensoxe = rowsAffected.GetString(sl++);

                    lichTrinhPhuXes.Add(lichTrinhPhuXeDTO);
                    sl = 0;
                }
            }
            return lichTrinhPhuXes;
        }

        public List<LichTrinhPhuXeDTO> GetLichTrinhDateNow(string tentaikhoan, string manhanvien)
        {
            List<LichTrinhPhuXeDTO> lichTrinhPhuXes = new List<LichTrinhPhuXeDTO>();
            String sql = " SELECT cx.ngayhoatdong," +
                "  xe.maxe, " +
                "  cx.machuyenxe, " +
                        " lt.tenlotrinh," +
                         "  kip.tenkip," +
                         " xe.biensoxe" +
                        " FROM NhanVien nv" +
                            " JOIN ChuyenXe cx ON nv.manhanvien = cx.maphuxe" +
                           " JOIN Kip kip ON cx.makip = kip.makip" +
                          "  JOIN LoTrinh lt ON cx.malotrinh = lt.malotrinh" +
                          "  JOIN Xe xe ON cx.maxe = xe.maxe" +
                           " WHERE cx.ngayhoatdong = Convert(datetime,@ngayhoatdong,103)";

            if (!String.IsNullOrEmpty(tentaikhoan))
            {
                sql += " AND nv.tentaikhoan = @tentaikhoan";
            }
            if (!String.IsNullOrEmpty(manhanvien))
            {
                sql += " AND nv.manhanvien = @manhanvien";
            }


            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@ngayhoatdong", DateTime.Now.ToString("dd-MM-yyyy"));

            if (!String.IsNullOrEmpty(tentaikhoan))
            {
                command.Parameters.AddWithValue("@tentaikhoan", tentaikhoan);
            }

            if (!String.IsNullOrEmpty(manhanvien))
            {
                command.Parameters.AddWithValue("@manhanvien", manhanvien);
            }


            SqlDataReader rowsAffected = command.ExecuteReader();

            if (rowsAffected.HasRows)
            {
                int sl = 0;
                while (rowsAffected.Read())
                {
                    LichTrinhPhuXeDTO lichTrinhPhuXeDTO = new LichTrinhPhuXeDTO();
                    lichTrinhPhuXeDTO.ngayhoatdong = rowsAffected.GetDateTime(sl++);
                    lichTrinhPhuXeDTO.maxe = rowsAffected.GetString(sl++);
                    lichTrinhPhuXeDTO.machuyenxe = rowsAffected.GetString(sl++);
                    lichTrinhPhuXeDTO.tenlotrinh = rowsAffected.GetString(sl++);
                    lichTrinhPhuXeDTO.tenkip = rowsAffected.GetString(sl++);
                    lichTrinhPhuXeDTO.biensoxe = rowsAffected.GetString(sl++);

                    lichTrinhPhuXes.Add(lichTrinhPhuXeDTO);
                    sl = 0;
                }
            }
            return lichTrinhPhuXes;

        }
    }
}