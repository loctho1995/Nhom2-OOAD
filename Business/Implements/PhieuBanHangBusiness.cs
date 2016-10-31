using Common.Models;
using Common.ViewModels;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class PhieuBanHangBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuBanHangRepository _phieuBanHangRepo;
        //private readonly ChiTietPhieuBanHang _chiTietPhieuKiemKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;

        private NhanVienBusiness _nhanVienBus;

        public PhieuBanHangBusiness()
        {
            dbContext = new SMSEntities();
            _phieuBanHangRepo = new PhieuBanHangRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);           
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<PhieuBanHangViewModel> ListView(string nhanVienCode)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<PhieuBanHangViewModel> all = new List<PhieuBanHangViewModel>();
            List<PhieuBanHangViewModel> allForManager = new List<PhieuBanHangViewModel>();

            if(_nhanVienBus.layMaChucVu(nhanVienCode) == 4)
            {
                all = (from phieuBanHang in danhSachPhieuBanHang
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuBanHang.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.NhanVienCode.Equals(nhanVienCode))
                       select new
                       {
                           SoPhieuBanHang = phieuBanHang.SoPhieuBanHang,
                           NgayBan = phieuBanHang.NgayBan,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TenKhachHang = phieuBanHang.TenKhachHang,
                           SoDienThoai = phieuBanHang.SoDienThoai,
                           TongTien = phieuBanHang.TongTien,
                           GhiChu = phieuBanHang.Ghichu,

                       }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                       {
                           soPhieuBanHang = x.SoPhieuBanHang,
                           ngayBan = x.NgayBan,
                           tenNhanVien = x.TenNhanVien,
                           tenKhachHang = x.TenKhachHang,
                           soDienThoai = x.SoDienThoai,
                           tongTien = x.TongTien,
                           ghiChu = x.GhiChu,
                       }).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieuBanHang in danhSachPhieuBanHang
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieuBanHang.MaNhanVien equals nhanvien.MaNhanVien
                                 select new
                                 {
                                     SoPhieuBanHang = phieuBanHang.SoPhieuBanHang,
                                     NgayBan = phieuBanHang.NgayBan,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TenKhachHang = phieuBanHang.TenKhachHang,
                                     SoDienThoai = phieuBanHang.SoDienThoai,
                                     TongTien = phieuBanHang.TongTien,
                                     GhiChu = phieuBanHang.Ghichu,

                                 }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                 {
                                     soPhieuBanHang = x.SoPhieuBanHang,
                                     ngayBan = x.NgayBan,
                                     tenNhanVien = x.TenNhanVien,
                                     tenKhachHang = x.TenKhachHang,
                                     soDienThoai = x.SoDienThoai,
                                     tongTien = x.TongTien,
                                     ghiChu = x.GhiChu,

                                 }).ToList();
                return allForManager;
            }
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuBanHangRepo.GetByIdAsync(ID);
        }

        public async Task Delete(object deleteModel)
        {
            PhieuBanHang xoaPhieuBanHang = (PhieuBanHang)deleteModel;

            await _phieuBanHangRepo.DeleteAsync(xoaPhieuBanHang);
        }

        public IEnumerable<PhieuBanHangViewModel> thongTinPhieuBanHangTheoMa(int soPhieuBanHang)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<PhieuBanHangViewModel> all = new List<PhieuBanHangViewModel>();

            all = (from phieubanhang in danhSachPhieuBanHang
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                   where (phieubanhang.SoPhieuBanHang.Equals(soPhieuBanHang))
                   select new
                   {
                       SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                       NgayBan = phieubanhang.NgayBan,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TenKhachHang = phieubanhang.TenKhachHang,
                       SoDienThoai = phieubanhang.SoDienThoai,
                       TongTien = phieubanhang.TongTien,
                       GhiChu = phieubanhang.Ghichu,

                   }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                   {
                       soPhieuBanHang = x.SoPhieuBanHang,
                       ngayBan = x.NgayBan,
                       tenNhanVien = x.TenNhanVien,
                       tenKhachHang = x.TenKhachHang,
                       soDienThoai = x.SoDienThoai,
                       tongTien = x.TongTien,
                       ghiChu = x.GhiChu,
                   }).ToList();        

            return all;
        }
    }
}
