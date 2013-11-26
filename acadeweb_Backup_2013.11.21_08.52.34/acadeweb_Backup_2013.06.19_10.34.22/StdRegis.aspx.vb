Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Threading
Imports System.Data.OleDb
Imports System.Data.Common
Imports System.IO

Public Class StdRegis
    Inherits System.Web.UI.Page
    Dim FeeStatus As String = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Y") = "" Then
            Response.Redirect("Default.aspx")
        End If
        If Session("Unit") = "5" Then   '出納組
            Lab_importTxt.Text = "匯入已繳費學生資料"
            FeeStatus = "1"  '已繳費
        ElseIf Session("Unit") = "4" Then  '生輔組
            Lab_importTxt.Text = "匯入已辦理就貸學生資料"
            FeeStatus = "2"  '已辦就貸

        End If

        Acade_DB = DBName_init & Session("Y") & Session("M")

        If Not IsPostBack Then
            RD_Data(0)  '預設初始,依承辦單位,全部呈現
        Else
            RD_Data(1)  '依學號搜尋
        End If

        Label1.Visible = False

    End Sub

    Sub RD_Data(ByVal T As Integer)
        Dim SQL As String
        Dim Da As SqlDataAdapter
        Dim Dt As New DataTable
        If T = 0 Then
            SQL = "SELECT niu_dbase.dbo.FeeState.RegsNo, stdid.STDNAME, stdid.Class, DEPT.classname, "
            SQL += "stdid.IDNO, case niu_dbase.dbo.FeeState.FeeStatus when '1' then '已繳費' when '2' then '已辦理貸款' end as FeeStatus FROM niu_dbase.dbo.FeeState INNER JOIN stdid ON "
            SQL += "niu_dbase.dbo.FeeState.RegsNo = stdid.REGSNO INNER JOIN DEPT ON "
            SQL += "stdid.Class =DEPT.class where niu_dbase.dbo.FeeState.Feestatus = '" & FeeStatus & "'"
        Else
            SQL = "SELECT niu_dbase.dbo.FeeState.RegsNo, stdid.STDNAME, stdid.Class, DEPT.classname, "
            SQL += "stdid.IDNO, case niu_dbase.dbo.FeeState.FeeStatus when '1' then '已繳費' when '2' then '已辦理貸款' end as FeeStatus FROM niu_dbase.dbo.FeeState INNER JOIN stdid ON "
            SQL += "niu_dbase.dbo.FeeState.RegsNo = stdid.REGSNO INNER JOIN DEPT ON "
            SQL += "stdid.Class =DEPT.class where niu_dbase.dbo.FeeState.Feestatus = '" & FeeStatus & "'"
            SQL += "and stdid.regsno like '%" & Trim(UCase(Search_text.Text)) & "%'"

        End If

        Da = Connect2SQLDB(Acade_DB, SQL)
        Da.Fill(Dt)

        RadGrid1.DataSource = Dt.Rows
        RadGrid1.DataBind()

    End Sub
    Protected Sub Import_StdData_Click(sender As Object, e As EventArgs) Handles Import_StdData.Click
        '匯入學生已繳費或已辦理就貸資料
        Dim SQL2 As String
        Dim FileName As String = Nothing
        Dim ExcelConnectionString As String = Nothing
        Dim ExtFile As Array = Nothing

        For Each file As UploadedFile In RadUpload1.UploadedFiles 'RadUpload1.UploadedFiles
            If RadUpload1.UploadedFiles IsNot Nothing Then
                FileName = file.FileName.Substring(file.FileName.LastIndexOf("\") + 1)
                ExtFile = FileName.Split(".")
            End If
        Next


        '====先清除現有資料庫內的所有資料====
        'SQL = "select count(*) as Total where FeeStatus = '" & FeeStatus & "'"
        'cnt_da = Connect2SQLDB("NIU_DBASE", SQL)
        'cnt_da.Fill(cnt_dt)
        'If cnt_dt.Rows(0).Item(0) > 0 Then

        'End If
        'SQL1 = "Delete From Feestate where FeeStatus ='" & FeeStatus & "'"
        'executeSqlQuery("NIU_DBASE", SQL1)

        '==============讀取Excel後匯入學生註冊資料表Feestate===============
        Dim ExcelPath As String = "~/Upload/" & FileName
        If ExtFile(1).ToString = "xls" Then
            ExcelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server.MapPath(ExcelPath) & ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'"
        ElseIf ExtFile(1).ToString = "xlsx" Then
            ExcelConnectionString = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" & Server.MapPath(ExcelPath) & ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'"
        End If
        'Label1.Text = ExcelConnectionString
        'Exit Sub

        Dim Cn As New OleDbConnection(ExcelConnectionString)
        Dim ExcelSQLCmd As OleDbCommand = New OleDbCommand("Select RegsNo from [sheet1$]", Cn)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter
        Dim dt As New DataTable

        da.SelectCommand = ExcelSQLCmd
        da.Fill(dt)

        For Each dr In dt.Rows
            Try
                SQL2 = "insert into FeeState (RegsNo, Lyear, Lterm, FeeStatus) values ('" & dr("RegsNo").ToString & "', '" & Session("Y") & "', '" & Session("M") & "','" & FeeStatus & "')"
                executeSqlQuery("NIU_DBASE", SQL2)

            Catch ex As Exception

            End Try
        Next

        '=======刪除上傳匯入後的檔案==========
        Try
            Dim targetdir As New DirectoryInfo(Server.MapPath(ExcelPath))
            For Each file1 As FileInfo In targetdir.GetFiles()
                If (file1.Attributes And FileAttributes.[ReadOnly]) = 0 Then
                    file1.Delete()
                End If
            Next

        Catch generatedExceptionName As IOException
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "msg", "alert('" & generatedExceptionName.Message & "')", True)
        End Try

        RD_Data(0)

    End Sub

    Protected Sub But_Search_Click(sender As Object, e As EventArgs) Handles But_Search.Click
        RD_Data(1)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim SQL As String
        SQL = "Delete From Feestate where FeeStatus ='" & FeeStatus & "'"
        executeSqlQuery("NIU_DBASE", SQL)
        RD_Data(0)
    End Sub
End Class