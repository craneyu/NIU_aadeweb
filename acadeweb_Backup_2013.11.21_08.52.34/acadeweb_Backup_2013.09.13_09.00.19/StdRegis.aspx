<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StdRegis.aspx.vb" Inherits="acadeweb.StdRegis" %>

<%@ Register src="Registry_menu.ascx" tagname="Registry_menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 18px;
            height: 18px;
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
              <h2><img src="image/icon_agenda_info.gif" alt="" />學生註冊管理</h2>
               <table border="0" cellpadding="3" cellspacing="1">
                 <tr>
                   <td><asp:Label ID="Lab_importTxt" runat="server" Text="Label"></asp:Label>
                  ：</td>
                  <td> 
                      <telerik:RadUpload ID="RadUpload1" Runat="server" 
                    ControlObjectsVisibility="None" Skin="Windows7" Height="21px" Width="226px" 
                          AllowedFileExtensions=".xls" ClientIDMode="Inherit" 
                          TargetFolder="~/Upload">
                    <Localization Select="選擇檔案" />
                </telerik:RadUpload>
                  </td>
                  <td>
                      <asp:Button ID="Button1" runat="server" Text="載入資料" />
                     </td>
                     <td>
                         <asp:Button ID="But_DeleteALL" runat="server" 
                             onclientclick="return confirm('是否刪除本學期現存繳費資料？')" Text="刪除本學期現存資料" />
                     </td>
                 </tr>
                 <tr>
                   <td>&nbsp;</td>
                  <td> 
                      <span style="color:Red; font-size:15px">*學生證欄位務必設定為 <b>RegsNo</b></span></td>
                  <td>
                      <asp:Button ID="Import_StdData" runat="server" Text="匯入資料" Enabled="False" />
                     </td>
                     <td>
                         &nbsp;</td>
                 </tr>
               </table>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
              <p>查詢學號：<asp:TextBox ID="Search_text" runat="server" BorderStyle="Dashed" 
                      Columns="10" MaxLength="8"></asp:TextBox>
                <asp:Button ID="But_Search" runat="server" Text="查詢" />　　　<img alt="add" 
                      class="style1" src="image/AddRecord%20.gif" />新增繳費學號：<asp:TextBox 
                      ID="Txt_AddRegsno" runat="server" Columns="10"></asp:TextBox>
                  <asp:Button ID="But_Add" runat="server" Text="新增" />
                </p>
                    <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" 
                    Culture="zh-TW" GridLines="None" Width="600px" Visible="False" 
                    DataSourceID="SqlDataSource1">
<ExportSettings>
<Pdf>
<PageHeader>
<LeftCell Text=""></LeftCell>

<MiddleCell Text=""></MiddleCell>

<RightCell Text=""></RightCell>
</PageHeader>

<PageFooter>
<LeftCell Text=""></LeftCell>

<MiddleCell Text=""></MiddleCell>

<RightCell Text=""></RightCell>
</PageFooter>
</Pdf>
</ExportSettings>

<MasterTableView AutoGenerateColumns="False"  AllowPaging="True" DataSourceID="SqlDataSource1">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
      <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" 
            HeaderText=" 序號" UniqueName="TemplateColumn" DataType="System.Int32" 
            DefaultInsertValue="1">
            <ItemTemplate>
               <%#Container.DataSetIndex + 1%>
            </ItemTemplate>
            <HeaderStyle Width="40px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </telerik:GridTemplateColumn>

        <telerik:GridBoundColumn DataField="classname" 
            FilterControlAltText="Filter classname column" HeaderText="系所別" 
            SortExpression="classname" UniqueName="classname">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Class" 
            FilterControlAltText="Filter Class column" HeaderText="班級" 
            SortExpression="Class" UniqueName="Class">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="RegsNo" 
            FilterControlAltText="Filter RegsNo column" HeaderText="學號" 
            SortExpression="RegsNo" UniqueName="RegsNo">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="STDNAME" 
            FilterControlAltText="Filter STDNAME column" HeaderText="姓名" 
            SortExpression="STDNAME" UniqueName="STDNAME">
        </telerik:GridBoundColumn>
         <telerik:GridTemplateColumn HeaderText="身份證號">
            <ItemTemplate>
                <asp:Label runat="server" ID="IdNoLab" Text='<%# Hide_IdNo(eval("IDNO").tostring) %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle  HorizontalAlign="Center" />
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn HeaderText="繳費情形">
            <ItemTemplate>
                <asp:Label runat="server" ID="FeestatusLab" Text='<%# Registry_Check(eval("RegsNo").tostring) %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle  HorizontalAlign="Center" />
        </telerik:GridTemplateColumn>
 
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

<BatchEditingSettings EditType="Cell"></BatchEditingSettings>

<PagerStyle PageSizeControlType="RadComboBox" Mode="NumericPages"></PagerStyle>
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
                    </telerik:RadGrid>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="" 
                    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" >
                </asp:SqlDataSource>
                <br />
                <telerik:RadGrid ID="RG2" runat="server" Visible="False" AllowPaging="True" 
                    CellSpacing="0" Culture="zh-TW" GridLines="None" Skin="WebBlue">
<ExportSettings>
<Pdf>
<PageHeader>
<LeftCell Text=""></LeftCell>

<MiddleCell Text=""></MiddleCell>

<RightCell Text=""></RightCell>
</PageHeader>

<PageFooter>
<LeftCell Text=""></LeftCell>

<MiddleCell Text=""></MiddleCell>

<RightCell Text=""></RightCell>
</PageFooter>
</Pdf>
</ExportSettings>

<MasterTableView PageSize="20">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

<BatchEditingSettings EditType="Cell"></BatchEditingSettings>

<PagerStyle PageSizeControlType="RadComboBox" PageButtonCount="20"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
                </telerik:RadGrid>
            </div>
    </div>
    <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server" 
        Skin="Windows7" />
    </form>
</body>
</html>
