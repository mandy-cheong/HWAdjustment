using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_productprice_PriceComparison : System.Web.UI.Page
{
    private readonly PriceComparisonService _priceComparisonService;
    public static List<PriceComparison> _uploadedExcelPrices;
    public adm_productprice_PriceComparison()
    {
        _priceComparisonService = new PriceComparisonService();

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (IsValidFile())
        {
            ReadExcelPrices();
            if (_uploadedExcelPrices.Count == 0)
                return;

            MatchExcelPriceWithCurrentPrice();
            BindMatchResultToTable();
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
        else if (extension != ".xls" && extension != ".xlsx")
        {
            msg = "請上傳Excel";
        }

        if (!string.IsNullOrEmpty(msg))
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "showError", "alert('" + msg + "')", true);
        return string.IsNullOrEmpty(msg);

    }
    private void ReadExcelPrices( )
    {
        var dt = ConvertFileToDT();
        ValidateColumn(dt);
        _uploadedExcelPrices = (from DataRow dr in dt.Rows
                                select new PriceComparison()
                                {
                                    ProductId = ConvertHelper.ConvertInt(dr["商品ID"].ToString()),
                                    ProductName = dr["商品名稱"].ToString(),
                                    OptionName = dr["規格名稱"].ToString(),
                                    PMPrice = ConvertHelper.ConvertDecimal(dr["假售價"].ToString()),
                                    PMSellPrice = ConvertHelper.ConvertDecimal(dr["常售價"].ToString()),
                                    PMEventPrice = ConvertHelper.ConvertDecimal(dr["活動價"].ToString()),
                                }).ToList();
    }
    private DataTable ConvertFileToDT()
    {
        var fileName = flProduct.PostedFile.FileName;
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
    private bool ValidateColumn(DataTable dt)
    {

        var msg = "";
        if (dt.Rows == null || dt.Rows.Count == 0)
        {
            msg = "找不到任何資料";
            return ReturnMsg(msg);
        }

        var columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToList();
        if (!columnNames.Any(x => x.Contains("商品ID")))
            msg += " 找不到商品ID欄位";
        if (!columnNames.Any(x => x.Contains("商品名稱")))
            msg += " 找不到商品名稱欄位";
        if (!columnNames.Any(x => x.Contains("規格名稱")))
            msg += " 找不到規格名稱欄位";
        if (!columnNames.Any(x => x.Contains("假售價")))
            msg += " 找不到假售價欄位";
        if (!columnNames.Any(x => x.Contains("常售價")))
            msg += " 找不到常售價欄位";
        if (!columnNames.Any(x => x.Contains("活動價")))
            msg += " 找不到活動價欄位";

        return ReturnMsg(msg);
    }
    private bool ReturnMsg(string msg)
    {
        if (!string.IsNullOrEmpty(msg))
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "showError", "alert('" + msg + "')", true);

        return string.IsNullOrEmpty(msg);
    }
    private void MatchExcelPriceWithCurrentPrice()
    {
        var currentPrices = _priceComparisonService.GetProductPrices(_uploadedExcelPrices);
        foreach (var uploadPrice in _uploadedExcelPrices)
        {
            var currentPrice = currentPrices.Where(x => x.OptionName == uploadPrice.OptionName).FirstOrDefault();
            if (currentPrice == null)
            {
                uploadPrice.CompareResult = "<span class='am-icon-times' style='color: red;'></span>";
                FindRelatedOptionName(currentPrices, uploadPrice);
                continue;
            }
            uploadPrice.CurrentPrice = currentPrice.Price;
            uploadPrice.CurrentEventPrice = currentPrice.EventPrice;
            uploadPrice.CurrentSellPrice = currentPrice.SellPrice;
            ValidateUploadPriceMatch(uploadPrice);
        }
    }
    private void FindRelatedOptionName(List<ProductPriceCheck> currentPrices, PriceComparison uploadPrice)
    {
        var relatedPriceSearch = currentPrices.Where(x => x.OptionName.Contains(uploadPrice.OptionName) && x.ProductId == uploadPrice.ProductId).ToList();
        if (relatedPriceSearch.Count > 0)
            uploadPrice.CompareMsg = "找不到此規格名稱, 相似規格名稱 : " + GetRelatedOption(relatedPriceSearch);
        else
            uploadPrice.CompareMsg = "找不到此規格名稱";
    }
    private static void ValidateUploadPriceMatch(PriceComparison uploadPrice)
    {
        if (uploadPrice.AllPriceMatched)
        {
            uploadPrice.CompareResult = "<span class='am-icon-check-circle' style='color: green;'></span>";
        }
        else
        {
            uploadPrice.CompareResult = "<span class='am-icon-times' style='color: red;'></span>";
            if (!uploadPrice.IsSameEventPrice)
                uploadPrice.CompareMsg += " 活動價對比錯誤,";
            if (!uploadPrice.IsSamePrice)
                uploadPrice.CompareMsg += " 假動價對比錯誤,";
            if (!uploadPrice.IsSameEventPrice)
                uploadPrice.CompareMsg += " 常售價對比錯誤,";
        }
    }
    private void BindMatchResultToTable()
    {
        gvProducts.DataSource = _uploadedExcelPrices.OrderBy(x => x.AllPriceMatched).OrderBy(x => x.ProductId).Skip(0).Take(10).ToList();
        gvProducts.DataBind();
        var totalcount = _uploadedExcelPrices.Count;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Paging", "CreatePaging(" + totalcount + ",10, 1) ", true);
    }
    private string GetRelatedOption(List<ProductPriceCheck> productPriceChecks)
    {
        var result = "";
        foreach (var priceCheck in productPriceChecks)
        {
            result += priceCheck.OptionName + ",";
        }

        return result.Length > 0 ? result.TrimEnd(',') : result;
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
    [WebMethod]
    public static dynamic GetPriceComparisons(int page)
    {
        var pageSize = 10;
        var skiprows = (page - 1) * pageSize;
        var data = _uploadedExcelPrices.OrderBy(x => x.AllPriceMatched).OrderBy(x => x.ProductId).Skip(skiprows).Take(pageSize).ToList();
        var totalCount = _uploadedExcelPrices.Count();
        return new { data =data, totalcount = totalCount };
    }

}
