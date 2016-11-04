﻿using Common.Models;
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
    public class PhieuXuatKhoBusiness
    {
        SMSEntities dbContext = null;
        private readonly PhieuXuatKhoRepository _phieuXuatKhoRepo;
        private readonly ChiTietPhieuXuathoRepository _chiTietPhieuXuatKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private NhanVienBusiness _nhanVienBus;
        private HangHoaBusiness _hangHoaBus;

        public PhieuXuatKhoBusiness()
        {
            dbContext = new SMSEntities();
            _phieuXuatKhoRepo = new PhieuXuatKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _chiTietPhieuXuatKhoRepo = new ChiTietPhieuXuathoRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
            _hangHoaBus = new HangHoaBusiness();
        }

        public IList<PhieuXuatKhoViewModel> SearchDanhSachPhieuXuatKho(String key, DateTime tungay, DateTime denngay, string nhanVienCode)
        {
            IQueryable<PhieuXuatKho> danhSachPhieuXuatKho = _phieuXuatKhoRepo.GetAll();
            List<PhieuXuatKhoViewModel> all = new List<PhieuXuatKhoViewModel>();
            List<PhieuXuatKhoViewModel> allForManager = new List<PhieuXuatKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(nhanVienCode) == 5)
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    all = (from phieuxuat in danhSachPhieuXuatKho
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieuxuat.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.NhanVienCode.Equals(nhanVienCode) && phieuxuat.NgayXuat >= tungay.Date && phieuxuat.NgayXuat <= denngay.Date)
                           select new
                           {
                               SoPhieuXuatKho = phieuxuat.SoPhieuXuatKho,
                               NgayXuat = phieuxuat.NgayXuat,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTien = phieuxuat.TongTien,
                               LyDoXuat = phieuxuat.LyDoXuat,

                           }).AsEnumerable().Select(x => new PhieuXuatKhoViewModel()
                                     {
                                         soPhieuXuatKho = x.SoPhieuXuatKho,
                                         ngayXuatKho = x.NgayXuat,
                                         tenNhanVien = x.TenNhanVien,
                                         tongTien = x.TongTien,
                                         lyDoXuat = x.LyDoXuat,
                                     }).OrderByDescending(x => x.soPhieuXuatKho).ToList();
                    return all;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    all = (from phieuxuat in danhSachPhieuXuatKho
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieuxuat.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.NhanVienCode.Equals(nhanVienCode) && (
                                     phieuxuat.SoPhieuXuatKho.ToString().Contains(key)
                                  || nhanvien.TenNhanvien.Contains(key)))
                           select new
                           {
                               SoPhieuXuatKho = phieuxuat.SoPhieuXuatKho,
                               NgayXuat = phieuxuat.NgayXuat,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTien = phieuxuat.TongTien,
                               LyDoXuat = phieuxuat.LyDoXuat,

                           }).AsEnumerable().Select(x => new PhieuXuatKhoViewModel()
                           {
                               soPhieuXuatKho = x.SoPhieuXuatKho,
                               ngayXuatKho = x.NgayXuat,
                               tenNhanVien = x.TenNhanVien,
                               tongTien = x.TongTien,
                               lyDoXuat = x.LyDoXuat,
                           }).OrderByDescending(x => x.soPhieuXuatKho).ToList();
                    return all;
                }

                all = (from phieuxuat in danhSachPhieuXuatKho
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuxuat.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.NhanVienCode.Equals(nhanVienCode))
                       select new
                       {
                           SoPhieuXuatKho = phieuxuat.SoPhieuXuatKho,
                           NgayXuat = phieuxuat.NgayXuat,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TongTien = phieuxuat.TongTien,
                           LyDoXuat = phieuxuat.LyDoXuat,

                       }).AsEnumerable().Select(x => new PhieuXuatKhoViewModel()
                       {
                           soPhieuXuatKho = x.SoPhieuXuatKho,
                           ngayXuatKho = x.NgayXuat,
                           tenNhanVien = x.TenNhanVien,
                           tongTien = x.TongTien,
                           lyDoXuat = x.LyDoXuat,
                       }).OrderByDescending(x => x.soPhieuXuatKho).ToList();
                return all;
            }
            else
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieuxuat in danhSachPhieuXuatKho
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuxuat.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieuxuat.NgayXuat >= tungay.Date && phieuxuat.NgayXuat <= denngay.Date)
                                     select new
                                     {
                                         SoPhieuXuatKho = phieuxuat.SoPhieuXuatKho,
                                         NgayXuat = phieuxuat.NgayXuat,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TongTien = phieuxuat.TongTien,
                                         LyDoXuat = phieuxuat.LyDoXuat,

                                     }).AsEnumerable().Select(x => new PhieuXuatKhoViewModel()
                                     {
                                         soPhieuXuatKho = x.SoPhieuXuatKho,
                                         ngayXuatKho = x.NgayXuat,
                                         tenNhanVien = x.TenNhanVien,
                                         tongTien = x.TongTien,
                                         lyDoXuat = x.LyDoXuat,
                                     }).OrderByDescending(x => x.soPhieuXuatKho).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieuxuat in danhSachPhieuXuatKho
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuxuat.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieuxuat.SoPhieuXuatKho.ToString().Contains(key)
                                            || nhanvien.TenNhanvien.Contains(key))
                                     select new
                                     {
                                         SoPhieuXuatKho = phieuxuat.SoPhieuXuatKho,
                                         NgayXuat = phieuxuat.NgayXuat,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TongTien = phieuxuat.TongTien,
                                         LyDoXuat = phieuxuat.LyDoXuat,

                                     }).AsEnumerable().Select(x => new PhieuXuatKhoViewModel()
                                     {
                                         soPhieuXuatKho = x.SoPhieuXuatKho,
                                         ngayXuatKho = x.NgayXuat,
                                         tenNhanVien = x.TenNhanVien,
                                         tongTien = x.TongTien,
                                         lyDoXuat = x.LyDoXuat,
                                     }).OrderByDescending(x => x.soPhieuXuatKho).ToList();
                    return allForManager;
                }

                allForManager = (from phieuxuat in danhSachPhieuXuatKho
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieuxuat.MaNhanVien equals nhanvien.MaNhanVien
                                 select new
                                 {
                                     SoPhieuXuatKho = phieuxuat.SoPhieuXuatKho,
                                     NgayXuat = phieuxuat.NgayXuat,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TongTien = phieuxuat.TongTien,
                                     LyDoXuat = phieuxuat.LyDoXuat,

                                 }).AsEnumerable().Select(x => new PhieuXuatKhoViewModel()
                                 {
                                     soPhieuXuatKho = x.SoPhieuXuatKho,
                                     ngayXuatKho = x.NgayXuat,
                                     tenNhanVien = x.TenNhanVien,
                                     tongTien = x.TongTien,
                                     lyDoXuat = x.LyDoXuat,
                                 }).OrderByDescending(x => x.soPhieuXuatKho).ToList();
                return allForManager;
            }
        }

        public int LoadSoPhieuXuatKho()
        {
            var soPhieuXuatKho = from phieuxuatkho in _phieuXuatKhoRepo.GetAll()
                                 orderby phieuxuatkho.SoPhieuXuatKho descending
                                 select phieuxuatkho.SoPhieuXuatKho;

            int a = _phieuXuatKhoRepo.GetAll().Count();
            if (a == 0)
            {
                return 1;
            }
            return (soPhieuXuatKho.First() + 1);
        }

        public IList<PhieuXuatKhoViewModel> thongTinChiTietPhieuXuatKhoTheoMa(int soPhieuXuatKho)
        {
            IQueryable<ChiTietPhieuXuatKho> dsChiTietPhieuNhapKho = _chiTietPhieuXuatKhoRepo.GetAll();
            var all = (from chitietphieuxuatkho in dsChiTietPhieuNhapKho
                       join hanghoa in _hangHoaRepo.GetAll()
                       on chitietphieuxuatkho.MaHangHoa equals hanghoa.MaHangHoa
                       select new
                       {
                           SoPhieuXuatKho = chitietphieuxuatkho.SoPhieuXuatKho,
                           MaHangHoa = hanghoa.MaHangHoa,
                           TenHangHoa = hanghoa.TenHangHoa,
                           DonViTinh = hanghoa.DonViTinh,
                           SoLuong = chitietphieuxuatkho.SoLuong,
                           Gia = chitietphieuxuatkho.Gia,
                           ThanhTien = chitietphieuxuatkho.ThanhTien,

                       }).AsEnumerable().Select(x => new PhieuXuatKhoViewModel()
                   {
                       soPhieuXuatKho = x.SoPhieuXuatKho,
                       maHangHoa = x.MaHangHoa,
                       tenHangHoa = x.TenHangHoa,
                       donViTinh = x.DonViTinh,
                       soLuong = x.SoLuong,
                       gia = x.Gia,
                       thanhTien = x.ThanhTien,
                   }).ToList();

            var information = (from i in all
                               where (soPhieuXuatKho == null || i.soPhieuXuatKho == soPhieuXuatKho)
                               select i).ToList();
            return information.ToList();
        }

        public IEnumerable<PhieuXuatKhoViewModel> thongTinPhieuXuatKhoTheoMa(int soPhieuXuatKho)
        {
            IQueryable<PhieuXuatKho> danhSachPhieuXuatKho = _phieuXuatKhoRepo.GetAll();
            List<PhieuXuatKhoViewModel> all = new List<PhieuXuatKhoViewModel>();

            all = (from phieuxuatkho in danhSachPhieuXuatKho
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieuxuatkho.MaNhanVien equals nhanvien.MaNhanVien
                   where (phieuxuatkho.SoPhieuXuatKho.Equals(soPhieuXuatKho))
                   select new
                   {
                       SoPhieuXuatKho = phieuxuatkho.SoPhieuXuatKho,
                       NgayXuatKho = phieuxuatkho.NgayXuat,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TongTien = phieuxuatkho.TongTien,
                       LyDoXuat = phieuxuatkho.LyDoXuat,
                   }).AsEnumerable().Select(x => new PhieuXuatKhoViewModel()
                   {
                       soPhieuXuatKho = x.SoPhieuXuatKho,
                       ngayXuatKho = x.NgayXuatKho,
                       tenNhanVien = x.TenNhanVien,
                       tongTien = x.TongTien,
                       lyDoXuat = x.LyDoXuat,
                   }).ToList();
            return all;
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuXuatKhoRepo.GetByIdAsync(ID);
        }

        public async Task DeletePhieuXuatKho(object deleteModel)
        {
            PhieuXuatKho xoaPhieuXuatKho = (PhieuXuatKho)deleteModel;


            await _phieuXuatKhoRepo.DeleteAsync(xoaPhieuXuatKho);
        }

        public bool DeleteChiTietPhieuXuatKho(int id)
        {
            try
            {
                var phieuXuatKho = dbContext.ChiTietPhieuXuatKhoes.Where(x => x.SoPhieuXuatKho == id);
                int thang = dbContext.PhieuXuatKhoes.SingleOrDefault(x => x.SoPhieuXuatKho == id).NgayXuat.Month;
                int nam = dbContext.PhieuXuatKhoes.SingleOrDefault(x => x.SoPhieuXuatKho == id).NgayXuat.Year;

                foreach (var i in phieuXuatKho)
                {
                    _hangHoaBus.CapNhatHangHoaKhiXoaPhieuXuat(i.MaHangHoa, i.SoLuong);
                    _hangHoaBus.CapNhatHangHoaVaoBaoCaoTonKhoKhiXoaPhieuXuat(i.MaHangHoa, i.SoLuong, thang, nam);
                }

                dbContext.ChiTietPhieuXuatKhoes.RemoveRange(phieuXuatKho);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Create(PhieuXuatKhoViewModel O)
        {
            PhieuXuatKho phieuXuat = new PhieuXuatKho
            {
                SoPhieuXuatKho = O.soPhieuXuatKho,
                SoPhieuXuatKhoCode = "a",
                NgayXuat = O.ngayXuatKho,
                MaNhanVien = O.maNhanVien,
                TongTien = O.tongTien,
                LyDoXuat = O.lyDoXuat
            };

            DateTime today = DateTime.Now;
            int thang = today.Month;
            int nam = today.Year;

            foreach (var i in O.chiTietPhieuXuatKho)
            {
                phieuXuat.ChiTietPhieuXuatKhos.Add(i);
                _hangHoaBus.CapNhatHangHoaKhiTaoPhieuXuat(i.MaHangHoa, i.SoLuong);
                _hangHoaBus.CapNhatHangHoaVaoBaoCaoTonKhoKhiTaoPhieuXuat(i.MaHangHoa, i.SoLuong, thang, nam);
            }
            await _phieuXuatKhoRepo.InsertAsync(phieuXuat);
        }
    }
}