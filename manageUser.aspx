<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="manageUser.aspx.vb" Inherits="acadeweb.manageUser" %>

<%@ Register Src="Registry_menu.ascx" TagName="Registry_menu" TagPrefix="uc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>國立宜蘭大學-教務資訊系統Web</title>
    <link href="CSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 16px;
            height: 16px;
        }
        .style2
        {
            width: 16px;
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc1:Registry_menu ID="Registry_menu1" runat="server" />
    <div class="spacer">
    </div>
    <div class="textMain2">
        <p>
            <img alt="icon" class="style1" src="image/18.gif" /> 個人權限管理</p>
    </div>
    <div class="content_left">
        <p>
            依單位搜尋：<asp:DropDownList ID="DDL_Unit" runat="server" 
                AutoPostBack="True" DataSourceID="SqlData_UserGroup" DataTextField="GRPName" 
                DataValueField="ID">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlData_UserGroup" runat="server" 
                ConnectionString="<%$ ConnectionStrings:Connect_NIU_DBASE %>" 
                ProviderName="<%$ ConnectionStrings:Connect_NIU_DBASE.ProviderName %>" 
                SelectCommand="select '' as ID, '...請選擇...' as GRPName union SELECT [ID], [GRPName] FROM [UserGroupDB]">
            </asp:SqlDataSource>
        </p>
        <telerik:RadGrid ID="RG_List" runat="server" CellSpacing="0" Culture="zh-TW" DataSourceID="SqlDataSource1"
            GridLines="None" Width="200px" Skin="Office2010Blue">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="Code" DataSourceID="SqlDataSource1">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText=" 序號"
                        UniqueName="TemplateColumn" DataType="System.Int32" DefaultInsertValue="1">
                        <ItemTemplate>
                            <%#Container.DataSetIndex + 1%>
                        </ItemTemplate>
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Name" FilterControlAltText="Filter Name column"
                        HeaderText="姓名" SortExpression="Name" UniqueName="Name">
                        <ItemTemplate>
                           <asp:Label ID="NameLab" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Code" FilterControlAltText="Filter Code column"
                        HeaderText="帳號" SortExpression="Code" UniqueName="Code" ReadOnly="True" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="CodeLab" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <asp:Button ID="click" runat="server" Text="Select" CommandName="select" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="80px" />
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </MasterTableView>
            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </telerik:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connect_NIU_DBASE %>"
            ProviderName="<%$ ConnectionStrings:Connect_NIU_DBASE.ProviderName %>" SelectCommand="">
        </asp:SqlDataSource>
    </div>
    <div class="content_right">
        <img alt="" class="style2" src="image/ico_02.jpg" />
        權限設定<br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <table width="100%" border="0">
            <tr>
                <td width="300px" valign="top">
                    <telerik:RadTreeView ID="RadTreeView1" runat="server" DataFieldID="ID" DataFieldParentID="ParentID"
                        DataSourceID="SqlDataSource2" DataTextField="Menu_Name" Skin="Silk" CheckBoxes="True"
                        CheckChildNodes="true" DataValueField="ID">
                    </telerik:RadTreeView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connect_NIU_DBASE %>"
                        ProviderName="<%$ ConnectionStrings:Connect_NIU_DBASE.ProviderName %>" SelectCommand="SELECT [ID], [Menu_Name], [ParentID]  FROM [webapp_menu] order by Sort">
                    </asp:SqlDataSource>
                </td>
                <td width="100px" valign="top" align="center">
                    <asp:Button ID="Button1" runat="server" Text="設定權限" />
                </td>
                <td valign="top">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="clear: both">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Telerik" />
    </div>
    </form>
</body>
</html>