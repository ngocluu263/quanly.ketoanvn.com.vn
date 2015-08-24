<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="thu-vien-files-cong-no.aspx.cs" Inherits="ThanhLapDN.Pages.thu_vien_files_cong_no" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Thư viện files công nợ dịch vụ kế toán
        </div>
        <div style="clear: both"></div>
    </div>
    <div id="div-search">
    <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSearch">
        <table style="width:100%;">
            <tbody>
                <tr>
                    <td>
                    <div class="div-sub">
                        <label>Từ khóa</label>
                        <input class="k-textbox k-input search-noidung fill-input" style="width:200px !important;" id="txtKeyword"
                                name="txtKeyword" type="text" value="" runat="server" clientidmode="Static"/>
                    </div>
                    <div class="div-sub">
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><img alt="Tìm kiếm" src="../Images/search.png" /></asp:LinkButton>
                    </div>
                    </td>
                </tr>
            </tbody>
        </table>
        </asp:Panel>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_project" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="MST" Theme="Aqua"
                    onbeforecolumnsortinggrouping="ASPxGridView1_project_BeforeColumnSortingGrouping" 
                    onpageindexchanged="ASPxGridView1_project_PageIndexChanged" 
                    ondatabound="ASPxGridView1_project_DataBound" 
                    onheaderfilterfillitems="ASPxGridView1_project_HeaderFilterFillItems">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" 
                            Width="20px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã số thuế"
                            Width="130px">
                            <DataItemTemplate>
                                <%# Eval("MST")%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <Settings AllowHeaderFilter="True"/>
                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên công ty">
                            <DataItemTemplate>
                                <%# GetName(Eval("MST"))%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Dung lượng sử dụng" Width="130px">
                            <DataItemTemplate>
                                <%# GetFileSize(Eval("MST"))%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                        </dx:GridViewDataTextColumn>
						<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Files" Width="40px">
                            <DataItemTemplate>
                                <a href="<%# getlink(Eval("MST")) %>" title="Xem files" target="_blank">
                                    <img src="/Images/library.png" width="28" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="Chưa có hồ sơ mới" />
                    <SettingsPager PageSize="100">
                    </SettingsPager>
                    <Settings showfilterrow="True" ShowFilterRowMenu="True"/>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>

    <!--Popup-->
    <link rel="stylesheet" href="/Styles/dhtmlwindow.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/dhtmlwindow.js"></script>
    <link rel="stylesheet" href="/Styles/modal.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/modal.js"></script>
    <script type="text/javascript">
        function openDSTV(id, status) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-danh-sach-thanh-vien.aspx?id=' + id + '', 'Thêm thành viên', 'width=450px,height=500px,center=1,resize=1,scrolling=1');
        }
        function openDSTV1(id, status) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-thanh-vien.aspx?id=' + id + '', 'Danh sách thành viên', 'width=450px,height=500px,center=1,resize=1,scrolling=1');
        }
    </script>
    <!--End Popup-->
</asp:Content>