namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuChi")]
    public partial class PhieuChi
    {
        [Key]
        public int SoPhieuChi { get; set; }

        [StringLength(50)]
        public string SoPhieuChiCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayChi { get; set; }

        public int MaNhanvien { get; set; }

        public int MaPhieuNhap { get; set; }

        public decimal TongTienChi { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }
}
