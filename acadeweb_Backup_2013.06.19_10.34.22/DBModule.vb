Imports System.Data.SqlClient

Module DBModule
    Dim SQLSRV As String = "120.101.1.61"  '正式資料庫
    'Dim SQLSRV As String = "120.101.1.45"  '測試資料庫
    Public DBName_init As String = "Iltacade"
    Public SysDBName As String = "NIU_DBASE"
    Public Acade_DB As String
#Region "連結SQL資料庫"

    Function Connect(ByVal dbname As String, ByVal uid As String, ByVal psd As String)  '登入時連結資料庫
        Dim connStr As String = "Data Source=" & SQLSRV & ";Initial Catalog=" & dbname & ";User ID=syscc;Password=vu4wj/3yj3"
        Dim conn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(connStr)
        Try
            conn.Open()
        Catch ex As Exception
            MsgBox("連結開啟失敗，帳號或密碼錯誤", MsgBoxStyle.OkOnly, "訊息視窗")
        Finally
            conn.Close()
        End Try

        Return conn

    End Function

    Function Connect2SQLDB(ByVal DBName As String, ByVal SQL As String) As System.Data.SqlClient.SqlDataAdapter '輸入sql語法連結資料表
        Dim connStr As String = "Data Source=" & SQLSRV & ";Initial Catalog=" & DBName & ";User ID=syscc;Password=vu4wj/3yj3"
        Dim conn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(connStr)

        Dim DataAdapter As New SqlDataAdapter(SQL, conn)
        Connect2SQLDB = DataAdapter

        Try
            conn.Open()
        Catch ex As Exception
            MsgBox("連結開啟失敗", MsgBoxStyle.OkOnly, "訊息視窗")
        Finally
            conn.Close()
        End Try
    End Function

    Sub executeSqlQuery(ByVal DBName As String, ByVal SqlQuery As String)
        Dim connStr As String = "Data Source=" & SQLSRV & ";Initial Catalog=" & DBName & ";User ID=syscc;Password=vu4wj/3yj3"
        Dim conn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(connStr)

        Try
            conn.Open()
            Dim CMD As New System.Data.SqlClient.SqlCommand(SqlQuery, conn)

            CMD.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
 
#End Region

End Module
