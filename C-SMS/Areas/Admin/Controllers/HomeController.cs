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

namespace WebBanHang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        static HomeController curController;

        NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        ChucVuBusiness _chucVuBus = new ChucVuBusiness();

        public static string nhanVienCode = string.Empty;

        /// <summary>
        /// Home page, return notification if incorrect CSMS ID or Password
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            curController = this;
            if (Session["Account"] != null && Session["Account"].ToString() == "Error")
            {
                ViewBag.notify = "Incorrect CSMS ID or Password!";
            }
            return View();
        }

        /// <summary>
        /// Action Login:
        ///     - Check account
        ///     - Login
        ///     - Get authority
        /// </summary>
        /// <param name="f">input from form</param>
        /// <returns>Index page</returns>
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
                    TempData["notify"] = "Your account is deactive!!!<br> Please contact your Manager!!!";
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

        /// <summary>
        /// Thời gian làm việc of Seasson
        /// </summary>
        /// <param name="Username">Username</param>
        /// <param name="Aut">authority string</param>
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

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>
        ///     - Allways return to Index page: return RedirectToAction("Index")
        /// </returns>
        public ActionResult Logout()
        {
            Session["Account"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
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
            var menuModel = _chucVuBus.GetMenu(((NhanVienViewModel)Session["Account"]).maChucVu);
            ViewBag.listParent = _chucVuBus.GetListParent(((NhanVienViewModel)Session["Account"]).maChucVu);
            return PartialView("~/Areas/Admin/Views/PartitalView/MenuManagerPartial.cshtml", menuModel);
        }
    }
}