<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewStdQuit.aspx.vb" Inherits="acadeweb.NewStdQuit" %>

<%@ Register src="Registry_menu.ascx" tagname="Registry_menu" tagprefix="uc1" %>

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
        <div class="spacer"></div>

        <div class="textMain2">
          <br />
            學生證號：<telerik:RadTextBox ID="Txt_StdNo" Runat="server" Columns="10" 
                MaxLength="10" Width="80px">
            </telerik:RadTextBox>
            <telerik:RadButton ID="But_Search" runat="server" Skin="WebBlue" Text="查詢新生">
                <Icon PrimaryIconCssClass="rbSearch" PrimaryIconLeft="4" PrimaryIconTop="4"></Icon>
            </telerik:RadButton>
            <p>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </p>
            <telerik:RadGrid ID="RadGrid1" runat="server" Skin="WebBlue" 
                Width="450px" CellSpacing="0" Culture="zh-TW" GridLines="None">
                <MasterTableView AutoGenerateColumns="False">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                       <telerik:GridTemplateColumn HeaderText="學號">
                          <ItemTemplate>
                             <asp:Label runat="server" ID="StdNoLab" Text='<%# Eval("RegsNo") %>'></asp:Label>
                          </ItemTemplate>
                       </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="StdName" 
                            FilterControlAltText="Filter StdName column" HeaderText="姓名" 
                            UniqueName="StdName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="class" 
                            FilterControlAltText="Filter class column" HeaderText="班級代碼" UniqueName="class">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="classname" 
                            FilterControlAltText="Filter classname column" HeaderText="班級名稱" 
                            UniqueName="classname">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn ButtonType="PushButton" CommandName="Delete" 
                            FilterControlAltText="Filter column column" HeaderText="移除" Text="移除" 
                            UniqueName="column" ConfirmTitle="警告!!" ConfirmText="您確定要刪除該學生學籍嗎?">
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

        </div>

    </div>
    </form>
</body>
</html>
