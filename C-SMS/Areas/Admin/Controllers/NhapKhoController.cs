using Business.Implements;
using Common.Ultil;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class NhapKhoController : Controller
    {
        readonly PhieuNhapKhoBusiness _phieuNhapKhoBus = new PhieuNhapKhoBusiness();
        readonly ChiTietPhieuNhapKhoBusiness _chiTietPhieuNhapKhoBus = new ChiTietPhieuNhapKhoBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        FormatNumber formatNumber = new FormatNumber();
        //
        // GET: /Admin/NhapKho/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult ChiTietPhieuNhapKho(int soPhieuNhapKho)
        {
            return View(_chiTietPhieuNhapKhoBus.DanhSachPhieuNhapKhoTheoMa(soPhieuNhapKho));
        }    
    }
}
