Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class Grd_Door
    Inherits System.Web.UI.Page
    Dim SQL_R As String
    Dim Acade_DB As String
    Public Shared Sele_Regsno As String
    Public Shared mesg As New StringBuilder

    Private Sub Grd_Door_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim SQL As String
        Dim da As SqlDataAdapter
        Dim DT_C As New DataTable
        Acade_DB = DBName_init & Session("Y") & Session("M")

        If Session("Y") = "" Then
            Response.Redirect("Default.aspx")
        End If
        '===先找出學院====
        SQL = "select '0' as code, '..請選擇..' as disp from code union select CODE, DISP from CODE where KIND = 'SCH'"
        da = Connect2SQLDB(Acade_DB, SQL)
        da.Fill(DT_C)

        For Each dr In DT_C.Rows
            DDL_College.Items.Add(New ListItem(dr("DISP").ToString, dr("CODE").ToString))
        Next

        Label1.Text = ""

        SqlDataSource1.ConnectionString = DBstringClass.DBString("Acade", Session("Y"), Session("M"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Str As String = "B"

        If Session("CName") = "" Then
            Response.Redirect("Default.aspx")
        End If

        If IsPostBack Then
            Str += DDL_Dept.SelectedValue.ToString

            If DDL_Grade.SelectedValue <> 0 Then
                Str += DDL_Grade.SelectedValue
                If DDL_Class.SelectedValue <> 0 Then
                    Str += DDL_Class.SelectedValue
                End If
            End If
            SQL_R = "SELECT [REGSNO], [STDNAME], [Class] FROM [stdid] where class like "
            SQL_R += "'" & Str & "%' order by class,regsno"
            SqlDataSource1.SelectCommand = SQL_R
        Else
            RadGrid1.Visible = False
        End If
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
        SQL += "WHERE (DEPT.c = '4') AND (DEPT.SpecMark2 = 0) AND (CODE.KIND = 'subjid') AND DEPT.Type = " & DDL_College.SelectedValue
        da = Connect2SQLDB(Acade_DB, SQL)
        da.Fill(DT_D)

        For Each dr In DT_D.Rows
            DDL_Dept.Items.Add(New ListItem(Trim(dr("DISP")).ToString, Trim(dr("s_dept")).ToString))
        Next

    End Sub

    Private Sub DDL_Dept_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Dept.SelectedIndexChanged
        Clear_DDL("ALL")
        SQL_R = "SELECT [REGSNO], [STDNAME], [Class] FROM [stdid] where class like 'B" & DDL_Dept.SelectedValue & "%' order by class,regsno"
        SqlDataSource1.SelectCommand = SQL_R
        RadGrid1.Visible = True
    End Sub

    Private Sub DDL_Grade_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Grade.SelectedIndexChanged
        Clear_DDL("2")
        SQL_R = "SELECT [REGSNO], [STDNAME], [Class] FROM [stdid] where class like "
        SQL_R += "'B" & DDL_Dept.SelectedValue & DDL_Grade.SelectedValue & "%' order by class,regsno "
        SqlDataSource1.SelectCommand = SQL_R
    End Sub


    Private Sub DDL_Class_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Class.SelectedIndexChanged
        If DDL_Dept.SelectedValue <> "0" And DDL_Grade.SelectedValue <> "0" Then
            SQL_R = "SELECT [REGSNO], [STDNAME], [Class] FROM [stdid] where class like "
            SQL_R += "'B" & DDL_Dept.SelectedValue & DDL_Grade.SelectedValue & DDL_Class.SelectedValue & "' order by class,regsno "
            SqlDataSource1.SelectCommand = SQL_R
        Else
            DDL_Class.ClearSelection()
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "msg", "alert('請先選擇年級。')", True)
        End If

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

    Function ContextTemplete()
        Dim CT As New StringBuilder

        CT.Append("<div><div><p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'><!stdname>同學你好：</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>99學年度起入學學生除在課程學分數需達到各系要求學分數外，尚需通過三項畢業門檻(多元學習100小時、體適能、英語能力)。截至本學期為止在各項條件達成狀況如下：</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>&nbsp;</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>A.應修課程（主辦單位：教務處註冊組　承辦人：張淑華　聯絡電話：03-9317094）</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 5px; LINE-HEIGHT: 12pt; padding-left: 15px;'>應修未修或應修不及格之必修課程</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; padding-left: 15px; LINE-HEIGHT: 12pt; color:#F00;'><!Astr></p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt;  padding-left: 15px;'>")
        CT.Append("凡重補修課程名稱與原課程名稱不同，或跨系、跨學制重補修者，請<b>務必</b>至註冊組填寫「課程抵免單」進行抵免，以免損及權益。<b><br>")
        CT.Append("各系有特殊規定者從其規定，詳見<a href='http://www.niu.edu.tw/acade/curriculum/course/class_std.htm'>各系課程學分一覽表</a></b></p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>&nbsp;</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>B.多元學習100小時（主辦單位：學務處就業務務組　承辦人：陳千美　聯絡電話：03-9317178）</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 5px; LINE-HEIGHT: 12pt; padding-left: 15px;'>")
        'CT.Append("服務奉獻累計時數：<!B1><br/>多元成長累計時數：<!B2><br/>專業進取累計時數：<!B3><br/>彈性進取累計時數：<!B4>")
        CT.Append("<!B>")
        CT.Append("</p><p style='MARGIN: 0cm 0cm 5px; LINE-HEIGHT: 12pt; padding-left: 15px;'>&nbsp;</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>C.體適能（主辦單位：體育室　承辦人：曾秋美　聯絡電話：03-9317191）</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 5px; LINE-HEIGHT: 12pt; padding-left: 15px;'><!CR></p></p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>D.英語能力（主辦單位：通識委員會語言中心　承辦人：曾秋美　聯絡電話：03-9317191）</p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 5px; LINE-HEIGHT: 12pt; padding-left: 15px;'><!DR></p>")
        CT.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 24pt;'>&nbsp;</p><p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>")
        CT.Append("各門檻詳細的資訊可在「<a href='https://osa.niu.edu.tw/ep/login.php' target='_blank'>學生學習歷程系統</a>」內查詢得知或洽詢各主辦單位。</p>")
        CT.Append("</div></div>")
        Return CT

    End Function

    Private Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand

        If e.CommandName = "Select" Then
            Sele_Regsno = CType(e.Item.FindControl("RegsnoLab"), Label).Text
            Label1.Text = RD_Data(Sele_Regsno)
            'mesg = RD_Data(Sele_Regsno)
            But_SendSingle.Visible = True
        End If

    End Sub

    Function RD_Data(ByVal sid As String)
        Dim sql_A, sql_B, sql_C, sql_D As String
        Dim C As New StringBuilder
        Dim da As SqlDataAdapter
        Dim DTA, DTB, DTC, DTD As New DataTable
        'Dim i As Integer
        Dim BString As String = Nothing
        Dim BString1 As String = Nothing

        sql_A = "select a.regsno as regsno,a.stdname ,b.A,b.A1,b.A2,b.A3,b.A4, b.astr as astr from std..stdid a inner join std..stddata c "
        sql_A += "on a.regsno = c.regsno left join stdscor..stdscors b on  a.regsno = b.regsno "
        sql_A += "where a.regsno = '" & sid & "'"

        da = Connect2SQLDB(Acade_DB, sql_A)
        da.Fill(DTA)

        For Each dr In DTA.Rows
            C = ContextTemplete()

            C.Replace("<!stdname>", dr("STDNAME").ToString & "(" & dr("REGSNO").ToString & ")")      '學生姓名
            C.Replace("<!Astr>", dr("Astr").ToString)      '學生姓名

            sql_B = "exec [dbo].[GetStdCAHR] @regsno=N'" & sid & "'"
            da = Connect2SQLDB("activity", sql_B)
            da.Fill(DTB)
            For Each drB In DTB.Rows

                If CInt(drB("tcahr").ToString) = 0 Then
                    BString1 = "不列入門檻時數"
                Else
                    BString1 = "畢業門檻時數：" & drB("tcahr").ToString
                End If
                If BString = "" Then
                    BString = drB("ability").ToString & "累計時數：" & drB("cahr").ToString & "　　" & BString1 & "<br>"
                Else
                    BString += drB("ability").ToString & "累計時數：" & drB("cahr").ToString & "　　" & BString1 & "<br>"
                End If
            Next
            C.Replace("<!B>", BString)

            sql_C = "SELECT DISTINCT TOP 1 Sta.[statusName],Sil.[Syear] ,Sil.[Seme] "
            sql_C += "FROM [FitnessGradSill] as Sil ,[FitnessStatus] as Sta "
            sql_C += "WHERE (Sta.[StatusID] = Sil.[StatusID]) AND Sil.[Regsno]=N'" & dr("REGSNO").ToString & "' ORDER BY Sil.[Syear] DESC,Sil.[Seme] DESC"
            da = Connect2SQLDB("SportCenter", sql_C)
            da.Fill(DTC)
            If DTC.Rows.Count <> 0 Then
                C.Replace("<!CR>", DTC.Rows(0).Item(0).ToString)
            Else
                C.Replace("<!CR>", "尚未檢測")
            End If

            sql_D = "SELECT DISTINCT TOP 1 Sil.[RegsNo] ,Sil.[Syear] ,Sil.[Seme] ,Sil.[StatusID] ,Sta.[statusName] ,Sta.[isPass]"
            sql_D += ",Sil.[memo] ,Sil.[SignIn], Sil.[UpdateDT] FROM [SportCenter].[dbo].[LanguageGradSill] as Sil ,[SportCenter].[dbo].[LanguageStatus] as Sta "
            sql_D += "WHERE (Sta.[StatusID] = Sil.[StatusID]) AND Sil.[Regsno]=N'" & dr("REGSNO").ToString & "' ORDER BY Sil.[UpdateDT] desc ,Sil.[Syear] DESC,Sil.[Seme] DESC"
            da = Connect2SQLDB("SportCenter", sql_D)
            da.Fill(DTD)
            If DTD.Rows.Count <> 0 Then
                C.Replace("<!DR>", DTD.Rows(0).Item(4).ToString)
            Else
                C.Replace("<!DR>", "尚未檢測")
            End If

        Next

        Return C.ToString


    End Function

    Protected Sub But_SendSingle_Click(sender As Object, e As EventArgs) Handles But_SendSingle.Click
        SendMail("畢業門檻提醒通知", Sele_Regsno, Label1.Text)

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "訊息", "alert('學生：" & Sele_Regsno & "'，畢業門檻通知已寄出。)", True)
    End Sub

    Protected Sub But_Search_Click(sender As Object, e As EventArgs) Handles But_Search.Click

        SQL_R = "SELECT [REGSNO], [STDNAME], [Class] FROM [stdid] where regsno = '" & Txt_Regsno.Text & "' "
        SqlDataSource1.SelectCommand = SQL_R

        RadGrid1.Visible = True
        Txt_Regsno.Text = ""

    End Sub
End Class