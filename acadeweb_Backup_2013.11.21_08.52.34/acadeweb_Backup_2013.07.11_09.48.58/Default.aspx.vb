Imports System.Data.SqlClient

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub But_Send_Click(sender As Object, e As EventArgs) Handles But_Send.Click
        Dim SQL, SQL2 As String
        Dim da As SqlDataAdapter
        Dim dt, dt_YS As New DataTable

        If account.Text <> "" And PassWD.Text <> "" Then
            '檢查登入帳號
            'If StaffCheckAccount(account.Text, PassWD.Text) = True Then
            '    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('LDAP Connection is Successfully !!')", True)
            'Else
            '    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('Connecttion Error!!')", True)
            'End If

            SQL = "Select Name, unit from UserDB where Code = '" & account.Text & "' and pw = '" & PassWD.Text & "'"
            da = Connect2SQLDB("NIU_DBASE", SQL)
            da.Fill(dt)

            '查詢當期學年度及學期
            SQL2 = "exec GetSYear"
            da = Connect2SQLDB("NIU_DBASE", SQL2)
            da.Fill(dt_YS)

            If dt.Rows.Count > 0 Then
                Session("CName") = dt.Rows(0).Item("Name").ToString
                Session("Unit") = dt.Rows(0).Item("unit").ToString
                Session("Y") = Microsoft.VisualBasic.Right(dt_YS.Rows(0).Item("SYear"), 2)
                Session("M") = dt_YS.Rows(0).Item("Semester")
                Session("SeleY") = CInt(dt_YS.Rows(0).Item("SYear"))
                Session("SeleM") = CInt(dt_YS.Rows(0).Item("Semester"))
                Response.Redirect("main.aspx")
            End If

        End If

    End Sub
End Class