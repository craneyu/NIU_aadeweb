<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="manageUnit.aspx.vb" Inherits="acadeweb.manageUnit" %>

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
    <div class="spacer">
    </div>
    <div class="textMain2">
       <p><img alt="icon" class="style1" src="image/18.gif" />單位群組權限設定</p> 
       <div class="content_left">單位列表<br />

           <telerik:RadGrid ID="RG" runat="server" Culture="zh-TW" 
               DataSourceID="Unit_SQL" Skin="WebBlue" CellSpacing="0" 
               GridLines="None" AutoGenerateColumns="False" Width="300px">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
            </ClientSettings>
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

<MasterTableView AutoGenerateColumns="False" DataSourceID="Unit_SQL" DataKeyNames="ID">
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
         <telerik:GridTemplateColumn DataField="ID" DataType="System.Int32" 
            FilterControlAltText="Filter ID column" HeaderText="ID" ReadOnly="True" 
            SortExpression="ID" UniqueName="ID" Visible="False">
           <ItemTemplate>
              <asp:Label runat="server" ID="Uid_Lab" Text='<%# Eval("ID") %>'></asp:Label>
           </ItemTemplate>
        </telerik:GridTemplateColumn>
         <telerik:GridTemplateColumn DataField="GRPName" 
            FilterControlAltText="Filter GRPName column" HeaderText="單位群組" 
            SortExpression="GRPName" UniqueName="GRPName">
           <ItemTemplate>
               <asp:Label runat="server" ID="GRPNameLab" Text='<%# Eval("GRPName") %>'></asp:Label>
           </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn>
            <ItemTemplate>
                <asp:Button ID="click" runat="server" Text="Select" CommandName="select" 
                    CausesValidation="False" UseSubmitBehavior="False" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle Width="80px" />
        </telerik:GridTemplateColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

<BatchEditingSettings EditType="Cell"></BatchEditingSettings>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
           </telerik:RadGrid>
           <asp:SqlDataSource ID="Unit_SQL" runat="server" ConnectionString="<%$ ConnectionStrings:Connect_NIU_DBASE %>"
            ProviderName="<%$ ConnectionStrings:Connect_NIU_DBASE.ProviderName %>"
            Selectcommand="SELECT [ID], [GRPName], [GRP_Right] FROM [UserGroupDB]">
           </asp:SqlDataSource>

       </div>
       <div class="content_right">
         <div>
             <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
             <telerik:RadTreeView ID="RadTree" Runat="server" DataFieldID="ID" 
                 DataFieldParentID="ParentID" DataSourceID="APP_SqlData" 
                 DataTextField="Menu_Name" Skin="Silk" CheckBoxes="True"
                         CheckChildNodes="true" 
                 DataValueField="ID">
             </telerik:RadTreeView>
             <asp:Button ID="But_Send" runat="server" Text="送出" />
             <asp:SqlDataSource ID="APP_SqlData" runat="server" 
                 ConnectionString="<%$ ConnectionStrings:Connect_NIU_DBASE %>" 
                 ProviderName="<%$ ConnectionStrings:Connect_NIU_DBASE.ProviderName %>" 
                 SelectCommand="SELECT [ID], [Menu_Name], [ParentID]  FROM [webapp_menu] order by Sort">
             </asp:SqlDataSource>
           </div>
       </div>
       <div class="spacer"></div>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RG">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Label1" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="RadTree" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    </form>
</body>
</html>