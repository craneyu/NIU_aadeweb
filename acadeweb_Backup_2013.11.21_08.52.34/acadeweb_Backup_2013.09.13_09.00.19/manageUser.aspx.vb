Imports Telerik.Web.UI
Imports System.Data.SqlClient

Public Class manageUser
    Inherits System.Web.UI.Page
    Public Shared Code As String
    Public Shared mid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Y") = "" Then
            Response.Redirect("Default.aspx")
        End If


        If IsPostBack And DDL_Unit.SelectedValue <> "NULL" Then
            SqlDataSource1.SelectCommand = "SELECT [Code], [Name], [pw] FROM [UserDB] WHERE [unit] = " & DDL_Unit.SelectedValue
        Else
            SqlDataSource1.SelectCommand = "SELECT [Code], [Name], [pw] FROM [UserDB] "
        End If
    End Sub

    Private Sub RG_List_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RG_List.ItemCommand
        Dim acc As String = Nothing
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim RightArray As Array = Nothing  '讀取使用者已設定的權限
        Dim aa As RadTreeNode
        Dim bb As RadTreeNode
        Dim cc As RadTreeNode

        If e.CommandName = "Select" Then
            Code = CType(e.Item.FindControl("CodeLab"), Label).Text
            SQL = "select Name,RightSingle from UserDB where Code = '" & Code & "'"
            da = Connect2SQLDB("NIU_DBASE", SQL)
            da.Fill(dt)
            RadTreeView1.UncheckAllNodes() '清空先前已指定的資料

            '=====設定已有的選項=======================
            If dt.Rows(0).Item(1).ToString <> "" Then
                RightArray = dt.Rows(0).Item(1).ToString.Split(",")
                Dim n As Integer = RightArray.Length
                'ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('" & n & "');", True)
                For i = 0 To n - 1


                    For Each aa In RadTreeView1.Nodes  '第一層目錄
                        If aa.Value = RightArray(i) Then
                            aa.Checked = True
                        End If

                        For Each bb In aa.Nodes         '第二層目錄
                            If bb.Value = RightArray(i) Then
                                bb.Checked = True
                            End If

                            For Each cc In bb.Nodes     '第三層目錄
                                If cc.Value = RightArray(i) Then
                                    cc.Checked = True
                                End If
                            Next
                        Next
                    Next
                Next
            End If
            '============================

            Label1.Text = "姓名：" & CType(e.Item.FindControl("NameLab"), Label).Text & " / 登入帳號：" & Code
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim SQL As String

        ShowCheckedNodes(RadTreeView1, Label2)
        If Code <> "" Then
            SQL = "Update UserDB set RightSingle = '" & mid & "' where Code = '" & Code & "'"
            executeSqlQuery("NIU_DBASE", SQL)
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('完成設定!!');location.href='manageUser.aspx';", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請先選擇欲設定的人員!!');", True)
        End If
    End Sub

    Private Shared Sub ShowCheckedNodes(ByVal treeView As RadTreeView, ByVal label As Label)
        Dim message As String = String.Empty
        Dim nodeCollection As IList(Of RadTreeNode) = treeView.CheckedNodes

        mid = ""

        For Each node As RadTreeNode In nodeCollection
            message += node.FullPath & "<br/>"
            If mid = "" Then
                mid = node.Value
            Else
                mid += "," & node.Value
            End If

        Next
        label.Text = message + mid
    End Sub
End Class