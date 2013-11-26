<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="astr_mail.aspx.vb" Inherits="acadeweb.astr_mail" %>

<%@ Register src="Registry_menu.ascx" tagname="Registry_menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>國立宜蘭大學-教務資訊系統Web</title>
    <link href="CSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
         .style2
        {
            width: 23px;
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc1:Registry_menu ID="Registry_menu1" runat="server" />
        <div class="spacer"></div>
            <div class="textMain2"><h2>
                <img alt="ico" class="style2" src="image/icon_agenda_info.gif" />大學部必修科目-應修未修及應修不及格-通知名單</h2>
                請輸入查詢年級：<asp:TextBox ID="Txt_Y" runat="server" Columns="5" 
                    ValidationGroup="Y">4</asp:TextBox>
                <asp:Button ID="But_Search" runat="server" Text="送出" />
                <asp:Button ID="But_SendMail" runat="server" Text="寄送通知信" />
                <br />
                <span class="style1"><strong>輸入範例:3與4年級,請輸入 3,4 、若單一年級,請輸入 4</strong></span></div><br />
            <div class="textMain2">
        <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" Culture="zh-TW" 
            GridLines="None" Width="95%" DataSourceID="SqlDataSource1">
<MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                AllowPaging="True">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridTemplateColumn DataField="regsno" FilterControlAltText="Filter 學號 column" 
            HeaderText="學號" SortExpression="Regsno" UniqueName="Regsno">
           <ItemTemplate>
               <asp:Label ID="RegsnoLab" runat="server" Text='<%# Eval("regsno") %>'></asp:Label>
           </ItemTemplate>
           <itemStyle HorizontalAlign="Center" />
        </telerik:GridTemplateColumn>
        <telerik:GridBoundColumn DataField="姓名" FilterControlAltText="Filter 姓名 column" 
            HeaderText="姓名" SortExpression="姓名" UniqueName="姓名">
            <HeaderStyle Width="80px" />
            <itemStyle HorizontalAlign="Center" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="未通過必修科目" FilterControlAltText="Filter 未通過必修科目 column" 
            HeaderText="必修科目-應修未修及應修不及格" SortExpression="未通過必修科目" UniqueName="未通過必修科目">
        </telerik:GridBoundColumn>
        <telerik:GridTemplateColumn HeaderText="寄送狀態">
          <ItemTemplate>
           最後寄發時間：<%# Get_Result(Eval("regsno").ToString)%><asp:Label ID="Result" runat="server" text='<%# Get_Result(Eval("regsno").ToString)%>' Visible="false"></asp:Label> 
          </ItemTemplate>
          <itemStyle HorizontalAlign="Center" />
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn>
          <ItemTemplate>
            <a href='astr_view.aspx?RNO=<%# Eval("regsno")%>' target="_blank">查看結果</a>
          </ItemTemplate>
          <itemStyle HorizontalAlign="Center" />

        </telerik:GridTemplateColumn>
    </Columns>
    <HeaderStyle  HorizontalAlign="Center"/>
<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
        </telerik:RadGrid>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="">
        </asp:SqlDataSource>
        </div>
    </div>
    <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server" Skin="Sunset" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
