using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EventCheckItems
/// </summary>
public class EventCheckItem:EventCheckProduct
{
    public EventCheckItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string ProductCode { get; set; }
    public int Qty { get; set; }
    public int Pool1 { get; set; }
    public int Pool2 { get; set; }
    public int Pool3 { get; set; }

}