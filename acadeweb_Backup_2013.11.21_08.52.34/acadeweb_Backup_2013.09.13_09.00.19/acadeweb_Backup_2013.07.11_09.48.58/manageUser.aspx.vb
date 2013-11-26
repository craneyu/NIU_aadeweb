Public Class manageUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack And DDL_Unit.SelectedValue <> "NULL" Then
            SqlDataSource1.SelectCommand = "SELECT [Code], [Name], [pw] FROM [UserDB] WHERE [unit] = " & DDL_Unit.SelectedValue
        Else
            SqlDataSource1.SelectCommand = "SELECT [Code], [Name], [pw] FROM [UserDB] "
        End If
    End Sub

    
    Private Sub RG_List_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RG_List.ItemCommand
        Dim acc As String = Nothing

        If e.CommandName = "Select" Then
            Label1.Text = CType(e.Item.FindControl("CodeLab"), Label).Text

        End If
    End Sub
End Class