<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-thanh-vien.aspx.cs" Inherits="ThanhLapDN.Pages.popup_thanh_vien" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="header">
        <div class="toolbar">
            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
        </div>
        <div style="clear: both"></div>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_project" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua" 
                    onbeforecolumnsortinggrouping="ASPxGridView1_project_BeforeColumnSortingGrouping" 
                    onpageindexchanged="ASPxGridView1_project_PageIndexChanged" 
                    ondatabound="ASPxGridView1_project_DataBound">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" 
                            Width="30px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Loại thành viên" FieldName="MEM_TYPE"
                            Width="170px">
                            <DataItemTemplate>
                                <%# GetType(Eval("MEM_TYPE"))%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên thành viên" FieldName="MEM_FULLNAME">
                            <DataItemTemplate>
                            <%# Eval("MEM_FULLNAME")%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <div class="ProjChild">
                                <%# GetInfoChild(Eval("MEM_TYPE"), Eval("MEM_BIRTHDAY"), Eval("MEM_FIELD1"), Eval("MEM_SEX"), Eval("MEM_NATIONALITY"), Eval("MEM_CMND"), Eval("MEM_DATE_CMND"), Eval("MEM_ADDRESS_CMND"), Eval("MEM_HOUSEHOLD"), Eval("MEM_ADDRESS"), Eval("MEM_CAPITAL"), Eval("MEM_PERCENT"), Eval("MEM_POSTION"), Eval("MEM_FIELD2"))%>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsText EmptyDataRow="Hồ sơ này chưa khai báo thành viên!" />
                    <SettingsDetail ShowDetailRow="True" />
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
