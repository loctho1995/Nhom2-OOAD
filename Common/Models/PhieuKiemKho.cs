namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuKiemKho")]
    public partial class PhieuKiemKho
    {
        public PhieuKiemKho()
        {
            ChiTietPhieuKiemKhos = new HashSet<ChiTietPhieuKiemKho>();
        }
        [Key]
        public int SoPhieuKiemKho { get; set; }

        [Required]
        [StringLength(50)]
        public string SoPhieuKiemKhoCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayKiemKho { get; set; }

        public int MaNhanVien { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }

        public bool TrangThai { get; set; }
         public virtual ICollection<ChiTietPhieuKiemKho> ChiTietPhieuKiemKhos { get; set; }

    
    }
}
