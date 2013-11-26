<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="acadeweb._Default" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

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
                <img src="image/AcadeApp_Banner.jpg"  alt="" />
            </div>
            <div class="hr2"></div>
        <p></p>
            <div class="spacer"></div>
            <div class="textMain">
              <table width="250px" align="center" cellpadding="4" cellspacing="2">
                <tr>
                  <td colspan="2" align="center" height="50px"><font size="6" 
                          style="font-family: 標楷體">教務資訊系統</font></td>
                </tr>
                <tr>
                  <td align="right">帳號：</td>
                  <td>
                      <asp:TextBox ID="account" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                  <td align="right">密碼：</td>
                  <td>
                      <asp:TextBox ID="PassWD" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                  <td colspan="2" align="center">
                      <asp:Button ID="But_Send" runat="server" Text="登入系統" />
                      <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                          onclientclick="document.form1.reset();" Text="清除重填" UseSubmitBehavior="False" />
                    </td>
                </tr>
                
              </table>
            </div>
    </div>
    <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server" 
        Skin="Windows7" />
    </form>
</body>
</html>
