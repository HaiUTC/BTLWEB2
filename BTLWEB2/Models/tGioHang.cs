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
    
    public partial class tGioHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tGioHang()
        {
            this.tChiTietGioHangs = new HashSet<tChiTietGioHang>();
        }
    
        public int MaGioHang { get; set; }
        public string TenTK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tChiTietGioHang> tChiTietGioHangs { get; set; }
    }
}