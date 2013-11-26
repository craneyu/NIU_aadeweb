Public Class astr_view
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CName") = "" Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('登入逾時，請重新登入!');window.location='Default.aspx';", True)
            'Response.Redirect("Default.aspx")
        End If

    End Sub

End Class