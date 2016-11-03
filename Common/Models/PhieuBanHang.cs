namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuBanHang")]
    public partial class PhieuBanHang
    {
        [Key]
        public int SoPhieuBanHang { get; set; }

        [StringLength(50)]
        public string SoPhieuBanHangCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayBan { get; set; }

        public int MaNhanVien { get; set; }

        [StringLength(200)]
        public string TenKhachHang { get; set; }

        [StringLength(15)]
        public string SoDienThoai { get; set; }

        public decimal TongTien { get; set; }

        [StringLength(200)]
        public string Ghichu { get; set; }
    }
}
