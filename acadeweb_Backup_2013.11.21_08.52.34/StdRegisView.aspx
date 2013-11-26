<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StdRegisView.aspx.vb" Inherits="acadeweb.StdRegisView" %>

<%@ Register Src="Registry_menu.ascx" TagName="Registry_menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>國立宜蘭大學-教務資訊系統Web</title>
    <link href="CSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc1:Registry_menu ID="Registry_menu1" runat="server" />
        <div class="spacer">
        </div>
        <div class="textMain2">
            <h2>
                <img src="image/icon_agenda_info.gif" alt="" />學生註冊情形查詢</h2>
            <p>
                學院：<asp:DropDownList ID="DDL_College" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;學制：<asp:DropDownList ID="DDL_C" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="0">..請選擇..</asp:ListItem>
                    <asp:ListItem Value="1">大學部</asp:ListItem>
                    <asp:ListItem Value="2">進修部</asp:ListItem>
                    <asp:ListItem Value="3">研究所</asp:ListItem>
                    <asp:ListItem Value="4">碩士專班</asp:ListItem>
                    <asp:ListItem Value="5">博士班</asp:ListItem>
                </asp:DropDownList>
                系所：<asp:DropDownList ID="DDL_Dept" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="0">..請選擇..</asp:ListItem>
                </asp:DropDownList>
                <br />
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
                繳費狀態：<asp:DropDownList ID="DDL_FeeStatus" runat="server" AutoPostBack="True">
                <asp:ListItem Selected="True" Value="0">..選擇狀態..</asp:ListItem>
                <asp:ListItem Value="1">已繳費</asp:ListItem>
                <asp:ListItem Value="2">已辦理貸款</asp:ListItem>
                <asp:ListItem Value="3">未註冊</asp:ListItem>
                </asp:DropDownList>
                &nbsp; 
                </p>
 
               <table >
                  <tr>
                     <td>依學號查詢：</td>
                     <td><asp:TextBox ID="Txt_StdNo" runat="server" Columns="10"></asp:TextBox></td>
                     <td>
                        <telerik:RadButton ID="SearchByStdNo" runat="server" Text="查詢">
                            <Icon PrimaryIconCssClass="rbSearch" PrimaryIconLeft="4" PrimaryIconTop="4"></Icon>
                        </telerik:RadButton>      
                     </td>
                     <td>
                         <telerik:RadButton ID="Export" runat="server" Text="匯出Excel檔">
                            <Icon PrimaryIconCssClass="rbDownload" PrimaryIconLeft="4" PrimaryIconTop="4"></Icon>
                         </telerik:RadButton>
                     </td>
                  </tr>
               </table>
                
           <telerik:RadGrid ID="RGD_View" runat="server" CellSpacing="0" Culture="zh-TW" 
                GridLines="None" Width="400px" Skin="Windows7" 
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
    <Excel Format="Biff" />
</ExportSettings>

                <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowPaging="True">
<CommandItemSettings ShowExportToExcelButton="True"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                   <Columns>
                      <telerik:GridBoundColumn DataField="RegsNo" FilterControlAltText="Filter RegsNo column"
                       HeaderText="學號" SortExpression="RegsNo" UniqueName="RegsNo">
                         <ItemStyle HorizontalAlign="Center" />
                      </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="StdName" FilterControlAltText="Filter StdName column"
                       HeaderText="姓名" SortExpression="StdName" UniqueName="StdName">
                         <ItemStyle HorizontalAlign="Center" />
                      </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="Class" FilterControlAltText="Filter Class column"
                       HeaderText="班級" SortExpression="Class" UniqueName="Class">
                         <ItemStyle HorizontalAlign="Center" /> 
                      </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="繳費狀態">
                          <ItemTemplate>
                             <asp:Label runat="server" ID="FeestateLab" Text='<%# Registry_Check(Eval("RegsNo").tostring) %>' ></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                       </telerik:GridTemplateColumn>
                       
                   </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

<BatchEditingSettings EditType="Cell"></BatchEditingSettings>

<PagerStyle PageSizeControlType="RadComboBox" Mode="NumericPages"></PagerStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                </MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
            </telerik:RadGrid>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                 ConnectionString="" 
                 ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" >
            </asp:SqlDataSource>
        </div>
    </div>
    <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server" 
        Skin="WebBlue" />
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    </form>
</body>
</html>