//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTLWEB2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tSanPham()
        {
            this.tChiTietGioHangs = new HashSet<tChiTietGioHang>();
            this.tChiTietHoaDons = new HashSet<tChiTietHoaDon>();
        }
    
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<int> Gia { get; set; }
        public string GioiTinh { get; set; }
        public string TheLoai { get; set; }
        public string Anh { get; set; }
        public Nullable<int> sale { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tChiTietGioHang> tChiTietGioHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tChiTietHoaDon> tChiTietHoaDons { get; set; }
    }
}
