using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for EventCheck
/// </summary>
public class EventCheckService
{
    private  string _sql = "";
    public EventCheckService()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetExportProducts(EventCheckFilter eventCheckFilter)
    {
        var sql = @"SELECT
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
                    WPA08=1 {0}   ORDER BY WP.WP01 ";
        var cmd = GetSearchFilter(eventCheckFilter);
        cmd.CommandText = string.Format(sql, cmd.CommandText);
        return SqlDbmanager.queryBySql(cmd);
    }

    public DataTable GetExportProductItems(EventCheckFilter eventCheckFilter)
    {
        var sql = @" SELECT
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
                                WPB05=1
                                AND WPA08=1 {0} ORDER BY WP.WP01";
        var cmd = GetSearchFilter(eventCheckFilter);
        cmd.CommandText = string.Format(sql, cmd.CommandText);
        return SqlDbmanager.queryBySql(cmd);
    }

    public List<EventCheckProduct> GetProducts(EventCheckFilter eventCheckFilter)
    {
        var skiprows = (eventCheckFilter.CurrentPage - 1) * eventCheckFilter.PageSize;
        var sql = @"WITH TotalCountCTE AS ( SELECT COUNT(1) AS TotalCount 
                     FROM WP
                        INNER JOIN WPA ON WP.WP01=WPA.WP01
                        INNER JOIN fnGetGoodsOptionPrice('HW') TA ON TA.OptionId=WPA01
                        WHERE
                        WPA08=1 {0}) , 
                    DataCTE AS ( SELECT
                        WP.WP01 as 'ProductId',
                        WP02 as 'ProductName',
                        WPA01 as 'OptionID',
                        WPA02 as 'OptionName',
                        CONVERT(DECIMAL(10, 2),  (WPA06/7.6)) as 'Price',
                        CONVERT(DECIMAL(10, 2), ((WPA06-WPA07)/7.6)) as 'EventPrice',
                        (CASE WHEN WP28=1 THEN '是' ELSE '否' END) as 'IsPreOrder',
                        StockQty as 'PreorderQty',
                        (CASE WP66 WHEN 1 THEN '庫存1' WHEN 2 THEN '庫存2' WHEN 3 THEN '庫存3' END) as 'POOL',
                        (CASE WP55 WHEN 1 THEN '可' WHEN 0 THEN '否' END) as 'WestMalaysia'
                    FROM WP
                    INNER JOIN WPA ON WP.WP01=WPA.WP01
                    INNER JOIN fnGetGoodsOptionPrice('HW') TA ON TA.OptionId=WPA01
                    WHERE
                    WPA08=1 {0} ) 
                    SELECT * FROM TotalCountCTE, DataCTE ORDER BY ProductId
                    OFFSET " + skiprows + " ROWS FETCH NEXT "+eventCheckFilter.PageSize+" ROWS ONLY ";
        
        var cmd = GetSearchFilter(eventCheckFilter);
        cmd.CommandText = string.Format(sql, cmd.CommandText);
        var dt= SqlDbmanager.queryBySql(cmd);
        var eventCheckProducts = new List<EventCheckProduct>();
        foreach (DataRow dr in dt.Rows)
        {
            var eventProduct = new EventCheckProduct();
            eventProduct.TotalCount = int.Parse(dr["TotalCount"].ToString());
            eventProduct.ProductId = int.Parse(dr["ProductId"].ToString());
            eventProduct.ProductName = dr["ProductName"].ToString();
            eventProduct.OptionId = dr["OptionId"].ToString();
            eventProduct.OptionName = dr["OptionName"].ToString();
            eventProduct.Price = Math.Round( decimal.Parse(dr["Price"].ToString()), 2);
            eventProduct.EventPrice = Math.Round(decimal.Parse(dr["EventPrice"].ToString()), 2);
            eventProduct.IsPreOrder = dr["IsPreOrder"].ToString();
            eventProduct.PreorderQty = int.Parse(dr["PreorderQty"].ToString());

            eventProduct.Pool = dr["Pool"].ToString();
            eventProduct.WestMalaysia = dr["WestMalaysia"].ToString();
            eventCheckProducts.Add(eventProduct);
        }

