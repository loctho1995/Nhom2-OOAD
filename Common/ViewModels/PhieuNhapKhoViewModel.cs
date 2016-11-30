﻿using Common.Models;
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
        public int maNhaCungCap { get; set; } 
        public decimal tongTien { get; set; }
        public string ghiChu { get; set; }

        public string tenNhaCungCap { get; set; }

        public int maHangHoa { get; set; }

        public string tenHangHoa { get; set; }

        public string donViTinh { get; set; }

        public int soLuong { get; set; }

        public decimal gia { get; set; }

        public decimal thanhTien { get; set; }

        public bool trangThai { get; set; }
        public List<ChiTietPhieuNhap> chiTietPhieuNhap { get; set; }
    }
}
