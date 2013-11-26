Imports System.Data.SqlClient

Public Class NewStdQuit
    Inherits System.Web.UI.Page

    Private Sub NewStdQuit_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Label1.Text = ""
        RadGrid1.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Y") = "" Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('登入逾時，請重新登入!');window.location='Default.aspx';", True)
            'Response.Redirect("Default.aspx")
        End If
    End Sub

    Protected Sub But_Search_Click(sender As Object, e As EventArgs) Handles But_Search.Click
        Dim SQLStr As String
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        If Mid(Trim(Txt_StdNo.Text), 2, 2) = Right(Session("Y"), 2) Then
            SQLStr = "SELECT StdData.RegsNo, StdData.Name as StdName, CASE StdData.Sex WHEN 1 THEN '男' WHEN 2 THEN '女' END AS sex, " _
                   & "StdData.Class as class, DEPT.classname as classname FROM StdData INNER JOIN DEPT ON StdData.Class = DEPT.class " _
                   & "where Regsno = '" & Trim(Txt_StdNo.Text) & "'" ' and (SUBSTRING(DEPT.class, 4, 1) IN ('1'))"

            da = Connect2SQLDB("std", SQLStr)
            da.Fill(dt)

            RadGrid1.DataSource = dt
            RadGrid1.DataBind()
            RadGrid1.Visible = True
            Label1.Text = ""

        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('您輸入的學生非新生!');", True)

        End If

    End Sub

    Private Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Dim StdNo As String = Nothing

        If e.CommandName = "Delete" Then
            StdNo = CType(e.Item.FindControl("StdNoLab"), Label).Text
            If StdQuit(StdNo) = True Then
                Label1.Text = "學號：" & StdNo & "，入學資料已移除。"
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('新生：" & StdNo & "'，入學資料已移除。')", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('無法刪除。')", True)
            End If
            RadGrid1.Visible = False
        End If
    End Sub

#Region "移除放棄入學新生資料"
    Function StdQuit(ByVal stdno As String)
        Dim SQL As String

        Try
            SQL = "Delete from StdData where regsno ='" & stdno & "'"
            executeSqlQuery("std", SQL)

            SQL = "Delete from StdID where regsno = '" & stdno & "'"
            executeSqlQuery("std", SQL)

            SQL = "Delete from StdTran where regsno = '" & stdno & "'"
            executeSqlQuery("std", SQL)

            SQL = "Delete from StdStudy where regsno = '" & stdno & "'"
            executeSqlQuery("std", SQL)

            SQL = "Delete from StdRFIDKey where regsno = '" & stdno & "'"
            executeSqlQuery("std", SQL)

            Return True

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('" & ex.Message & "')", True)
            Return False

        End Try

    End Function


#End Region

End Class