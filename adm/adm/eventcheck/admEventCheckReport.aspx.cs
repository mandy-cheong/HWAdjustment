using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_eventcheck_admEventCheckReport : System.Web.UI.Page
{
    private readonly EventCheckService _eventCheckService;
    public adm_eventcheck_admEventCheckReport()
    {

        _eventCheckService = new EventCheckService();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    protected void btnExport_Click(object sender, EventArgs e)
    {
        var eventid = txtSearchEventId.Text;
        var productIds = txtSearchProductID.Text;

        if (string.IsNullOrEmpty(eventid) && string.IsNullOrEmpty(productIds))
            return;

        var dtProducts = _eventCheckService.GetProducts(eventid, productIds);
        var dtItems = _eventCheckService.GetProductItems(eventid, productIds);

        var dts = new List<DataTable>();
        if (dtProducts.Rows != null && dtProducts != null)
        {
            dtProducts.TableName = "商品總覽";
            dts.Add(dtProducts);
        }
        if (dtItems.Rows != null && dtItems != null)
        {
            dtItems.TableName = "品項明細";
            dts.Add(dtItems);
        }
        
        ExportDataTableToExcelUseNpoi(dts, "活動檢查報表");

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var eventid = txtSearchEventId.Text;
        var productIds = txtSearchProductID.Text;

        if (string.IsNullOrEmpty(eventid) && string.IsNullOrEmpty(productIds))
            return;

        var dtProducts = _eventCheckService.GetProducts(eventid, productIds);
        var dtItems = _eventCheckService.GetProductItems(eventid, productIds);

        gvProductItems.DataSource = dtItems;
        gvProductItems.DataBind();
        gvProducts.DataSource = dtProducts;
        gvProducts.DataBind();
    }

    public static void ExportDataTableToExcelUseNpoi(List<DataTable> dts, string fileName)
    {
        MemoryStream ms = new MemoryStream();
        HSSFWorkbook wb = new HSSFWorkbook();
        int sval = 0;
        foreach (DataTable dt in dts)
        {
            //建立Excel 2003檔案

            ISheet ws;
            ////建立Excel 2007檔案
            //IWorkbook wb = new XSSFWorkbook();
            //ISheet ws;

            if (dt.TableName != string.Empty)
            {
                ws = wb.CreateSheet(dt.TableName);
            }
            else
            {
                sval += 1;
                ws = wb.CreateSheet("Sheet" + sval);
            }

            ws.CreateRow(0);//第一行為欄位名稱
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ws.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ws.GetRow(i + 1).CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                }
            }
        }
        wb.Write(ms);
        HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + fileName + ".xls"));
        HttpContext.Current.Response.BinaryWrite(ms.ToArray());
        wb = null;
        ms.Close();
        ms.Dispose();
    }

}