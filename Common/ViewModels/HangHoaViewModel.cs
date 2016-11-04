using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class HangHoaViewModel
    {
        public int maHangHoa { get; set; }

        public string tenHangHoa { get; set; }
        public string tenLoaiHangHoa { get; set; }
        public int soLuongTon { get; set; }
        public decimal giaBan { get; set; }

        public decimal giamGia { get; set; }
        public string hinhAnh { get; set; }
    }
}
