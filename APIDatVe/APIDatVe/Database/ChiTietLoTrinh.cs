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
    
    public partial class ChiTietLoTrinh
    {
        public int machitietlotrinh { get; set; }
        public string malotrinh { get; set; }
        public string idtinhthanh { get; set; }
    
        public virtual LoTrinh LoTrinh { get; set; }
        public virtual TinhThanh TinhThanh { get; set; }
    }
}
