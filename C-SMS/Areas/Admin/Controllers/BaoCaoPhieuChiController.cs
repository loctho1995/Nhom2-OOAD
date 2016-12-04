using Business.Implements;
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
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "BaoCaoPhieuChiRP.pdf");
        }

        public ActionResult XuatFileEXE()
        {
            GridView gv = new GridView();
            gv.DataSource = _baoCaoPhieuChiBUS.ListView(HomeController.nhanVienCode, _dateFrom, _dateTo).ToList();

            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=CustomerReport.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        }
    }
}
