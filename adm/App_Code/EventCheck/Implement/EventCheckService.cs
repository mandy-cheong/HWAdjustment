using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for EventCheck
/// </summary>
public class EventCheckService
{
    public EventCheckService()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetProducts(string eventid, string productIds)
    {
        var cmd = new SqlCommand();
        cmd.CommandText = @"SELECT
                    WP.WP01 as '商品ID',
                    WP02 as '商品名稱',
                    WPA01 as '規格ID',
                    WPA02 as '規格名稱',
                    (WPA06/7.6) as '常售價',
                    ((WPA06-WPA07)/7.6) as '活動價',
                    (CASE WHEN WP28=1 THEN '是' ELSE '否' END) as '是否預購',
                    StockQty as '可售數量',
                    (CASE WP66 WHEN 1 THEN '庫存1' WHEN 2 THEN '庫存2' WHEN 3 THEN '庫存3' END) as '庫存POOL',
                    (CASE WP55 WHEN 1 THEN '可' WHEN 0 THEN '否' END) as '東馬'
                    FROM WP
                    INNER JOIN WPA ON WP.WP01=WPA.WP01
                    INNER JOIN fnGetGoodsOptionPrice('HW') TA ON TA.OptionId=WPA01
                    WHERE
                    WPA08=1 ";
        cmd = GetSearchFilter(eventid, productIds, cmd);
        cmd.CommandText += " ORDER BY WP.WP01 ";
        return SqlDbmanager.queryBySql(cmd);
    }

    public DataTable GetProductItems(string eventId, string productIds)
    {
        var cmd = new SqlCommand();
        cmd.CommandText = @" SELECT
                                WP.WP01 as '商品ID',
                                WP02 as '商品名稱',
                                WPA01 as '規格ID',
                                WPA02 as '規格名稱',
                                (WPA06/7.6) as '常售價',
                                ((WPA06-WPA07)/7.6) as '活動價',
                                (CASE WHEN WP28=1 THEN '是' ELSE '否' END) as '是否預購',
                                (CASE WP66 WHEN 1 THEN '庫存1' WHEN 2 THEN '庫存2' WHEN 3 THEN '庫存3' END) as '庫存POOL',
                                StockQty as '可售數量',
                                D02 as '品編',
                                WPB04 as '單份數量',
                                ISNULL(D10,0) as '庫存1',
                                ISNULL(D34,0) as '庫存2',
                                ISNULL(D35,0) as '庫存3'
                                FROM WP
                                INNER JOIN WPA ON WP.WP01=WPA.WP01
                                INNER JOIN fnGetGoodsOptionPrice('HW') TA ON TA.OptionId=WPA01
                                INNER JOIN WPB ON WPB.WPB02=WPA01
                                INNER JOIN D ON D01=WPB03
                                WHERE
                                --WP07 IN (1,2)
                                WPB05=1
                                AND WPA08=1 ";
        cmd = GetSearchFilter(eventId, productIds, cmd);
        cmd.CommandText += " ORDER BY WP.WP01 ";
        return SqlDbmanager.queryBySql(cmd);
    }

    private SqlCommand GetSearchFilter(string eventid, string productIds, SqlCommand cmd)
    {
        if (!string.IsNullOrEmpty(productIds))
        {
            cmd.CommandText += "  AND WP.WP01 IN (SELECT NAME from fnSplit (@productids, ',' )) ";
            cmd.Parameters.Add(SafeSQL.CreateInputParam("@productids", SqlDbType.NVarChar, productIds));
        }
        if (!string.IsNullOrEmpty(eventid))
        {
            cmd.CommandText += "  AND EXISTS (SELECT 1 FROM SPRODUCTSD WHERE SPD01 IN (@eventid) AND SPD02 = WP.WP01) ";
            cmd.Parameters.Add(SafeSQL.CreateInputParam("@eventid", SqlDbType.Int, eventid));
        }
        return cmd;
    }

}
