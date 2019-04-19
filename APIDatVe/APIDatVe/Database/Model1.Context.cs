﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<APIQuanLy> APIQuanLies { get; set; }
        public virtual DbSet<BangGia> BangGias { get; set; }
        public virtual DbSet<ChiTietLaiXe> ChiTietLaiXes { get; set; }
        public virtual DbSet<ChiTietLoTrinh> ChiTietLoTrinhs { get; set; }
        public virtual DbSet<ChiTietVeXe> ChiTietVeXes { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<ChuyenXe> ChuyenXes { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<DiemTrungChuyen> DiemTrungChuyens { get; set; }
        public virtual DbSet<DoiTuong> DoiTuongs { get; set; }
        public virtual DbSet<Ghe> Ghes { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Kip> Kips { get; set; }
        public virtual DbSet<LoTrinh> LoTrinhs { get; set; }
        public virtual DbSet<ManHinhQuanLy> ManHinhQuanLies { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhomAPI> NhomAPIs { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<QuyenAPIQuanLy> QuyenAPIQuanLies { get; set; }
        public virtual DbSet<QuyenManHinhQuanLy> QuyenManHinhQuanLies { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TinhThanh> TinhThanhs { get; set; }
        public virtual DbSet<TrangThaiGhe> TrangThaiGhes { get; set; }
        public virtual DbSet<TrangThaiVeXe> TrangThaiVeXes { get; set; }
        public virtual DbSet<VeXe> VeXes { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_checkLogin(string sodienthoai, string matkhau)
        {
            var sodienthoaiParameter = sodienthoai != null ?
                new ObjectParameter("sodienthoai", sodienthoai) :
                new ObjectParameter("sodienthoai", typeof(string));
    
            var matkhauParameter = matkhau != null ?
                new ObjectParameter("matkhau", matkhau) :
                new ObjectParameter("matkhau", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_checkLogin", sodienthoaiParameter, matkhauParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_gettripbytripId_Result> sp_gettripbytripId(string malotrinh, Nullable<System.DateTime> ngayhoatdong)
        {
            var malotrinhParameter = malotrinh != null ?
                new ObjectParameter("malotrinh", malotrinh) :
                new ObjectParameter("malotrinh", typeof(string));
    
            var ngayhoatdongParameter = ngayhoatdong.HasValue ?
                new ObjectParameter("ngayhoatdong", ngayhoatdong) :
                new ObjectParameter("ngayhoatdong", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_gettripbytripId_Result>("sp_gettripbytripId", malotrinhParameter, ngayhoatdongParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_laythongtingiave_Result> sp_laythongtingiave(string madiemtrungchuyendon, string madiemtrungchuyentra)
        {
            var madiemtrungchuyendonParameter = madiemtrungchuyendon != null ?
                new ObjectParameter("madiemtrungchuyendon", madiemtrungchuyendon) :
                new ObjectParameter("madiemtrungchuyendon", typeof(string));
    
            var madiemtrungchuyentraParameter = madiemtrungchuyentra != null ?
                new ObjectParameter("madiemtrungchuyentra", madiemtrungchuyentra) :
                new ObjectParameter("madiemtrungchuyentra", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_laythongtingiave_Result>("sp_laythongtingiave", madiemtrungchuyendonParameter, madiemtrungchuyentraParameter);
        }
    
        public virtual ObjectResult<sp_listdiemtrungchuyen_Result> sp_listdiemtrungchuyen(string malotrinh)
        {
            var malotrinhParameter = malotrinh != null ?
                new ObjectParameter("malotrinh", malotrinh) :
                new ObjectParameter("malotrinh", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listdiemtrungchuyen_Result>("sp_listdiemtrungchuyen", malotrinhParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual ObjectResult<sp_search_customerbyusername_Result> sp_search_customerbyusername(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_search_customerbyusername_Result>("sp_search_customerbyusername", usernameParameter);
        }
    
        public virtual int sp_themkhachhang(string makhachhang, string hoten, string diachi, string sodienthoai, string email, string matkhau, string gioitinh)
        {
            var makhachhangParameter = makhachhang != null ?
                new ObjectParameter("makhachhang", makhachhang) :
                new ObjectParameter("makhachhang", typeof(string));
    
            var hotenParameter = hoten != null ?
                new ObjectParameter("hoten", hoten) :
                new ObjectParameter("hoten", typeof(string));
    
            var diachiParameter = diachi != null ?
                new ObjectParameter("diachi", diachi) :
                new ObjectParameter("diachi", typeof(string));
    
            var sodienthoaiParameter = sodienthoai != null ?
                new ObjectParameter("sodienthoai", sodienthoai) :
                new ObjectParameter("sodienthoai", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var matkhauParameter = matkhau != null ?
                new ObjectParameter("matkhau", matkhau) :
                new ObjectParameter("matkhau", typeof(string));
    
            var gioitinhParameter = gioitinh != null ?
                new ObjectParameter("gioitinh", gioitinh) :
                new ObjectParameter("gioitinh", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_themkhachhang", makhachhangParameter, hotenParameter, diachiParameter, sodienthoaiParameter, emailParameter, matkhauParameter, gioitinhParameter);
        }
    
        public virtual int sp_update_password_customer(string username, string matkhau)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var matkhauParameter = matkhau != null ?
                new ObjectParameter("matkhau", matkhau) :
                new ObjectParameter("matkhau", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_update_password_customer", usernameParameter, matkhauParameter);
        }
    
        public virtual int sp_updateCustomeInfo(string makhachhang, string tenkhachhang, string sodienthoai, string diachi, string gioitinh, string matkhau)
        {
            var makhachhangParameter = makhachhang != null ?
                new ObjectParameter("makhachhang", makhachhang) :
                new ObjectParameter("makhachhang", typeof(string));
    
            var tenkhachhangParameter = tenkhachhang != null ?
                new ObjectParameter("tenkhachhang", tenkhachhang) :
                new ObjectParameter("tenkhachhang", typeof(string));
    
            var sodienthoaiParameter = sodienthoai != null ?
                new ObjectParameter("sodienthoai", sodienthoai) :
                new ObjectParameter("sodienthoai", typeof(string));
    
            var diachiParameter = diachi != null ?
                new ObjectParameter("diachi", diachi) :
                new ObjectParameter("diachi", typeof(string));
    
            var gioitinhParameter = gioitinh != null ?
                new ObjectParameter("gioitinh", gioitinh) :
                new ObjectParameter("gioitinh", typeof(string));
    
            var matkhauParameter = matkhau != null ?
                new ObjectParameter("matkhau", matkhau) :
                new ObjectParameter("matkhau", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_updateCustomeInfo", makhachhangParameter, tenkhachhangParameter, sodienthoaiParameter, diachiParameter, gioitinhParameter, matkhauParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int updatetrangthaighe(string maghe, Nullable<int> trangthai, string vexeid, Nullable<int> matrangthaive)
        {
            var magheParameter = maghe != null ?
                new ObjectParameter("maghe", maghe) :
                new ObjectParameter("maghe", typeof(string));
    
            var trangthaiParameter = trangthai.HasValue ?
                new ObjectParameter("trangthai", trangthai) :
                new ObjectParameter("trangthai", typeof(int));
    
            var vexeidParameter = vexeid != null ?
                new ObjectParameter("vexeid", vexeid) :
                new ObjectParameter("vexeid", typeof(string));
    
            var matrangthaiveParameter = matrangthaive.HasValue ?
                new ObjectParameter("matrangthaive", matrangthaive) :
                new ObjectParameter("matrangthaive", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updatetrangthaighe", magheParameter, trangthaiParameter, vexeidParameter, matrangthaiveParameter);
        }
    }
}
