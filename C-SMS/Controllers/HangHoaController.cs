using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace WebBanHang.Controllers
{
    public class HangHoaController : Controller
    {
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();

        // GET: HangHoa
        public ActionResult Index()
        {
            ViewBag.hangHoaMoiNhat = _hangHoaBus.DanhSachHangHoaMoiNhat();
            return View();
        }

        public ActionResult ChiTietSanPham(int id)
        {
            var a = _hangHoaBus.LoadHangHoaTheoMa(id);
            return View(a);
        }

        public ActionResult DanhSachSanPham(int id, int page = 1, int pageSize = 5)
        {
            ViewBag.tenLoaiHangHoa = _hangHoaBus.TenLoaiHangHoaTheoMaLoaiHangHoa(id);
            var a = _hangHoaBus.DanhSachHangHoaTheoMaLoaiHangHoa(id).ToPagedList(page, pageSize);
            return View(a);
        }
    }
}