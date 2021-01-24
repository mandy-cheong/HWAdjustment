using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EventCheckFilter
/// </summary>
public class EventCheckFilter
{
    public EventCheckFilter()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int EventId { get; set; }
    public string ProductIds { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}