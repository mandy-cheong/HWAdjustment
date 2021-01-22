<%@ Page Title="" Language="C#" MasterPageFile="~/adm/adm.master" AutoEventWireup="true" CodeFile="PriceComparison.aspx.cs" Inherits="adm_productprice_PriceComparison" %>

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
                        <table class="am-table am-table-striped am-table-hover table-main">
                            <thead>
                                <tr>
                                    <th>商品ID</th>
                                    <th>商品名稱</th>
                                    <th>PM填寫假售價</th>
                                    <th>PM填寫常售價</th>
                                    <th>PM填寫活動價</th>
                                    <th>維運設定假售價</th>
                                    <th>運設定常售價</th>
                                    <th>維運設定活動價</th>
                                                                        <th>價格確認</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>活動ID
                                            </td>
                                            <td>活動名稱
                                            </td>
                                            <td>使用開始日
                                            </td>
                                            <td>使用結束日
                                            </td>
                                            <td>新增日期
                                            </td>
                                            <td>活動狀態
                                            </td>
                                            <td>活動備註
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btn_edit" runat="server" Text="編輯資料" CssClass="am-btn am-btn-success am-btn-xs" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btn_create" runat="server" Text="卷號編輯" CssClass="am-btn am-btn-primary  am-btn-xs" />
                                                        </td>
                                                    </tr>

                                                </table>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </tbody>
                        </table>
                        <div class="am-cf">
                            <ul class="am-pagination am-pagination-centered">
                                <asp:Literal ID="lit_page" runat="server"></asp:Literal>
                            </ul>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

</asp:Content>

  <%--  <asp:GridView ID="gvProducts" runat="server" class="am-table am-table-bordered am-table-hover" AutoGenerateColumns="false">
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