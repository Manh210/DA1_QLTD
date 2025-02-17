//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TinUngTuyen
    {
        public int ID_TUT { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public System.DateTime DOB { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string PhucLoi { get; set; }
        public string MucLuong { get; set; }
        public string CapBac { get; set; }
        public string KinhNghiemLV { get; set; }
        public string TrinhDoHocVan { get; set; }
        public string KyNangChuyenMon { get; set; }
        public Nullable<System.DateTime> TgDang { get; set; }
        public string TrangThaiTUT { get; set; }
        public Nullable<System.DateTime> TgianCapNhatTT { get; set; }
        public Nullable<int> ID_NN { get; set; }
        public Nullable<int> ID_Admin { get; set; }
        public Nullable<int> ID_UV { get; set; }
        public Nullable<int> ID_DD { get; set; }
        public Nullable<int> ID_LV { get; set; }
        public Nullable<bool> Xoa { get; set; }
    
        public virtual DiaDiem DiaDiem { get; set; }
        public virtual LinhVuc LinhVuc { get; set; }
        public virtual ND_Admin ND_Admin { get; set; }
        public virtual NganhNghe NganhNghe { get; set; }
        public virtual UngVien UngVien { get; set; }
    }
}
