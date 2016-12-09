﻿using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;
using Common.ViewModels;
using Common.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class DatHangController :BaseController
    {
        //
        // GET: /Admin/BanHang/

        readonly PhieuDatHangBusiness _phieuDatHangBUS = new PhieuDatHangBusiness();
        readonly ChiTietPhieuDatHangBusiness _chiTietPhieuDatHangBus = new ChiTietPhieuDatHangBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();

        public ActionResult Index()
        {
            ViewBag.trangthai = new SelectList(new[]{ new { Value = "true", Text = "Hoàn thành" },
                                                    new { Value = "false", Text = "Đã hủy" }},
                                               "Value", "Text");

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

        //public ActionResult DanhSachPhieuDatHang(string searchString, int page = 1, int pageSize = 5)
        //{
        //    //if(!string.IsNullOrEmpty(searchString))
        //    //{
        //    //    return View(_phieuBanHangBUS.SearchDanhSachPhieuKiemKho(searchString, HomeController.nhanVienCode).ToPagedList(page, pageSize));
        //    //}

        //    return View(_phieuDatHangBUS.ListView(HomeController.nhanVienCode).ToPagedList(page, pageSize));

        //    //return View();
        //}

        public ActionResult DanhSachPhieuDatHang(string searchString, string trangthai, string dateFrom, string dateTo, int page = 1, int pageSize = 5)
        {
            return View(_phieuDatHangBUS.SearchDanhSachPhieuDatHang(searchString, trangthai, Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), HomeController.nhanVienCode).ToPagedList(page, pageSize));
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
      
        public async Task<ActionResult> XacNhanNhanHang(int id)
        {
            var updatePhieuDatHang = _phieuDatHangBUS.LayPhieuDatHang(id);
            updatePhieuDatHang.DaXacNhan = true;

            await _phieuDatHangBUS.Update(updatePhieuDatHang);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> XacNhanThanhToan(int id)
        {
            var updatePhieuDatHang = _phieuDatHangBUS.LayPhieuDatHang(id);
            updatePhieuDatHang.DaThanhToan = true;

            await _phieuDatHangBUS.Update(updatePhieuDatHang);

            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult>  Delete(int id)
        {
            PhieuDatHang deletePhieu = (PhieuDatHang)await _phieuDatHangBUS.Find(id);

            if(deletePhieu == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    await _phieuDatHangBUS.DeletePhieuDatHang(deletePhieu);

                    SetAlert("Đã xóa phiếu đặt hàng thành công!!!", "success");
                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy xóa lại", "error");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
