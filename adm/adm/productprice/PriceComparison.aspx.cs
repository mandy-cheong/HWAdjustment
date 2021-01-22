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

public partial class adm_productprice_PriceComparison : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        var fileName = flProduct.PostedFile.FileName;
        if (IsValidFile())
        {
            DataTable dt = ConvertFileToDT(fileName);
            List<PriceComparison> prices = (from DataRow dr in dt.Rows
                           select new PriceComparison()
                           {
                               ProductId = Convert.ToInt32(dr["商品ID"]),
                               PMPrice = Convert.ToDecimal(dr["假售價"]),
                               PMSellPrice = Convert.ToDecimal(dr["常售價"]),
                               PMEventPrice = Convert.ToDecimal(dr["活動價"]),
                           }).ToList();
        }
    }
    private bool IsValidFile()
    {
        string msg = "";
        var extension = Path.GetExtension(flProduct.PostedFile.FileName);
        if (!flProduct.HasFile)
        {
            msg = "請上傳文檔";
        }
        else if (extension != ".xls"&& extension != ".xlsx")
        {
            msg = "請上傳Excel";
        }

        if(!string.IsNullOrEmpty(msg))
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "showError", "alert('" + msg + "')", true);
        return string.IsNullOrEmpty(msg);

    }
    private DataTable ConvertFileToDT(string fileName)
    {
        var path = Server.MapPath("ProductPrice");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var fullpath = path + "\\" + fileName;


        flProduct.SaveAs(fullpath);
        var dt = ReadExcelAsTableNPOI(fullpath);
        return dt;
    }
    public static DataTable ReadExcelAsTableNPOI(string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.Open))
        {
            IWorkbook wb = NPOI.SS.UserModel.WorkbookFactory.Create(fs);
            ISheet sheet = wb.GetSheetAt(0);
            DataTable table = new DataTable();
            //由第一列取標題做為欄位名稱
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            DataFormatter formatter = new DataFormatter();
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            //以欄位文字為名新增欄位，此處全視為字串型別以求簡化
            {
                //headerRow.GetCell(i).SetCellType(CellType.String);
                String val = formatter.FormatCellValue(headerRow.GetCell(i));
                //table.Columns.Add(new DataColumn(headerRow.GetCell(i).StringCellValue));
                table.Columns.Add(new DataColumn(val));
            }

            //略過第零列(標題列)，一直處理至最後一列
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;
                DataRow dataRow = table.NewRow();
                //依先前取得的欄位數逐一設定欄位內容
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        dataRow[j] = row.GetCell(j).ToString();
                    }
                }      //如要針對不同型別做個別處理，可善用.CellType判斷型別
                        //再用.StringCellValue, .DateCellValue, .NumericCellValue...取值
                        //此處只簡單轉成字串
                table.Rows.Add(dataRow);
            }
            return table;
        }
    }
}