using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EventCheckProduct
/// </summary>
public class EventCheckProduct
{
    public EventCheckProduct()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int TotalCount { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string OptionId { get; set; }
    public string OptionName { get; set; }
    public decimal Price { get; set; }
    public decimal EventPrice { get; set; }
    public string IsPreOrder { get; set; }
    public int PreorderQty { get; set; }
    public string  Pool { get; set; }
    public string WestMalaysia { get; set; }
}