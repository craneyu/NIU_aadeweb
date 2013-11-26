Imports System.Data.SqlClient

Public Class astr_mail
    Inherits System.Web.UI.Page
    Dim Da As SqlDataAdapter
    Dim dt As New DataTable
    Dim Y As String

    Private Sub astr_mail_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Label1.Visible = False : Label1.Text = ""
        Txt_Y.Text = 4
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Read_initDB(Txt_Y.Text)

    End Sub

    Sub Read_initDB(ByVal YY As String)
        Dim Sql As String

        Sql = "select a.regsno as regsno,a.stdname as 姓名,b.A,b.A1,b.A2,b.A3,b.A4, b.astr as 未通過必修科目 from std..stdid a inner join std..stddata c "
        Sql += "on a.regsno = c.regsno left join stdscor..stdscors b on  a.regsno = b.regsno "
        Sql += "where substring(a.class,4,1) in (" & YY & ") and a.regsno like '[B,C]%' and isnull(b.astr,'') <> ''"
        'Sql += "and a.regsno ='B0111003'"

        SqlDataSource1.SelectCommand = Sql
    End Sub

    Sub Read_All(ByVal YY As String)
        Dim sqlstr As String

        sqlstr = "SELECT top 3 a.Class, a.REGSNO , a.STDNAME , b.A, b.A1, b.A2, b.A3, b.A4, b.Astr, "
        sqlstr += "StdScor.dbo.DeptScors.CReqr, StdScor.dbo.DeptScors.PReqr, "
        sqlstr += "StdScor.dbo.DeptScors.PSele, StdScor.dbo.DeptScors.CSele, StdScor.dbo.DeptScors.GradTotal "
        sqlstr += "FROM StdScor.dbo.StdScors AS b INNER JOIN "
        sqlstr += "StdScor.dbo.DeptScors ON b.DeptYear = StdScor.dbo.DeptScors.DeptYear RIGHT OUTER JOIN "
        sqlstr += "std.dbo.StdID AS a INNER JOIN std.dbo.StdData AS c ON a.REGSNO = c.RegsNo ON b.RegsNo = a.REGSNO "
        sqlstr += "WHERE (a.REGSNO LIKE '[B,C]%') AND (SUBSTRING(a.Class, 4, 1) IN (" & YY & ")) AND (ISNULL(b.Astr, '') <> '')"
        'sqlstr += "and a.regsno ='B0111003')"

        Da = Connect2SQLDB(DBName, sqlstr)
        Da.Fill(dt)

    End Sub


    Function mail_Templete()  '2013-03-22新範本
        Dim Mail_Context As New StringBuilder

        Mail_Context.Append("<div style='font-size:12px'><div'>")
        Mail_Context.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt;'>")
        Mail_Context.Append("<span style='color:#00F;'><b><!stdname></b></span> 同學你好：</p>")
        Mail_Context.Append("<p style='MARGIN: 0cm 0cm 1pt; background-color: #FFC; color:#00F;'>最低畢業學分數：<!GradTotal>；已修(含抵免)學分數：<!A>；</p>")
        Mail_Context.Append("<p style='MARGIN: 0cm 0cm 1pt; background-color: #FFC; color:#0C0;'><!R1></p>")
        Mail_Context.Append("<p style='MARGIN: 0cm 0cm 1pt; background-color: #FFC; color:#F00;'><!R2></p>")
        Mail_Context.Append("<p style='MARGIN: 0cm 0cm 1pt; LINE-HEIGHT: 12pt'>")
        Mail_Context.Append("截至本學期為止，你應修未修或應修不及格之必修課程如下：</p>")
        Mail_Context.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt; background-color:#CCF; color:#F00'><!Astr></p><br/>")
        Mail_Context.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt'>")
        Mail_Context.Append("凡重補修課程名稱與原課程名稱不同，或跨系、跨學制重補修者，請<b>務必</b>至註冊組填寫「課程抵免單」進行抵免，以免損及權益。若有疑問，請至教務處註冊組查詢。</p>")
        Mail_Context.Append("<p style='MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 12pt'><font color='#800000'><strong>")
        Mail_Context.Append("*各系有特殊規定者從其規定，詳見</strong></font><a href='http://www.niu.edu.tw/acade/curriculum/course/class_std.htm'><font color='#800000'><strong>各系課程學分一覽表</strong></font></a></p>")
        Mail_Context.Append("<div align='right'>教務處註冊組啟</div></div></div>")
        Return Mail_Context

    End Function

    Protected Sub But_SendMail_Click(sender As Object, e As EventArgs) Handles But_SendMail.Click
        Dim context As New StringBuilder
        Dim stdyear, sc, Reqr, Sele, i As Integer
        Dim SQLG, SQL_FL As String
        Dim daG, Da_FL As SqlDataAdapter
        Dim dtG, DT_FL As New DataTable
        Dim R1 As String = ""
        Dim R2 As String = ""
        Dim tmp As String = ""

        '呼叫SQL,連畢業標準學分及已修學分都列出
        Read_All(Txt_Y.Text)
        Label1.Visible = True


        For Each dr In dt.Rows
            context = mail_Templete()
            context.Replace("<!stdname>", dr("STDNAME").ToString & "(" & dr("REGSNO").ToString & ")")      '學生姓名
            context.Replace("<!GradTotal>", dr("GradTotal").ToString)  '最低畢業學分數
            context.Replace("<!A>", dr("A").ToString)                  '已修(含抵免)學分數
            Reqr = CInt(dr("CReqr").ToString) + CInt(dr("PReqr").ToString) '應修必修學分數
            Sele = CInt(dr("CSele").ToString) + CInt(dr("PSele").ToString) '應修選修學分數

            If Len(Trim(dr("Regsno").ToString)) = 8 Then
                stdyear = Mid(dr("regsno").ToString, 2, 2)
                If stdyear >= 0 And stdyear <= 80 Then
                    If Mid(dr("Regsno").ToString, 6, 1) > 0 Then
                        '轉學生學號第6位為3的入學年-2  ,2的入學年-1
                        stdyear = stdyear + 100 - Mid(dr("Regsno").ToString, 6, 1)
                        sc = 8
                    Else
                        stdyear += 100
                        sc = 9   '通識畢業學分規定為9學分

                    End If
                Else
                    '99學年度前的通識畢業學分規定為8學分
                    sc = 8
                End If


            End If
            'dtG.Reset()
            dtG.Clear()
            '找出學生的學群分組
            SQLG = "select deptyear,class from stddata where regsno='" & dr("REGSNO").ToString & "'"
            daG = Connect2SQLDB(DBName, SQLG)
            daG.Fill(dtG)
            Dim GP As String

            DT_FL.Clear()

            If dtG.Rows.Count > 0 Then
                If dtG.Rows(0).Item(0).ToString <> "" Then
                    GP = Mid(dtG.Rows(0).Item(0).ToString, 2, 2)
                Else
                    GP = Mid(dtG.Rows(0).Item(1).ToString, 2, 2)
                End If

                Select Case GP
                    Case "E1", "E2", "E3"  '電資學院三學群
                        R1 = "【應修學分數】必修：" & Reqr & "(含通識課程：" & sc & ")；至少選修：" & Sele & "(含2個選修學程)"
                        R2 = "【已修學分數】必修：" & dr("A1").ToString & "(含通識課程：" & dr("A2").ToString & ")；　　選修：" & dr("A3").ToString
                    Case "FL"  '外語系
                        R1 = "【應修學分數】必修：" & Reqr & "(含通識課程：" & sc & ")；至少選修：" & Sele & "(含專業選修至少：" & dr("PSele").ToString & "，"
                        R2 = "【已修學分數(FL)】必修：" & dr("A1").ToString & "(含通識課程：" & dr("A2").ToString & ")；　　選修：" & dr("A3").ToString & "(含專業選修：" & dr("A4").ToString & "，"

                        SQL_FL = "select CatName, RScor, scor from StdCatScors INNER JOIN  CatName ON StdCatScors.Cat = CatName.Cat where regsno ='" & dr("regsno").ToString & "'"
                        Da_FL = Connect2SQLDB("StdScor", SQL_FL)
                        Da_FL.Fill(DT_FL)

                        tmp = "" : i = 0
                        For Each dr1 In DT_FL.Rows
                            If i > 0 Then tmp = "，"
                            R1 = R1 & tmp & dr1("CatName").ToString & "：" & dr1("Rscor").ToString
                            R2 = R2 & tmp & dr1("CatName").ToString & "：" & dr1("scor").ToString
                            i += 1
                        Next

                        R1 = R1 & ")"
                        R2 = R2 & ")"

                    Case Else
                        R1 = "【應修學分數】必修：" & Reqr & "(含通識課程：" & sc & ")；至少選修：" & Sele & "(含專業選修至少：" & dr("PSele").ToString & ")"
                        R2 = "【已修學分數】必修：" & dr("A1").ToString & "(含通識課程：" & dr("A2").ToString & ")；　　選修：" & dr("A3").ToString & "(含專業選修：" & dr("A4").ToString & ")"

                End Select

                context.Replace("<!R1>", R1)   '含專業選修
                context.Replace("<!R2>", R2)    '已修專業選修

            End If

            context.Replace("<!sc>", sc)  '通識課程必修學分數
            context.Replace("<!A1>", dr("A1").ToString)    '已修必修
            context.Replace("<!A2>", dr("A2").ToString)    '已修通識
            context.Replace("<!A3>", dr("A3").ToString)    '已修選修
            context.Replace("<!Astr>", dr("Astr").ToString)

            '寄信--一個個寄
            'SendMail("課程抵免提醒通知", dr("regsno").ToString, context)

            '紀錄寄信結果與內容
            Try
                Dim R_SQL As String
                R_SQL = "insert into AstrMail_SendLog (regsno, stdname, Context, SendDateTime) values "
                R_SQL += "('" & dr("regsno").ToString & "',N'" & dr("STDNAME").ToString & "',N'" & context.ToString.Replace("'", """") & "',getdate())"
                executeSqlQuery("NIU_DBASE", R_SQL)

            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "msg", "alert('" & ex.Message & "')", True)
            End Try

            'If Label1.Text <> "" Then
            '    Label1.Text = Label1.Text & "<br/>" & context.ToString
            'Else
            '    Label1.Text = context.ToString
            'End If



        Next

    End Sub

    Protected Sub But_Search_Click(sender As Object, e As EventArgs) Handles But_Search.Click

        Read_initDB(Txt_Y.Text)
    End Sub

    Function Get_Result(ByVal regsno As String)
        Dim R As String = ""
        Dim SQL As String
        Dim dat As SqlDataAdapter
        Dim dtt As New DataTable

        Try
            SQL = "select top 1 sendDateTime from AstrMail_SendLog where regsno='" & regsno & "' order by sendDateTime desc"
            dat = Connect2SQLDB("NIU_DBASE", SQL)
            dat.Fill(dtt)

            If dtt.Rows(0).Item(0).ToString <> "" Then
                R = dtt.Rows(0).Item(0).ToString
            Else
                R = ""
            End If

        Catch ex As Exception

        End Try

        Return R
    End Function
End Class