using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class DatHangController : Controller
    {
        //
        // GET: /Admin/BanHang/

        readonly PhieuDatHangBusiness _phieuDatHangBUS = new PhieuDatHangBusiness();
        readonly ChiTietPhieuDatHangBusiness _chiTietPhieuDatHangBus = new ChiTietPhieuDatHangBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DanhSachPhieuDatHang(string searchString, int page = 1, int pageSize = 5)
        {
            //if(!string.IsNullOrEmpty(searchString))
            //{
            //    return View(_phieuBanHangBUS.SearchDanhSachPhieuKiemKho(searchString, HomeController.nhanVienCode).ToPagedList(page, pageSize));
            //}

            return View(_phieuDatHangBUS.ListView(HomeController.nhanVienCode).ToPagedList(page, pageSize));

            //return View();
        }

        public ActionResult ThongTinPhieuDatHang(int id)
        {
            ViewBag.chiTietPhieuDatHang = _chiTietPhieuDatHangBus.danhSachPhieuDatHangTheoMa(id).ToList();
            ViewBag.phieuDatHang = _phieuDatHangBUS.thongTinPhieuDatHangTheoMa(id).ToList();
            return View();
        }

        public ActionResult LoadThongTinHangHoa(int id)
        {
            var result = _hangHoaBus.LayThongTinHangHoa(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
