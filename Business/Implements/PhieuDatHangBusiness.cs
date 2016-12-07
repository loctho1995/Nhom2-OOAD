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
        private SMSEntities dbContext;

        private readonly PhieuDatHangRepository _phieuDatHangRepo;
        //private readonly ChiTietPhieuDatHang _chiTietPhieuKiemKhoRepo;
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
            }
            catch (Exception)
            {

            }

            return true;
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuDatHangRepo.GetByIdAsync(ID);
        }

        public PhieuDatHang LayPhieuDatHang(int soPhieuDatHang)
        {
            return dbContext.PhieuDatHangs.Find(soPhieuDatHang);
        }

        public async Task Create(PhieuDatHangViewModel obj)
        {
            PhieuDatHang order = new PhieuDatHang
            {
                SoPhieuDatHang = obj.soPhieuDatHang,
                NgayDat = obj.ngayDat,
                NgayGiao = obj.ngayGiao,
                MaNhanVien = obj.maNhanVien,
                TenKhachHang = obj.tenKhachHang,
                SoDienThoai = obj.soDienThoai,
                Email = obj.email,
                HinhThucThanhToan = obj.hinhThucThanhToan,
                Ghichu = obj.ghiChu,
                DaXacNhan = obj.daXacNhan,
                DaThanhToan = obj.daThanhToan,
                Diachi = obj.diaChi                
            };

            order.ChiTetPhieuDatHangs = new List<ChiTietPhieuDatHang>();

            foreach(var i in obj.chiTietPhieuDatHang)
            {
                order.ChiTetPhieuDatHangs.Add(i);
            }

            await _phieuDatHangRepo.InsertAsync(order);
        }

        public IList<PhieuDatHangViewModel> ListView(string nhanVienCode)
        {
            var phieuBanHang = (new PhieuBanHangRepository(dbContext)).GetAll();
            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            List<PhieuDatHangViewModel> all = new List<PhieuDatHangViewModel>();
            List<PhieuDatHangViewModel> allForManager = new List<PhieuDatHangViewModel>();

            if(_nhanVienBus.layMaChucVu(nhanVienCode) != 4 &&
                _nhanVienBus.layMaChucVu(nhanVienCode) != 3)
            {
                return all;
            }

            if(_nhanVienBus.layMaChucVu(nhanVienCode) == 4)
            {
                all = (from phieuDatHang in danhSachPhieuDatHang
                       select new
                       {
                           SoPhieuDatHang = phieuDatHang.SoPhieuDatHang,
                           NgayDat = phieuDatHang.NgayDat,
                           MaNhanVien = phieuDatHang.MaNhanVien,
                           TenKhachHang = phieuDatHang.TenKhachHang,
                           SoDienThoai = phieuDatHang.SoDienThoai,
                           DiaChi = phieuDatHang.Diachi,
                           Email = phieuDatHang.Email,
                           TongTien = phieuDatHang.TongTien,
                           HinhThucThanhToan = phieuDatHang.HinhThucThanhToan,
                           GhiChu = phieuDatHang.Ghichu,
                           NgayGiao = phieuDatHang.NgayGiao,
                           DaXacNhan = phieuDatHang.DaXacNhan,
                           DaThanhToan = phieuDatHang.DaThanhToan

                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           soPhieuDatHang = x.SoPhieuDatHang,
                           ngayDat = x.NgayDat,
                           maNhanVien = x.MaNhanVien,
                           tenKhachHang = x.TenKhachHang,
                           soDienThoai = x.SoDienThoai,
                           diaChi = x.DiaChi,
                           email = x.Email,
                           tongTien = x.TongTien,
                           hinhThucThanhToan = x.HinhThucThanhToan,
                           ghiChu = x.GhiChu,
                           ngayGiao = x.NgayGiao,
                           daXacNhan = x.DaXacNhan,
                           daThanhToan = x.DaThanhToan
                       }).ToList();

                return all;
            }
            else
            {
                allForManager = (from phieuDatHang in danhSachPhieuDatHang
                                 select new
                                 {
                                     SoPhieuDatHang = phieuDatHang.SoPhieuDatHang,
                                     NgayDat = phieuDatHang.NgayDat,
                                     MaNhanVien = phieuDatHang.MaNhanVien,
                                     TenKhachHang = phieuDatHang.TenKhachHang,
                                     SoDienThoai = phieuDatHang.SoDienThoai,
                                     DiaChi = phieuDatHang.Diachi,
                                     Email = phieuDatHang.Email,
                                     TongTien = phieuDatHang.TongTien,
                                     HinhThucThanhToan = phieuDatHang.HinhThucThanhToan,
                                     GhiChu = phieuDatHang.Ghichu,
                                     NgayGiao = phieuDatHang.NgayGiao,
                                     DaXacNhan = phieuDatHang.DaXacNhan,
                                     DaThanhToan = phieuDatHang.DaThanhToan

                                 }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                                 {
                                     soPhieuDatHang = x.SoPhieuDatHang,
                                     ngayDat = x.NgayDat,
                                     maNhanVien = x.MaNhanVien,                                     
                                     tenKhachHang = x.TenKhachHang,
                                     soDienThoai = x.SoDienThoai,
                                     diaChi = x.DiaChi,
                                     email = x.Email,
                                     tongTien = x.TongTien,
                                     hinhThucThanhToan = x.HinhThucThanhToan,
                                     ghiChu = x.GhiChu,
                                     ngayGiao = x.NgayGiao,
                                     daXacNhan = x.DaXacNhan,
                                     daThanhToan = x.DaThanhToan

                                 }).ToList();

                return allForManager;
            }
        }

        public IEnumerable<PhieuDatHangViewModel> thongTinPhieuDatHangTheoMa(int soPhieuDatHang)
        {
            IQueryable<PhieuDatHang> danhSachPhieuBanHang = _phieuDatHangRepo.GetAll();
            List<PhieuDatHangViewModel> all = new List<PhieuDatHangViewModel>();

            all = (from phieuDatHang in danhSachPhieuBanHang
                   where phieuDatHang.SoPhieuDatHang == soPhieuDatHang
                   select new
                   {
                       SoPhieuDatHang = phieuDatHang.SoPhieuDatHang,
                       NgayDat = phieuDatHang.NgayDat,
                       MaNhanVien = phieuDatHang.MaNhanVien,
                       TenKhachHang = phieuDatHang.TenKhachHang,
                       SoDienThoai = phieuDatHang.SoDienThoai,
                       DiaChi = phieuDatHang.Diachi,
                       Email = phieuDatHang.Email,
                       TongTien = phieuDatHang.TongTien,
                       HinhThucThanhToan = phieuDatHang.HinhThucThanhToan,
                       GhiChu = phieuDatHang.Ghichu,
                       NgayGiao = phieuDatHang.NgayGiao,
                       DaXacNhan = phieuDatHang.DaXacNhan,
                       DaThanhToan = phieuDatHang.DaThanhToan

                   }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                   {
                       soPhieuDatHang = x.SoPhieuDatHang,
                       ngayDat = x.NgayDat,
                       maNhanVien = x.MaNhanVien,
                       tenKhachHang = x.TenKhachHang,
                       soDienThoai = x.SoDienThoai,
                       diaChi = x.DiaChi,
                       email = x.Email,
                       tongTien = x.TongTien,
                       hinhThucThanhToan = x.HinhThucThanhToan,
                       ghiChu = x.GhiChu,
                       ngayGiao = x.NgayGiao,
                       daXacNhan = x.DaXacNhan,
                       daThanhToan = x.DaThanhToan
                   }).ToList();

            return all;
        }

        public async Task DeletePhieuDatHang(object deleteModel)
        {
            PhieuDatHang xoaPhieuKiemKho = (PhieuDatHang)deleteModel;

            await _phieuDatHangRepo.DeleteAsync(xoaPhieuKiemKho);
        }

        public int LaySoDonDatHang()
        {
            return _phieuDatHangRepo.GetAll().Where(i => i.DaXacNhan == false).Count();
        }
    }
}
