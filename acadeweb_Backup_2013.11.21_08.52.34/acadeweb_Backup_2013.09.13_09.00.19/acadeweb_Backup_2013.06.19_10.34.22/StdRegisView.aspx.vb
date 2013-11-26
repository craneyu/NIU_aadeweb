Public Class StdRegisView
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Acade_DB = DBName_init & Session("Y") & "0" & Session("M")

    End Sub

End Class