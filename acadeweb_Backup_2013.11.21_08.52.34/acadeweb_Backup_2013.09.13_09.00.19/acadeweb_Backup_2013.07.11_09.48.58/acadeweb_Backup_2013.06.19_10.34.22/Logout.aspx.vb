Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim inpage As String = Nothing
        'Request.Url
        Session.RemoveAll()

        Response.Redirect("Default.aspx")
    End Sub

End Class