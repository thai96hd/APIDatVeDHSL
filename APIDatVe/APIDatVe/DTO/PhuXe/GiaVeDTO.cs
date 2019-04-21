using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDatVe.DTO.PhuXe
{
    public class GiaVeDTO
    {

        public class BangGiaDTO
        {
            public string banggiaid { get; set; }
            public string madiemtrungchuyendon { get; set; }
            public string madiemtrungchuyentra { get; set; }
            public float giave { get; set; }
            public float thoigiandukien { get; set; }
        }
    }
}