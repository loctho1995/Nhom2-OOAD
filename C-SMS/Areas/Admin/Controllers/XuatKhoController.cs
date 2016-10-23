using Business.Implements;
using Common.Ultil;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class XuatKhoController : Controller
    {
        readonly PhieuXuatKhoBusiness _phieuXuatKhoBus = new PhieuXuatKhoBusiness();
        readonly ChiTietPhieuXuatKhoBusiness _chiTietPhieuXuatKhoBus = new ChiTietPhieuXuatKhoBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        //
        // GET: /Admin/XuatKho/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.maNhanVien = HomeController.nhanVienCode;
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.nhanVienCode);
            ViewBag.soPhieuXuatKhoTuTang = _phieuXuatKhoBus.LoadSoPhieuXuatKho();
            ViewBag.danhSachHangHoa = new SelectList(_hangHoaBus.LoadSanhSachHangHoa(), "Value", "Text");
            return View();
        }

        public ActionResult DanhSachPhieuXuatKho(string searchString, int page = 1, int pageSize = 5)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return View(_phieuXuatKhoBus.SearchDanhSachPhieuXuatKho(Convert.ToInt32(searchString), HomeController.nhanVienCode).ToPagedList(page, pageSize));
            }

            return View(_phieuXuatKhoBus.ListView(HomeController.nhanVienCode).ToPagedList(page, pageSize));
        }

        public ActionResult ThongTinPhieuXuatKho(int id)
        {
            ViewBag.chiTietPhieuXuatKho = _phieuXuatKhoBus.thongTinChiTietPhieuXuatKhoTheoMa(id).ToList();
            ViewBag.phieuXuatKho = _phieuXuatKhoBus.thongTinPhieuXuatKhoTheoMa(id).ToList();
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}
