using APIDatVe.DTO.PhuXe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL.PhuXe
{
    public class DiemTrungChuyenDAL
    {
        public List<DiemTrungChuyenDTO> GetDiemTrungChuyens(string malotrinh)
        {
            List<DiemTrungChuyenDTO> diemTrungChuyens = new List<DiemTrungChuyenDTO>();
            String sql = "SELECT  dt.*"+
                      "  FROM DiemTrungChuyen dt"+
                      "  JOIN TinhThanh th ON dt.matinh = dt.matinh"+
                     "   JOIN ChiTietLoTrinh ct ON ct.idtinhthanh = dt.matinh"+
                      "  AND ct.idtinhthanh = dt.matinh"+
                      "  WHERE ct.malotrinh = @malotrinh ";

            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@malotrinh", malotrinh);

            SqlDataReader rowsAffected = command.ExecuteReader();

            int indexMadiemtrungchuyen = rowsAffected.GetOrdinal("madiemtrungchuyen");
            int indexDiaChi = rowsAffected.GetOrdinal("diachi");
            int indexMaTinh = rowsAffected.GetOrdinal("matinh");
            int indextenDiemTrungChuyen = rowsAffected.GetOrdinal("tendiemtrungchuyen");
            int indexLat = rowsAffected.GetOrdinal("lat");
            int indexLong = rowsAffected.GetOrdinal("long");


            if (rowsAffected.HasRows)
            {

                while (rowsAffected.Read())
                {
                    DiemTrungChuyenDTO diemTrungChuyen = new DiemTrungChuyenDTO();
                    diemTrungChuyen.madiemtrungchuyen = rowsAffected.GetString(indexMadiemtrungchuyen);
                    diemTrungChuyen.matinh = rowsAffected.IsDBNull(indexMaTinh) ? "" : rowsAffected.GetString(indexMaTinh);
                    diemTrungChuyen.tendiemtrungchuyen = rowsAffected.IsDBNull(indextenDiemTrungChuyen) ? "" : rowsAffected.GetString(indextenDiemTrungChuyen);
                    diemTrungChuyen.diachi = rowsAffected.IsDBNull(indexDiaChi) ? "" : rowsAffected.GetString(indexDiaChi);
                    diemTrungChuyen.lat = rowsAffected.IsDBNull(indexLat) ? "" : rowsAffected.GetString(indexLat);
                    diemTrungChuyen.longt = rowsAffected.IsDBNull(indexLong) ? "" : rowsAffected.GetString(indexLong);

                    diemTrungChuyens.Add(diemTrungChuyen);

                }
            }

            return diemTrungChuyens;
        }

        //lay ttin khach xg,len tai diem trung chuyen

        public DataTable GetKhachHangByDiemTrungChuyen(string madiemtrungchuyen, string machuyenxe)
        {

            String sql = " SELECT kh.hoten,kh.sodienthoai,vx.*, ct.maghe,ct.sodienthoaikhach,ct.tenhanhkhach, ghe.tenghe" +
                       " FROM KhachHang kh" +
                      "  JOIN VeXe vx ON kh.khachhangId = vx.khachhangId" +
                       " JOIN DiemTrungChuyen dtc ON vx.madiemtrungchuyentra = dtc.madiemtrungchuyen" +
                       " JOIN ChuyenXe cx ON vx.machuyenxe = cx.machuyenxe" +
                       " JOIN  ChiTietVeXe ct ON ct.vexeId = vx.vexeId" +
                       " JOIN Ghe ghe ON ghe.maghe = ct.maghe" +
                      "  WHERE dtc.madiemtrungchuyen = @madiemtrungchuyen" +
                       "   AND cx.machuyenxe = @machuyenxe";
            SqlCommand command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@madiemtrungchuyen", madiemtrungchuyen);

            command.Parameters.AddWithValue("@machuyenxe", machuyenxe);

            SqlDataReader rowsAffected = command.ExecuteReader();

            var dataTable = new DataTable();
            if (rowsAffected.HasRows)
            {
                dataTable.Load(rowsAffected);
            }

            sql = "SELECT kh.hoten,kh.sodienthoai,vx.*, ct.maghe,ct.sodienthoaikhach,ct.tenhanhkhach, ghe.tenghe" +
                     " FROM KhachHang kh" +
                    "  JOIN VeXe vx ON kh.khachhangId = vx.khachhangId" +
                     " JOIN DiemTrungChuyen dtc ON vx.madiemtrungchuyendon = dtc.madiemtrungchuyen" +
                     " JOIN ChuyenXe cx ON vx.machuyenxe = cx.machuyenxe" +
                     " JOIN  ChiTietVeXe ct ON ct.vexeId = vx.vexeId" +
                     " JOIN Ghe ghe ON ghe.maghe = ct.maghe" +
                    "  WHERE dtc.madiemtrungchuyen = @madiemtrungchuyen" +
                     "   AND cx.machuyenxe = @machuyenxe";
            command = DataProvider.Instance.getCommand(sql);

            command.Parameters.AddWithValue("@madiemtrungchuyen", madiemtrungchuyen);

            command.Parameters.AddWithValue("@machuyenxe", machuyenxe);

            rowsAffected = command.ExecuteReader();

            if (rowsAffected.HasRows)
            {
                dataTable.Load(rowsAffected);
            }

            return dataTable;

        }

        }
}