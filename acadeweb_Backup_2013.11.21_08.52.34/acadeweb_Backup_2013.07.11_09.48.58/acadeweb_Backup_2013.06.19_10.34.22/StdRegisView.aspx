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
        </div>
    </div>
    
    </form>
</body>
</html>
