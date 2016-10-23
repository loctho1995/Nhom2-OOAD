﻿using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Common.ViewModels;
using System.Threading.Tasks;
using Common.Models;
using System.Net;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class NhanVienController : BaseController
    {
        readonly NhanVienBusiness _nhanVienKhoBus = new NhanVienBusiness();
        readonly ChucVuBusiness _chucVuKhoBus = new ChucVuBusiness();
        //
        // GET: /Admin/NhanVien/

        public ActionResult Index()
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang Hoạt Động", Value = "1" });
            trangThai.Add(new SelectListItem { Text = "Không Hoạt Động", Value = "0" });
            ViewBag.data = trangThai;
            return View();
        }

        public ActionResult DanhSachNhanVien(string searchString, int page = 1, int pageSize = 5)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return View(_nhanVienKhoBus.SearchDanhSachNhanVien(searchString).ToPagedList(page, pageSize));
            }

            return View(_nhanVienKhoBus.LoadDanhSachNhanVien().ToPagedList(page, pageSize));
        }

        public ActionResult ThongTinNhanVien(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang Hoạt Động", Value = "1" });
            trangThai.Add(new SelectListItem { Text = "Không Hoạt Động", Value = "0" });
            ViewBag.data = trangThai;
            ViewBag.chucvu = _chucVuKhoBus.LoadChucVu();
            ViewBag.thongTinNhanVien = _nhanVienKhoBus.LoadDanhSachNhanVienTheoMa(id).ToList();
      
            return View(ViewBag.thongTinNhanVien);
        }
        public ActionResult Create()
        {
            ViewBag.chucvu = _chucVuKhoBus.LoadChucVu();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(NhanVienViewModel nhanVien)
        {
            if (nhanVien.avatar == null)
            {
                nhanVien.avatar = "default.png";
            }

            try
            {
                await _nhanVienKhoBus.Create(nhanVien);
                SetAlert("Đã thêm nhân viên thành công!!!", "success");
            }
            catch
            {
                TempData["nhanVien"] = nhanVien;
                SetAlert("Đã xảy ra lỗi! Bạn hãy thêm lại", "error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang hoạt động", Value = "1" });
            trangThai.Add(new SelectListItem { Text = "Không hoạt động", Value = "0" });
            ViewBag.data = trangThai;
            ViewBag.chucvu = _chucVuKhoBus.LoadChucVu();
            return View(_nhanVienKhoBus.LoadDanhSachNhanVienTheoMa(id).ToList());
        }
       
        [HttpPost]
        public async Task<ActionResult> Edit(int id, NhanVienViewModel nhanVien)
        {
             //Get nhân viên muốn update (find by ID)
            NhanVien edit = (NhanVien)await _nhanVienKhoBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Access Update from Business
                try
                {
                    await _nhanVienKhoBus.Update(nhanVien, edit);
                    SetAlert("Đã cập nhật nhân viên thành công!!!", "success");

                }
                catch
                {
                    TempData["nhanVien"] = nhanVien;
                    SetAlert("Đã xảy ra lỗi! Bạn hãy thêm lại", "error");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang hoạt động", Value = "1" });
            trangThai.Add(new SelectListItem { Text = "Không hoạt động", Value = "0" });
            ViewBag.data = trangThai;
            ViewBag.chucvu = _chucVuKhoBus.LoadChucVu();
            return View(_nhanVienKhoBus.LoadDanhSachNhanVienTheoMa(id).ToList());
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //Get nhân viên muốn delete (find by ID)
            NhanVien edit = (NhanVien)await _nhanVienKhoBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Access delete from Business
                try
                {
                    await _nhanVienKhoBus.Delete(edit);
                    SetAlert("Đã xóa nhân viên thành công!!!", "success");

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