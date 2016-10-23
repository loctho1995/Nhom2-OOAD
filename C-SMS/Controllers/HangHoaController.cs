using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult ChiTietSanPham(string id)
        {
            var a = _hangHoaBus.LoadHangHoaTheoMa(id);
            return View(a);
        }

        public ActionResult DanhSachSanPham(string id)
        {
            var a = _hangHoaBus.DanhSachHangHoaTheoMaLoaiHangHoa(id);
            return View(a);
        }
    }
}