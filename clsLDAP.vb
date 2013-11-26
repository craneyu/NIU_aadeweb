Module clsLDAP

    Function StaffCheckAccount(ByVal strAccount As String, ByVal strPWD As String) As Boolean
        Try
            Dim LdapServer As String = "LDAP://ldap.niu.edu.tw/ou=teacher,dc=ldap,dc=niu,dc=edu,dc=tw" 'IP Address 
            'Dim strTmp0 As String = LdapServer
            Dim StrUID As String = "uid=" & strAccount & ",ou=teacher,dc=ldap,dc=niu,dc=edu,dc=tw"

            Dim LdapEntry As DirectoryServices.DirectoryEntry = New DirectoryServices.DirectoryEntry(LdapServer, StrUID, strPWD, DirectoryServices.AuthenticationTypes.None)
            Dim LdapSearcher As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher(LdapEntry)
            Dim LdapResult As DirectoryServices.SearchResult = LdapSearcher.FindOne

            If Not LdapResult Is Nothing Then
                LdapSearcher.Dispose()
                LdapEntry.Dispose()
                Return True
            End If

            LdapSearcher.Dispose()
            LdapEntry.Dispose()
            Return False

        Catch ex As Exception
            Return False
        End Try

    End Function

    Function StdCheckAccount(ByVal strAccount As String, ByVal strPWD As String) As Boolean
        Try
            Dim LdapServer As String = "LDAP://ldap.niu.edu.tw" 'IP Address 
            Dim strTmp0 As String = LdapServer


            Dim LdapEntry As DirectoryServices.DirectoryEntry = New DirectoryServices.DirectoryEntry(LdapServer, strAccount, strPWD, DirectoryServices.AuthenticationTypes.Secure)

            Dim LdapSearcher As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher(LdapEntry)
            Dim LdapResult As DirectoryServices.SearchResult = LdapSearcher.FindOne

            If Not LdapResult Is Nothing Then
                LdapSearcher.Dispose()
                LdapEntry.Dispose()
                Return True
            End If

            LdapSearcher.Dispose()
            LdapEntry.Dispose()
            Return False

        Catch ex As Exception
            Return False
        End Try

    End Function

End Module
