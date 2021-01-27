using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConvertHelper
/// </summary>
public class ConvertHelper
{
    public ConvertHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int ConvertInt(string text, int defaultValue = 0)
    {
        int.TryParse(text, out defaultValue);
        return defaultValue;
    }
    public static decimal ConvertDecimal(string text, decimal defaultValue = 0)
    {
        decimal.TryParse(text, out defaultValue);
        return defaultValue;
    }
    public static Guid ConvertGuid(string text)
    {
        Guid id = Guid.NewGuid();
        Guid.TryParse(text, out id);
        return id;
    }
}