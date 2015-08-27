<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-cong-no-web.aspx.cs" Inherits="ThanhLapDN.Pages.danh_sach_cong_no_web" %>
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
            Theo dõi hợp đồng web
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a onClick="openCongNoWeb(0); return false" title="Thêm công nợ" class="k-button">
                <img alt="Thêm mới" src="../Images/addNew.png" /></a> &nbsp;
                <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
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
                        <label>
                            Từ khóa
                        </label>
                    </td>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lbtnDeleteKeyword" />
                        </Triggers>
                        <ContentTemplate>
                        <input class="k-textbox k-input search-noidung fill-input" width="300" id="txtKeyword"
                            name="txtKeyword" type="text" value="" runat="server" clientidmode="Static"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnDeleteKeyword" ToolTip="Xóa tìm kiếm"
                            runat="server" OnClick="lbtnDeleteKeyword_Click"><img alt="Tìm kiếm" src="../Images/back.png" /></asp:LinkButton> 
                    </td>
                    <td>
                        <label>Tình trạng</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTinhtrang" runat="server">
                            <asp:ListItem Value="0" Text="--Tất cả--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Đang chờ"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Đang triển khai"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Hoàn tất"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Hủy"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <label>Công nợ</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCongnno" runat="server">
                            <asp:ListItem Value="0" Text="--Tất cả--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Có"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Không"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <label>
                            Tháng
                        </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlThang" runat="server" Width="80">
                            <asp:ListItem Value="0" Text="--Tất cả--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="01"></asp:ListItem>
                            <asp:ListItem Value="2" Text="02"></asp:ListItem>
                            <asp:ListItem Value="3" Text="03"></asp:ListItem>
                            <asp:ListItem Value="4" Text="04"></asp:ListItem>
                            <asp:ListItem Value="5" Text="05"></asp:ListItem>
                            <asp:ListItem Value="6" Text="06"></asp:ListItem>
                            <asp:ListItem Value="7" Text="07"></asp:ListItem>
                            <asp:ListItem Value="8" Text="08"></asp:ListItem>
                            <asp:ListItem Value="9" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <label>
                            Năm
                        </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNam" runat="server" Width="80">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><img alt="Tìm kiếm" src="../Images/search.png" /></asp:LinkButton>
                    </td>
                    <td style="width:40%;text-align:right;">
                        <asp:LinkButton ID="imgBtnExport" runat="server" CssClass="k-button" onclick="imgBtnExport_Click1"><img src="/Images/export_excel.png" title="Export Excel" width="20"/></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="imgBtnImport" runat="server" CssClass="k-button" onclick="imgBtnImport_Click1"><img src="/Images/import_excel.png" title="Import Excel" width="20"/></asp:LinkButton>
                        <asp:FileUpload ID="fileUpload" runat="server" />
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
                    onheaderfilterfillitems="ASPxGridView1_project_HeaderFilterFillItems">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" FixedStyle="Left"
                            Width="30px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sửa" FixedStyle="Left" Width="40px">
                            <DataItemTemplate>
                                <a onClick="openCongNoWeb(<%# Eval("ID")%>); return false" title="Chi tiết công nợ">
                                    <img src="/Images/file_edit.png" width="26" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" Caption="Số hợp đồng" 
                            FieldName="SO_HOPDONG" Width="100px" FixedStyle="Left">
                            <DataItemTemplate>
                                <%# Eval("SO_HOPDONG")%>
                            </DataItemTemplate>
                            <Settings ShowFilterRowMenu="False" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Ngày ký hợp đồng" 
                            FieldName="NGAYKY_HOPDONG" FixedStyle="Left" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("NGAYKY_HOPDONG", "{0:dd/MM/yyyy}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" Caption="Tên khách hàng" 
                            FieldName="TEN_KHACHHANG" FixedStyle="Left" Width="180px">
                            <DataItemTemplate>
                                <%# Eval("TEN_KHACHHANG")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Thông tin liên hệ" 
                            FieldName="THONGTINLIENHE_KHACHHAN"
                            Width="150px">
                            <DataItemTemplate>
                                <%# Eval("THONGTINLIENHE_KHACHHANG")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" Caption="Nội dung"
                            FieldName="NOIDUNG" Width="180px">
                            <DataItemTemplate>
                                <%# Eval("NOIDUNG")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="7" Caption="Tên domain" 
                            FieldName="TEN_DOMAIN" Width="180px">
                            <DataItemTemplate>
                                <%# Eval("TEN_DOMAIN")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="9" Caption="NVXL" FieldName="NVXL">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="NVKD" FieldName="NVKD" 
                            VisibleIndex="8" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewBandColumn Caption="DOANH SỐ" VisibleIndex="10" 
                            HeaderStyle-HorizontalAlign="Left">
                            <Columns>
                                <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Domain" Width="100px" 
                                    FieldName="DOMAIN_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("DOMAIN_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Hosting" Width="100px" 
                                    FieldName="HOSTING_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("HOSTING_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Chi phí triển khai" 
                                    Width="100px" FieldName="CHIPHI_TRIENKHAI_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("CHIPHI_TRIENKHAI_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Web" Width="120px" 
                                    FieldName="WEB_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("WEB_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="6" Caption="Logo, banner" 
                                    Width="120px" FieldName="LOGO_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("LOGO_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="7" Caption="Esell" Width="120px" 
                                    FieldName="ESELL_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("ESELL_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="8" Caption="Chụp hình" Width="120px" 
                                    FieldName="CHUPHINH_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("CHUPHINH_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="10" Caption="Catalogue" Width="120px" 
                                    FieldName="CATALOGUE_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("CATALOGUE_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="11" Caption="Seo từ khóa" 
                                    Width="120px" FieldName="SEOTUKHOA_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("SEOTUKHOA_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="12" Caption="Google adword" 
                                    Width="120px" FieldName="GOOGLEADWORD_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("GOOGLEADWORD_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="13" Caption="Phần mềm" Width="120px" 
                                    FieldName="PHANMEM_PRICE">
                                    <DataItemTemplate>
                                        <%# Eval("PHANMEM_PRICE", "{0:###,###}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="11" Caption="Hoa hồng" Width="200px" 
                            FieldName="HOAHONGKH_PRICE">
                            <DataItemTemplate>
                                <%# Eval("HOAHONGKH_PRICE", "{0:###,###}")%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="VAT" FieldName="VAT" VisibleIndex="12">
                            <DataItemTemplate>
                                <%# Eval("VAT", "{0:###,###}")%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tổng cộng" FieldName="TONGCONG" 
                            VisibleIndex="13">
                            <DataItemTemplate>
                                <%# Eval("TONGCONG", "{0:###,###}")%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thanh toán" FieldName="THANHTOAN" 
                            VisibleIndex="14">
                            <DataItemTemplate>
                                <%# Eval("THANHTOAN", "{0:###,###}")%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ngày thanh toán" FieldName="NGAYTHANHTOAN" 
                            VisibleIndex="15">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ngày xuất hđơn" FieldName="NGAYXUATHOADON" 
                            VisibleIndex="16">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Công nợ" FieldName="CONGNO" 
                            VisibleIndex="18">
                            <DataItemTemplate>
                                <%# Eval("CONGNO", "{0:###,###}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tình trạng" FieldName="TINHTRANG" 
                            VisibleIndex="19">
                            <DataItemTemplate>
                                <%# getTinhtrang(Eval("TINHTRANG"))%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="GHICHU"  Width="200px"
                            VisibleIndex="20">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="Chưa có công nợ mới" />
                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                    <Settings showfilterrow="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="True"
                        showverticalscrollbar="True" verticalscrollableheight="400"/>
                    <Styles>
                        <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True"></Header>
                    </Styles>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
     </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pagination">
        <asp:Literal ID="ltrPage" runat="server"></asp:Literal>
    </div>
    <div style="margin:0px;text-align:center;">
        Số bài: 
        <asp:DropDownList ID="ddlCountPage" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlCountPage_SelectedIndexChanged">
            <asp:ListItem Value="10" Text="10"></asp:ListItem>
            <asp:ListItem Value="20" Text="20"></asp:ListItem>
            <asp:ListItem Value="30" Text="30"></asp:ListItem>
            <asp:ListItem Value="40" Text="40"></asp:ListItem>
            <asp:ListItem Value="50" Text="50"></asp:ListItem>
            <asp:ListItem Value="60" Text="60"></asp:ListItem>
            <asp:ListItem Value="70" Text="70"></asp:ListItem>
            <asp:ListItem Value="80" Text="80"></asp:ListItem>
            <asp:ListItem Value="90" Text="90"></asp:ListItem>
            <asp:ListItem Value="100" Text="100"></asp:ListItem>
            <asp:ListItem Value="150" Text="150"></asp:ListItem>
            <asp:ListItem Value="200" Text="200"></asp:ListItem>
            <asp:ListItem Value="250" Text="250"></asp:ListItem>
            <asp:ListItem Value="300" Text="300"></asp:ListItem>
        </asp:DropDownList>
    </div>
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
        function openCongNoWeb(id) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-cong-no-web.aspx?id=' + id + '', 'Công nợ', 'width=750px,height=550px,center=1,resize=1,scrolling=1');
        }
    </script>
    <!--End Popup-->
    <link href="/Styles/template.css" rel="stylesheet" type="text/css" />
</asp:Content>