Public Class StdRegisCheck
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Y") = "" Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('登入逾時，請重新登入!');location.href='Default.aspx';", True)
            'Response.Redirect("Default.aspx")
        End If

    End Sub

End Class