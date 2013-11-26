<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Registry_menu.ascx.vb" Inherits="acadeweb.Registry_menu" %>

<telerik:RadToolBar ID="RadToolBar1" Runat="server" Skin="Windows7">
    <Items>
        <telerik:RadToolBarButton>
            <ItemTemplate>
              <div style="font:bold; padding: 0 10px ;">註冊組</div>
            </ItemTemplate>
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton runat="server" IsSeparator="True" Text="Button 1">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarDropDown runat="server" Text="成績通知">
            <Buttons>
                <telerik:RadToolBarButton runat="server" Text="大學部-必修學分未修或不及格通知" NavigateUrl="~/astr_mail.aspx">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton runat="server" Text="畢業門檻通知" NavigateUrl="~/Grd_Door.aspx">
                </telerik:RadToolBarButton>
            </Buttons>
        </telerik:RadToolBarDropDown>
        <telerik:RadToolBarButton runat="server" IsSeparator="True" Text="Button 3">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton runat="server" NavigateUrl="Logout.aspx" Text="登出系統">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>


