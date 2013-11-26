Imports Telerik.Web.UI
Imports System.Data.SqlClient

Public Class manageUnit
    Inherits System.Web.UI.Page
    Public Shared UID As Integer
    Public Shared mid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub RG_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RG.ItemCommand
        Dim acc As String = Nothing
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim RightArray As Array = Nothing  '讀取使用者已設定的權限
        Dim aa As RadTreeNode
        Dim bb As RadTreeNode
        Dim cc As RadTreeNode

        If e.CommandName = "Select" Then
            UID = CType(e.Item.FindControl("Uid_Lab"), Label).Text
            SQL = "select GRPName,GRP_Right from UserGroupDB where ID = '" & UID & "'"
            da = Connect2SQLDB("NIU_DBASE", SQL)
            da.Fill(dt)

            RadTree.UncheckAllNodes() '清空先前已指定的資料
            '=====設定已有的選項=======================
            If dt.Rows(0).Item(1).ToString <> "" Then

                RightArray = dt.Rows(0).Item(1).ToString.Split(",")
                Dim n As Integer = RightArray.Length
                'ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('" & n & "');", True)
                For i = 0 To n - 1
                    'ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('" & RightArray(i) & "');", True)


                    For Each aa In RadTree.Nodes      '第一層目錄
                        If aa.Value = RightArray(i) Then
                            aa.Checked = True

                        End If

                        For Each bb In aa.Nodes       '第二層目錄
                            If bb.Value = RightArray(i) Then
                                bb.Checked = True
                            End If

                            For Each cc In bb.Nodes   '第三層目錄
                                If cc.Value = RightArray(i) Then
                                    cc.Checked = True
                                End If
                            Next
                        Next


                    Next



                Next
           

            End If
            '============================

            Label1.Text = "目前選擇的單位群組名稱：<b>" & CType(e.Item.FindControl("GRPNameLab"), Label).Text & "</b>"
        End If
    End Sub

    Private Shared Sub ShowCheckedNodes(ByVal treeView As RadTreeView)
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
        'label.Text = message + mid
    End Sub


    Protected Sub But_Send_Click(sender As Object, e As EventArgs) Handles But_Send.Click
        Dim SQL As String

        ShowCheckedNodes(RadTree)
        If UID >= 0 Then
            SQL = "Update UserGroupDB set GRP_Right = '" & mid & "' where ID = '" & UID & "'"
            executeSqlQuery("NIU_DBASE", SQL)
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('完成設定!!');location.href='manageUnit.aspx';", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請先選擇欲設定的單位!!');", True)
        End If
    End Sub
End Class