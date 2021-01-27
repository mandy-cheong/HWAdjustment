using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PriceComparison
/// </summary>
public class PriceComparison
{
    public PriceComparison()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public string OptionName { get; set; }
    public decimal PMPrice { get; set; }
    public decimal PMSellPrice { get; set; }
    public decimal PMEventPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal CurrentSellPrice { get; set; }
    public decimal CurrentEventPrice { get; set; }
    public string CompareResult { get; set; }

    public string CompareMsg { get; set; }

    public bool IsSamePrice{get{return PMPrice == CurrentPrice;}}
    public bool IsSameEventPrice { get { return PMEventPrice == CurrentEventPrice; } }
    public bool IsSameSellPrice { get { return PMSellPrice == CurrentSellPrice; } }

    public bool AllPriceMatched { get { return IsSamePrice && IsSameEventPrice && IsSameSellPrice; } }
}