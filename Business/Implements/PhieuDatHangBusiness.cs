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
    public class PhieuDatHangBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuDatHangRepository _phieuDatHangRepo;
        //private readonly ChiTietPhieuBanHang _chiTietPhieuKiemKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;

        private NhanVienBusiness _nhanVienBus;

        public PhieuDatHangBusiness()
        {
            dbContext = new SMSEntities();
            _phieuDatHangRepo = new PhieuDatHangRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<PhieuDatHangViewModel> ListView(string nhanVienCode)
        {
            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            List<PhieuDatHangViewModel> all = new List<PhieuDatHangViewModel>();
            List<PhieuDatHangViewModel> allForManager = new List<PhieuDatHangViewModel>();

            if(_nhanVienBus.layMaChucVu(nhanVienCode) == 4)
            {
                all = (from phieuDatHang in danhSachPhieuDatHang
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuDatHang.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.NhanVienCode.Equals(nhanVienCode))
                       select new
                       {
                           SoPhieuDatHang = phieuDatHang.SoPhieuDatHang,
                           NgayDat= phieuDatHang.NgayDat,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TenKhachHang = phieuDatHang.TenKhachHang,
                           SoDienThoai = phieuDatHang.SoDienThoai,
                           TongTien = phieuDatHang.TongTien,
                           HinhThucThanhToan = phieuDatHang.HinhThucThanhToan,
                           NgayGiao = phieuDatHang.NgayGiao,
                           DaXacNhan = phieuDatHang.DaXacNhan,
                           DaThanhToan = phieuDatHang.DaThanhToan,
                           GhiChu = phieuDatHang.Ghichu,

                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           soPhieuDatHang = x.SoPhieuDatHang,
                           ngayDat = x.NgayDat,
                           tenNhanVien = x.TenNhanVien,
                           tenKhachHang = x.TenKhachHang,
                           soDienThoai = x.SoDienThoai,
                           tongTien = x.TongTien,
                           hinhThucThanhToan = x.HinhThucThanhToan,
                           ngayGiao = x.NgayGiao,
                           daXacNhan = x.DaXacNhan,
                           daThanhToan = x.DaThanhToan,
                           ghiChu = x.GhiChu,
                       }).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieuDatHang in danhSachPhieuDatHang
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieuDatHang.MaNhanVien equals nhanvien.MaNhanVien
                                 select new
                                 {
                                     SoPhieuDatHang = phieuDatHang.SoPhieuDatHang,
                                     NgayDat = phieuDatHang.NgayDat,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TenKhachHang = phieuDatHang.TenKhachHang,
                                     SoDienThoai = phieuDatHang.SoDienThoai,
                                     TongTien = phieuDatHang.TongTien,
                                     HinhThucThanhToan = phieuDatHang.HinhThucThanhToan,
                                     NgayGiao = phieuDatHang.NgayGiao,
                                     DaXacNhan = phieuDatHang.DaXacNhan,
                                     DaThanhToan = phieuDatHang.DaThanhToan,
                                     GhiChu = phieuDatHang.Ghichu,

                                 }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                                 {
                                     soPhieuDatHang = x.SoPhieuDatHang,
                                     ngayDat = x.NgayDat,
                                     tenNhanVien = x.TenNhanVien,
                                     tenKhachHang = x.TenKhachHang,
                                     soDienThoai = x.SoDienThoai,
                                     tongTien = x.TongTien,
                                     hinhThucThanhToan = x.HinhThucThanhToan,
                                     ngayGiao = x.NgayGiao,
                                     daXacNhan = x.DaXacNhan,
                                     daThanhToan = x.DaThanhToan,
                                     ghiChu = x.GhiChu,

                                 }).ToList();
                return allForManager;
            }
			
        public PhieuDatHangBusiness()
        {
            dbContext = new SMSEntities();          
        }

        public int Insert(PhieuDatHang order)
        {
            dbContext.PhieuDatHangs.Add(order);
            dbContext.SaveChanges();
            return order.SoPhieuDatHang;
        }

        public bool Update(PhieuDatHang entity)
        {
            try
            {
                var user = dbContext.PhieuDatHangs.Find(entity.SoPhieuDatHang);
                user.TongTien = entity.TongTien;

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
