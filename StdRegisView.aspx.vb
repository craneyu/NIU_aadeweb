Imports System.Data.SqlClient

Public Class StdRegisView
    Inherits System.Web.UI.Page

    Public Shared dt As New DataTable
    Public Shared str As String

    Private Sub StdRegisView_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim SQL As String = Nothing
        Dim sql_col As String = Nothing
        Dim da As SqlDataAdapter
        Dim DT_C As New DataTable

        If Session("Y") = "" Then
            Response.Redirect("Default.aspx")
        End If
        Acade_DB = DBName_init & Session("Y") & Session("M")

        If Session("collage") <> "" Then
            sql_col = " and code = " & Session("collage")
        End If
        '===先找出學院====
        SQL = "select '0' as code, '..請選擇..' as disp from code union select rtrim(CODE) as CODE, DISP from CODE where KIND = 'SCH'" & sql_col
        da = Connect2SQLDB(Acade_DB, SQL)
        da.Fill(DT_C)

        For Each dr In DT_C.Rows
            DDL_College.Items.Add(New ListItem(dr("DISP").ToString, dr("CODE").ToString))
        Next

        SqlDataSource1.ConnectionString = DBstringClass.DBString("Acade", Session("Y"), Session("M"))

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim str As String = Nothing

        If Session("Y") = "" Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('登入逾時，請重新登入!');window.location='Default.aspx';", True)
            'Response.Redirect("Default.aspx")
        End If


        If IsPostBack Then

            If DDL_Dept.SelectedValue <> "0" And (DDL_Grade.SelectedValue = "0" And DDL_Class.SelectedValue = "0") Then
                str = DDL_Dept.SelectedValue
                Rd_Data(str, 1)

            ElseIf (DDL_Dept.SelectedValue <> "0" And DDL_Grade.SelectedValue <> "0") Or DDL_Class.SelectedValue = "0" Then
                str = DDL_Dept.SelectedValue & DDL_Grade.SelectedValue
                Rd_Data(str, 1)

            ElseIf DDL_Dept.SelectedValue <> "0" And DDL_Grade.SelectedValue <> "0" And DDL_Class.SelectedValue = "0" Then
                str = DDL_Dept.SelectedValue & DDL_Grade.SelectedValue & DDL_Class.SelectedValue
                Rd_Data(str, 1)

            End If

            If DDL_FeeStatus.SelectedValue <> "0" Then
                Rd_Data(str, 2)
            End If

            If Txt_StdNo.Text <> "" Then
                Rd_Data(Trim(UCase(Txt_StdNo.Text)), 3)

            End If

        End If
    End Sub

