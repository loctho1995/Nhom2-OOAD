﻿using Business.Implements;
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
    public class BanHangController :BaseController
    {
        //
        // GET: /Admin/BanHang/

        readonly PhieuBanHangBusiness _phieuBanHangBUS = new PhieuBanHangBusiness();
        readonly ChiTietPhieuBanHangBusiness _chiTietPhieuBanHangBus = new ChiTietPhieuBanHangBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();

        public ActionResult Index()
        {
            ViewBag.trangthai = new SelectList(new[]{ new { Value = "true", Text = "Hoàn thành" },
                                                    new { Value = "false", Text = "Đã hủy" }},
                                               "Value", "Text");

            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            PhieuBanHang deletePhieuBanHang = (PhieuBanHang)await _phieuBanHangBUS.Find(id);

            if(deletePhieuBanHang == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Access delete from Business
                try
                {
                    await _phieuBanHangBUS.Delete(deletePhieuBanHang);
                    SetAlert("Đã xóa phiếu bán hàng thành công!!!", "success");

                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy xóa lại", "error");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> LuuPhieuBanHang(PhieuBanHangViewModel phieuBanHang)
        {
            bool status = false;

            try
            {
                if(ModelState.IsValid)
                {
                    await _phieuBanHangBUS.Create(phieuBanHang);
                    status = true;
                    SetAlert("Đã lưu phiếu bán hàng thành công!", "success");
                }
                else
                {
                    status = false;
                    SetAlert("Đã xảy ra lỗi! xin hãy tạo lại phiếu bán hàng", "error");
                }
            }
            catch(Exception ex)
            {
                SetAlert(ex.ToString(), "success");
            }

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.maNhanVien = _nhanVienBus.LoadMaNhanVien(HomeController.nhanVienCode);
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.nhanVienCode);
            ViewBag.danhSachHangHoa = new SelectList(_hangHoaBus.LoadSanhSachHangHoa(), "Value", "Text");
            ViewBag.soPhieuBanHang = _phieuBanHangBUS.LoadSoPhieuBanHang();

            return View();
        }

        //public ActionResult DanhSachPhieuBanHang(string searchString, int page = 1, int pageSize = 5)
        //{
        //    //if(!string.IsNullOrEmpty(searchString))
        //    //{
        //    //    return View(_phieuBanHangBUS.SearchDanhSachPhieuKiemKho(searchString, HomeController.nhanVienCode).ToPagedList(page, pageSize));
        //    //}

        //    return View(_phieuBanHangBUS.ListView(HomeController.nhanVienCode).ToPagedList(page, pageSize));
        //}

        public ActionResult DanhSachPhieuBanHang(string searchString, string trangthai, string dateFrom, string dateTo, int page = 1, int pageSize = 5)
        {
            return View(_phieuBanHangBUS.SearchDanhSachPhieuBanHang(searchString, trangthai, Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), HomeController.nhanVienCode).ToPagedList(page, pageSize));
        }

        public ActionResult ThongTinPhieuBanHang(int id)
        {
            ViewBag.chiTietPhieuBanHang = _chiTietPhieuBanHangBus.danhSachPhieuBanHangTheoMa(id).ToList();
            ViewBag.phieuBanHang = _phieuBanHangBUS.thongTinPhieuBanHangTheoMa(id).ToList();
            return View();
        }

        public ActionResult LoadThongTinHangHoa(int id)
        {
            var result = _hangHoaBus.LayThongTinHangHoa(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
