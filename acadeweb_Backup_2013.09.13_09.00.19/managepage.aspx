<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="managepage.aspx.vb" Inherits="acadeweb.managepage" %>

<%@ Register src="Registry_menu.ascx" tagname="Registry_menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc1:Registry_menu ID="Registry_menu1" runat="server" />
        <div class="spacer"></div>

        <div class="textMain2">
            <div><img alt="icon" class="style1" src="image/18.gif" /> 功能及網頁管理</div>
            <div class="content_left">
                <telerik:RadTreeView ID="RadTreeView1" runat="server" DataFieldID="ID"
                    DataFieldParentID="ParentID" DataSourceID="SqlDataTreeView"
                    DataTextField="Menu_Name" DataValueField="ID" Skin="Default">
                </telerik:RadTreeView>
                <asp:SqlDataSource ID="SqlDataTreeView" runat="server"
                    ConnectionString="<%$ ConnectionStrings:Connect_NIU_DBASE %>"
                    ProviderName="<%$ ConnectionStrings:Connect_NIU_DBASE.ProviderName %>"
                    SelectCommand="SELECT [ID], [Menu_Name], [ParentID], [URL] FROM [webapp_menu] order by sort">
                </asp:SqlDataSource>
            </div>
            <div class="content_right">

                <label>目前主選單：</label><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="Lab_PID" runat="server" Text="Label" Visible="False"></asp:Label>
                <br />
                <p>
                <asp:Button ID="But_AddMenu" runat="server" Text="新增" Visible="False" />
                </p>
                <asp:Panel ID="Panel1" runat="server">
                    <label>功能名稱：</label><asp:TextBox ID="Menu_Name" runat="server"></asp:TextBox>  <br />
                    <label>檔案名稱：</label><asp:TextBox ID="UrlName" runat="server"></asp:TextBox> <br />
                    <asp:Button ID="But_Send" runat="server" Text="送出" />
                </asp:Panel>
            </div>
        </div>
        <div style="clear:both">
        <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server"
            Skin="Telerik" /></div>
    </form>
</body>
</html>