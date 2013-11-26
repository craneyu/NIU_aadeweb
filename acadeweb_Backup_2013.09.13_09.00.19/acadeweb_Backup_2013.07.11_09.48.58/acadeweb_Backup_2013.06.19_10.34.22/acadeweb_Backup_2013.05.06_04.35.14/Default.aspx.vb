Imports System.Data.SqlClient

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub But_Send_Click(sender As Object, e As EventArgs) Handles But_Send.Click
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        If account.Text <> "" And PassWD.Text <> "" Then
            SQL = "Select Name,unit from UserDB where Code = '" & account.Text & "' and pw = '" & PassWD.Text & "'"
            da = Connect2SQLDB("NIU_DBASE", SQL)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Session("CName") = dt.Rows(0).Item("name").ToString
                Session("unit") = dt.Rows(0).Item("unit").ToString
                Response.Redirect("main.aspx")
            End If

        End If

    End Sub
End Class