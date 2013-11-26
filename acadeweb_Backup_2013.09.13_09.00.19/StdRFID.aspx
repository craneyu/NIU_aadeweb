<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StdRFID.aspx.vb" Inherits="acadeweb.StdRFID" %>
<%@ Register src="Registry_menu.ascx" tagname="Registry_menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .R
        {
            border: 1px solid #808000;
            margin: 5px;
            width: 600px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc1:Registry_menu ID="Registry_menu1" runat="server" />
        <div class="spacer">
        </div>
        <div class="textMain2">
            <h2><img src="image/icon_agenda_info.gif" alt="" />學生證RFID編輯管理</h2>
            <div>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" 
                    MultiPageID="RadMultiPage1" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="RFID管理" Owner="RadTabStrip1" 
                            Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="批次作業" Owner="RadTabStrip1">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" Runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <div class="R">
                           <p>　請輸入學生證號：<asp:TextBox ID="Txt_StdNo" runat="server"></asp:TextBox>
                            <asp:Button ID="But_Serach" runat="server" Text="送出查詢" />&nbsp;</p>
                            　<asp:Panel ID="Panel1" runat="server">
                                  <table border="0" cellspacing="1" cellpadding="5">
                                    <tr>
                                      <td>
                                          姓名：<asp:Label ID="Lab_name" runat="server" Text="Label"></asp:Label>
                                　        學號：<asp:Label ID="Lab_StdNo" runat="server" Text="Label"></asp:Label>
                                　        系所：<asp:Label ID="Lab_Classname" runat="server" Text="Label"></asp:Label>
                                　        班級： <asp:Label ID="Lab_Class" runat="server" Text="Label"></asp:Label>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>
                                          RFID內碼：<asp:TextBox ID="Txt_RFID_incode" runat="server"></asp:TextBox>
                                　        RFID外碼：<asp:TextBox ID="Txt_RFID_outcode" runat="server"></asp:TextBox>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td><asp:Button ID="But_Modify" runat="server" Text="送出修改" /></td>
                                    </tr>
                                  </table>
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <table cellpadding="5">
                           <tr>
                              <td>大量匯入學生RFID資料：</td>
                              <td>
                                <telerik:RadUpload ID="RadUpload1" Runat="server" ControlObjectsVisibility="None" Skin="Windows7" Height="21px" Width="226px" 
                                    AllowedFileExtensions=".xls" ClientIDMode="Inherit">
                                    <Localization Select="選擇檔案" />
                                </telerik:RadUpload>
                              </td>
                              <td>
                                  <asp:Button ID="But_Preview" runat="server" Text="讀檔預覽" />
                                  <asp:Button ID="Import_RFID" runat="server" Text="匯入資料庫" Enabled="False" />
                              </td>
                           </tr>
                           <tr>
                              <td colspan="3">備註：<br /> 　欄位名稱：1.學生證號 RegsNo&nbsp;&nbsp; 2.RFID內碼：incode&nbsp;&nbsp; 3.RFID外碼：outcode</td>
                           </tr>
                        </table>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        <br />
                                        <telerik:RadGrid ID="RGD_preview" runat="server" Visible="False" 
                    CellSpacing="0" Culture="zh-TW" GridLines="None">
<ExportSettings>
<Pdf>
<PageHeader>
<LeftCell Text=""></LeftCell>

<MiddleCell Text=""></MiddleCell>

<RightCell Text=""></RightCell>
</PageHeader>

<PageFooter>
<LeftCell Text=""></LeftCell>

<MiddleCell Text=""></MiddleCell>

<RightCell Text=""></RightCell>
</PageFooter>
</Pdf>
</ExportSettings>

<MasterTableView AllowPaging="True" PageSize="20">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

<BatchEditingSettings EditType="Cell"></BatchEditingSettings>

<PagerStyle PageSizeControlType="RadComboBox" HorizontalAlign="Center" 
        Mode="NumericPages"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
                </telerik:RadGrid>

                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </div>
        <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server" 
            Skin="Office2007" />
    </form>
    &nbsp;
</body>
</html>
