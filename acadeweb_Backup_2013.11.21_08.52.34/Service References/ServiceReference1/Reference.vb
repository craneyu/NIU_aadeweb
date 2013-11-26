﻿'------------------------------------------------------------------------------
' <auto-generated>
'     這段程式碼是由工具產生的。
'     執行階段版本:4.0.30319.1008
'
'     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
'     變更將會遺失。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace ServiceReference1
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://niu.edu.tw/", ConfigurationName:="ServiceReference1.TeacServiceSoap")>  _
    Public Interface TeacServiceSoap
        
        'CODEGEN: 命名空間 http://niu.edu.tw/ 的項目名稱  username 未標示為 nillable，正在產生訊息合約
        <System.ServiceModel.OperationContractAttribute(Action:="http://niu.edu.tw/ValidateUser", ReplyAction:="*")>  _
        Function ValidateUser(ByVal request As ServiceReference1.ValidateUserRequest) As ServiceReference1.ValidateUserResponse
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ValidateUserRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ValidateUser", [Namespace]:="http://niu.edu.tw/", Order:=0)>  _
        Public Body As ServiceReference1.ValidateUserRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As ServiceReference1.ValidateUserRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://niu.edu.tw/")>  _
    Partial Public Class ValidateUserRequestBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public username As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=1)>  _
        Public password As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal username As String, ByVal password As String)
            MyBase.New
            Me.username = username
            Me.password = password
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ValidateUserResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ValidateUserResponse", [Namespace]:="http://niu.edu.tw/", Order:=0)>  _
        Public Body As ServiceReference1.ValidateUserResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As ServiceReference1.ValidateUserResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://niu.edu.tw/")>  _
    Partial Public Class ValidateUserResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(Order:=0)>  _
        Public ValidateUserResult As Boolean
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal ValidateUserResult As Boolean)
            MyBase.New
            Me.ValidateUserResult = ValidateUserResult
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface TeacServiceSoapChannel
        Inherits ServiceReference1.TeacServiceSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class TeacServiceSoapClient
        Inherits System.ServiceModel.ClientBase(Of ServiceReference1.TeacServiceSoap)
        Implements ServiceReference1.TeacServiceSoap
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function ServiceReference1_TeacServiceSoap_ValidateUser(ByVal request As ServiceReference1.ValidateUserRequest) As ServiceReference1.ValidateUserResponse Implements ServiceReference1.TeacServiceSoap.ValidateUser
            Return MyBase.Channel.ValidateUser(request)
        End Function
        
        Public Function ValidateUser(ByVal username As String, ByVal password As String) As Boolean
            Dim inValue As ServiceReference1.ValidateUserRequest = New ServiceReference1.ValidateUserRequest()
            inValue.Body = New ServiceReference1.ValidateUserRequestBody()
            inValue.Body.username = username
            inValue.Body.password = password
            Dim retVal As ServiceReference1.ValidateUserResponse = CType(Me,ServiceReference1.TeacServiceSoap).ValidateUser(inValue)
            Return retVal.Body.ValidateUserResult
        End Function
    End Class
End Namespace
