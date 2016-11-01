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
    public class PhieuNhapKhoBusiness
    {
        SMSEntities dbContext = null;
        private readonly PhieuNhapKhoRepository _phieuNhapKhoRepo;
        private readonly ChiTietPhieuNhapKhoRepository _chiTietPhieuNhapKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private readonly NhaCungCapRepository _nhaCungCapRepo;
        private HangHoaBusiness _hangHoaBus;
        private NhanVienBusiness _nhanVienBus;

        public PhieuNhapKhoBusiness()
        {
            dbContext = new SMSEntities();
            _phieuNhapKhoRepo = new PhieuNhapKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _nhaCungCapRepo = new NhaCungCapRepository(dbContext);
            _chiTietPhieuNhapKhoRepo = new ChiTietPhieuNhapKhoRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _hangHoaBus = new HangHoaBusiness();
            _nhanVienBus = new NhanVienBusiness();
        }
        public IList<PhieuNhapKhoViewModel> ListView(string maNhanVien)
        {
            IQueryable<PhieuNhap> dsPhieuNhap = _phieuNhapKhoRepo.GetAll();
            List<PhieuNhapKhoViewModel> all = new List<PhieuNhapKhoViewModel>();
            List<PhieuNhapKhoViewModel> allForManager = new List<PhieuNhapKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(maNhanVien) == 5)
            {
                all = (from phieunhap in dsPhieuNhap
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                       join nhacungcap in _nhaCungCapRepo.GetAll()
                       on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                       where (phieunhap.MaNhanVien.Equals(maNhanVien))
                       select new
                       {
                           SoPhieuNhap = phieunhap.SoPhieuNhap,
                           NgayNhap = phieunhap.NgayNhap,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TenNhaCungCap = nhacungcap.TenNhaCungCap,
                           TongTien = phieunhap.TongTien,
                           GhiChu = phieunhap.Ghichu,

                       }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                       {
                           soPhieuNhapKho = x.SoPhieuNhap,
                           ngayNhapKho = x.NgayNhap,
                           tenNhanVien = x.TenNhanVien,
                           tenNhaCungCap = x.TenNhaCungCap,
                           tongTien = x.TongTien,
                           ghiChu = x.GhiChu,
                       }).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieunhap in dsPhieuNhap
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                                 join nhacungcap in _nhaCungCapRepo.GetAll()
                                 on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                                 select new
                                 {
                                     SoPhieuNhap = phieunhap.SoPhieuNhap,
                                     NgayNhap = phieunhap.NgayNhap,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TenNhaCungCap = nhacungcap.TenNhaCungCap,
                                     TongTien = phieunhap.TongTien,
                                     GhiChu = phieunhap.Ghichu,

                                 }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                                 {
                                     soPhieuNhapKho = x.SoPhieuNhap,
                                     ngayNhapKho = x.NgayNhap,
                                     tenNhanVien = x.TenNhanVien,
                                     tenNhaCungCap = x.TenNhaCungCap,
                                     tongTien = x.TongTien,
                                     ghiChu = x.GhiChu,
                                 }).ToList();
                return allForManager;
            }
        }

        public int LoadSoPhieuNhapKho()
        {
            var soPhieuKiemKho = from phieunhapkho in _phieuNhapKhoRepo.GetAll()
                                 orderby phieunhapkho.SoPhieuNhap descending
                                 select phieunhapkho.SoPhieuNhap;

            int a = _phieuNhapKhoRepo.GetAll().Count();
            if (a == 0)
            {
                return 1;
            }
            return (soPhieuKiemKho.First() + 1);
        }

        public IList<PhieuNhapKhoViewModel> SearchDanhSachPhieuNhapKho(int soPhieuNhap, string nhanVienCode)
        {
            IQueryable<PhieuNhap> dsPhieuNhap = _phieuNhapKhoRepo.GetAll();
            List<PhieuNhapKhoViewModel> all = new List<PhieuNhapKhoViewModel>();
            List<PhieuNhapKhoViewModel> allForManager = new List<PhieuNhapKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(nhanVienCode) == 5)
            {
                all = (from phieunhap in dsPhieuNhap
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                       where (phieunhap.SoPhieuNhap.Equals(soPhieuNhap) && phieunhap.MaNhanVien.Equals(nhanVienCode))
                       select new
                       {
                           SoPhieuNhap = phieunhap.SoPhieuNhap,
                           NgayNhap = phieunhap.NgayNhap,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TongTien = phieunhap.TongTien,
                           GhiChu = phieunhap.Ghichu,

                       }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                       {
                           soPhieuNhapKho = x.SoPhieuNhap,
                           ngayNhapKho = x.NgayNhap,
                           tenNhanVien = x.TenNhanVien,
                           tongTien = x.TongTien,
                           ghiChu = x.GhiChu,
                       }).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieunhap in dsPhieuNhap
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                                 where (phieunhap.SoPhieuNhap.Equals(soPhieuNhap))
                                 select new
                                 {
                                     SoPhieuNhap = phieunhap.SoPhieuNhap,
                                     NgayNhap = phieunhap.NgayNhap,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TongTien = phieunhap.TongTien,
                                     GhiChu = phieunhap.Ghichu,

                                 }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                                 {
                                     soPhieuNhapKho = x.SoPhieuNhap,
                                     ngayNhapKho = x.NgayNhap,
                                     tenNhanVien = x.TenNhanVien,
                                     tongTien = x.TongTien,
                                     ghiChu = x.GhiChu,
                                 }).ToList();
                return allForManager;
            }
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuNhapKhoRepo.GetByIdAsync(ID);
        }

        public async Task DeletePhieuNhapKho(object deleteModel)
        {
            PhieuNhap xoaPhieuNhapKho = (PhieuNhap)deleteModel;

            await _phieuNhapKhoRepo.DeleteAsync(xoaPhieuNhapKho);
        }

        public bool DeleteChiTietPhieuNhapKho(int id)
        {
            try
            {
                var phieuNhapKho = dbContext.ChiTietPhieuNhaps.Where(x => x.SoPhieuNhap == id);
                foreach (var i in phieuNhapKho)
                {
                    _hangHoaBus.CapNhatHangHoaKhiXoaPhieuNhap(i.SoPhieuNhap, i.MaHangHoa, i.SoLuong, i.GiaNhap);
                }
                dbContext.ChiTietPhieuNhaps.RemoveRange(phieuNhapKho);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Create(PhieuNhapKhoViewModel O)
        {
            PhieuNhap phieuNhap = new PhieuNhap
            {
                SoPhieuNhap = O.soPhieuNhapKho,
                SoPhieuNhapCode = "a",
                NgayNhap = O.ngayNhapKho,
                MaNhanVien = O.maNhanVien,
                MaNhaCungCap = O.maNhaCungCap,
                TongTien = O.tongTien,
                Ghichu = O.ghiChu
            };
            foreach (var i in O.chiTietPhieuNhap)
            {
                _hangHoaBus.CapNhatHangHoaKhiTaoPhieuNhap(i.MaHangHoa, i.SoLuong, i.GiaNhap);            
                phieuNhap.ChiTietPhieuNhaps.Add(i);
                
            }
            await _phieuNhapKhoRepo.InsertAsync(phieuNhap);
        }

        public IList<PhieuNhapKhoViewModel> thongTinChiTietPhieuNhapKhoTheoMa(int soPhieuNhapKho)
        {
            IQueryable<ChiTietPhieuNhap> dsChiTietPhieuNhapKho = _chiTietPhieuNhapKhoRepo.GetAll();
            var all = (from chitietphieunhapkho in dsChiTietPhieuNhapKho
                       join hanghoa in _hangHoaRepo.GetAll()
                       on chitietphieunhapkho.MaHangHoa equals hanghoa.MaHangHoa
                       select new
                       {
                           SoPhieuNhapKho = chitietphieunhapkho.SoPhieuNhap,
                           MaHangHoa = hanghoa.MaHangHoa,
                           TenHangHoa = hanghoa.TenHangHoa,
                           DonViTinh = hanghoa.DonViTinh,
                           SoLuong = chitietphieunhapkho.SoLuong,
                           Gia = chitietphieunhapkho.GiaNhap,
                           ThanhTien = chitietphieunhapkho.ThanhTien,

                       }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                       {
                           soPhieuNhapKho = x.SoPhieuNhapKho,
                           maHangHoa = x.MaHangHoa,
                           tenHangHoa = x.TenHangHoa,
                           donViTinh = x.DonViTinh,
                           soLuong = x.SoLuong,
                           gia = x.Gia,
                           thanhTien = x.ThanhTien,
                       }).ToList();

            var information = (from i in all
                               where (soPhieuNhapKho == null || i.soPhieuNhapKho == soPhieuNhapKho)
                               select i).ToList();
            return information.ToList();
        }

        public IEnumerable<PhieuNhapKhoViewModel> thongTinPhieuNhapKhoTheoMa(int soPhieuNhapKho)
        {
            IQueryable<PhieuNhap> danhSachPhieuNhapKho = _phieuNhapKhoRepo.GetAll();
            List<PhieuNhapKhoViewModel> all = new List<PhieuNhapKhoViewModel>();

            all = (from phieunhapkho in danhSachPhieuNhapKho
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieunhapkho.MaNhanVien equals nhanvien.MaNhanVien
                   join nhacungcap in _nhaCungCapRepo.GetAll()
                   on phieunhapkho.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                   where (phieunhapkho.SoPhieuNhap.Equals(soPhieuNhapKho))
                   select new
                   {
                       SoPhieuNhapKho = phieunhapkho.SoPhieuNhap,
                       NgayNhapKho = phieunhapkho.NgayNhap,
                       TenNhaCungCap = nhacungcap.TenNhaCungCap,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TongTien = phieunhapkho.TongTien,
                       GhiChu = phieunhapkho.Ghichu,
                   }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                   {
                       soPhieuNhapKho = x.SoPhieuNhapKho,
                       ngayNhapKho = x.NgayNhapKho,
                       tenNhaCungCap = x.TenNhaCungCap,
                       tenNhanVien = x.TenNhanVien,
                       tongTien = x.TongTien,
                       ghiChu = x.GhiChu,
                   }).ToList();
            return all;
        }
    }
}
