Imports System.Net.Mail
Imports System.Threading

Module MailModule

    Sub SendMail(ByVal MailSubject As String, ByVal MailAddr As String, ByVal content As String)
        Dim mailclient As System.Net.Mail.SmtpClient
        Dim mailmsg As System.Net.Mail.MailMessage
        Dim FromEmail As String = "registry@niu.edu.tw"
        Dim mailtoHistory As String = Nothing
        Dim SendToEmail As String = MailAddr & "@ms.niu.edu.tw"
        Dim BccEmail As String = "shuhua@niu.edu.tw,ccwu@niu.edu.tw"
        Try
            mailclient = New SmtpClient("niu.edu.tw", 25)

            mailmsg = New MailMessage
            mailmsg.From = New MailAddress(FromEmail, "國立宜蘭大學-教務處")

            mailmsg.To.Add(SendToEmail)
            'mailmsg.Bcc.Add("cwhsu@mail.ntsu.edu.tw")
            'mailmsg.CC.Add(FromEmail)
            'mailmsg.Bcc.Add(Mailto.ToString)
            mailmsg.Subject = MailSubject   '設定郵件主旨
            mailmsg.Body = content.ToString     '郵件內容
            mailmsg.BodyEncoding = System.Text.Encoding.UTF8  '設定郵件編碼格式
            mailmsg.IsBodyHtml = True                         '設定郵件內容是否為HTML格式


            mailclient.Send(mailmsg)  '寄出信件


        Catch ex As Exception

        End Try

    End Sub
End Module
