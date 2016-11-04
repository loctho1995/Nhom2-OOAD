using Business.Implements;
using Common.Ultil;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;
using Common.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class NhapKhoController : BaseController
    {
        readonly PhieuNhapKhoBusiness _phieuNhapKhoBus = new PhieuNhapKhoBusiness();
        readonly ChiTietPhieuNhapKhoBusiness _chiTietPhieuNhapKhoBus = new ChiTietPhieuNhapKhoBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        readonly NhaCungCapBusiness _nhanCungCapBus = new NhaCungCapBusiness();
        //
        // GET: /Admin/NhapKho/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.nhaCungCap = _nhanCungCapBus.LoadNhaCungCap();
            ViewBag.maNhanVien = _nhanVienBus.LoadMaNhanVien(HomeController.nhanVienCode);
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.nhanVienCode);
            ViewBag.soPhieuNhapKhoTuTang = _phieuNhapKhoBus.LoadSoPhieuNhapKho();
            ViewBag.danhSachHangHoa = new SelectList(_hangHoaBus.LoadSanhSachHangHoa(), "Value", "Text");
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult LoadThongTinHangHoa(int id)
        {
            var result = _hangHoaBus.LayThongTinHangHoa(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult DanhSachPhieuNhapKho(string searchString, string dateFrom, string dateTo, int page = 1, int pageSize = 5)
        {
            return View(_phieuNhapKhoBus.SearchDanhSachPhieuNhapKho(searchString, Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), HomeController.nhanVienCode).ToPagedList(page, pageSize));
        }

        [HttpPost]
        public async Task<JsonResult> LuuPhieuNhapKho(PhieuNhapKhoViewModel phieuNhapKho)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                await _phieuNhapKhoBus.Create(phieuNhapKho);
                status = true;
                SetAlert("Đã Lưu Phiếu Nhập Kho Thành Công!!!", "success");
            }
            else
            {
                status = false;
                SetAlert("Đã Xảy Ra Lỗi! Bạn Hãy Tạo Lại Phiếu Nhập Kho", "error");
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            PhieuNhap deletePhieuNhapKho = (PhieuNhap)await _phieuNhapKhoBus.Find(id);

            if (deletePhieuNhapKho == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    _phieuNhapKhoBus.DeleteChiTietPhieuNhapKho(id);
                    await _phieuNhapKhoBus.DeletePhieuNhapKho(deletePhieuNhapKho);

                    SetAlert("Đã xóa phiếu nhập kho thành công!!!", "success");
                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy xóa lại", "error");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult ThongTinPhieuNhapKho(int id)
        {
            ViewBag.chiTietPhieuNhapKho = _phieuNhapKhoBus.thongTinChiTietPhieuNhapKhoTheoMa(id).ToList();
            ViewBag.phieuNhapKho = _phieuNhapKhoBus.thongTinPhieuNhapKhoTheoMa(id).ToList();
            return View();
        }
    }
}
