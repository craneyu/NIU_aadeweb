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
          <div class="hr1"></div>
          <div class="topMain">
                <img src="image/niu_logo1.gif"  alt="" />
            </div>
            <div class="hr2"></div>
            <div class="spacer"></div>
            <div class="menu">
                <uc1:Registry_menu ID="Registry_menu1" runat="server" />
                </div>
             <div class="spacer"></div>
               YES...Login Success !!

    </div>
    </form>
</body>
</html>
