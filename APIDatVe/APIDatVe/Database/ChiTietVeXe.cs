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
    
    public partial class ChiTietVeXe
    {
        public string chitietvexeId { get; set; }
        public string vexeId { get; set; }
        public string maghe { get; set; }
        public string tenhanhkhach { get; set; }
        public string sodienthoai { get; set; }
        public string doituong { get; set; }
    
        public virtual VeXe VeXe { get; set; }
    }
}
