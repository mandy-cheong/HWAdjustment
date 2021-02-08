using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Banner
/// </summary>
public class Banner
{
    public Banner()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int BannerId { get; set; }
    public string AreaCode { get; set; }
    public string Url { get; set; }
    public string  Name { get; set; }
    public int ProductId { get; set; }
    public string Brand { get; set; }
    public string  Category { get; set; }

    public int Platform { get; set; }
    public string MatchWords { get; set; }
}