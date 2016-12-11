﻿using Business.Implements;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class BaoCaoPhieuChiController : Controller
    {
        //
        // GET: /Admin/BaoCaoPhieuChi/

        static DateTime _dateTo;
        static DateTime _dateFrom;

        readonly BaoCaoPhieuChiBusiness _baoCaoPhieuChiBUS = new BaoCaoPhieuChiBusiness();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachBaoCaoPhieuChi(string dateFrom, string dateTo)
        {
            if (dateFrom != "")
            {
                _dateFrom = Convert.ToDateTime(dateFrom);
            }
            if (dateTo != "")
            {
                _dateTo = Convert.ToDateTime(dateTo);
            }
            return View(_baoCaoPhieuChiBUS.ListView(HomeController.nhanVienCode, _dateFrom, _dateTo).ToList());
        }

        public ActionResult XuatFilePDF()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoPhieuChiRP.rpt")));
            rd.SetDataSource(_baoCaoPhieuChiBUS.ListView(HomeController.nhanVienCode, _dateFrom, _dateTo).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            rd.SetParameterValue("txtDateFrom", _dateFrom.ToString("dd/MM/yyyy"));
            rd.SetParameterValue("txtDateTo", _dateTo.ToString("dd/MM/yyyy"));
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "BaoCaoPhieuChiRP.pdf");
        }

        public ActionResult XuatFileEXE()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoPhieuChiRP.rpt")));
            rd.SetDataSource(_baoCaoPhieuChiBUS.ListView(HomeController.nhanVienCode, _dateFrom, _dateTo).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            rd.SetParameterValue("txtDateFrom", _dateFrom.ToString("dd/MM/yyyy"));
            rd.SetParameterValue("txtDateTo", _dateTo.ToString("dd/MM/yyyy"));
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/xls", "BaoCaoPhieuChiRP.xls");
        }
    }
}
