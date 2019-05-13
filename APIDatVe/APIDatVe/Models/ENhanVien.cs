using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDatVe.Models
{
    public class ENhanVien
    {
        public string manhanvien { get; set; }
        public string hoten { get; set; }
        public string machucvu { get; set; }
        public Nullable<System.DateTime> ngaysinh { get; set; }
        public string diachi { get; set; }
        public string sodienthoai { get; set; }
        public string email { get; set; }
        public string socmt { get; set; }
        public string ngaycap { get; set; }
        public string tentaikhoan { get; set; }
        public string noicap { get; set; }
        public string maquyen { get; set; }
        public string matkhau { get; set; }
        public int trangthai { get; set; }
        // chi tiết lái xe
        public string giaypheplaixe { get; set; }
        public string hanglai { get; set; }
        public string ngaycaplaixe { get; set; }
        public string noicaplaixe { get; set; }
        public string ghichu { get; set; }
        public string gioitinh { get; set; }
    }
}