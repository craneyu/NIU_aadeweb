<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="main.aspx.vb" Inherits="acadeweb.WebForm1" %>

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
          <br /><p>
          請先選擇學年度：<asp:TextBox ID="Txt_Y" runat="server" Columns="4" MaxLength="4"></asp:TextBox>
              學期：<asp:DropDownList ID="DDL_M" runat="server" CausesValidation="True">
                      <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                  <asp:ListItem Value="1">01</asp:ListItem>
                  <asp:ListItem Value="2">02</asp:ListItem>
              </asp:DropDownList>
              <asp:Button ID="But_Confirm" runat="server" Text="確定" />&nbsp;<br />
                  
              </p>
          </div>   

    </div>
    </form>
</body>
</html>
