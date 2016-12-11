using Common.Models;
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
                              where (hanghoa.MaHangHoa == maHangHoa && hanghoa.TrangThai == true)
                              select new
                              {
                                  hanghoa.TenHangHoa,
                                  hanghoa.DonViTinh,
                                  hanghoa.SoLuongTon,
                                  hanghoa.GiaBan,
                                  hanghoa.GiamGia,
                                  hanghoa.ModelName
                              };
            return producInfor;
        }

        public IEnumerable<HangHoa> DanhSachHangHoaMoiNhat()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where (hanghoa.TrangThai == true)
                   orderby hanghoa.MaHangHoa descending
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
                   }).Take(6).ToList();
            return all;
        }

        public IEnumerable<HangHoa> DanhSachHangHoaBanChayNhat()
        {
            var orders = (from od in _chiTietPhieuBanHangRepo.GetAll().GroupBy(m => m.MaHangHoa)
                          join hanghoa in _hangHoaRepo.GetAll()
                          on od.Key equals hanghoa.MaHangHoa
                          where (hanghoa.TrangThai == true)
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
                   where (hanghoa.MaHangHoa.Equals(maHangHoa) && hanghoa.TrangThai == true)
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
                   where (hanghoa.MaLoaiHangHoa.Equals(maLoaiHangHoa) && hanghoa.TrangThai == true)
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

        public int LaySoLuongTonCuoiCuaThangTruoc(int maHangHoa, int thang, int nam)
        {
            if (thang == 1)
            {
                var result = dbContext.BaoCaoTonKhoes.FirstOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == 12 && x.Nam == (nam - 1));
                if (result != null)
                {
                    return result.SoLuongTonCuoi;
                }
                else
                {
                    return 0;
                }
            }

            var result1 = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == (thang - 1) && x.Nam == nam);
            if (result1 != null)
            {
                return result1.SoLuongTonCuoi;
            }
            else
            {
                return 0;
            }
        }

        public bool CapNhatHangHoaVaoBaoCaoTonKhoKhiTaoPhieuXuat(int maHangHoa, int soLuongXuat, int thang, int nam)
        {
            try
            {
                var result = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == thang && x.Nam == nam);
                if (result != null)
                {
                    result.SoLuongXuat += soLuongXuat;
                    result.SoLuongTonCuoi = result.SoLuongTonDau + result.SoLuongNhap - result.SoLuongXuat;
                    dbContext.SaveChanges();
                }
                else
                {
                    BaoCaoTonKho baoCaoTonKho = new BaoCaoTonKho
                    {
                        Thang = thang,
                        Nam = nam,
                        MaHangHoa = maHangHoa,
                        SoLuongTonDau = LaySoLuongTonCuoiCuaThangTruoc(maHangHoa, thang, nam),
                        SoLuongNhap = 0,
                        SoLuongXuat = soLuongXuat,
                        SoLuongTonCuoi = LaySoLuongTonCuoiCuaThangTruoc(maHangHoa, thang, nam) + 0 - soLuongXuat,
                    };

                    dbContext.BaoCaoTonKhoes.Add(baoCaoTonKho);
                    dbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaVaoBaoCaoTonKhoKhiTaoPhieuNhap(int maHangHoa, int soLuongNhap, int thang, int nam)
        {
            try
            {
                var result = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == thang && x.Nam == nam);
                if (result != null)
                {
                    result.SoLuongNhap += soLuongNhap;
                    result.SoLuongTonCuoi = result.SoLuongTonDau + result.SoLuongNhap - result.SoLuongXuat;
                    dbContext.SaveChanges();
                }
                else
                {
                    BaoCaoTonKho baoCaoTonKho = new BaoCaoTonKho
                    {
                        Thang = thang,
                        Nam = nam,
                        MaHangHoa = maHangHoa,
                        SoLuongTonDau = LaySoLuongTonCuoiCuaThangTruoc(maHangHoa, thang, nam),
                        SoLuongNhap = soLuongNhap,
                        SoLuongXuat = 0,
                        SoLuongTonCuoi = LaySoLuongTonCuoiCuaThangTruoc(maHangHoa, thang, nam) + soLuongNhap - 0,
                    };

                    dbContext.BaoCaoTonKhoes.Add(baoCaoTonKho);
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

        public bool CapNhatHangHoaVaoBaoCaoTonKhoKhiXoaPhieuXuat(int maHangHoa, int soLuongXuat, int thang, int nam)
        {
            try
            {
                var result = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == thang && x.Nam == nam);
                if (result != null)
                {
                    result.SoLuongXuat -= soLuongXuat;
                    result.SoLuongTonCuoi = result.SoLuongTonDau + result.SoLuongNhap - result.SoLuongXuat;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaVaoBaoCaoTonKhoKhiXoaPhieuNhap(int maHangHoa, int soLuongNhap, int thang, int nam)
        {
            try
            {
                var result = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == thang && x.Nam == nam);
                if (result != null)
                {
                    result.SoLuongNhap -= soLuongNhap;
                    result.SoLuongTonCuoi = result.SoLuongTonDau + result.SoLuongNhap - result.SoLuongXuat;
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
        public IList<HangHoaViewModel> SearchDanhSachHangHoa(String key, string trangthai, string maloaihanghoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoaViewModel> all = new List<HangHoaViewModel>();

            all = (from hanghoa in danhSachHangHoa
                   join loaihanghoa in _loaiHangHoaRepo.GetAll()
                   on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                   where (hanghoa.TrangThai.ToString().Equals(trangthai)
                        || hanghoa.MaLoaiHangHoa.ToString().Equals(maloaihanghoa)
                        || hanghoa.TenHangHoa.ToString().Contains(key)
                        || hanghoa.XuatXu.ToString().Contains(key))
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       SoLuongTon = hanghoa.SoLuongTon,
                       DonViTinh = hanghoa.DonViTinh,
                       MoTa = hanghoa.MoTa,
                       ThongSoKyThuat = hanghoa.ThongSoKyThuat,
                       XuatXu = hanghoa.XuatXu,
                       ThoiGianBaoHanh = hanghoa.ThoiGianBaoHanh,
                       HinhAnh = hanghoa.HinhAnh,
                       TenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,

                   }).AsEnumerable().Select(x => new HangHoaViewModel()
                   {
                       maHangHoa = x.MaHangHoa,
                       tenHangHoa = x.TenHangHoa,
                       giaBan = x.GiaBan,
                       giamGia = x.GiamGia,
                       soLuongTon = x.SoLuongTon,
                       donViTinh = x.DonViTinh,
                       moTa = x.MoTa,
                       thongSoKyThuat = x.ThongSoKyThuat,
                       xuatXu = x.XuatXu,
                       thoiGianBaoHanh = x.ThoiGianBaoHanh,
                       hinhAnh = x.HinhAnh,
                       tenLoaiHangHoa = x.TenLoaiHangHoa
                   }).ToList();
            return all;
        }
        public IEnumerable<HangHoaViewModel> LoadDanhSachHangHoa()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoaViewModel> all = new List<HangHoaViewModel>();

            all = (from hanghoa in danhSachHangHoa
                   join loaihanghoa in _loaiHangHoaRepo.GetAll()
                   on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                   where (hanghoa.TrangThai.Equals(true))
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       SoLuongTon = hanghoa.SoLuongTon,
                       DonViTinh = hanghoa.DonViTinh,
                       MoTa = hanghoa.MoTa,
                       ThongSoKyThuat = hanghoa.ThongSoKyThuat,
                       XuatXu = hanghoa.XuatXu,
                       ThoiGianBaoHanh = hanghoa.ThoiGianBaoHanh,
                       HinhAnh = hanghoa.HinhAnh,
                       TrangThai = hanghoa.TrangThai,
                       TenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,
                   }).AsEnumerable().Select(x => new HangHoaViewModel()
                   {
                       maHangHoa = x.MaHangHoa,
                       tenHangHoa = x.TenHangHoa,
                       giaBan = x.GiaBan,
                       giamGia = x.GiamGia,
                       soLuongTon = x.SoLuongTon,
                       donViTinh = x.DonViTinh,
                       moTa = x.MoTa,
                       thongSoKyThuat = x.ThongSoKyThuat,
                       xuatXu = x.XuatXu,
                       thoiGianBaoHanh = x.ThoiGianBaoHanh,
                       hinhAnh = x.HinhAnh,
                       trangThai = x.TrangThai,
                       tenLoaiHangHoa = x.TenLoaiHangHoa
                   }).ToList();
            return all;

        }
        public async Task Create(object model)
        {
            var hangHoa = new HangHoa();
            HangHoaViewModel input = (HangHoaViewModel)model;

            hangHoa.TenHangHoa = input.tenHangHoa;
            hangHoa.GiaBan = input.giaBan;
            hangHoa.DonViTinh = input.donViTinh;
            hangHoa.MoTa = input.moTa;
            hangHoa.ThongSoKyThuat = input.thongSoKyThuat;
            hangHoa.XuatXu = input.xuatXu;
            hangHoa.ThoiGianBaoHanh = input.thoiGianBaoHanh;
            hangHoa.HinhAnh = input.hinhAnh;
            hangHoa.MaLoaiHangHoa = input.maLoaiHangHoa;
            hangHoa.TrangThai = true;

            await _hangHoaRepo.InsertAsync(hangHoa);
        }
        public IEnumerable<HangHoaViewModel> LoadDanhSachHangHoaTheoMa(int maHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();

            var all = (from hanghoa in danhSachHangHoa
                       join loaihanghoa in _loaiHangHoaRepo.GetAll()
                       on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                       where (hanghoa.MaHangHoa.Equals(maHangHoa))
                       select new HangHoaViewModel
                       {
                           maHangHoa = hanghoa.MaHangHoa,
                           tenHangHoa = hanghoa.TenHangHoa,
                           giaBan = hanghoa.GiaBan,
                           giamGia = hanghoa.GiamGia,
                           soLuongTon = hanghoa.SoLuongTon,
                           donViTinh = hanghoa.DonViTinh,
                           moTa = hanghoa.MoTa,
                           thongSoKyThuat = hanghoa.ThongSoKyThuat,
                           xuatXu = hanghoa.XuatXu,
                           thoiGianBaoHanh = hanghoa.ThoiGianBaoHanh,
                           hinhAnh = hanghoa.HinhAnh,
                           tenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,
                           trangThai = hanghoa.TrangThai,
                       }).ToList();

            return all;
        }
        public async Task<object> Find(int ID)
        {
            return await _hangHoaRepo.GetByIdAsync(ID);
        }
        public async Task Update(object inputModel, object editModel)
        {
            HangHoaViewModel input = (HangHoaViewModel)inputModel;
            HangHoa editHangHoa = (HangHoa)editModel;

            editHangHoa.TenHangHoa = input.tenHangHoa;
            editHangHoa.GiaBan = input.giaBan;
            editHangHoa.DonViTinh = input.donViTinh;
            editHangHoa.MoTa = input.moTa;
            editHangHoa.ThongSoKyThuat = input.thongSoKyThuat;
            editHangHoa.XuatXu = input.xuatXu;
            editHangHoa.ThoiGianBaoHanh = input.thoiGianBaoHanh;
            editHangHoa.HinhAnh = "abc.png";
            editHangHoa.MaLoaiHangHoa = input.maLoaiHangHoa;
            editHangHoa.TrangThai = true;

            await _hangHoaRepo.EditAsync(editHangHoa);
        }

        public async Task Delete(object editModel)
        {
            HangHoa editHangHoa = (HangHoa)editModel;

            editHangHoa.TrangThai = false;

            await _hangHoaRepo.EditAsync(editHangHoa);
        }

        
    }
}