using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class PhieuDatHangViewModel
    {
        public int maHangHoa { set; get; }
        public string tenHangHoa { set; get; }
        public int soLuong { set; get; }
        public string hinhAnh { set; get; }

        public string diaChi { set; get; }

        public string soDienThoai { set; get; }

        public string email { set; get; }

        public string tenKhachHang { set; get; }

        public string hinhThucThanhToan { set; get; }

        public string ghiChu { set; get; }

        public decimal giaBan { get; set; }

        public decimal giamGia { get; set; }
        public decimal tongTien { get; set; }
    }
}
