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
    public class BaoCaoHangHoaController : Controller
    {
        //
        // GET: /Admin/BaoCaoHangHoa/
        static bool _trangThai = true;

        readonly BaoCaoHangHoaBusiness _baoCaoHangHoaBUS = new BaoCaoHangHoaBusiness();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachBaoCaoHangHoa()//string trangThai
        {
            //if (trangThai != "")
            //{
            //    _trangThai = Convert.ToBoolean(trangThai);
            //}
            return View(_baoCaoHangHoaBUS.ListView(HomeController.nhanVienCode, _trangThai).ToList());
        }
        
        //xét cứng trạng thái true
        public ActionResult XuatFilePDF()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoHangHoaRP.rpt")));
            rd.SetDataSource(_baoCaoHangHoaBUS.ListView(HomeController.nhanVienCode, true).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "BaoCaoHangHoaRP.pdf");
        }

        public ActionResult XuatFileEXE()
        {
            GridView gv = new GridView();
            gv.DataSource = _baoCaoHangHoaBUS.ListView(HomeController.nhanVienCode, true).ToList();

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
