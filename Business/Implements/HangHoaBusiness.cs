using Common.Models;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business.Implements
{
    public class HangHoaBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuKiemKhoRepository _phieuKiemKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private NhanVienBusiness _nhanVienBus;

        public HangHoaBusiness()
        {
            dbContext = new SMSEntities();
            _phieuKiemKhoRepo = new PhieuKiemKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public List<Object> LoadSanhSachHangHoa()
        {
            var list = (from hanghoa in dbContext.HangHoas
                        where (hanghoa.TrangThai == true)
                        select new SelectListItem
                        {
                            Text = hanghoa.TenHangHoa,
                            Value = hanghoa.MaHangHoa.ToString(),
                        }).Distinct().ToList();

            return new List<Object>(list);
        }

        public Object LayThongTinHangHoa(int maHangHoa)
        {
            var producInfor = from hanghoa in dbContext.HangHoas
                              where (hanghoa.MaHangHoa == maHangHoa)
                              select new
                              {
                                  hanghoa.TenHangHoa,
                                  hanghoa.DonViTinh,
                                  hanghoa.SoLuongTon,
                                  hanghoa.GiaBan,
                              };
            return producInfor;
        }

        public IEnumerable<HangHoa> DanhSachHangHoaMoiNhat()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa                
                   select new
                   {
                       MaNhanVien = hanghoa.MaHangHoa,
                       TenNhanVien = hanghoa.TenHangHoa,
                       DiaChi = hanghoa.HinhAnh,
                       SoDienThoai = hanghoa.GiaBan,
                       Email = hanghoa.GiamGia,                   
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaNhanVien,
                       TenHangHoa = x.TenNhanVien,
                       HinhAnh = x.DiaChi,
                       GiaBan = x.SoDienThoai,
                       GiamGia = x.Email,                     
                   }).ToList();
            return all;
        }


        public IEnumerable<HangHoa> LoadHangHoaTheoMa(string maHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where (hanghoa.MaHangHoa.Equals(maHangHoa))
                   select new
                   {
                       MaNhanVien = hanghoa.MaHangHoa,
                       TenNhanVien = hanghoa.TenHangHoa,
                       DiaChi = hanghoa.HinhAnh,
                       SoDienThoai = hanghoa.GiaBan,
                       Email = hanghoa.GiamGia,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaNhanVien,
                       TenHangHoa = x.TenNhanVien,
                       HinhAnh = x.DiaChi,
                       GiaBan = x.SoDienThoai,
                       GiamGia = x.Email,
                   }).ToList();
            return all;
        }

        public IList<HangHoa> DanhSachHangHoaTheoMaLoaiHangHoa(string maLoaiHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   //where (hanghoa.MaLoaiHangHoa.Equals(maLoaiHangHoa))
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiaKhuyenMai = hanghoa.GiamGia,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiaKhuyenMai,
                   }).ToList();
            return all;
        }
    }
}

