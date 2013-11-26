<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="astr_view.aspx.vb" Inherits="acadeweb.astr_view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="astrview">
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
              <HeaderTemplate>
                <div align="right"><asp:Button runat="server" OnclientClick="window.close();" Text="關閉本視窗"></asp:Button></div><br />
              </HeaderTemplate>
              <ItemTemplate>
                 寄送時間：<asp:Label runat="server" id="Label1" text='<%# Eval("SendDateTime") %>'></asp:Label>
                 <asp:Label runat="server" id="context" text='<%# Eval("context") %>'></asp:Label>
                 <hr />
              </ItemTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:Connect_NIU_DBASE %>" 
                ProviderName="<%$ ConnectionStrings:Connect_NIU_DBASE.ProviderName %>" 
                SelectCommand="SELECT [regsno], [stdname], [context], [SendDateTime] FROM [AstrMail_SendLog] WHERE ([regsno] = ?) order by sendDateTime desc">
                <SelectParameters>
                    <asp:QueryStringParameter Name="regsno" QueryStringField="RNO" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>
    <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server" 
        Skin="Telerik" />
    </form>
</body>
</html>
