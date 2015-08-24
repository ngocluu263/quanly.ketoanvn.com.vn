<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-cong-no-cks.aspx.cs" Inherits="ThanhLapDN.Pages.danh_sach_cong_no_cks" %>
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
            Công nợ tổng hợp chữ ký số
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a onClick="openCongNoCKS(0); return false" title="Thêm công nợ" class="k-button">
                <img alt="Thêm mới" src="../Images/addNew.png" /></a> &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
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
                        <label>
                            Tháng
                        </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlThang" runat="server" Width="80">
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
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
                            <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                            <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                            <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                            <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                            <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                            <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                            <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><img alt="Tìm kiếm" src="../Images/search.png" /></asp:LinkButton>
                    </td>
                    <td style="width:60%;text-align:right;">
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
                                <a onClick="openCongNoCKS(<%# Eval("ID")%>); return false" title="Chi tiết công nợ">
                                    <img src="/Images/file_edit.png" width="26" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" Caption="STT" FieldName="STT" Width="40px" FixedStyle="Left">
                            <DataItemTemplate>
                                <%# Eval("STT")%>
                            </DataItemTemplate>
                            <Settings ShowFilterRowMenu="False" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" Caption="TÊN CÔNG TY" FieldName="TEN_CTY" FixedStyle="Left" Width="150px">
                            <DataItemTemplate>
                                <%# Eval("TEN_CTY")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" Caption="MST" FieldName="MST" FixedStyle="Left" Width="90px">
                            <DataItemTemplate>
                                <%# Eval("MST")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" Caption="NVKD" FieldName="NV_KD" FixedStyle="Left"
                            Width="150px">
                            <DataItemTemplate>
                                <%# GetUser(Eval("NV_KD"))%>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="True"/>
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" Caption="Ngày nhận TB của NVKD" FixedStyle="Left"
                            FieldName="NGAY_NHAN_TB" Width="100px">
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                            <DataItemTemplate>
                                <%# Eval("NGAY_NHAN_TB" , "{0:dd/MM/yyyy}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="7" Caption="TÌNH TRẠNG" FieldName="TINH_TRANG" FixedStyle="Left" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("TINH_TRANG")%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="8" Caption="LOẠI HỢP ĐỒNG" FieldName="LOAI_HOPDONG" FixedStyle="Left" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("LOAI_HOPDONG")%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewBandColumn Caption="DỊCH VỤ CHỮ KÝ SỐ + PHẦN MỀM" VisibleIndex="9" HeaderStyle-HorizontalAlign="Left">
                            <Columns>
                                <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nhà cung cấp" Width="100px" FieldName="CKS_NHA_CUNG_CAP">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_NHA_CUNG_CAP")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Sản phẩm" Width="100px" FieldName="CKS_SAN_PHAM">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_SAN_PHAM")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Ngày hết hạn TB" Width="100px" FieldName="CKS_NGAY_HH_TB">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_NGAY_HH_TB", "{0:dd/MM/yyyy}")%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" Caption="PHÍ DV" Width="120px" FieldName="CKS_PHI_DV">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_PHI_DV", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="6" Caption="GIÁ TB TOKEN" Width="120px" FieldName="CKS_GIA_TK">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_GIA_TK", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="7" Caption="VAT" Width="120px" FieldName="CKS_VAT">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_VAT", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="8" Caption="TỔNG CỘNG" Width="120px" FieldName="CKS_TONG_CONG">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_TONG_CONG", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="10" Caption="HOA HỒNG ĐẠI LÝ CỦA KL" Width="120px" FieldName="CKS_HOA_HONG_DL">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_HOA_HONG_DL", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="11" Caption="VAT HOA HỒNG" Width="120px" FieldName="CKS_HOA_HONG_VAT">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_HOA_HONG_VAT", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="12" Caption="TỔNG HOA HỒNG" Width="120px" FieldName="CKS_HOA_HONG">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_HOA_HONG", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="13" Caption="TỔNG THANH TOÁN NHÀ CUNG CẤP" Width="120px" FieldName="CKS_TONG_TT_NCC">
                                    <DataItemTemplate>
                                        <%# Eval("CKS_TONG_TT_NCC", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="THU TIỀN KHÁCH HÀNG" VisibleIndex="10" HeaderStyle-HorizontalAlign="Left">
                            <Columns>
                                <dx:GridViewDataTextColumn VisibleIndex="1" Caption="TOKEN + CKS" Width="120px" FieldName="TT_TK_CKS">
                                    <DataItemTemplate>
                                        <%# Eval("TT_TK_CKS", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="2" Caption="PHẦN MỀM" Width="120px" FieldName="TT_PHAN_MEM">
                                    <DataItemTemplate>
                                        <%# Eval("TT_PHAN_MEM", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewBandColumn Caption="NVKD TRÍCH HOA HỒNG" VisibleIndex="3">
                                    <Columns>
                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="CHỮ KÝ SỐ" Width="120px" FieldName="TT_HH_CKS">
                                            <DataItemTemplate>
                                                <%# Eval("TT_HH_CKS", "{0:###,##0}")%>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="PHẦN MỀM" Width="120px" FieldName="TT_HH_PM">
                                            <DataItemTemplate>
                                                <%# Eval("TT_HH_PM", "{0:###,##0}")%>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="4" Caption="CÒN NỢ" Width="120px" FieldName="CON_NO">
                                    <DataItemTemplate>
                                        <%# Eval("CON_NO", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" Caption="NGÀY THU" Width="120px" FieldName="NGAY_THU">
                                    <DataItemTemplate>
                                        <%# Eval("NGAY_THU")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="11" Caption="GHI CHÚ" Width="200px" FieldName="GHI_CHU">
                            <DataItemTemplate>
                                <%# Eval("GHI_CHU")%>
                            </DataItemTemplate>
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
    <!--Popup-->
    <link rel="stylesheet" href="/Styles/dhtmlwindow.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/dhtmlwindow.js"></script>
    <link rel="stylesheet" href="/Styles/modal.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/modal.js"></script>
    <script type="text/javascript">
        function openCongNoCKS(id) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-cong-no-cks.aspx?id=' + id + '', 'Công nợ', 'width=750px,height=550px,center=1,resize=1,scrolling=1');
        }
    </script>
    <!--End Popup-->
    <link href="/Styles/template.css" rel="stylesheet" type="text/css" />
</asp:Content>