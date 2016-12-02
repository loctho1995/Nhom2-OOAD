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
    public class LoaiHangHoaBusiness
    {
        SMSEntities dbContext = null;
        private readonly LoaiHangHoaRepository _loaiHangHoaRepo;

        public LoaiHangHoaBusiness()
        {
            dbContext = new SMSEntities();
            _loaiHangHoaRepo = new LoaiHangHoaRepository(dbContext);
        }

        public IList<LoaiHangHoa> LoadDSLoaiHangHoa()
        {
            IQueryable<LoaiHangHoa> loaiHangHoa = _loaiHangHoaRepo.GetAll();
            return loaiHangHoa.ToList();
        }

        public IEnumerable<LoaiHangHoaViewModel> LoadDanhSachLoaiHangHoa()
        {
            IQueryable<LoaiHangHoa> danhSachLoaiHangHoa = _loaiHangHoaRepo.GetAll();
            List<LoaiHangHoaViewModel> all = new List<LoaiHangHoaViewModel>();

            all = (from nhanvien in danhSachLoaiHangHoa               
                   select new
                   {
                       MaLoaiHangHoa = nhanvien.MaLoaiHangHoa,
                       TenLoaiHangHoa = nhanvien.TenLoaiHangHoa,
                       PhanTramLoiNhuan = nhanvien.PhanTramLoiNhuan,
                   }).AsEnumerable().Select(x => new LoaiHangHoaViewModel()
                   {
                       maLoaiHangHoa = x.MaLoaiHangHoa,
                       tenLoaiHangHoa = x.TenLoaiHangHoa,
                       phanTramLoiNhuan = x.PhanTramLoiNhuan,                     
                   }).ToList();
            return all;

        }

        public async Task Create(object model)
        {
            var loaihanghoa = new LoaiHangHoa();
            LoaiHangHoaViewModel input = (LoaiHangHoaViewModel)model;

            loaihanghoa.TenLoaiHangHoa = input.tenLoaiHangHoa;
            loaihanghoa.PhanTramLoiNhuan = input.phanTramLoiNhuan;
            loaihanghoa.LoaiHangHoaCode = "";

            await _loaiHangHoaRepo.InsertAsync(loaihanghoa);
        }
        public List<Object> LoadLoaiHangHoa()
        {
            IQueryable<LoaiHangHoa> dsLoaiHangHoa = _loaiHangHoaRepo.GetAll();
            var list = (from loaihanghoa in dsLoaiHangHoa
                        select new SelectListItem
                        {
                            Text = loaihanghoa.TenLoaiHangHoa,
                            Value = loaihanghoa.MaLoaiHangHoa.ToString(),
                        });
            return new List<Object>(list);
        }
    }
}
