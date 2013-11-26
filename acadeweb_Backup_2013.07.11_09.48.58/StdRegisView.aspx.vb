Imports System.Data.SqlClient

Public Class StdRegisView
    Inherits System.Web.UI.Page
    Dim SQL_R As String

    Private Sub StdRegisView_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim DT_C As New DataTable

        If Session("Y") = "" Then
            Response.Redirect("Default.aspx")
        End If
        Acade_DB = DBName_init & Session("Y") & Session("M")

        '===先找出學院====
        SQL = "select '0' as code, '..請選擇..' as disp from code union select CODE, DISP from CODE where KIND = 'SCH'"
        da = Connect2SQLDB(Acade_DB, SQL)
        da.Fill(DT_C)


        For Each dr In DT_C.Rows
            DDL_College.Items.Add(New ListItem(dr("DISP").ToString, dr("CODE").ToString))
        Next

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

    Private Sub DDL_College_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_College.SelectedIndexChanged
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim DT_D As New DataTable

        Clear_DDL("ALL")
        DT_D.Clear()
        DDL_Dept.Items.Clear()
        DDL_Dept.Items.Add(New ListItem("..請選擇..", 0))

        SQL = "SELECT DISTINCT DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
        SQL += "WHERE (DEPT.c in ('4','R','P','N','D')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue

        da = Connect2SQLDB(Acade_DB, SQL)
        da.Fill(DT_D)

        For Each dr In DT_D.Rows
            DDL_Dept.Items.Add(New ListItem(Trim(dr("DISP")).ToString, Trim(dr("s_dept")).ToString))
        Next

    End Sub

    Sub Clear_DDL(ByVal S As String)  '重置已選的DDL選項內容
        Select Case S
            Case "ALL"
                DDL_Grade.ClearSelection()  '年級
                DDL_Class.ClearSelection()  '班級
            Case "1"
                DDL_Grade.ClearSelection()
            Case "2"
                DDL_Class.ClearSelection()

        End Select

    End Sub

    Private Sub DDL_Dept_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Dept.SelectedIndexChanged
        Clear_DDL("ALL")
        SQL_R = "SELECT [REGSNO], [STDNAME], [Class] FROM [stdid] where class like 'B" & DDL_Dept.SelectedValue & "%' order by class,regsno"

    End Sub

    Private Sub DDL_Grade_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Grade.SelectedIndexChanged
        Clear_DDL("2")
        SQL_R = "SELECT [REGSNO], [STDNAME], [Class] FROM [stdid] where class like "
        SQL_R += "'B" & DDL_Dept.SelectedValue & DDL_Grade.SelectedValue & "%' order by class,regsno "

    End Sub
End Class