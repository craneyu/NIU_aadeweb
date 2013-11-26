Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Not Session("Y") Is Nothing Then
                Txt_Y.Text = "1" & Session("Y")
            Else
                If Date.Now.Month > 7 Then
                    Txt_Y.Text = Date.Now.Year - 1911
                Else
                    Txt_Y.Text = Date.Now.Year - 1911 - 1
                End If

            End If

            If Not Session("M") Is Nothing Then
                DDL_M.SelectedItem.Text = Session("M").ToString
            Else
                If Date.Now.Month > 7 Then
                    DDL_M.SelectedValue = "01"
                Else
                    DDL_M.SelectedValue = "02"
                End If

            End If

        End If


    End Sub

    Protected Sub But_Confirm_Click(sender As Object, e As EventArgs) Handles But_Confirm.Click
        'Session.Clear()
        Session("Y") = Microsoft.VisualBasic.Right(Txt_Y.Text, 2)
        Session("M") = DDL_M.SelectedValue
        Server.Transfer("Main.aspx")
    End Sub
  
End Class