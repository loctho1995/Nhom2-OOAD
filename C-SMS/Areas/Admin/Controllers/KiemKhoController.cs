using Business.Implements;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Common.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class KiemKhoController : BaseController
    {
        readonly PhieuKiemKhoBusiness _phieuKiemKhoBus = new PhieuKiemKhoBusiness();
        readonly ChiTietPhieuKiemKhoBusiness _chiTietPhieuKiemKhoBus = new ChiTietPhieuKiemKhoBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        //
        // GET: /Admin/KiemKho/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.maNhanVien = _nhanVienBus.LoadMaNhanVien(HomeController.nhanVienCode);
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.nhanVienCode);
            ViewBag.soPhieuKiemKhoTuTang = _phieuKiemKhoBus.LoadSoPhieuKiemKho();
            ViewBag.danhSachHangHoa = new SelectList(_hangHoaBus.LoadSanhSachHangHoa(), "Value", "Text");
            return View();
        }

        public ActionResult LoadThongTinHangHoa(int id)
        {
            var result = _hangHoaBus.LayThongTinHangHoa(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> LuuPhieuKiemKho(KiemKhoViewModel phieuKiemKho)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                await _phieuKiemKhoBus.Create(phieuKiemKho);
                status = true;
                SetAlert("Đã Lưu Phiếu Kiểm Kho Thành Công!!!", "success");
            }
            else
            {
                status = false;
                SetAlert("Đã Xảy Ra Lỗi! Bạn Hãy Tạo Lại Phiếu Kiểm Kho", "error");
            }
            return new JsonResult { Data = new { status = status } };
        }
      
        public ActionResult DanhSachPhieuKiemKho(string searchString, int page = 1, int pageSize = 5)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return View(_phieuKiemKhoBus.SearchDanhSachPhieuKiemKho(Convert.ToInt32(searchString), HomeController.nhanVienCode).ToPagedList(page, pageSize));
            }
            return View(_phieuKiemKhoBus.ListView(HomeController.nhanVienCode).ToPagedList(page, pageSize));
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            PhieuKiemKho deletePhieuKiemKho = (PhieuKiemKho)await _phieuKiemKhoBus.Find(id);

            if (deletePhieuKiemKho == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    _phieuKiemKhoBus.DeleteChiTietPhieuKiemKho(id);
                    await _phieuKiemKhoBus.DeletePhieuKiemKho(deletePhieuKiemKho);

                    SetAlert("Đã xóa phiếu kiểm kho thành công!!!", "success");
                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy xóa lại", "error");
                }
            }
            return RedirectToAction("Index");
        }   

        public ActionResult ThongTinPhieuKiemKho(int id)
        {
            ViewBag.chiTietPhieuKiemKho = _phieuKiemKhoBus.thongTinChiTietPhieuKiemKhoTheoMa(id).ToList();
            ViewBag.phieuKiemKho = _phieuKiemKhoBus.thongTinPhieuKiemKhoTheoMa(id).ToList();
            return View();
        }
    }
}
