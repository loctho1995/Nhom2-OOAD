using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class PhieuDatHangViewModel
    {
        public int soPhieuDatHang { get; set; }
        public DateTime ngayDat { get; set; }
        public string tenNhanVien { get; set; }
        public decimal tongTien { get; set; }
        public int maHangHoa { get; set; }
        public string tenHangHoa { get; set; }
        public int soLuong { get; set; }
        public decimal gia { get; set; }
        public decimal thanhTien { get; set; }
        public string donViTinh { get; set; }
        public string tenKhachHang { get; set; }
        public string soDienThoai { get; set; }
        public string ghiChu { get; set; }
        public string hinhThucThanhToan { get; set; }
        public bool daXacNhan { get; set; }
        public bool daThanhToan { get; set; }
        public DateTime ngayGiao { get; set; }

        public List<ChiTietPhieuBanHang> chiTietPhieuBanHang { get; set; }
    }
}
