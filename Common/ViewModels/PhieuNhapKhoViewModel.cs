using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class PhieuNhapKhoViewModel
    {
        public int soPhieuNhapKho { get; set; }
        public DateTime ngayNhapKho { get; set; }
        public int maNhanVien { get; set; }
        public string tenNhanVien { get; set; }
        public string nhaCungCap { get; set; } 
        public decimal tongTien { get; set; }
        public string ghiChu { get; set; }

        public List<ChiTietPhieuNhap> chiTietPhieuNhap { get; set; }
    }
}
