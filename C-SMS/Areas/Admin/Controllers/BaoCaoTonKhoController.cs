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
    public class BaoCaoTonKhoController : Controller
    {
        //
        // GET: /Admin/BaoCaoTonKho/
        static int _thang, _nam;

        readonly BaoCaoTonKhoBusiness _baoCaoTonKhoBUS = new BaoCaoTonKhoBusiness();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachBaoCaoTonKho(string thang, string nam)
        {
            if (thang != "")
            {
                _thang = Convert.ToInt32(thang);
            }
            if (nam != "")
            {
                _nam = Convert.ToInt32(nam);
            }
            return View(_baoCaoTonKhoBUS.ListView(HomeController.nhanVienCode, _thang, _nam).ToList());
        }

        public ActionResult XuatFilePDF()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoTonKhoRP.rpt")));
            rd.SetDataSource(_baoCaoTonKhoBUS.ListView(HomeController.nhanVienCode, _thang, _nam).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "BaoCaoTonKhoRP.pdf");
        }

        public ActionResult XuatFileEXE()
        {
            GridView gv = new GridView();
            gv.DataSource = _baoCaoTonKhoBUS.ListView(HomeController.nhanVienCode, _thang, _nam).ToList();

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
