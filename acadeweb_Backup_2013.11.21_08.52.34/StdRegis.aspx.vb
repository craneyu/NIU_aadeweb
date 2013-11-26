Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Threading
Imports System.Data.OleDb
Imports System.Data.Common
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.POIFS
Imports NPOI.POIFS.FileSystem

Public Class StdRegis
    Inherits System.Web.UI.Page
    Dim FeeStatus As String = Nothing
    Public Shared table As New DataTable

    Private Sub StdRegis_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        SqlDataSource1.ConnectionString = DBstringClass.DBString("Acade", Session("Y"), Session("M"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Y") = "" Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('登入逾時，請重新登入!');window.location='Default.aspx';", True)
            'Response.Redirect("Default.aspx")
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
        RG2.Visible = False
        Label1.Visible = False

    End Sub

    Sub RD_Data(ByVal T As Integer)
        Dim SQL As String

        Dim Dt As New DataTable


        If T = 0 Then  '預設初始,依承辦單位,全部呈現
            SQL = "SELECT niu_dbase.dbo.FeeState.RegsNo, stdid.STDNAME, stdid.Class, DEPT.classname, "
            SQL += "stdid.IDNO as IDNO, case niu_dbase.dbo.FeeState.FeeStatus when '1' then '已繳費' when '2' then '已辦理貸款' end as FeeStatus FROM niu_dbase.dbo.FeeState LEFT OUTER JOIN stdid ON "
            SQL += "niu_dbase.dbo.FeeState.RegsNo = stdid.REGSNO LEFT OUTER JOIN DEPT ON stdid.Class =DEPT.class "
            SQL += "where niu_dbase.dbo.FeeState.Feestatus = '" & FeeStatus & "' "
            SQL += "and niu_dbase.dbo.FeeState.Lyear = " & Session("SeleY") & " and  niu_dbase.dbo.FeeState.Lterm = " & Session("SeleM")
        Else   '依學號搜尋
            SQL = "SELECT niu_dbase.dbo.FeeState.RegsNo, stdid.STDNAME, stdid.Class, DEPT.classname, "
            SQL += "stdid.IDNO as IDNO, case niu_dbase.dbo.FeeState.FeeStatus when '1' then '已繳費' when '2' then '已辦理貸款' end as FeeStatus FROM niu_dbase.dbo.FeeState LEFT OUTER JOIN stdid ON "
            SQL += "niu_dbase.dbo.FeeState.RegsNo = stdid.REGSNO LEFT OUTER JOIN DEPT ON "
            SQL += "stdid.Class =DEPT.class where niu_dbase.dbo.FeeState.Feestatus = '" & FeeStatus & "'"
            SQL += "and stdid.regsno like '%" & Trim(UCase(Search_text.Text)) & "%'"
            SQL += "and niu_dbase.dbo.FeeState.Lyear = " & Session("SeleY") & " and  niu_dbase.dbo.FeeState.Lterm = " & Session("SeleM")

        End If
        SqlDataSource1.SelectCommand = SQL
       
        RadGrid1.Visible = True
    End Sub
    Protected Sub Import_StdData_Click(sender As Object, e As EventArgs) Handles Import_StdData.Click
        '匯入學生已繳費或已辦理就貸資料
        Dim SQL1, SQL2 As String
        Dim FileName As String = Nothing
        Dim ExcelConnectionString As String = Nothing
        Dim ExtFile As Array = Nothing



        For Each dr In table.Rows
            Try
                SQL2 = "insert into FeeState (RegsNo, Lyear, Lterm, FeeStatus) values "
                SQL2 += "('" & dr("RegsNo").ToString & "', " & Session("SeleY") & ", " & Session("SeleM") & ",'" & FeeStatus & "')"
                executeSqlQuery("NIU_DBASE", SQL2)

                SQL1 = "insert into FeeState (RegsNo, FeeStatus) values ('" & dr("RegsNo").ToString & "','" & FeeStatus & "')"
                executeSqlQuery(Acade_DB, SQL1)

            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "msg", "alert('" & ex.Message & "')", True)
            End Try
        Next

        RD_Data(0)

    End Sub

    Protected Sub But_Search_Click(sender As Object, e As EventArgs) Handles But_Search.Click
        RD_Data(1)
    End Sub

    Protected Sub But_Add_Click(sender As Object, e As EventArgs) Handles But_Add.Click
        Dim SQL, SQL2 As String

        If Txt_AddRegsno.Text <> "" Then
            Try
                SQL = "insert into FeeState (RegsNo, Lyear, Lterm, FeeStatus) values "
                SQL += "('" & Trim(Txt_AddRegsno.Text) & "', " & Session("SeleY") & ", " & Session("SeleM") & ",'" & FeeStatus & "')"
                executeSqlQuery("NIU_DBASE", SQL)

                SQL2 = "insert into FeeState (RegsNo, FeeStatus) values ('" & Trim(Txt_AddRegsno.Text) & "','" & FeeStatus & "')"
                executeSqlQuery(Acade_DB, SQL2)

            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('" & ex.Message & "')", True)

            End Try

            RD_Data(0)

        End If
    End Sub

    Protected Sub But_DeleteALL_Click(sender As Object, e As EventArgs) Handles But_DeleteALL.Click
        Dim SQL, SQL2 As String

        '====清除現有資料庫內的所有資料====
        Try
            SQL = "Delete From Feestate where FeeStatus ='" & FeeStatus & "'"
            SQL += "and FeeState.Lyear = " & Session("SeleY") & " and Lterm = " & Session("SeleM") & ""

            executeSqlQuery("NIU_DBASE", SQL)

            SQL2 = "Delete From Feestate where FeeStatus ='" & FeeStatus & "'"
            executeSqlQuery(Acade_DB, SQL2)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('" & ex.Message & "')", True)
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '預覽上傳的Excel檔內容
        Dim FileName As String = Nothing
        Dim FT As Array
        ' Dim table As New DataTable
        RadGrid1.Visible = False

        For Each file As UploadedFile In RadUpload1.UploadedFiles 'RadUpload1.UploadedFiles
            If RadUpload1.UploadedFiles IsNot Nothing Then
                FileName = file.FileName.Substring(file.FileName.LastIndexOf("\") + 1)
                FT = FileName.Split(".")
                'Dim ExcelPath As String = "~/Upload/" & FileName
                If FT(1) = "xls" Or FT(1) = "xlsx" Then
                    table = DataTableRenderToExcel.RenderDataTableFromExcel(file.InputStream, 0, 0)

                Else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請使用 Excel 格式(xls,xlsx)存檔')", True)
                End If


                If check_tablecolumns() = True Then
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請確實將Excel檔內的學生證號欄位名稱修改為RegsNo。')", True)
                    Exit Sub
                End If

                RG2.DataSource = table
                RG2.DataBind()
                RG2.Visible = True
                Import_StdData.Enabled = True

            End If
        Next


    End Sub

    Function Hide_IdNo(ByVal IdNo As String) As String
        '將RadGridView內的身份證字號，隱藏中段數字後再呈現
        Dim idnomark As String

        idnomark = Left(IdNo, 2) & "*******" & Right(IdNo, 1)

        Return idnomark
    End Function

    Function check_tablecolumns()
        '檢查上傳的Excel內是否含有的欄位名稱
        Dim Result As Boolean

        '確認匯入的table欄位名稱'
        If table.Columns.Contains("RegsNo") = False Then

            Result = True
        Else
            Result = False
        End If

        Return Result

    End Function

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

    Private Sub RG2_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RG2.ItemCommand
        RG2.DataSource = table
    End Sub
End Class