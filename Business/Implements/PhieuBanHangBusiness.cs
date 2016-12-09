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

        public async Task Create(PhieuBanHangViewModel obj)
        {
            PhieuBanHang order = new PhieuBanHang
            {
                SoPhieuBanHang = obj.soPhieuBanHang,
                NgayBan = obj.ngayBan,
                MaNhanVien = obj.maNhanVien,
                Ghichu = obj.ghiChu,
                TenKhachHang = obj.tenKhachHang,
                SoDienThoai = obj.soDienThoai,
                TongTien = obj.tongTien,   
                TrangThai = true,
                NgayChinhSua = DateTime.Now            
            };

            order.ChiTetPhieuBanHangs = new List<ChiTietPhieuBanHang>();

            foreach(var i in obj.chiTietPhieuBanHang)
            {
                order.ChiTetPhieuBanHangs.Add(i);
            }

            await _phieuBanHangRepo.InsertAsync(order);
        }

        public IList<PhieuBanHangViewModel> ListView(string userName)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<PhieuBanHangViewModel> all = new List<PhieuBanHangViewModel>();
            List<PhieuBanHangViewModel> allForManager = new List<PhieuBanHangViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 4)
            {
                all = (from phieuBanHang in danhSachPhieuBanHang
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuBanHang.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.UserName.Equals(userName) 
                                && phieuBanHang.TrangThai.Equals(true))
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

        public IList<PhieuBanHangViewModel> SearchDanhSachPhieuBanHang(String key, string trangthai, DateTime tungay, DateTime denngay, string userName)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<PhieuBanHangViewModel> all = new List<PhieuBanHangViewModel>();
            List<PhieuBanHangViewModel> allForManager = new List<PhieuBanHangViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 5)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    all = (from phieubanhang in danhSachPhieuBanHang
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.UserName.Equals(userName) && (
                                     phieubanhang.SoPhieuBanHang.ToString().Contains(key)
                                  || nhanvien.TenNhanvien.Contains(key)
                                  || phieubanhang.TrangThai.ToString().Equals(trangthai)))
                           select new
                           {
                               SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                               NgayBan = phieubanhang.NgayBan,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieubanhang.TrangThai,
                               ChuThich = phieubanhang.Ghichu,

                           }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                           {
                               soPhieuBanHang = x.SoPhieuBanHang,
                               ngayBan = x.NgayBan,
                               tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                           }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return all;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    all = (from phieubanhang in danhSachPhieuBanHang
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.UserName.Equals(userName) && phieubanhang.TrangThai.ToString().Equals(trangthai))
                           select new
                           {
                               SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                               NgayBan = phieubanhang.NgayBan,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieubanhang.TrangThai,
                               ChuThich = phieubanhang.Ghichu,

                           }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                           {
                               soPhieuBanHang = x.SoPhieuBanHang,
                               ngayBan = x.NgayBan,
                               tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                           }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return all;
                }

                all = (from phieubanhang in danhSachPhieuBanHang
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.UserName.Equals(userName) && phieubanhang.TrangThai.Equals(true))
                       select new
                       {
                           SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                           NgayBan = phieubanhang.NgayBan,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TrangThai = phieubanhang.TrangThai,
                           ChuThich = phieubanhang.Ghichu,

                       }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                       {
                           soPhieuBanHang = x.SoPhieuBanHang,
                           ngayBan = x.NgayBan,
                           tenNhanVien = x.TenNhanVien,
                           trangThai = x.TrangThai,
                           ghiChu = x.ChuThich,
                       }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                return all;
            }
            else
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieubanhang in danhSachPhieuBanHang
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieubanhang.NgayBan >= tungay.Date && phieubanhang.NgayBan <= denngay.Date)
                                     select new
                                     {
                                         SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                                         NgayBan = phieubanhang.NgayBan,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieubanhang.TrangThai,
                                         ChuThich = phieubanhang.Ghichu,

                                     }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                     {
                                         soPhieuBanHang = x.SoPhieuBanHang,
                                         ngayBan = x.NgayBan,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieubanhang in danhSachPhieuBanHang
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieubanhang.SoPhieuBanHang.ToString().Contains(key)
                                            || nhanvien.TenNhanvien.Contains(key))
                                     select new
                                     {
                                         SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                                         NgayBan = phieubanhang.NgayBan,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieubanhang.TrangThai,
                                         ChuThich = phieubanhang.Ghichu,

                                     }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                     {
                                         soPhieuBanHang = x.SoPhieuBanHang,
                                         ngayBan = x.NgayBan,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    allForManager = (from phieubanhang in danhSachPhieuBanHang
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                                     where phieubanhang.TrangThai.ToString().Equals(trangthai)
                                     select new
                                     {
                                         SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                                         NgayBan = phieubanhang.NgayBan,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieubanhang.TrangThai,
                                         ChuThich = phieubanhang.Ghichu,

                                     }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                     {
                                         soPhieuBanHang = x.SoPhieuBanHang,
                                         ngayBan = x.NgayBan,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return allForManager;
                }

                allForManager = (from phieubanhang in danhSachPhieuBanHang
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                                 where phieubanhang.TrangThai.Equals(true)
                                 select new
                                 {
                                     SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                                     NgayBan = phieubanhang.NgayBan,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TrangThai = phieubanhang.TrangThai,
                                     ChuThich = phieubanhang.Ghichu,

                                 }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                 {
                                     soPhieuBanHang = x.SoPhieuBanHang,
                                     ngayBan = x.NgayBan,
                                     tenNhanVien = x.TenNhanVien,
                                     trangThai = x.TrangThai,
                                     ghiChu = x.ChuThich,
                                 }).OrderByDescending(x => x.soPhieuBanHang).ToList();
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
            xoaPhieuBanHang.NgayChinhSua = DateTime.Now;
            xoaPhieuBanHang.TrangThai = false;

            await _phieuBanHangRepo.EditAsync(xoaPhieuBanHang);
            //await _phieuBanHangRepo.DeleteAsync(xoaPhieuBanHang);
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
                       TrangThai = phieubanhang.TrangThai,

                   }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                   {
                       soPhieuBanHang = x.SoPhieuBanHang,
                       ngayBan = x.NgayBan,
                       tenNhanVien = x.TenNhanVien,
                       tenKhachHang = x.TenKhachHang,
                       soDienThoai = x.SoDienThoai,
                       tongTien = x.TongTien,
                       ghiChu = x.GhiChu,
                       trangThai = x.TrangThai
                       
                   }).ToList();        

            return all;
        }

        public int LoadSoPhieuBanHang()
        {
            var soPhieuKiemKho = from phieuBanHang in _phieuBanHangRepo.GetAll()
                                 orderby phieuBanHang.SoPhieuBanHang descending
                                 select phieuBanHang.SoPhieuBanHang;

            int demSoPhieu = _phieuBanHangRepo.GetAll().Count();
            if(demSoPhieu == 0)
            {
                return 1;
            }

            return (soPhieuKiemKho.First() + 1);
        }

        public IEnumerable<ThongTinHoatDongViewModel> ThongTinHoatDong()
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<ThongTinHoatDongViewModel> all = new List<ThongTinHoatDongViewModel>();

            all = (from phieubanhang in danhSachPhieuBanHang
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                   orderby phieubanhang.NgayChinhSua descending
                   select new
                   {
                       SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                       NgayChinhSua = phieubanhang.NgayChinhSua,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TrangThai = phieubanhang.TrangThai,
                   }).AsEnumerable().Select(x => new ThongTinHoatDongViewModel()
                   {
                       soPhieuBanHang = x.SoPhieuBanHang,
                       ngayChinhSuaBanHang = x.NgayChinhSua,
                       tenNhanVienBanHang = x.TenNhanVien,
                       trangThaiBanHang = x.TrangThai,
                   }).Take(1).ToList();
            return all;
        }
    }
}
