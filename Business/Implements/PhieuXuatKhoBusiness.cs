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
    public class PhieuXuatKhoBusiness
    {
        SMSEntities dbContext = null;
        private readonly PhieuXuatKhoRepository _phieuXuatKhoRepo;
        private readonly ChiTietPhieuXuathoRepository _chiTietPhieuXuatKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private NhanVienBusiness _nhanVienBus;

        public PhieuXuatKhoBusiness()
        {
            dbContext = new SMSEntities();
            _phieuXuatKhoRepo = new PhieuXuatKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _chiTietPhieuXuatKhoRepo = new ChiTietPhieuXuathoRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<PhieuXuatKhoViewModel> ListView(string nhanVienCode)
        {
            IQueryable<PhieuXuatKho> dsPhieuXuat = _phieuXuatKhoRepo.GetAll();
            List<PhieuXuatKhoViewModel> all = new List<PhieuXuatKhoViewModel>();
            List<PhieuXuatKhoViewModel> allForManager = new List<PhieuXuatKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(nhanVienCode) == 5)
            {
                all = (from phieuxuat in dsPhieuXuat
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
                           ngayXuat = x.NgayXuat,
                           tenNhanVien = x.TenNhanVien,
                           tongTien = x.TongTien,
                           lyDoXuat = x.LyDoXuat,
                       }).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieuxuat in dsPhieuXuat
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
                                     ngayXuat = x.NgayXuat,
                                     tenNhanVien = x.TenNhanVien,
                                     tongTien = x.TongTien,
                                     lyDoXuat = x.LyDoXuat,
                                 }).ToList();
                return allForManager;
            }
        }

         public string LoadSoPhieuXuatKho()
        {
            CodeMasterGen codeMasterGen = new CodeMasterGen();
            string autoNo = "PXK" + codeMasterGen.AutoNumber(_phieuXuatKhoRepo.CountPhieuXuatKho(_phieuXuatKhoRepo) + 1);
            return autoNo;
        }

         public IList<PhieuXuatKhoViewModel> SearchDanhSachPhieuXuatKho(int soPhieuXuatKho, string nhanVienCode)
         {
             IQueryable<PhieuXuatKho> dsPhieuXuat = _phieuXuatKhoRepo.GetAll();
             List<PhieuXuatKhoViewModel> all = new List<PhieuXuatKhoViewModel>();
             List<PhieuXuatKhoViewModel> allForManager = new List<PhieuXuatKhoViewModel>();

             if (_nhanVienBus.layMaChucVu(nhanVienCode) == 3)
             {
                 all = (from phieuxuat in dsPhieuXuat
                        join nhanvien in _nhanVienRepo.GetAll()
                        on phieuxuat.MaNhanVien equals nhanvien.MaNhanVien
                        where (phieuxuat.SoPhieuXuatKho.Equals(soPhieuXuatKho) && phieuxuat.MaNhanVien.Equals(nhanVienCode))
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
                            ngayXuat = x.NgayXuat,
                            tenNhanVien = x.TenNhanVien,
                            tongTien = x.TongTien,
                            lyDoXuat = x.LyDoXuat,
                        }).ToList();
                 return all;
             }
             else
             {
                 allForManager = (from phieuxuat in dsPhieuXuat
                                  join nhanvien in _nhanVienRepo.GetAll()
                                  on phieuxuat.MaNhanVien equals nhanvien.MaNhanVien
                                  where (phieuxuat.SoPhieuXuatKho.Equals(soPhieuXuatKho))
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
                                      ngayXuat = x.NgayXuat,
                                      tenNhanVien = x.TenNhanVien,
                                      tongTien = x.TongTien,
                                      lyDoXuat = x.LyDoXuat,
                                  }).ToList();
                 return allForManager;
             }
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
                        ngayXuat = x.NgayXuatKho,
                        tenNhanVien = x.TenNhanVien,
                        tongTien = x.TongTien,
                        lyDoXuat = x.LyDoXuat,
                    }).ToList();
             return all;
         }
    }
}