        return eventCheckProducts;
    }

    public List<EventCheckItem> GetProductItems(EventCheckFilter eventCheckFilter)
    {
        var skiprows = (eventCheckFilter.CurrentPage - 1) * eventCheckFilter.PageSize;
        var sql = @"WITH TotalCountCTE AS(SELECT COUNT(1)  AS TotalCount
                                FROM WP
                                INNER JOIN WPA ON WP.WP01=WPA.WP01
                                INNER JOIN fnGetGoodsOptionPrice('HW') TA ON TA.OptionId=WPA01
                                INNER JOIN WPB ON WPB.WPB02=WPA01
                                INNER JOIN D ON D01=WPB03
                                WHERE
                                WPB05=1
                                AND WPA08=1 {0} ) , DataCTE AS(
                                SELECT
                                WP.WP01 as 'ProductId',
                                WP02 as 'ProductName',
                                WPA01 as 'OptionId',
                                WPA02 as 'OptionName',
                               CONVERT(DECIMAL(10, 2),  (WPA06/7.6)) as 'Price',
                                CONVERT(DECIMAL(10, 2), ((WPA06-WPA07)/7.6)) as 'EventPrice',
                                (CASE WHEN WP28=1 THEN '是' ELSE '否' END) as 'IsPreOrder',
                                (CASE WP66 WHEN 1 THEN '庫存1' WHEN 2 THEN '庫存2' WHEN 3 THEN '庫存3' END) as 'Pool',
                                StockQty as 'PreorderQty',
                                D02 as 'ProductCode',
                                WPB04 as 'Qty',
                                ISNULL(D10,0) as 'Pool1',
                                ISNULL(D34,0) as 'Pool2',
                                ISNULL(D35,0) as 'Pool3'
                                FROM WP
                                INNER JOIN WPA ON WP.WP01=WPA.WP01
                                INNER JOIN fnGetGoodsOptionPrice('HW') TA ON TA.OptionId=WPA01
                                INNER JOIN WPB ON WPB.WPB02=WPA01
                                INNER JOIN D ON D01=WPB03
                                WHERE
                                WPB05=1
                                AND WPA08=1 {0}  ) 
                                SELECT * FROM TotalCountCTE, DataCTE ORDER BY ProductId
                                OFFSET " + skiprows + " ROWS FETCH NEXT " + eventCheckFilter.PageSize + " ROWS ONLY ";

        var cmd = GetSearchFilter(eventCheckFilter);
        cmd.CommandText = string.Format(sql, cmd.CommandText);
        var dt = SqlDbmanager.queryBySql(cmd);
        var eventCheckProducts = new List<EventCheckItem>();
        foreach (DataRow dr in dt.Rows)
        {
            var eventProduct = new EventCheckItem();
            eventProduct.TotalCount = int.Parse(dr["TotalCount"].ToString());
            eventProduct.ProductId = int.Parse(dr["ProductId"].ToString());
            eventProduct.ProductName = dr["ProductName"].ToString();
            eventProduct.OptionId = dr["OptionId"].ToString();
            eventProduct.OptionName = dr["OptionName"].ToString();
            eventProduct.Price = Math.Round(decimal.Parse(dr["Price"].ToString()), 2);
            eventProduct.EventPrice = Math.Round(decimal.Parse(dr["EventPrice"].ToString()), 2);
            eventProduct.IsPreOrder = dr["IsPreOrder"].ToString();
            eventProduct.PreorderQty = int.Parse(dr["PreorderQty"].ToString());
            eventProduct.Pool = dr["Pool"].ToString();
            eventProduct.ProductCode = dr["ProductCode"].ToString();
            eventProduct.Qty = int.Parse(dr["Qty"].ToString());
            eventProduct.Pool1 = int.Parse(dr["Pool1"].ToString());
            eventProduct.Pool2 = int.Parse(dr["Pool2"].ToString());
            eventProduct.Pool3 = int.Parse(dr["Pool3"].ToString());
            eventCheckProducts.Add(eventProduct);
        }

        return eventCheckProducts;
    }
  
    private SqlCommand GetSearchFilter(EventCheckFilter eventCheckFilter)
    {
        var cmd = new SqlCommand();
        if (!string.IsNullOrEmpty(eventCheckFilter.ProductIds))
        {
            cmd.CommandText += "  AND WP.WP01 IN (SELECT NAME from fnSplit (@productids, ',' )) ";
            cmd.Parameters.Add(SafeSQL.CreateInputParam("@productids", SqlDbType.NVarChar, eventCheckFilter.ProductIds));
        }
        if (eventCheckFilter.EventId>0)
        {
            cmd.CommandText += "  AND EXISTS (SELECT 1 FROM SPRODUCTSD WHERE SPD01 IN (@eventid) AND SPD02 = WP.WP01) ";
            cmd.Parameters.Add(SafeSQL.CreateInputParam("@eventid", SqlDbType.Int, eventCheckFilter.EventId));
        }
        return cmd;
    }
}
