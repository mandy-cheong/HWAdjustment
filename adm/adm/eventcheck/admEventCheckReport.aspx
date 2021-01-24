<%@ Page Title="" Language="C#" MasterPageFile="~/adm/adm.master" AutoEventWireup="true" CodeFile="admEventCheckReport.aspx.cs" Inherits="adm_eventcheck_admEventCheckReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="am-cf am-padding">
        <div class="am-fl am-cf"><strong class="am-text-primary am-text-lg">報表管理</strong> / <small>活動檢查報表</small></div>
    </div>
    <hr />
    <div class="am-u-sm-12">
        <div class="am-form-inline">
            <div class="am-form-group">
                商品ID
            </div>
            <div class="am-form-group">
                <asp:TextBox runat="server" ID="txtSearchProductID" CssClass="am-form-field"></asp:TextBox>
            </div>
            <div class="am-form-group">
                活動ID
            </div>
            <div class="am-form-group">
                <asp:TextBox runat="server" ID="txtSearchEventId" CssClass="am-form-field"></asp:TextBox>
            </div>
            <div class="am-form-group">
                <%--<asp:Button ID="btnSearch" runat="server" Text="搜尋" CssClass="am-btn am-btn-success" OnClientClick="SearchProduct(1)" />--%>
                <input type="button" class="am-btn am-btn-success" onclick="Search(1)" value="Search" />
            </div>
            <div class="am-form-group">
                <asp:Button ID="btnExport" runat="server" Text="匯出" CssClass="am-btn am-btn-success" OnClick="btnExport_Click" />
            </div>

            <div style="margin-top: 30px;" class="am-tabs" id="mainTab" data-am-tabs="{noSwipe: 1}">
                <ul class="am-tabs-nav am-nav am-nav-tabs">
                    <li class="am-active"><a href="#tab1">商品總覽</a></li>
                    <li class=""><a href="#tab2">品項明細</a></li>
                </ul>

                <div class="am-tabs-bd">
                    <div class="am-tab-panel am-active" id="tab1">
                        <h3>商品總覽</h3>
                        <table class="am-table am-table-bordered am-table-hover" id="productList">
                            <tr>
                                <th>商品ID</th>
                                <th>商品名稱</th>
                                <th>規格ID</th>
                                <th>規格名稱</th>
                                <th>常售價</th>
                                <th>活動價</th>
                                <th>是否預購</th>
                                <th>可售數量</th>
                                <th>庫存POOL</th>
                                <th>東馬</th>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                          <div class="am-cf">
                            <ul class="am-pagination am-pagination-centered" id="product-paging">
                            </ul>
                        </div>
                     <%--   <asp:GridView ID="gvProducts" runat="server" class="am-table am-table-bordered am-table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="商品ID" HeaderText="商品ID" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="商品名稱" HeaderText="商品名稱" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="規格ID" HeaderText="規格ID" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="規格名稱" HeaderText="規格名稱" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="常售價" HeaderText="常售價" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="活動價" HeaderText="活動價" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="是否預購" HeaderText="是否預購" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="可售數量" HeaderText="可售數量" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="庫存POOL" HeaderText="庫存POOL" HeaderStyle-CssClass="filter" />
                                <asp:BoundField DataField="東馬" HeaderText="東馬" HeaderStyle-CssClass="filter" />

                            </Columns>
                        </asp:GridView>--%>
                    </div>
            <div class="am-tab-panel" id="tab2">
                <h3>品項明細 </h3>

                 <table class="am-table am-table-bordered am-table-hover" id="itemList">
                            <tr>
                                <th>商品ID</th>
                                <th>商品名稱</th>
                                <th>規格ID</th>
                                <th>規格名稱</th>
                                <th>常售價</th>
                                <th>活動價</th>
                                <th>是否預購</th>
                                <th>可售數量</th>
                                <th>庫存POOL</th>
                                <th>品編</th>
                                <th>單份數量</th>
                                <th>庫存1</th>
                                <th>庫存2</th>
                                <th>庫存3</th>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                  <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                          <div class="am-cf">
                            <ul class="am-pagination am-pagination-centered" id="item-paging">
                            </ul>
                        </div>
                <%--<asp:GridView ID="gvProductItems" runat="server" class="am-table am-table-bordered am-table-hover" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="商品ID" HeaderText="商品ID" />
                        <asp:BoundField DataField="商品名稱" HeaderText="商品名稱" />
                        <asp:BoundField DataField="規格ID" HeaderText="規格ID" />
                        <asp:BoundField DataField="規格名稱" HeaderText="規格名稱" />
                        <asp:BoundField DataField="常售價" HeaderText="常售價" />
                        <asp:BoundField DataField="活動價" HeaderText="活動價" />
                        <asp:BoundField DataField="是否預購" HeaderText="是否預購" />
                        <asp:BoundField DataField="庫存POOL" HeaderText="庫存POOL" />
                        <asp:BoundField DataField="可售數量" HeaderText="可售數量" />
                        <asp:BoundField DataField="品編" HeaderText="品編" />
                        <asp:BoundField DataField="單份數量" HeaderText="單份數量" />
                        <asp:BoundField DataField="庫存1" HeaderText="庫存1" />
                        <asp:BoundField DataField="庫存2" HeaderText="庫存2" />
                        <asp:BoundField DataField="庫存3" HeaderText="庫存3" />
                    </Columns>
                </asp:GridView>--%>
            </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function Search(page) {
            SearchProduct(page);
            SearchProductItems(page);
        }

        function SearchProduct(page) {
            var searchdata = GetSearchFilter(page);
            console.log(searchdata);
            $.ajax({
                type: "POST",
                url: "admEventCheckReport.aspx/GetProducts",
                data: "{eventCheckFilter:" + JSON.stringify(searchdata) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                    var record = response.d;
                    var totalcount = 0;
                    var table = $('#productList');


                    if (record.length > 0) {
                        table.show();
                        var row = table.find("tr:last-child").clone(true);
                        $("tr", table).not($("[id*=productList] tr:first-child")).remove();
                        $.each(record, function () {
                            totalcount = $(this)[0].TotalCount;
                            $("td", row).eq(0).html($(this)[0].ProductId);
                            $("td", row).eq(1).html($(this)[0].ProductName);
                            $("td", row).eq(2).html($(this)[0].OptionId);
                            $("td", row).eq(3).html($(this)[0].OptionName);
                            $("td", row).eq(4).html($(this)[0].Price);
                            $("td", row).eq(5).html($(this)[0].EventPrice);
                            $("td", row).eq(6).html($(this)[0].IsPreOrder);
                            $("td", row).eq(7).html($(this)[0].PreorderQty);
                            $("td", row).eq(8).html($(this)[0].Pool);
                            $("td", row).eq(9).html($(this)[0].WestMalaysia);
                            table.append(row);
                            row = table.find("tr:last-child").clone(true);
                        });

                    }
                    else {
                        table.hide();
                    }
                    CreatePaging(totalcount, searchdata.PageSize, searchdata.CurrentPage, 'product-paging', 'SearchProduct');

                }
            });
        }

        function SearchProductItems(page) {
            var searchdata = GetSearchFilter(page);
            console.log(searchdata);
            $.ajax({
                type: "POST",
                url: "admEventCheckReport.aspx/GetProductItems",
                data: "{eventCheckFilter:" + JSON.stringify(searchdata) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                    var record = response.d;
                    var totalcount = 0;
                    var table = $('#itemList');


                    if (record.length > 0) {
                        table.show();
                        var row = table.find("tr:last-child").clone(true);
                        $("tr", table).not($("[id*=itemList] tr:first-child")).remove();
                        $.each(record, function () {
                            totalcount = $(this)[0].TotalCount;
                            $("td", row).eq(0).html($(this)[0].ProductId);
                            $("td", row).eq(1).html($(this)[0].ProductName);
                            $("td", row).eq(2).html($(this)[0].OptionId);
                            $("td", row).eq(3).html($(this)[0].OptionName);
                            $("td", row).eq(4).html($(this)[0].Price);
                            $("td", row).eq(5).html($(this)[0].EventPrice);
                            $("td", row).eq(6).html($(this)[0].IsPreOrder);
                            $("td", row).eq(7).html($(this)[0].PreorderQty);
                            $("td", row).eq(8).html($(this)[0].Pool);
                            $("td", row).eq(9).html($(this)[0].ProductCode);
                            $("td", row).eq(10).html($(this)[0].Qty);
                            $("td", row).eq(11).html($(this)[0].Pool1);
                            $("td", row).eq(12).html($(this)[0].Pool2);
                            $("td", row).eq(13).html($(this)[0].Pool3);
                            table.append(row);
                            row = table.find("tr:last-child").clone(true);
                        });

                    }
                    else {
                        table.hide();
                    }
                    CreatePaging(totalcount, searchdata.PageSize, searchdata.CurrentPage, 'item-paging', 'SearchProductItems');

                }
            });
        }

        function GetSearchFilter(page) {
            var searchdata = new Object();
            searchdata.EventId = $('#ContentPlaceHolder1_txtSearchEventId').val();
            searchdata.ProductIds = $('#ContentPlaceHolder1_txtSearchProductID').val();
            searchdata.PageSize = 10;
            searchdata.CurrentPage = page;

            return searchdata;
        }

        function CreatePaging(totalcount, pagesize, currentPage, id, methodname) {
            $('#'+id).html('');

            if (totalcount == 0)
                return;
            var totalPages = parseInt((totalcount + pagesize - 1) / pagesize);
            var middlepage = 5;
            var startPage = 1;

            if (currentPage > middlepage && totalPages > 10)
                startPage = (currentPage - middlepage) + 1;

            var minStartPage = totalPages - 9 < 0 ? 1 : totalPages - 9;
            startPage = startPage < minStartPage ? startPage : minStartPage;

            var endPage = startPage + 9;
            if (endPage >= totalPages)
                endPage = totalPages;
            while (startPage <= endPage) {
                var classs = startPage == currentPage ? "style='color: #fff;background-color: #29ABB3'" : "";
                $('#' + id).append("<li> <a href='#' " + classs + " onclick='" + methodname+"(" + startPage + ")' >" + startPage + "</a></li>");
                startPage++;
            }
        }
    </script>
</asp:Content>