#Region "下拉選單控制"

    Private Sub DDL_College_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_College.SelectedIndexChanged
        '學院
        Clear_DDL("ALL")  '清空學制、系所、年級、班級的選擇

    End Sub

    Private Sub DDL_C_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_C.SelectedIndexChanged
        '學制
        Dim SQL As String = Nothing
        Dim SQL_Dept As String = Nothing
        Dim da As SqlDataAdapter
        Dim DT_D As New DataTable

        Clear_DDL(3)  '清空系所、年級、班級的選擇
        DT_D.Clear()
        DDL_Dept.Items.Clear()
        DDL_Dept.Items.Add(New ListItem("..請選擇..", 0))

        If Session("s_dept") <> "" And Session("Unit") = 6 Then
            SQL_Dept = " and s_dept = '" & Session("s_dept") & "' "
        Else
            SQL_Dept = ""
        End If

        Select Case DDL_C.SelectedValue
            Case 1   '大學部
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('4')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue
                SQL += " and substring(dept.class,1,1) = 'B' and substring(dept.class,3,1) not in ('1','2','3') " & SQL_Dept
                SQL += " order by Code.DISP"

            Case 2   '進修學士班
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('8','9')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue
                SQL += " and substring(dept.class,1,1) = 'C' " & SQL_Dept
                SQL += " order by Code.DISP"

            Case 3   '碩士班
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('R')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue & SQL_Dept
                SQL += " order by Code.DISP"

            Case 4   '碩專班(含數位碩專班)
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('P','N')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue & SQL_Dept
                SQL += " order by Code.DISP"

            Case 5   '博士班
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('D')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue & SQL_Dept
                SQL += " order by Code.DISP"

            Case 0
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請確實選擇學制！')", True)
                Exit Sub
        End Select

        da = Connect2SQLDB(Acade_DB, SQL)
        da.Fill(DT_D)

        For Each dr In DT_D.Rows
            DDL_Dept.Items.Add(New ListItem(Trim(dr("class")).ToString & Trim(dr("DISP")).ToString, Trim(dr("class")).ToString))
        Next

    End Sub

    Sub Clear_DDL(ByVal S As String)
        '重置已選的DDL選項內容

        Select Case S
            Case "ALL"
                DDL_C.ClearSelection()      '學制
                DDL_Grade.ClearSelection()  '年級
                DDL_Dept.ClearSelection()   '系所
                DDL_Class.ClearSelection()  '班級
            Case "1"
                DDL_Grade.ClearSelection()  '年級
                DDL_Class.ClearSelection()  '班級
            Case "2"
                DDL_Class.ClearSelection()  '班級
            Case "3"
                DDL_Grade.ClearSelection()  '年級
                DDL_Dept.ClearSelection()   '系所
                DDL_Class.ClearSelection()  '班級

        End Select
        DDL_FeeStatus.ClearSelection()  '繳費狀態
        Txt_StdNo.Text = ""
    End Sub

    Private Sub DDL_Dept_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Dept.SelectedIndexChanged
        '系所

        Clear_DDL("1") '清空年級、班級的選擇
        If DDL_Dept.SelectedValue <> "0" Then
            'RGD_View.Dispose()
            str = DDL_Dept.SelectedValue
            Rd_Data(str, 1)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請確實選擇系所名稱！')", True)
            Exit Sub
        End If


    End Sub

    Private Sub DDL_Grade_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Grade.SelectedIndexChanged
        '年級

        Clear_DDL("2")  '清空班級的選擇
        If DDL_Grade.SelectedValue <> "0" Then
            'RGD_View.Dispose()
            str = DDL_Dept.SelectedValue & DDL_Grade.SelectedValue
            Rd_Data(str, 1)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請確實選擇年級！')", True)
            Exit Sub

        End If

    End Sub

    Private Sub DDL_Class_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Class.SelectedIndexChanged
        '班級

        If DDL_Class.SelectedValue <> "0" Then

            'RGD_View.Dispose()
            str = DDL_Dept.SelectedValue & DDL_Grade.SelectedValue & DDL_Class.SelectedValue
            Rd_Data(str, 1)

        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請確實選擇班級！')", True)
            Exit Sub
        End If
    End Sub

    Private Sub DDL_FeeStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_FeeStatus.SelectedIndexChanged
        '繳費狀態
        If DDL_FeeStatus.SelectedValue <> "0" Then

            Rd_Data(str, 2)

        End If
    End Sub

#End Region

#Region "資料庫讀取"

    Sub Rd_Data(ByVal str As String, ByVal i As Integer)
        Dim SQL_R As String

        '
        If i = 1 Then
            SQL_R = "SELECT StdID.REGSNO, StdID.STDNAME, StdID.Class, niu_dbase.dbo.FeeState.FeeStatus as FeeStatus "
            SQL_R += "FROM StdID LEFT OUTER JOIN NIU_DBASE.dbo.FeeState ON StdID.REGSNO = NIU_DBASE.dbo.FeeState.RegsNo "
            SQL_R += "where class like '" & str & "%'"
            SQL_R += " order by StdID.class,StdID.regsno"
        ElseIf i = 2 Then
            If DDL_FeeStatus.SelectedValue <> 3 Then
                SQL_R = "SELECT StdID.REGSNO, StdID.STDNAME, StdID.Class, niu_dbase.dbo.FeeState.FeeStatus as FeeStatus "
                SQL_R += "FROM StdID LEFT OUTER JOIN NIU_DBASE.dbo.FeeState ON StdID.REGSNO = NIU_DBASE.dbo.FeeState.RegsNo "
                SQL_R += " where class like '" & str & "%' and NIU_DBASE.dbo.FeeState.FeeStatus = " & DDL_FeeStatus.SelectedValue
                SQL_R += " order by StdID.class,StdID.regsno"
            Else
                SQL_R = "SELECT REGSNO, STDNAME, Class, '' as FeeStatus "
                SQL_R += "from StdID where regsno not in (select regsno from NIU_DBASE.dbo.FeeState) "
                SQL_R += "and class like '" & str & "%'"

            End If

        Else  '
            SQL_R = "SELECT StdID.REGSNO, STDNAME, Class, NIU_DBASE.dbo.FeeState.FeeStatus "
            SQL_R += "from StdID LEFT OUTER JOIN NIU_DBASE.dbo.FeeState ON StdID.REGSNO = NIU_DBASE.dbo.FeeState.RegsNo "
            SQL_R += "where StdID.regsno like '" & Trim(UCase(str)) & "%'"

        End If

        SqlDataSource1.SelectCommand = SQL_R

    End Sub

