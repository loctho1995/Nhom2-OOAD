using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;
using Common.ViewModels;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class LoaiHangHoaController : BaseController
    {
        readonly LoaiHangHoaBusiness _loaiHangHoaBus = new LoaiHangHoaBusiness();
        //
        // GET: /Admin/LoaiHangHoa/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(LoaiHangHoaViewModel loaiHangHoa)
        {                      
            try
            {
                await _loaiHangHoaBus.Create(loaiHangHoa);
                SetAlert("Đã thêm loại hàng hóa thành công!!!", "success");
            }
            catch
            {
                TempData["loaiHangHoa"] = loaiHangHoa;
                SetAlert("Đã xảy ra lỗi! Bạn hãy thêm lại", "error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DanhSachLoaiHangHoa(string searchString, int page = 1, int pageSize = 5)
        {          
            return View(_loaiHangHoaBus.LoadDanhSachLoaiHangHoa().ToPagedList(page, pageSize));
        }
    }
}
