<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Registry_menu.ascx.vb"
    Inherits="acadeweb.Registry_menu" %>
<style type="text/css">
    .style1
    {
      font-size:18px;    
    }
    .red
    {
       color:Red;
       font-weight: bold;
    }
</style>
<div class="hr1">
</div>
<div class="topMain">
    <span class="left">
        <img src="image/AcadeApp_Banner.jpg" alt="" /></span> <span class="right">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </span></div>
<div class="spacer"></div>
<div class="hr2"></div>
<div class="menu">
    <telerik:RadMenu ID="RadMenu2" runat="server" DataFieldID="ID" DataFieldParentID="ParentID"
        DataSourceID="AppMenu" DataTextField="Menu_Name" Skin="Windows7" DataNavigateUrlField="URL">
    </telerik:RadMenu>
    <asp:SqlDataSource ID="AppMenu" runat="server" ConnectionString="<%$ ConnectionStrings:Connect_NIU_DBASE %>"
        ProviderName="<%$ ConnectionStrings:Connect_NIU_DBASE.ProviderName %>" SelectCommand="SELECT [ID], [Menu_Name], [ParentID], rtrim(URL) as URL FROM [webapp_menu] order by Sort">
    </asp:SqlDataSource>
</div>