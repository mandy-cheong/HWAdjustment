<%@ Page Title="" Language="C#" MasterPageFile="~/adm/adm.master" AutoEventWireup="true" CodeFile="admPriceComparison.aspx.cs" Inherits="adm_productprice_PriceComparison" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="am-cf am-padding">
        <div class="am-fl am-cf"><strong class="am-text-primary am-text-lg">商品管理</strong> / <small>批次價格檢視</small></div>
    </div>
     <hr />
    <div class="am-g">
        <div class="am-u-sm-12">
            <div class="am-form-inline">
                <table>
                    <tr>
                            
                        <td><asp:FileUpload ID="flProduct" runat="server" /></td>
                        <td>

                            <asp:Button ID="btn_add" runat="server" Text="上傳" CssClass="am-btn am-btn-default" OnClick="btn_add_Click" />

                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
         <div class="am-u-sm-12">
            <div class="am-form">
                <asp:UpdatePanel ID="list_panel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                              <asp:GridView ID="gvProducts" runat="server" ClientIDMode="Static" class="am-table am-table-bordered am-table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="ProductId" HeaderText="商品ID"   ItemStyle-Width="5%" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="ProductName" HeaderText="商品名稱"   ItemStyle-Width="10%" ItemStyle-Wrap="true"  />
                                <asp:BoundField DataField="OptionName" HeaderText="規格名稱"   ItemStyle-Width="10%" ItemStyle-Wrap="true"  />
                                <asp:BoundField DataField="PMPrice" HeaderText="PM填寫假售價"   ItemStyle-Width="8%" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="PMSellPrice" HeaderText="PM填寫常售價"   ItemStyle-Width="8%" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="PMEventPrice" HeaderText="PM填寫活動價"   ItemStyle-Width="8%" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="CurrentPrice" HeaderText="維運設定假售價"   ItemStyle-Width="8%" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="CurrentSellPrice" HeaderText="運設定常售價"   ItemStyle-Width="8%" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="CurrentEventPrice" HeaderText="維運設定活動價"   ItemStyle-Width="8%" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="CompareResult" HeaderText="價格確認" HtmlEncode="false" ItemStyle-Width="8%" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="CompareMsg" HeaderText="備注"  ItemStyle-Width="22%" ItemStyle-Wrap="true"   />
                            </Columns>
                        </asp:GridView>
                        <%--<table class="am-table am-table-striped am-table-hover table-main" id="productList">
                            <thead>
                                <tr>
                                    <th>商品ID</th>
                                    <th>商品名稱</th>
                                      <th>規格名稱</th>
                                    <th>PM填寫假售價</th>
                                    <th>PM填寫常售價</th>
                                    <th>PM填寫活動價</th>
                                    <th>維運設定假售價</th>
                                    <th>運設定常售價</th>
                                    <th>維運設定活動價</th>
                                    <th>價格確認</th>
                                      <th>備注</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptPrice" ClientIDMode="Static" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("ProductId") %></td>
                                            <td><%#Eval("ProductName") %></td>
                                            <td><%#Eval("OptionName") %></td>
                                            <td><%#Eval("PMPrice") %></td>
                                            <td><%#Eval("PMSellPrice") %></td>
                                            <td><%#Eval("PMEventPrice") %></td>
                                            <td><%#Eval("CurrentPrice") %></td>
                                            <td><%#Eval("CurrentSellPrice") %></td>
                                            <td><%#Eval("CurrentEventPrice") %></td>
                                            <td><%#Eval("CompareResult") %></td>
                                            <td><%#Eval("CompareMsg") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </tbody>
                        </table>--%>
                        <div class="am-cf">
                            <ul class="am-pagination am-pagination-centered" id="paging">
                               
                            </ul>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    <script>

        function SearchData(page) {
            $.ajax({
                type: "POST",
                url: "admPriceComparison.aspx/GetPriceComparisons",
                data: "{'page': "+page+"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                    console.log(response.d.data);
                    var record = response.d.data;
                    var totalcount = response.d.totalcount;
                    var table = $('#gvProducts');


                    if (record.length > 0) {
                        table.show();
                        var row = table.find("tr:last-child").clone(true);
                        $("tr", table).not($("[id*=gvProducts] tr:first-child")).remove();
                        $.each(record, function () {
                            $("td", row).eq(0).html($(this)[0].ProductId);
                            $("td", row).eq(1).html($(this)[0].ProductName);
                            $("td", row).eq(2).html($(this)[0].OptionName);
                            $("td", row).eq(3).html($(this)[0].PMPrice);
                            $("td", row).eq(4).html($(this)[0].PMSellPrice);
                            $("td", row).eq(5).html($(this)[0].PMEventPrice);
                            $("td", row).eq(6).html($(this)[0].CurrentPrice);
                            $("td", row).eq(7).html($(this)[0].CurrentSellPrice);
                            $("td", row).eq(8).html($(this)[0].CurrentEventPrice);
                            $("td", row).eq(9).html($(this)[0].CompareResult);
                            $("td", row).eq(10).html($(this)[0].CompareMsg);
                            table.append(row);
                            row = table.find("tr:last-child").clone(true);
                        });

                    }
                    else {
                        table.hide();
                    }
                    CreatePaging(totalcount,10, page);

                }
            });
        }

        function CreatePaging(totalcount, pagesize, currentPage) {
            $('#paging' ).html('');

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
                $('#paging' ).append("<li> <a href='#' " + classs + " onclick='SearchData(" + startPage + ")' >" + startPage + "</a></li>");
                startPage++;
            }
        }
    </script>
</asp:Content>

  <%--  <asp:GridView ID="gvProducts" runat="server" class="am-table am-table-bordered am-table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="商品ID" HeaderText="商品ID"   />
                                <asp:BoundField DataField="商品名稱" HeaderText="商品名稱"   />
                                <asp:BoundField DataField="規格ID" HeaderText="規格ID"   />
                                <asp:BoundField DataField="規格名稱" HeaderText="規格名稱"   />
                                <asp:BoundField DataField="常售價" HeaderText="常售價"   />
                                <asp:BoundField DataField="活動價" HeaderText="活動價"   />
                                <asp:BoundField DataField="是否預購" HeaderText="是否預購"   />
                                <asp:BoundField DataField="可售數量" HeaderText="可售數量"   />
                                <asp:BoundField DataField="庫存POOL" HeaderText="庫存POOL"   />
                                <asp:BoundField DataField="東馬" HeaderText="東馬"   />

                            </Columns>
                        </asp:GridView>--%>

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