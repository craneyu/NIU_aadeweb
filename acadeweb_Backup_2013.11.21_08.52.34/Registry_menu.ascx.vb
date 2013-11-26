Public Class Registry_menu
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SQL As String = Nothing
        Dim sql2 As String = Nothing
        Dim rightstr As String = Nothing

        If Not Session("Y") Is Nothing Then
            Label1.Text = "<span class='style1'>登入者：<font style='color:#06C;'><b>" & Session("CName") & "</b></font> | "
            Label1.Text += "<img src='image/mailPersonalFolders.gif' alt='Logout' title='登出'><a href='Logout.aspx'>登出系統</a><br/>"
            Label1.Text += " <img src='image/ico_01.jpg' alt=''>目前處理學年度：<span class='red'> 1" & Session("Y") & "</span> 第<span class='red'> " & Session("M").ToString & "</span> 學期</span>"

        Else
            'Label1.Text = ""
            Response.Redirect("Default.aspx")
        End If

        If Session("RightGroup") <> "" Then
            'rightstr = Replace(Session("RightGroup"), ",", "','")
            rightstr = Session("RightGroup")
        End If

        If Session("RightSingle") <> "" Then
            'rightstr += Replace(Session("RightSingle"), ",", "','")
            rightstr += "," & Session("RightSingle")
        End If

        SQL = "SELECT [ID], [Menu_Name], [ParentID], rtrim(URL) as URL FROM [webapp_menu] "

        If Session("account") <> "admin" Then
            SQL += " where ID in (<SQLStr>) order by Sort"
            sql2 = Replace(SQL, "<SQLStr>", rightstr)
        Else
            sql2 = SQL & " order by Sort"
        End If

        AppMenu.SelectCommand = sql2
        

    End Sub

End Class