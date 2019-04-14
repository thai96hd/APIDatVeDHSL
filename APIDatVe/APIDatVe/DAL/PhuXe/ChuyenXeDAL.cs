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
        public List<ChuyenXeDTO> getLichTrinh(String maphuxe)
        {
            List<ChuyenXeDTO> chuyenXes = new List<ChuyenXeDTO>();
            String sql = "SELECT *" +
                   " FROM ChuyenXe" +
                    " WHERE ngayhoatdong>=Convert(datetime,@ngaybatdau,103) " +
                    " AND ngayhoatdong<= Convert(datetime,@ngayketthuc,103)" +
                    " AND maphuxe = @maphuxe";

            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(6);

            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@ngaybatdau", startDate.ToString("dd-MM-yyyy"));

            command.Parameters.AddWithValue("@ngayketthuc", endDate.ToString("dd-MM-yyyy"));

            command.Parameters.AddWithValue("@maphuxe", maphuxe);


            SqlDataReader rowsAffected = command.ExecuteReader();


            if (rowsAffected.HasRows)
            {
                int sl = 0;
                while (rowsAffected.Read())
                {
                    ChuyenXeDTO chuyenxe = new ChuyenXeDTO();
                    chuyenxe.machuyenxe = rowsAffected.GetString(sl++);
                    chuyenxe.malotrinh = rowsAffected.GetString(sl++);
                    chuyenxe.makip = rowsAffected.GetString(sl++);
                    chuyenxe.maxe = rowsAffected.GetString(sl++);
                    chuyenxe.mataixe = rowsAffected.GetString(sl++);
                    chuyenxe.maphuxe = rowsAffected.GetString(sl++);
                    chuyenxe.ngayhoatdong = rowsAffected.GetDateTime(sl++);
                    if (!rowsAffected.IsDBNull(sl))
                        chuyenxe.thoigiandungxe = rowsAffected.GetDateTime(sl++);
                    chuyenXes.Add(chuyenxe);
                    sl = 0;
                }
            }
            return chuyenXes;
        }


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
            String sql = "SELECT cx.ngayhoatdong," +
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