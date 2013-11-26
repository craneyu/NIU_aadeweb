Public Class Registry_menu
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("Y") Is Nothing Then
            Label1.Text = "登入者：<font style='color:#06C;'><b>" & Session("CName") & "</b></font> | "
            Label1.Text += " <img src='image/ico_01.jpg' alt=''>目前處理學年度： 1" & Session("Y") & " 第 " & Session("M") & " 學期 | "
            Label1.Text += "<img src='image/mailPersonalFolders.gif' alt='Logout' title='登出'><a href='Logout.aspx'>登出系統</a>"

        Else
            Label1.Text = ""
        End If
    End Sub

End Class