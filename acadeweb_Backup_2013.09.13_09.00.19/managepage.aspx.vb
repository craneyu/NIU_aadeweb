Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class managepage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Y") = "" Then
            Response.Redirect("Default.aspx")
        End If

    End Sub

    Private Sub RadTreeView1_NodeClick(sender As Object, e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles RadTreeView1.NodeClick
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        SQL = "select URL from webapp_menu where ID = " & RadTreeView1.SelectedValue
        da = Connect2SQLDB("NIU_DBASE", SQL)
        da.Fill(dt)

        Menu_Name.Text = RadTreeView1.SelectedNode.Text
        UrlName.Text = dt.Rows(0).Item(0).ToString
        Label1.Text = RadTreeView1.SelectedNode.FullPath
        Lab_PID.Text = RadTreeView1.SelectedValue
        But_AddMenu.Visible = True

    End Sub

    Protected Sub But_AddMenu_Click(sender As Object, e As EventArgs) Handles But_AddMenu.Click
        Menu_Name.Text = ""
        UrlName.Text = ""

    End Sub

    Protected Sub But_Send_Click(sender As Object, e As EventArgs) Handles But_Send.Click
        Dim SQL As String = String.Empty

        SQL = "Insert into webapp_menu (Menu_Name, ParentID, URL) values (N'" & Trim(Menu_Name.Text) & "'," & CInt(Lab_PID.Text) & ",'" & Trim(UrlName.Text) & "')"
        executeSqlQuery("NIU_DBASE", SQL)
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('新增完成!!');location.href='managepage.aspx';", True)
    End Sub
End Class