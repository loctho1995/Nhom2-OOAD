﻿using Common.Models;
using Common.ViewModels;
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
        private readonly ChiTietPhieuBanHangRepository _chiTietPhieuBanHangRepo;
        private readonly PhieuBanHangRepository _phieuBanHangRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private readonly LoaiHangHoaRepository _loaiHangHoaRepo;
        private NhanVienBusiness _nhanVienBus;

        public HangHoaBusiness()
        {
            dbContext = new SMSEntities();
            _phieuKiemKhoRepo = new PhieuKiemKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _phieuBanHangRepo = new PhieuBanHangRepository(dbContext);
            _chiTietPhieuBanHangRepo = new ChiTietPhieuBanHangRepository(dbContext);
            _loaiHangHoaRepo = new LoaiHangHoaRepository(dbContext);
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
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiamGia,
                   }).ToList();
            return all;
        }

        public IEnumerable<HangHoa> DanhSachHangHoaBanChayNhat()
        {
            var orders = (from od in _chiTietPhieuBanHangRepo.GetAll().GroupBy(m => m.MaHangHoa)
                          join hanghoa in _hangHoaRepo.GetAll()
                          on od.Key equals hanghoa.MaHangHoa
                         select new
                         {
                             MaHangHoa = od.Key,                           
                             SoLuong = od.Sum(m => m.SoLuong),
                             HinhAnh = hanghoa.HinhAnh,
                             TenHangHoa = hanghoa.TenHangHoa,
                             GiaBan = hanghoa.GiaBan,
                             GiamGia = hanghoa.GiamGia,

                         }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       SoLuongTon = x.SoLuong,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiamGia = x.GiamGia,
                       GiaBan = x.GiaBan,
                   }).Distinct().Take(6).ToList();

            return orders;
        }

        public IEnumerable<HangHoa> TimKiemHangHoa(string key)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where hanghoa.TenHangHoa.Equals(key)
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiamGia,
                   }).ToList();
            return all;
        }

        public IEnumerable<HangHoa> LoadHangHoaTheoMa(int maHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where (hanghoa.MaHangHoa.Equals(maHangHoa))
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       ThongSoKyThuat = hanghoa.ThongSoKyThuat,
                       SoLuongTon = hanghoa.SoLuongTon,
                       ThoiGianBaoHanh = hanghoa.ThoiGianBaoHanh,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiamGia,
                       ThongSoKyThuat = x.ThongSoKyThuat,
                       SoLuongTon = x.SoLuongTon,
                       ThoiGianBaoHanh = x.ThoiGianBaoHanh,
                   }).ToList();
            return all;
        }

        public IList<HangHoaViewModel> DanhSachHangHoaTheoMaLoaiHangHoa(int maLoaiHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoaViewModel> all = new List<HangHoaViewModel>();

            all = (from hanghoa in danhSachHangHoa
                   join loaihanghoa in _loaiHangHoaRepo.GetAll()
                   on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                   where (hanghoa.MaLoaiHangHoa.Equals(maLoaiHangHoa))
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiaKhuyenMai = hanghoa.GiamGia,
                       TenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,
                   }).AsEnumerable().Select(x => new HangHoaViewModel()
                   {
                       maHangHoa = x.MaHangHoa,
                       tenHangHoa = x.TenHangHoa,
                       hinhAnh = x.HinhAnh,
                       giaBan = x.GiaBan,
                       giamGia = x.GiaKhuyenMai,
                       tenLoaiHangHoa = x.TenLoaiHangHoa,
                   }).ToList();
            return all;
        }

        public IList<HangHoaViewModel> TenLoaiHangHoaTheoMaLoaiHangHoa(int maLoaiHangHoa)
        {          
            List<HangHoaViewModel> all = new List<HangHoaViewModel>();

            all = (from loaihanghoa in _loaiHangHoaRepo.GetAll()                  
                   where (loaihanghoa.MaLoaiHangHoa.Equals(maLoaiHangHoa))
                   select new
                   {
                       TenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,
                   }).AsEnumerable().Select(x => new HangHoaViewModel()
                   {
                       tenLoaiHangHoa = x.TenLoaiHangHoa,
                   }).ToList();
            return all;
        }

        public bool CapNhatHangHoaKhiTaoPhieuNhap(int maHangHoa, int soLuongNhap, decimal giaNhap)
        {
            try
            {
                var result = dbContext.HangHoas.FirstOrDefault(x => x.MaHangHoa == maHangHoa);
                if (result != null)
                {
                    result.HangHoaCode = "a";
                    result.GiaBan = Math.Round((result.SoLuongTon * result.GiaBan + soLuongNhap * giaNhap) / (result.SoLuongTon + soLuongNhap));
                    result.SoLuongTon += soLuongNhap;
                    dbContext.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaKhiXoaPhieuNhap(int soPhieuNhap, int maHangHoa, int soLuongNhap, decimal giaNhap)
        {
            try
            {
                var result = dbContext.ChiTietPhieuNhaps.FirstOrDefault(x => x.SoPhieuNhap == soPhieuNhap && x.MaHangHoa == maHangHoa);

                var a = dbContext.HangHoas.FirstOrDefault(x => x.MaHangHoa == maHangHoa);

                if (result != null)
                {
                    int sl = a.SoLuongTon - soLuongNhap;

                    a.HangHoaCode = "a";

                    a.GiaBan = Math.Round((a.GiaBan * a.SoLuongTon - soLuongNhap * giaNhap) / sl);

                    a.SoLuongTon -= soLuongNhap;

                    //result.GiaBan = Math.Round((result.SoLuongTon * result.GiaBan + soLuongNhap * giaNhap) / (result.SoLuongTon + soLuongNhap));
                    // result.SoLuongTon += soLuongNhap;
                    dbContext.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaKhiTaoPhieuXuat(int maHangHoa, int soLuongXuat)
        {
            try
            {
                var result = dbContext.HangHoas.FirstOrDefault(x => x.MaHangHoa == maHangHoa);
                if (result != null)
                {
                    result.HangHoaCode = "a";
                    result.SoLuongTon -= soLuongXuat;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaKhiXoaPhieuXuat(int maHangHoa, int soLuongXuat)
        {
            try
            {
                var result = dbContext.HangHoas.FirstOrDefault(x => x.MaHangHoa == maHangHoa);
                if (result != null)
                {
                    result.HangHoaCode = "a";
                    result.SoLuongTon += soLuongXuat;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public HangHoa ViewDetail(int id)
        {
            return dbContext.HangHoas.Find(id);
        }
    }
}

