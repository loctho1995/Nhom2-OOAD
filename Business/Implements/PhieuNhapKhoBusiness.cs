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
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly NhaCungCapRepository _nhaCungCapRepo;
        private HangHoaBusiness _hangHoaBus;
        private NhanVienBusiness _nhanVienBus;

        public PhieuNhapKhoBusiness()
        {
            dbContext = new SMSEntities();
            _phieuNhapKhoRepo = new PhieuNhapKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _nhaCungCapRepo = new NhaCungCapRepository(dbContext);
            _hangHoaBus = new HangHoaBusiness();
            _nhanVienBus = new NhanVienBusiness();
        }
        public IList<PhieuNhapKhoViewModel> ListView(string maNhanVien)
        {
            IQueryable<PhieuNhap> dsPhieuNhap = _phieuNhapKhoRepo.GetAll();
            List<PhieuNhapKhoViewModel> all = new List<PhieuNhapKhoViewModel>();
            List<PhieuNhapKhoViewModel> allForManager = new List<PhieuNhapKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(maNhanVien) == 3)
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
                           nhaCungCap = x.TenNhaCungCap,
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
                           nhaCungCap = x.TenNhaCungCap,
                           tongTien = x.TongTien,
                           ghiChu = x.GhiChu,
                       }).ToList();
                return allForManager;
            }
        }

        public string LoadSoPhieuNhapKho()
        {
            CodeMasterGen codeMasterGen = new CodeMasterGen();
            string autoNo = "PNK" + codeMasterGen.AutoNumber(_phieuNhapKhoRepo.CountPhieuNhapKho(_phieuNhapKhoRepo) + 1);
            return autoNo;
        }
    }
}
