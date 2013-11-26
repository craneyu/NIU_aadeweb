Public Class DBstringClass

    Public Shared Function DBString(ByVal str As String, ByVal Lyear As String, ByVal Lterm As String) As String
        Dim constring As String = Nothing

        Select Case str
            Case "Acade"
                constring = "Provider=SQLOLEDB;Data Source=120.101.1.61;Persist Security Info=True;Password=vu4wj/3yj3;User ID=syscc;Initial Catalog=IltAcade" & Lyear & Lterm
            Case "NIU"
                constring = "Provider=SQLOLEDB;Data Source=120.101.1.61;Persist Security Info=True;Password=vu4wj/3yj3;User ID=syscc;Initial Catalog=NIU_DBASE"
        End Select

        Return constring

    End Function

End Class
