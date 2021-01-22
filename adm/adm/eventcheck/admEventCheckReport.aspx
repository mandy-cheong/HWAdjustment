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
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" CssClass="am-btn am-btn-success" OnClick="btnSearch_Click" />
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
                        <asp:GridView ID="gvProducts" runat="server" class="am-table am-table-bordered am-table-hover" AutoGenerateColumns="false">
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
                        </asp:GridView>
                    </div>
            <div class="am-tab-panel" id="tab2">
                <h3>品項明細 </h3>
                <asp:GridView ID="gvProductItems" runat="server" class="am-table am-table-bordered am-table-hover" AutoGenerateColumns="false">
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
                </asp:GridView>
            </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

