using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductPriceCheck
/// </summary>
public class ProductPriceCheck
{
    public ProductPriceCheck()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string OptionID { get; set; }
    public string OptionName { get; set; }
    public decimal Price { get; set; }
    public decimal SellPrice { get; set; }
    public decimal EventPrice  { get; set; }
}