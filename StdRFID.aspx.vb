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

Public Class StdRFID
    Inherits System.Web.UI.Page
    Public Shared table As New DataTable

    Private Sub StdRFID_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Lab_LastUpdate.Text = ""
        Label1.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Y") = "" Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('登入逾時，請重新登入!');window.location='Default.aspx';", True)
            'Response.Redirect("Default.aspx")
        End If

        Panel1.Visible = False


    End Sub

    Protected Sub But_Serach_Click(sender As Object, e As EventArgs) Handles But_Serach.Click
        Dim SQL As String
        Dim DA As SqlDataAdapter
        Dim DT As New DataTable

        If Len(Txt_StdNo.Text) <> 0 Then

            SQL = "SELECT StdData.RegsNo, StdRFIDKey.RFID_incode, StdRFIDKey.RFID_outcode, StdRFIDKey.UPDT, StdData.Name, "
            SQL += "StdData.Class, DEPT.classname FROM StdData INNER JOIN DEPT ON StdData.Class = DEPT.class FULL OUTER JOIN "
            SQL += "StdRFIDKey ON StdData.RegsNo = StdRFIDKey.Regsno "
            SQL += "WHERE StdData.RegsNo = '" & UCase(Trim(Txt_StdNo.Text)) & "'"

            DA = Connect2SQLDB("Std", SQL)
            DA.Fill(DT)

            If DT.Rows.Count <> 0 Then
                Lab_name.Text = DT.Rows(0).Item("Name").ToString
                Lab_StdNo.Text = DT.Rows(0).Item("RegsNo").ToString
                Lab_Classname.Text = DT.Rows(0).Item("classname").ToString
                Lab_Class.Text = DT.Rows(0).Item("Class").ToString
                Txt_RFID_incode.Text = DT.Rows(0).Item("RFID_incode").ToString
                Txt_RFID_outcode.Text = DT.Rows(0).Item("RFID_outcode").ToString
                Lab_LastUpdate.Text = DT.Rows(0).Item("UPDT").ToString
            Else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('查詢不到該學生!!')", True)
                Panel1.Visible = False
            End If

            Panel1.Visible = True
            Txt_StdNo.Text = ""
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請輸入欲查詢的學生證號!')", True)
            Panel1.Visible = False

        End If

    End Sub

    Protected Sub But_Modify_Click(sender As Object, e As EventArgs) Handles But_Modify.Click
        Dim SQL As String

        SQL = "Update StdRFIDKey set RFID_incode = '" & Trim(Txt_RFID_incode.Text) & "', RFID_outcode = '" & Trim(Txt_RFID_outcode.Text) & "', UPDT = getdate() "
        SQL += "where RegsNo = '" & Lab_StdNo.Text & "'"

        executeSqlQuery("Std", SQL)
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('完成修改!')", True)

    End Sub

    Protected Sub But_Preview_Click(sender As Object, e As EventArgs) Handles But_Preview.Click
        '預覽上傳的Excel檔內容
        Dim FileName As String = Nothing
        Dim FT As Array


        For Each file As UploadedFile In RadUpload1.UploadedFiles 'RadUpload1.UploadedFiles
            If RadUpload1.UploadedFiles IsNot Nothing Then
                FileName = file.FileName.Substring(file.FileName.LastIndexOf("\") + 1)
                FT = FileName.Split(".")

                If FT(1) = "xls" Or FT(1) = "xlsx" Then
                    table = DataTableRenderToExcel.RenderDataTableFromExcel(file.InputStream, 0, 0)

                    If check_tablecolumns() = True Then
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請確實設定Excel檔內的欄位名稱')", True)
                        Exit Sub
                    End If

                    RGD_preview.DataSource = table
                    RGD_preview.DataBind()

                    RGD_preview.Visible = True
                    Import_RFID.Enabled = True
                Else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請使用 Excel 檔案')", True)
                End If

            End If
        Next



    End Sub

    Function check_tablecolumns()
        '檢查上傳的Excel內是否含有的欄位名稱
        Dim Result As Boolean

        If table.Columns.Contains("RegsNo") = False Then
            Result = True
        ElseIf table.Columns.Contains("incode") = False Then
            Result = True
        ElseIf table.Columns.Contains("outcode") = False Then
            Result = True
        Else
            Result = False
        End If

        Return Result

    End Function

    Protected Sub But_Export_Click(sender As Object, e As EventArgs) Handles Import_RFID.Click
        '匯入學生RFID資料
        Dim SQL1, SQLStr As String
        Dim da As SqlDataAdapter
        Dim RDT As New DataTable

        If check_tablecolumns() = True Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('請確實設定Excel檔內的欄位名稱')", True)
            Exit Sub
        End If

        For Each dr In table.Rows

            '先查詢學生是否已經有RFID資料
            SQLStr = "Select RegsNo from StdRFIDKey where RegsNo = '" & Replace((dr("Regsno").ToString), vbTab, "") & "'"
            da = Connect2SQLDB("Std", SQLStr)
            da.Fill(RDT)

            If RDT.Rows.Count = 0 Then
                Try
                    SQL1 = "Insert into StdRFIDKey (Regsno, RFID_incode, RFID_outcode, UPDT) values ('" & Replace((dr("Regsno").ToString), vbTab, "") & "'," _
                         & "'" & Replace(Trim(dr("incode").ToString), vbTab, "") & "', '" & Replace(Trim(dr("outcode").ToString), vbTab, "") & "', getdate())"

                    executeSqlQuery("std", SQL1)

                Catch ex As Exception
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "msg", "alert('" & ex.Message & "')", True)
                    Exit Sub
                End Try
            Else

                SQL1 = "Update StdRFIDKey set RFID_incode = '" & Replace(Trim(dr("incode").ToString), vbTab, "") & "'," _
                     & "RFID_outcode = '" & Replace(Trim(dr("outcode").ToString), vbTab, "") & "', UPDT = getdate() " _
                     & "where RegsNo = '" & Replace((dr("Regsno").ToString), vbTab, "") & "'"

                executeSqlQuery("std", SQL1)

            End If
        Next

        RGD_preview.Visible = False
        RGD_preview.Dispose()
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "msg", "alert('完成匯入。')", True)

    End Sub

End Class