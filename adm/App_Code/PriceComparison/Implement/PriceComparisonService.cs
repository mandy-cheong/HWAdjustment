using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PriceComparisonService
/// </summary>
public class PriceComparisonService
{
    public PriceComparisonService()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<ProductPriceCheck> GetProductPrices(List<PriceComparison> priceComparisons) {
        var temptable = @"DECLARE @TABLE  TABLE
                        (ProductId bigint , ProductName NVARCHAR(100) , OptionName NVARCHAR(100) , Channel VARCHAR(2) , Price DECIMAL(10,2) , SellPrice DECIMAL(10,2) ,
                        EventPrice DECIMAL(10,2) )";
        var cmd = new SqlCommand();
        var counter = 1;
        foreach(var price in priceComparisons)
        {
            temptable += @"INSERT INTO @TABLE (ProductId,OptionName)
                            VALUES
                             (@ProductId"+ counter + ", @OptionName"+ counter + ")";
            cmd.Parameters.Add(SafeSQL.CreateInputParam("@ProductId"+ counter, SqlDbType.Int, price.ProductId));
            cmd.Parameters.Add(SafeSQL.CreateInputParam("@OptionName"+ counter, SqlDbType.NVarChar, price.OptionName));
            counter++;
        }
        var sql = @" SELECT 
                        WP.WP01 ProductId, 
                        WP.WP02 ProductName, 
                        WPA.WPA01 OptionId , 
                        WPA.WPA02 OptionName , 
                        WPA.WPA10 Price, 
                        WPA.WPA06 SellPrice, 
                        WPA.WPA06-WPA.WPA07 EventPrice 
                        FROM WP INNER JOIN WPA ON WP.WP01=WPA.WP01
                        INNER JOIN @TABLE TA ON  WPA.WP01 = TA.ProductId AND ( WPA.WPA02= TA.OptionName OR CHARINDEX( OptionName, WPA02)>0)
                        ORDER BY PRODUCTID DESC, OptionId ";
        cmd.CommandText = temptable + sql;
        var dt = SqlDbmanager.queryBySql(cmd);
        var result =new List<ProductPriceCheck>();
        if (dt.Rows == null)
            return result;

        foreach (DataRow dr in dt.Rows)
        {
            var productprice = new ProductPriceCheck();
            productprice.ProductId = ConvertHelper.ConvertInt(dr["ProductId"].ToString());
            productprice.ProductName = dr["ProductName"].ToString();
            productprice.OptionName = dr["OptionName"].ToString();
            productprice.EventPrice = ConvertHelper.ConvertDecimal(dr["EventPrice"].ToString());
            productprice.Price = ConvertHelper.ConvertDecimal(dr["Price"].ToString());
            productprice.SellPrice = ConvertHelper.ConvertDecimal(dr["SellPrice"].ToString());
            result.Add(productprice);
        }
        return result;
    }
    public List<ProductPriceCheck> GetProductPrices()
    {
            var sql = @" SELECT 
                        WP.WP01 ProductId, 
                        WP.WP02 ProductName, 
                        WPA.WPA01 OptionId , 
                        WPA.WPA02 OptionName , 
                        WPA.WPA10 Price, 
                        WPA.WPA06 SellPrice, 
                        WPA.WPA06-WPA.WPA07 EventPrice 
                        FROM WP INNER JOIN WPA ON WP.WP01=WPA.WP01
                        ORDER BY PRODUCTID DESC, OptionId ";
        var dt = SqlDbmanager.queryBySql(sql);
        var result = new List<ProductPriceCheck>();
        if (dt.Rows == null)
            return result;

        foreach (DataRow dr in dt.Rows)
        {
            var productprice = new ProductPriceCheck();
            productprice.ProductId = ConvertHelper.ConvertInt(dr["ProductId"].ToString());
            productprice.ProductName = dr["ProductName"].ToString();
            productprice.OptionName = dr["OptionName"].ToString();
            productprice.EventPrice = ConvertHelper.ConvertDecimal(dr["EventPrice"].ToString());
            productprice.Price = ConvertHelper.ConvertDecimal(dr["Price"].ToString());
            productprice.SellPrice = ConvertHelper.ConvertDecimal(dr["SellPrice"].ToString());
            result.Add(productprice);
        }
        return result;
    }
}