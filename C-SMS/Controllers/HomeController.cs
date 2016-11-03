using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        readonly LoaiHangHoaBusiness _loaiHangHoaBus = new LoaiHangHoaBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.hangHoaMoiNhat = _hangHoaBus.DanhSachHangHoaMoiNhat();
            ViewBag.hangHoaBanChayNhat = _hangHoaBus.DanhSachHangHoaBanChayNhat();
            return View();
        }

        public PartialViewResult DanhSachLoaiHangHoa()
        {
            var menuLoaiHangHoa = _loaiHangHoaBus.LoadDSLoaiHangHoa();
            return PartialView("~/Views/PartitalView/MenuManagerPartial.cshtml", menuLoaiHangHoa);
        }

        public ActionResult TimKiemSanPham(string SearchString)
        {
            ViewBag.TimKiemSanPham = _hangHoaBus.TimKiemHangHoa(SearchString);
            return View();
        }
    }
}