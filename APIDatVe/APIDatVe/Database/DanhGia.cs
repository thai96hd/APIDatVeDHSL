//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIDatVe.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class DanhGia
    {
        public string madanhgia { get; set; }
        public string makhachhang { get; set; }
        public string maxe { get; set; }
        public Nullable<int> diemdanhgia { get; set; }
        public string noidungdanhgia { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        public virtual Xe Xe { get; set; }
    }
}
