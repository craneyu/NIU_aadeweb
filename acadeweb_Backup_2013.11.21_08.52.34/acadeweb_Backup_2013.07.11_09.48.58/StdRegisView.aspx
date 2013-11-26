<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StdRegisView.aspx.vb" Inherits="acadeweb.StdRegisView" %>

<%@ Register src="Registry_menu.ascx" tagname="Registry_menu" tagprefix="uc1" %>

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
         <uc1:Registry_menu ID="Registry_menu1" runat="server" />
        <div class="spacer"></div>
        <div class="textMain2">
            <h2><img src="image/icon_agenda_info.gif" alt="" />學生註冊情形查詢</h2>
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
            &nbsp; 依學號查詢：<asp:TextBox ID="TextBox1" runat="server" Columns="10"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="查詢" />
            </p>
            <p>&nbsp;</p>
        </div>
    </div>
    
    </form>
</body>
</html>
