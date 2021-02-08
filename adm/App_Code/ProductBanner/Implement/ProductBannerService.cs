using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for ProductBannerService
/// </summary>
public class ProductBannerService
{
    private List<Banner> _banners;
    public ProductBannerService()
    {
        //
        // TODO: Add constructor logic here
        //
        _banners = new List<Banner>(); ;
    }

    public List<Banner> GetBanners()
    {

        var sql = "SELECT F01 AS BannerId , F02 AS AreaCode,F03 As Name, F04 As Url from F where GETDATE() BETWEEN F06 AND F07 AND F12=1 ";
        var dt = SqlDbmanager.queryBySql(sql);
        if (dt == null || dt.Rows.Count == 0)
            return new List<Banner>();

        var queryResult = SqlExtension.ToList<Banner>(dt).ToList();
        foreach (var banner in queryResult)
            GetBannerProduct(banner);
           
        return _banners;
    }

    private void  GetBannerProduct(Banner banner )
    {
        if (string.IsNullOrEmpty(banner.Url))
            return  ;

        var mc = Regex.Match(banner.Url, "product(?=.)(.*)aspx(?=.)(.*)");
        if (mc.Success)
        {
            banner.MatchWords = mc.Captures[0].Value;
            string[] stringSeparators = new string[] { "id=" };
            var param = banner.MatchWords.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            int productId = 0;
            if (param.Length > 1 && int.TryParse(param[1], out productId))
                banner.ProductId = productId;

            if (productId > 0)
                _banners.Add(banner);

        }
    }
}