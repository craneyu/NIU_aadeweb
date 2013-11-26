Imports System.Data.SqlClient

Public Class StdRegisView
    Inherits System.Web.UI.Page

    Public Shared dt As New DataTable
    Public Shared str As String

    Private Sub StdRegisView_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim DT_C As New DataTable

        If Session("Y") = "" Then
            Response.Redirect("Default.aspx")
        End If
        Acade_DB = DBName_init & Session("Y") & Session("M")

        '===先找出學院====
        SQL = "select '0' as code, '..請選擇..' as disp from code union select rtrim(CODE) as CODE, DISP from CODE where KIND = 'SCH'"
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
            Response.Redirect("Default.aspx")
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

        End If
    End Sub

    Private Sub DDL_College_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_College.SelectedIndexChanged
        Clear_DDL("ALL")  '清空學制、年級、班級的選擇

    End Sub

    Private Sub DDL_C_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_C.SelectedIndexChanged
        Dim SQL As String = Nothing
        Dim da As SqlDataAdapter
        Dim DT_D As New DataTable

        Clear_DDL(1)  '清空年級、班級的選擇
        DT_D.Clear()
        DDL_Dept.Items.Clear()
        DDL_Dept.Items.Add(New ListItem("..請選擇..", 0))

        Select Case DDL_C.SelectedValue
            Case 1   '大學部
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('4')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue
                SQL += " and substring(dept.class,1,1) = 'B' and substring(dept.class,3,1) not in ('1','2','3') order by Code.DISP"

            Case 2   '進修學士班
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('8','9')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue
                SQL += " and substring(dept.class,1,1) = 'C' order by Code.DISP"

            Case 3   '碩士班
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('R')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue
                SQL += " order by Code.DISP"

            Case 4   '碩專班(含數位碩專班)
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('P','N')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue
                SQL += " order by Code.DISP"

            Case 5   '博士班
                SQL = "SELECT DISTINCT substring(DEPT.class,1,3) as class, DEPT.s_dept, CODE.DISP FROM DEPT INNER JOIN CODE ON DEPT.s_dept = CODE.CODE "
                SQL += "WHERE (DEPT.c in ('D')) AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue
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

    Sub Clear_DDL(ByVal S As String)  '重置已選的DDL選項內容
        Select Case S
            Case "ALL"
                DDL_C.ClearSelection()      '學制
                DDL_Grade.ClearSelection()  '年級
                DDL_Class.ClearSelection()  '班級
            Case "1"
                DDL_Grade.ClearSelection()  '年級
                DDL_Class.ClearSelection()  '班級
            Case "2"
                DDL_Class.ClearSelection()  '班級

        End Select

        'RGD_View.Dispose()

    End Sub

    Private Sub DDL_Dept_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Dept.SelectedIndexChanged
        'Dim str As String

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
        'Dim str As String

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
        'Dim str As String

        If DDL_C.SelectedValue <> "0" Then

            'RGD_View.Dispose()
            str = DDL_Dept.SelectedValue & DDL_Grade.SelectedValue & DDL_Class.SelectedValue
            Rd_Data(str, 1)

        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請確實選擇班級！')", True)
            Exit Sub
        End If
    End Sub

    Protected Sub But_SearchByStdNo_Click(sender As Object, e As EventArgs) Handles But_SearchByStdNo.Click
        Dim SQL_R As String
        
        RGD_View.Dispose()

        If Txt_StdNo.Text <> "" Then
            SQL_R = "SELECT niu_dbase.dbo.FeeState.RegsNo, stdid.STDNAME, stdid.Class, DEPT.classname, "
            SQL_R += "stdid.IDNO, niu_dbase.dbo.FeeState.FeeStatus as FeeStatus "
            SQL_R += "FROM niu_dbase.dbo.FeeState left outer JOIN stdid ON "
            SQL_R += "niu_dbase.dbo.FeeState.RegsNo = stdid.REGSNO INNER JOIN DEPT ON "
            SQL_R += "stdid.Class =DEPT.class where stdid.regsno like '" & Trim(UCase(Txt_StdNo.Text)) & "%'"
            'SQL_R += "or (niu_dbase.dbo.FeeState.Lyear = " & Session("SeleY") & " and  niu_dbase.dbo.FeeState.Lterm = " & Session("SeleM") & ") "
           
            SqlDataSource1.SelectCommand = SQL_R
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請輸入欲查詢的學號！')", True)
        End If

    End Sub

    Sub Rd_Data(ByVal str As String, ByVal i As Integer)
        Dim SQL_R As String
        
        If i = 1 Then
            SQL_R = "SELECT StdID.REGSNO, StdID.STDNAME, StdID.Class, niu_dbase.dbo.FeeState.FeeStatus as FeeStatus "
            SQL_R += "FROM StdID LEFT OUTER JOIN NIU_DBASE.dbo.FeeState ON StdID.REGSNO = NIU_DBASE.dbo.FeeState.RegsNo "
            SQL_R += "where class like '" & str & "%'"
            'SQL_R += "or (niu_dbase.dbo.FeeState.Lyear = " & Session("SeleY") & " and  niu_dbase.dbo.FeeState.Lterm = " & Session("SeleM") & ") "
            SQL_R += " order by StdID.class,StdID.regsno"
        Else
            SQL_R = "SELECT StdID.REGSNO, StdID.STDNAME, StdID.Class, niu_dbase.dbo.FeeState.FeeStatus as FeeStatus "
            SQL_R += "FROM StdID LEFT OUTER JOIN NIU_DBASE.dbo.FeeState ON StdID.REGSNO = NIU_DBASE.dbo.FeeState.RegsNo and NIU_DBASE.dbo.FeeState.FeeStatus = " & DDL_FeeStatus.SelectedValue
            SQL_R += " where class like '" & str & "%' "
            SQL_R += " order by StdID.class,StdID.regsno"
            Label1.Text = SQL_R
        End If

        SqlDataSource1.SelectCommand = SQL_R

    End Sub

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

    Private Sub DDL_FeeStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_FeeStatus.SelectedIndexChanged

        If DDL_FeeStatus.SelectedValue <> "0" Then
            
            Rd_Data(str, 2)

        End If
    End Sub

#Region "匯出Excel相關名稱設定"

    Protected Sub But_Export_Click(sender As Object, e As EventArgs) Handles But_Export.Click
        '匯出的Excel檔名設定
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


End Class