using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Models;
using System.Web.Security;
using System.Net;
using System.Timers;
using Business.Implements;
using Common.ViewModels;
using System.Threading.Tasks;
using Common;
using Common.Ultil;
using System.IO;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        static HomeController curController;

        NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        PhieuDatHangBusiness _phieuDatHangBus = new PhieuDatHangBusiness();
        PhieuKiemKhoBusiness _phieuKiemKhoBus = new PhieuKiemKhoBusiness();
        PhieuXuatKhoBusiness _phieuXuatKhoBus = new PhieuXuatKhoBusiness();
        PhieuNhapKhoBusiness _phieuNhapKhoBus = new PhieuNhapKhoBusiness();
        PhieuBanHangBusiness _phieuBanHangBus = new PhieuBanHangBusiness();
        ChucVuBusiness _chucVuBus = new ChucVuBusiness();

        public static string nhanVienCode = string.Empty;

        public ActionResult Index()
        {
            curController = this;
            if (Session["Account"] != null && Session["Account"].ToString() == "Error")
            {
                TempData["notify"] = "ID hoặc Password không đúng!!!";
            }
            ViewBag.soPhieuDatHang = _phieuDatHangBus.LaySoDonDatHang();
            
            return View();
        }


        public PartialViewResult ThongTinHoatDong()
        {
            ViewBag.thongTinHoatDongKiemKho = _phieuKiemKhoBus.ThongTinHoatDong();
            ViewBag.thongTinHoatDongXuatKho = _phieuXuatKhoBus.ThongTinHoatDong();
            ViewBag.thongTinHoatDongNhapKho = _phieuNhapKhoBus.ThongTinHoatDong();
            ViewBag.thongTinHoatDongBanHang = _phieuBanHangBus.ThongTinHoatDong();
           
            return PartialView();
        }

        
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string Username = f["username"].ToString();
            string Password = f["Password"].ToString();

            NhanVienViewModel account = _nhanVienBus.Login(Username, Md5Encode.EncodePassword(Password));

            if (account != null)
            {
                if (account.trangThai != true)
                {
                    TempData["notify"] = "Tài khoản của bạn đã bị khóa!!!";
                }
                else
                {
                    string aut = _nhanVienBus.Authority(account);
                    Decentralization(account.maNhanVien.ToString(), aut);
                    Session["Account"] = account;
                    nhanVienCode = Username;
                }
            }
            else
            {
                TempData["notify"] = "ID hoặc Password không đúng!!!";
            }
            return RedirectToAction("Index");
        }

        public void Decentralization(string userName, string aut)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                          userName,
                                          DateTime.Now,
                                          DateTime.Now.AddHours(180),
                                          false,
                                          aut,
                                          FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }

        public ActionResult Logout()
        {
            Session["Account"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> UpdatePassword()
        {
            if (Session["Account"] != null)
            {
                ViewBag.employee = await _nhanVienBus.Find(((NhanVienViewModel)(Session["Account"])).maNhanVien);
                return View();
            }
            else
                return RedirectToAction("PermissionError", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(String PassWord)
        {
            NhanVien editEmployee = (NhanVien)await _nhanVienBus.Find(((NhanVienViewModel)(Session["Account"])).maNhanVien);

            try
            {
                await _nhanVienBus.UpdatePassword(editEmployee, Md5Encode.EncodePassword(PassWord));
                SetAlert("Successfull!!!", "success");
            }
            catch
            {
                SetAlert("Đã xảy ra lỗi! Bạn hãy cập nhật lại", "error");
            }
            return RedirectToAction("UpdatePassword");
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
                TempData["AlertType"] = "alert-success";
            else if (type == "warning")
                TempData["AlertType"] = "alert-warning";
            else if (type == "error")
                TempData["AlertType"] = "alert-danger";
        }

        public PartialViewResult GetMenu()
        {
            var menuModel = _chucVuBus.GetMenu( ((NhanVienViewModel)Session["Account"]).maChucVu );
            ViewBag.listParent = _chucVuBus.GetListParent(((NhanVienViewModel)Session["Account"]).maChucVu);
            return PartialView("~/Areas/Admin/Views/PartitalView/MenuManagerPartial.cshtml", menuModel);
        }
    }
}