namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuBaoHanh")]
    public partial class PhieuBaoHanh
    {
        [Key]
        public int SoPhieuBaoHanh { get; set; }

        [Required]
        [StringLength(50)]
        public string SoPhieuBaoHanhCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayLap { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayGiao { get; set; }

        public int? MaNhanVien { get; set; }

        [StringLength(200)]
        public string TenKhachHang { get; set; }

        [StringLength(50)]
        public string SoDienThoai { get; set; }

        public decimal TongTien { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }

        public bool DaGiao { get; set; }
    }
}
