using Common.Models;
using Common.Ultil;
using Common.ViewModels;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class PhieuKiemKhoBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuKiemKhoRepository _phieuKiemKhoRepo;
        private readonly ChiTietPhieuKiemKhoRepository _chiTietPhieuKiemKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private NhanVienBusiness _nhanVienBus;

        public PhieuKiemKhoBusiness()
        {
            dbContext = new SMSEntities();
            _phieuKiemKhoRepo = new PhieuKiemKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _chiTietPhieuKiemKhoRepo = new ChiTietPhieuKiemKhoRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<KiemKhoViewModel> ListView(string nhanVienCode)
        {
            IQueryable<PhieuKiemKho> danhSachPhieuKiemKho = _phieuKiemKhoRepo.GetAll();
            List<KiemKhoViewModel> all = new List<KiemKhoViewModel>();
            List<KiemKhoViewModel> allForManager = new List<KiemKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(nhanVienCode) == 5)
            {
                all = (from phieukiemkho in danhSachPhieuKiemKho
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.NhanVienCode.Equals(nhanVienCode))
                       select new
                       {
                           SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                           NgayKiemKho = phieukiemkho.NgayKiemKho,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TrangThai = phieukiemkho.TrangThai,
                           ChuThich = phieukiemkho.GhiChu,
                       }).AsEnumerable().Select(x => new KiemKhoViewModel()
                       {
                           soPhieuKiemKho = x.SoPhieuKiemKho,
                           ngayKiemKho = x.NgayKiemKho,
                           tenNhanVien = x.TenNhanVien,
                           trangThai = x.TrangThai,
                           ghiChu = x.ChuThich,
                       }).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieukiemkho in danhSachPhieuKiemKho
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                                 select new
                                 {
                                     SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                                     NgayKiemKho = phieukiemkho.NgayKiemKho,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TrangThai = phieukiemkho.TrangThai,
                                     ChuThich = phieukiemkho.GhiChu,
                                 }).AsEnumerable().Select(x => new KiemKhoViewModel()
                                 {
                                     soPhieuKiemKho = x.SoPhieuKiemKho,
                                     ngayKiemKho = x.NgayKiemKho,
                                     tenNhanVien = x.TenNhanVien,
                                     trangThai = x.TrangThai,
                                     ghiChu = x.ChuThich,
                                 }).ToList();
                return allForManager;
            }
        }

        public IList<KiemKhoViewModel> SearchDanhSachPhieuKiemKho(int soPhieuKiemKho, string nhanVienCode)
        {
            IQueryable<PhieuKiemKho> danhSachPhieuKiemKho = _phieuKiemKhoRepo.GetAll();
            List<KiemKhoViewModel> all = new List<KiemKhoViewModel>();
            List<KiemKhoViewModel> allForManager = new List<KiemKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(nhanVienCode) == 5)
            {
                all = (from phieukiemkho in danhSachPhieuKiemKho
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.NhanVienCode.Equals(nhanVienCode) && phieukiemkho.SoPhieuKiemKho.Equals(soPhieuKiemKho))
                       select new
                       {
                           SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                           NgayKiemKho = phieukiemkho.NgayKiemKho,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TrangThai = phieukiemkho.TrangThai,
                           ChuThich = phieukiemkho.GhiChu,
                       }).AsEnumerable().Select(x => new KiemKhoViewModel()
                       {
                           soPhieuKiemKho = x.SoPhieuKiemKho,
                           ngayKiemKho = x.NgayKiemKho,
                           tenNhanVien = x.TenNhanVien,
                           trangThai = x.TrangThai,
                           ghiChu = x.ChuThich,
                       }).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieukiemkho in danhSachPhieuKiemKho
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                                 where (phieukiemkho.SoPhieuKiemKho.Equals(soPhieuKiemKho))
                                 select new
                                 {
                                     SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                                     NgayKiemKho = phieukiemkho.NgayKiemKho,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TrangThai = phieukiemkho.TrangThai,
                                     ChuThich = phieukiemkho.GhiChu,
                                 }).AsEnumerable().Select(x => new KiemKhoViewModel()
                                 {
                                     soPhieuKiemKho = x.SoPhieuKiemKho,
                                     ngayKiemKho = x.NgayKiemKho,
                                     tenNhanVien = x.TenNhanVien,
                                     trangThai = x.TrangThai,
                                     ghiChu = x.ChuThich,
                                 }).ToList();
                return allForManager;
            }
        }

        public int LoadSoPhieuKiemKho()
        {
            var soPhieuKiemKho = from phieukiemkho in _phieuKiemKhoRepo.GetAll()
                                 orderby phieukiemkho.SoPhieuKiemKho descending
                                 select phieukiemkho.SoPhieuKiemKho;

            int demSoPhieu = _phieuKiemKhoRepo.GetAll().Count();
            if (demSoPhieu == 0)
            {
                return 1;
            }
            return (soPhieuKiemKho.First() + 1);
        }

        public async Task Create(KiemKhoViewModel O)
        {
            PhieuKiemKho order = new PhieuKiemKho
            {
                SoPhieuKiemKho = O.soPhieuKiemKho,
                SoPhieuKiemKhoCode = "a",
                NgayKiemKho = O.ngayKiemKho,
                MaNhanVien = O.maNhanVien,
                TrangThai = false,
                GhiChu = O.ghiChu
            };
            foreach (var i in O.chiTietPhieuKiemKho)
            {
                order.ChiTietPhieuKiemKhos.Add(i);
            }
            await _phieuKiemKhoRepo.InsertAsync(order);
        }

        public IEnumerable<KiemKhoViewModel> thongTinChiTietPhieuKiemKhoTheoMa(int soPhieuKiemKho)
        {
            IQueryable<ChiTietPhieuKiemKho> dsPhieuKiemKho = _chiTietPhieuKiemKhoRepo.GetAll();

            var all = (from chitietphieukiemkho in dsPhieuKiemKho
                       join hanghoa in _hangHoaRepo.GetAll()
                       on chitietphieukiemkho.MaHangHoa equals hanghoa.MaHangHoa
                       select new
                       {
                           SoPhieuKiemKho = chitietphieukiemkho.SoPhieuKiemKho,                        
                           MaHangHoa = hanghoa.MaHangHoa,
                           SoLuongHienTai = chitietphieukiemkho.SoLuongHienTai,
                           SoLuongKiemTra = chitietphieukiemkho.SoLuongKiemTra,
                           TenHangHoa = hanghoa.TenHangHoa,
                           DonViTinh = hanghoa.DonViTinh,

                       }).AsEnumerable().Select(x => new KiemKhoViewModel()
                    {
                        soPhieuKiemKho = x.SoPhieuKiemKho,                   
                        maHangHoa = x.MaHangHoa,
                        soLuongHienTai = x.SoLuongHienTai,
                        soLuongKiemTra = x.SoLuongKiemTra,
                        tenHangHoa = x.TenHangHoa,
                        donViTinh = x.DonViTinh,
                    }).ToList();

            var information = (from i in all
                               where (soPhieuKiemKho == null || i.soPhieuKiemKho == soPhieuKiemKho)
                               select i).ToList();
            return information.ToList();
        }

        public IEnumerable<KiemKhoViewModel> thongTinPhieuKiemKhoTheoMa(int soPhieuKiemKho)
        {
            IQueryable<PhieuKiemKho> danhSachPhieuKiemKho = _phieuKiemKhoRepo.GetAll();
            List<KiemKhoViewModel> all = new List<KiemKhoViewModel>();

            all = (from phieukiemkho in danhSachPhieuKiemKho
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                   where (phieukiemkho.SoPhieuKiemKho.Equals(soPhieuKiemKho))
                   select new
                   {
                       SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                       NgayKiemKho = phieukiemkho.NgayKiemKho,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TrangThai = phieukiemkho.TrangThai,
                       ChuThich = phieukiemkho.GhiChu,
                   }).AsEnumerable().Select(x => new KiemKhoViewModel()
                   {
                       soPhieuKiemKho = x.SoPhieuKiemKho,
                       ngayKiemKho = x.NgayKiemKho,
                       tenNhanVien = x.TenNhanVien,
                       trangThai = x.TrangThai,
                       ghiChu = x.ChuThich,
                   }).ToList();
            return all;
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuKiemKhoRepo.GetByIdAsync(ID);
        }
        public async Task<object> FindChiTietPhieuKiemKho(int ID)
        {
            return await _chiTietPhieuKiemKhoRepo.GetByIdAsync(ID);
        }

        public async Task DeletePhieuKiemKho(object deleteModel)
        {
            PhieuKiemKho xoaPhieuKiemKho = (PhieuKiemKho)deleteModel;

            await _phieuKiemKhoRepo.DeleteAsync(xoaPhieuKiemKho);
        }

        public bool DeleteChiTietPhieuKiemKho(int id)
        {
            try
            {               
                var phieuKiemKho = dbContext.ChiTietPhieuKiemKhoes.Where(x => x.SoPhieuKiemKho == id);
                dbContext.ChiTietPhieuKiemKhoes.RemoveRange(phieuKiemKho);           
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
