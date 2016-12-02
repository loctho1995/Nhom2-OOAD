using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Common.ViewModels;
using System.Threading.Tasks;
using Common.Models;
using System.Drawing;
using System.IO;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class HangHoaController : BaseController
    {
        //
        // GET: /Admin/HangHoa/

        readonly HangHoaBusiness _hangHoaKhoBus = new HangHoaBusiness();
        readonly LoaiHangHoaBusiness _loaiHangHoaKhoBus = new LoaiHangHoaBusiness();
        public ActionResult Index()
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang Hoạt Động", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Không Hoạt Động", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            return View();
        }

        public ActionResult DanhSachHangHoa(string searchString, string trangthai, string loaihanghoa, int page = 1, int pageSize = 5)
        {
            if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(trangthai) || !string.IsNullOrEmpty(loaihanghoa))
            {
                return View(_hangHoaKhoBus.SearchDanhSachHangHoa(searchString, trangthai, loaihanghoa).ToPagedList(page, pageSize));
            }

            return View(_hangHoaKhoBus.LoadDanhSachHangHoa().ToPagedList(page, pageSize));
        }
        public ActionResult ThongTinHangHoa(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang Hoạt Động", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Không Hoạt Động", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            ViewBag.thongTinHangHoa = _hangHoaKhoBus.LoadDanhSachHangHoaTheoMa(id).ToList();

            return View(ViewBag.thongTinHangHoa);
        }

        [HttpPost]
        public async Task<ActionResult> Create(HangHoaViewModel hangHoa)
        {
            try
            {
                await _hangHoaKhoBus.Create(hangHoa);
                SetAlert("Đã thêm hàng hóa thành công!!!", "success");
            }
            catch
            {
                TempData["hangHoa"] = hangHoa;
                SetAlert("Đã xảy ra lỗi! Bạn hãy thêm lại", "error");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang hoạt động", Value = "1" });
            trangThai.Add(new SelectListItem { Text = "Không hoạt động", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            return View(_hangHoaKhoBus.LoadDanhSachHangHoaTheoMa(id).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, HangHoaViewModel HangHoa)
        {
            //Get nhân viên muốn update (find by ID)
            HangHoa edit = (HangHoa)await _hangHoaKhoBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Access Update from Business
                try
                {
                    await _hangHoaKhoBus.Update(HangHoa, edit);
                    SetAlert("Đã cập nhật hàng hóa thành công!!!", "success");

                }
                catch
                {
                    TempData["HangHoa"] = HangHoa;
                    SetAlert("Đã xảy ra lỗi! Bạn hãy cập nhật lại", "error");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            //Get nhân viên muốn update (find by ID)
            HangHoa edit = (HangHoa)await _hangHoaKhoBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Access Update from Business
                try
                {
                    await _hangHoaKhoBus.Delete(edit);
                    SetAlert("Đã xóa hàng hóa thành công!!!", "success");

                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy xóa lại", "error");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult ViewInfo(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang hoạt động", Value = "1" });
            trangThai.Add(new SelectListItem { Text = "Không hoạt động", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            return View(_hangHoaKhoBus.LoadDanhSachHangHoaTheoMa(id).ToList());
        }
    }
}
