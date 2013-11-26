Imports System.Data.SqlClient

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub But_Send_Click(sender As Object, e As EventArgs) Handles But_Send.Click
        Dim SQL, SQL2, SQL3 As String
        Dim da As SqlDataAdapter
        Dim dt, dt_YS, dt_dept As New DataTable
        Dim CheckAccount As New ServiceReference1.TeacServiceSoapClient

        If account.Text <> "" And PassWD.Text <> "" Then
            '檢查登入帳號

            If CheckAccount.ValidateUser(account.Text, PassWD.Text) = True Then
                'ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('Web Service Check Account Successfully !!');", True)
                'Exit Sub

                SQL = "Select UserDB.Name as Name, UserDB.unit as unit, UserDB.RightSingle as RightSingle, UserGroupDB.GRP_Right as GRP_Right, "
                SQL += "UserGroupDB.GRPName as GRPName, UserDB.code as s_dept from UserDB INNER JOIN UserGroupDB ON UserDB.unit = UserGroupDB.ID "
                SQL += "where email = '" & account.Text & "'"
                'SQL += "where UserDB.Code = '" & account.Text & "' and UserDB.pw = '" & PassWD.Text & "'"
                da = Connect2SQLDB("NIU_DBASE", SQL)
                da.Fill(dt)

                '查詢當期學年度及學期
                SQL2 = "exec GetSYear"
                da = Connect2SQLDB("NIU_DBASE", SQL2)
                da.Fill(dt_YS)

                If dt.Rows.Count > 0 Then
                    Session("s_dept") = dt.Rows(0).Item("s_dept").ToString
                    Session("account") = account.Text
                    Session("CName") = dt.Rows(0).Item("Name").ToString
                    Session("Unit") = dt.Rows(0).Item("unit").ToString
                    Session("RightGroup") = dt.Rows(0).Item("GRP_Right").ToString
                    Session("RightSingle") = dt.Rows(0).Item("RightSingle").ToString
                    Session("Y") = Microsoft.VisualBasic.Right(dt_YS.Rows(0).Item("SYear"), 2)
                    Session("M") = dt_YS.Rows(0).Item("Semester")
                    Session("SeleY") = CInt(dt_YS.Rows(0).Item("SYear"))
                    Session("SeleM") = CInt(dt_YS.Rows(0).Item("Semester"))

                    Acade_DB = DBName_init & Microsoft.VisualBasic.Right(dt_YS.Rows(0).Item("SYear"), 2) & dt_YS.Rows(0).Item("Semester")

                    '取得學院代號
                    If dt.Rows(0).Item("unit") = 6 Then
                        SQL3 = "select DISTINCT type from dept where s_dept = '" & dt.Rows(0).Item("s_dept").ToString & "' and specmark2=0"
                        da = Connect2SQLDB(Acade_DB, SQL3)
                        da.Fill(dt_dept)
                        Session("collage") = dt_dept.Rows(0).Item(0).ToString
                    End If

                    Response.Redirect("main.aspx")
                End If


            Else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('Connecttion Error!!');", True)
                Exit Sub

            End If


        End If

    End Sub
End Class