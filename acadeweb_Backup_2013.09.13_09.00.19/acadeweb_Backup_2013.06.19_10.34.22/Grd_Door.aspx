<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Grd_Door.aspx.vb" Inherits="acadeweb.Grd_Door" %>

<%@ Register src="Registry_menu.ascx" tagname="Registry_menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
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
        <div class="textMain2">
          <h2>
              <img alt="ico" class="style1" src="image/icon_agenda_info.gif" />大學部畢業門檻提醒通知</h2>
          <p>學院：<asp:DropDownList ID="DDL_College" runat="server" AutoPostBack="True">
              </asp:DropDownList>
              系所：<asp:DropDownList ID="DDL_Dept" runat="server" AutoPostBack="True">
                  <asp:ListItem Value="0">..請選擇..</asp:ListItem>
              </asp:DropDownList>
              年級：<asp:DropDownList ID="DDL_Grade" runat="server" AutoPostBack="True">
                  <asp:ListItem Value="0">..請選擇</asp:ListItem>
                  <asp:ListItem Value="4">四年級</asp:ListItem>
                  <asp:ListItem Value="3">三年級</asp:ListItem>
                  <asp:ListItem Value="2">二年級</asp:ListItem>
                  <asp:ListItem Value="1">一年級</asp:ListItem>
              </asp:DropDownList>
              班級：<asp:DropDownList ID="DDL_Class" runat="server" AutoPostBack="True">
                  <asp:ListItem Value="0">..請選擇</asp:ListItem>
                  <asp:ListItem Value="1">甲班</asp:ListItem>
                  <asp:ListItem Value="2">乙班</asp:ListItem>
              </asp:DropDownList>
            </p>
        
        </div>
       <div class="content_left">
          <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" Culture="zh-TW" 
              DataMember="DefaultView" DataSourceID="SqlDataSource1" GridLines="None" Skin="Metro">
            <ClientSettings EnableRowHoverStyle ="true">
                <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
            </ClientSettings>
<MasterTableView AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="REGSNO" DataMember="DefaultView"
                  DataSourceID="SqlDataSource1">
<CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False" 
        ShowRefreshButton="False"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridTemplateColumn DataField="REGSNO" 
            FilterControlAltText="Filter REGSNO column" HeaderText="學號" ReadOnly="True" 
            SortExpression="REGSNO" UniqueName="REGSNO">
           <ItemTemplate>
              <asp:Label ID="RegsnoLab" runat="server" Text='<%# Eval("REGSNO") %>'  ></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
        </telerik:GridTemplateColumn>
        <telerik:GridBoundColumn DataField="STDNAME" 
            FilterControlAltText="Filter STDNAME column" HeaderText="姓名" 
            SortExpression="STDNAME" UniqueName="STDNAME">
            <ItemStyle HorizontalAlign="Center" />
       </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Class" 
            FilterControlAltText="Filter Class column" HeaderText="班級" 
            SortExpression="Class" UniqueName="Class">
            <ItemStyle HorizontalAlign="Center" />
       </telerik:GridBoundColumn>
       <telerik:GridButtonColumn text="選擇>" commandName="Select">
            <ItemStyle HorizontalAlign="Center" />
       </telerik:GridButtonColumn>
   </Columns>
    <HeaderStyle HorizontalAlign="Center" />
<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

<PagerStyle PageSizeControlType="RadComboBox" Mode="NextPrev"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
          </telerik:RadGrid>
           <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
               ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
               ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
               SelectCommand="">
           </asp:SqlDataSource>
      </div>
      <div class="content_right">
          <asp:Button ID="But_SendSingle" runat="server" Text="寄發個人通知" Visible="False" />
          <br />
          <br />
          <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
          <br />
          <br />
      </div>
    <div class="spacer"></div>
    </div>
    <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server" 
        Skin="Telerik" />
    </form>
</body>
</html>