#End Region

#Region "RadGrid Templete 讀取 Function"

    Function Registry_Check(ByVal stdno As String) As String
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim Rdt As New DataTable

        SQL = "select FeeStatus from FeeState where regsno = '" & stdno & "' and FeeState.Lyear = " & Session("SeleY") & " and FeeState.Lterm = " & Session("SeleM")
        da = Connect2SQLDB("NIU_DBASE", SQL)
        da.Fill(Rdt)

        If Rdt.Rows.Count <> 0 Then
            If Rdt.Rows(0).Item(0).ToString = "1" Then
                Return "已繳費"
            Else
                Return "已辦理貸款"
            End If
        Else
            Return "未註冊"
        End If

    End Function

#End Region

#Region "匯出Excel相關名稱設定"

    Protected Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        '匯出的Excel及檔名設定
        Dim export_filename As String = Now.Date
        
        RGD_View.ExportSettings.ExportOnlyData = True
        RGD_View.ExportSettings.IgnorePaging = True
        RGD_View.ExportSettings.OpenInNewWindow = True

        If DDL_Dept.SelectedValue <> "0" Then
            export_filename = DDL_Dept.SelectedItem.ToString

        ElseIf DDL_Grade.SelectedValue <> "0" Then
            export_filename = DDL_Dept.SelectedItem.ToString & DDL_Grade.SelectedItem.ToString

        ElseIf DDL_Class.SelectedValue <> "0" Then
            export_filename = DDL_Dept.SelectedValue & DDL_Grade.SelectedValue & DDL_Class.SelectedValue

        End If

        RGD_View.ExportSettings.FileName = export_filename


        RGD_View.MasterTableView.ExportToExcel()
    End Sub

    Private Sub RGD_View_ExcelMLExportRowCreated(sender As Object, e As Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLRowCreatedArgs) Handles RGD_View.ExcelMLExportRowCreated
        '匯出的Excel檔內的工作表名稱設定
        Dim export_sheetname As String = Now.Date

        If DDL_Dept.SelectedValue <> "0" Then
            export_sheetname = DDL_Dept.SelectedItem.ToString

        ElseIf DDL_Grade.SelectedValue <> "0" Then
            export_sheetname = DDL_Dept.SelectedItem.ToString & DDL_Grade.SelectedItem.ToString

        ElseIf DDL_Class.SelectedValue <> "0" Then
            export_sheetname = DDL_Dept.SelectedValue & DDL_Grade.SelectedValue & DDL_Class.SelectedValue

        End If

        e.Worksheet.Name = export_sheetname

    End Sub
#End Region

#Region "Button"

    Protected Sub SearchByStdNo_Click(sender As Object, e As EventArgs) Handles SearchByStdNo.Click

        RGD_View.Dispose()

        If Txt_StdNo.Text <> "" Then
            Rd_Data(Trim(UCase(Txt_StdNo.Text)), 3)

            'SQL_R = "SELECT StdID.REGSNO, STDNAME, Class, NIU_DBASE.dbo.FeeState.FeeStatus "
            'SQL_R += "from StdID LEFT OUTER JOIN NIU_DBASE.dbo.FeeState ON StdID.REGSNO = NIU_DBASE.dbo.FeeState.RegsNo "
            'SQL_R += "where StdID.regsno like '" & Trim(UCase(Txt_StdNo.Text)) & "%'"

            'SqlDataSource1.SelectCommand = SQL_R
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請輸入欲查詢的學號！')", True)
        End If

    End Sub

#End Region


End Class