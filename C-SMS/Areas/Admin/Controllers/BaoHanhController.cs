﻿using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Common.Models;
using Common.ViewModels;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class BaoHanhController : BaseController
    {
        //
        // GET: /Admin/BaoHanh/

        readonly PhieuBaoHanhBusiness _phieuBaoHanhBus = new PhieuBaoHanhBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();


        public ActionResult Index()
        {
            ViewBag.trangthai = new SelectList(new[]{ new { Value = "true", Text = "Đang xử lý" },
                                                    new { Value = "false", Text = "Đã hủy" }},
                                               "Value", "Text");
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.maNhanVien = _nhanVienBus.LoadMaNhanVien(HomeController.nhanVienCode);
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.nhanVienCode);
            ViewBag.danhSachHangHoa = new SelectList(_hangHoaBus.LoadSanhSachHangHoa(), "Value", "Text");
            ViewBag.soPhieuBaoHanh = _phieuBaoHanhBus.LoadSoPhieuBaoHanh();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            PhieuBaoHanh delPhieuBaoHanh = (PhieuBaoHanh)await _phieuBaoHanhBus.Find(id);
            if (delPhieuBaoHanh == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    await _phieuBaoHanhBus.Delete(delPhieuBaoHanh);
                    SetAlert("Đã xóa phiếu bảo hành thành công!!!", "success");
                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy xóa lại", "error");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Confirm(int id)
        {
            PhieuBaoHanh confirmPhieuBaoHanh = (PhieuBaoHanh)await _phieuBaoHanhBus.Find(id);
            if (confirmPhieuBaoHanh == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    await _phieuBaoHanhBus.Confirm(confirmPhieuBaoHanh);
                    SetAlert("Đã xóa phiếu bảo hành thành công!!!", "success");
                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy xóa lại", "error");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> LuuPhieuBaoHanh(PhieuBaoHanhViewModel pbh)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    await _phieuBaoHanhBus.Create(pbh);
                    status = true;
                    SetAlert("Đã lưu phiếu bảo hành thành công!", "success");
                }
                else
                {
                    status = false;
                    SetAlert("Đã xảy ra lỗi! xin hãy tạo lại phiếu bán hàng", "error");
                }
            }
            catch (Exception ex)
            {
                SetAlert(ex.ToString(), "error");
            }

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult DanhSachPhieuBaoHanh(string searchString, string trangThai, string dateFrom, string dateTo, int page = 1, int pageSize = 5)
        {
            return View(_phieuBaoHanhBus.SearchDanhSachPhieuDatHang(searchString, trangThai, Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), HomeController.nhanVienCode).ToPagedList(page, pageSize));
        }

        public ActionResult ThongTinPhieuBaoHanh(int id)
        {
            ViewBag.phieuBaoHanh = _phieuBaoHanhBus.ThongTinPhieuBaoHanhTheoMa(id).ToList();
            return View();
        }
        public ActionResult LoadThongTinHangHoa(int id)
        {
            var result = _hangHoaBus.LayThongTinHangHoa(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
