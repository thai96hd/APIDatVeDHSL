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
    
    public partial class VeXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VeXe()
        {
            this.ChiTietVeXes = new HashSet<ChiTietVeXe>();
        }
    
        public int vexeId { get; set; }
        public int chitiethoatdongxeId { get; set; }
        public int khachhangId { get; set; }
        public Nullable<int> sokhach { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietVeXe> ChiTietVeXes { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}
