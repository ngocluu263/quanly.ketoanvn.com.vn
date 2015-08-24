<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-phu-luc-gia-han.aspx.cs" Inherits="ThanhLapDN.Pages.danh_sach_phu_luc_gia_han" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta http-equiv="Page-Enter" content="blendTrans(Duration=0)">

<meta http-equiv="Page-Exit" content="blendTrans(Duration=0)">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Phụ Lục Hợp Đồng Kế Toán
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            <%--&nbsp;<asp:LinkButton ID="lbtnCapNhapCongNo" runat="server" class="k-button" ToolTip="Cập nhật vào bảng công nợ" OnClientClick="return confirm('Bạn có muốn cập nhật thông tin những hợp đồng này vào công nợ kế toán? Hệ thống chỉ cập nhật mã số thuế chưa tồn tại trong công nợ và trạng thái đã hoàn thành!');"
                onclick="lbtnCapNhapCongNo_Click">
                <img alt="Cập nhật trạng thái" src="../Images/update-icon.png" /></asp:LinkButton>--%>
            &nbsp;<a href="phu-luc-gia-han-hop-dong-ke-toan.aspx" title="Tạo phụ lục gia hạn hợp đồng" class="k-button">
                <img alt="Thêm mới" src="../Images/addNew.png" /></a>
            &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
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
                            <input class="k-textbox k-input search-noidung fill-input" style="width:200px !important;" id="txtKeyword" placeholder="Tên công ty"
                                name="txtKeyword" type="text" value="" runat="server" clientidmode="Static"/>
                        </div>
                        <div class="div-sub">
                            <label>Từ ngày</label>
                            <uc1:pickerAndCalendar ID="pickdate_Begin" runat="server" />
                        </div>
                        <div class="div-sub">
                            <label>Đến ngày</label>
                            <uc1:pickerAndCalendar ID="pickdate_End" runat="server" />
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
    <asp:UpdatePanel id="UpdatePanel2" runat="server" OnLoad = "OnLoad">
    <ContentTemplate>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_project" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua"
                    onbeforecolumnsortinggrouping="ASPxGridView1_project_BeforeColumnSortingGrouping" 
                    onpageindexchanged="ASPxGridView1_project_PageIndexChanged" 
                    ondatabound="ASPxGridView1_project_DataBound" 
                    onheaderfilterfillitems="ASPxGridView1_project_HeaderFilterFillItems">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" 
                            Width="20px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã số thuế" FieldName="MER_TAXCODE" 
                            Width="120px">
                            <DataItemTemplate>
                                <%# Eval("MER_TAXCODE")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên công ty" FieldName="MER_NAME" 
                            Width="220px">
                            <DataItemTemplate>
                                <%# Eval("MER_NAME")%>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="True"/>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Địa chỉ" FieldName="MER_ADDRESS" 
                            Width="220px">
                            <DataItemTemplate>
                                <%# Eval("MER_ADDRESS")%>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="True"/>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày khởi tạo" 
                            FieldName="MER_DATE" Width="120px">
                            <DataItemTemplate>
                                <%# Eval("MER_DATE", "{0:dd/MM/yyyy}")%>
                            </DataItemTemplate>
							<PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yyyy hh:mm tt">
                            </PropertiesTextEdit>
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nhân viên tạo" FieldName="USER_ID" >
                            <DataItemTemplate>
                                <%# GetUser(Eval("USER_ID"))%>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="True"/>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Trạng thái" FieldName="MER_STATUS" 
                            Width="120px" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <%# getStatus(Eval("MER_STATUS"), Eval("MER_NGAY_HT","{0:dd/MM/yyyy}"))%>
                            </DataItemTemplate>
                            <Settings AllowAutoFilter="False"/>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ghi chú" FieldName="MER_CHI_CHU" 
                            Width="150px" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <%# Eval("MER_CHI_CHU")%>
                            </DataItemTemplate>
                            <Settings AllowAutoFilter="False"/>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tải về"
                            Width="70px">
                            <DataItemTemplate>
                                <a href='<%# getfiledinhkem(DataBinder.Eval(Container.DataItem, "ID")) %>'>
                                    <img src="../Images/download_icon.png" width="22" alt="Tải về"/></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
						<dx:GridViewDataTextColumn VisibleIndex="1" Caption="#" Width="40px">
                            <DataItemTemplate>
                                <a onClick="openTrangThai(<%# Eval("ID")%>,<%# Eval("MER_STATUS")%>); return false" title="Cập nhật trạng thái">
                                    <img src="/Images/notification_done_mini.png" width="24" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="DV kế toán" Width="30px">
                            <DataItemTemplate>
                                <%# getDVKT(Eval("MER_TAXCODE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sửa" Width="40px">
                            <DataItemTemplate>
                                <a href="<%# getlink(Eval("ID")) %>" title="Xem hồ sơ">
                                    <img src="/Images/file_edit.png" width="24" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="Chưa có hồ sơ mới" />
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                    <Settings showfilterrow="True" ShowFilterRowMenu="True"/>
                    <Styles>
                        <Header Font-Bold="True" HorizontalAlign="Center"></Header>
                        <Row HorizontalAlign="Center"></Row>
                    </Styles>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function GetCurrentTime() {
            __doPostBack("<%=UpdatePanel2.UniqueID %>", "");
        }
    </script>
    <!--Popup-->
    <link rel="stylesheet" href="/Styles/dhtmlwindow.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/dhtmlwindow.js"></script>
    <link rel="stylesheet" href="/Styles/modal.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/modal.js"></script>
    <script type="text/javascript">
        function openTrangThai(id, status) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-trang-thai-phu-luc-gia-han.aspx?id=' + id + '&status=' + status + '', 'Cập nhật trạng thái', 'width=350px,height=270px,center=1,resize=1,scrolling=1');
        }
    </script>
    <!--End Popup-->
</asp:Content>