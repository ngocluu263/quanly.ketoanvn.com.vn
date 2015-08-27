<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-cong-no.aspx.cs" Inherits="ThanhLapDN.Pages.danh_sach_cong_no" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v12.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Công Nợ
        </div>
        <div id="iListBtn" runat="server" class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a onClick="openCongNoNew(0); return false" title="Thêm mới công nợ" class="k-button">
            <img alt="Thêm mới" src="../Images/addNew.png" /></a>
            &nbsp;<asp:LinkButton ID="btnSync" OnClientClick="return confirm('Bạn có chắc là muốn cập nhật nhân viên giao nhận theo Quản lý thuế ?');" 
                ToolTip="Cập nhật nhân viên giao nhận theo quản lý thuế" 
                CssClass="k-button" runat="server" onclick="btnSync_Click" >
                <img alt="Sync" src="../Images/change_data.png" /></asp:LinkButton>
            &nbsp;<a onClick="openCheckError(); return false" title="Kiểm tra lỗi hệ thống" class="k-button">
                <img alt="Kiểm tra lỗi hệ thống" src="../Images/icon-testing.png" /></a>
            &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');" ToolTip="Xóa" CssClass="k-button" runat="server" 
                onclick="lbtnDelete_Click" ><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
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
                            Tìm theo
                        </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFields" runat="server" class="k-textbox k-input" 
                            style="padding-top: 4px;width:150px;cursor: pointer;">
                            <asp:ListItem Value="0" Text="----Tất cả----" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Tên công ty"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Mã số thuế"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Quản lý thuế"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Địa chỉ"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Điện thoại"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Nhân viên KT"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Nhân viên KD"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Nhân viên GN"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Biểu phí trống"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Nhân viên KT trống"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nhân viên KD trống"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Nhân viên GN trống"></asp:ListItem>
                            <asp:ListItem Value="13" Text="Công ty nộp thuế trể"></asp:ListItem>
                        </asp:DropDownList>
                        <input class="k-textbox k-input" id="txtKeyword" placeholder="từ khóa tìm kiếm"
                            name="txtKeyword" type="text" value="" runat="server" clientidmode="Static" style="width:200px;"/>
                        <asp:DropDownList ID="ddlStatus" runat="server" class="k-textbox k-input" style="padding-top: 4px;width:150px;cursor: pointer;">
                            <asp:ListItem Value="" Text="---Tất cả tình trạng---" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Đang hoạt động"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Tạm ngưng hoạt động"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Giải thể"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Ngừng dịch vụ"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Không thu phí"></asp:ListItem>
                        </asp:DropDownList>
                        <label>Năm</label>
                        <asp:DropDownList ID="ddlNam" runat="server" class="k-textbox k-input" style="padding-top: 4px;width:70px;font-weight: 700;cursor: pointer;">
                            <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                            <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                            <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                            <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                            <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                            <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                            <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click" ><img alt="Tìm kiếm" src="../Images/search.png" /></asp:LinkButton>
                    </td>
                    <td style="width:34%;text-align:right;">
                        <asp:LinkButton ID="imgBtnExport" runat="server" CssClass="k-button" onclick="imgBtnExport_Click1"><img src="/Images/export_excel.png" title="Export Excel" width="20"/></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="imgBtnImport" runat="server" CssClass="k-button" onclick="imgBtnImport_Click1"><img src="/Images/import_excel.png" title="Import Excel" width="20"/></asp:LinkButton>
                        <asp:FileUpload ID="fileUpload" runat="server" class="k-textbox k-input"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    </div>

    <div style="padding-bottom: 5px;">
        Số công ty: <b><asp:Literal ID="liSoCty" runat="server"></asp:Literal></b>&nbsp;|
        Nợ trong tháng: <b><asp:Literal ID="liNoTrongThang" runat="server"></asp:Literal></b>&nbsp;|
        Nợ trong năm: <b><asp:Literal ID="liNoTrongNam" runat="server"></asp:Literal></b>
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
                    onhtmlrowprepared="ASPxGridView1_project_HtmlRowPrepared" 
                    onheaderfilterfillitems="ASPxGridView1_project_HeaderFilterFillItems">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" FixedStyle="Left" Width="30px">
                            <clearfilterbutton visible="True">
                            </clearfilterbutton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="#" FixedStyle="Left" Width="50px">
                            <DataItemTemplate>
                                <a onClick="openCongNo(<%# Eval("ID")%>,'<%# Eval("MST")%>','<%# Eval("NAM")%>'); return false" title="Chi tiết công nợ">
                                    <img src="/Images/file_edit.png" width="26" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
							<cellstyle horizontalalign="Center"></cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" Caption="STT" Width="50px" FixedStyle="Left" FieldName="STT">
                            <DataItemTemplate>
                                <%# Eval("STT")%>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="True"/>
                            <Settings AllowHeaderFilter="True" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" Caption="Tình trạng" FixedStyle="Left"
                            FieldName="TINH_TRANG" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("TINH_TRANG")%>
                            </DataItemTemplate>
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Tên khách hàng" FixedStyle="Left"
                            FieldName="TEN_KH" Width="150px">
                            <DataItemTemplate>
                                <%# Eval("TEN_KH")%>
                            </DataItemTemplate>
                            <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True"/>
                            <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" Caption="Quản lý thuế" FixedStyle="Left"
                            FieldName="QL_THUE_DIST" Width="110px">
                            <DataItemTemplate>
                                <%# getPropertyName(Eval("QL_THUE_CITY"))%> <br /> <%# getPropertyName(Eval("QL_THUE_DIST"))%>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="True"/>
                            <Settings AllowHeaderFilter="True" />
                            <cellstyle horizontalalign="Left">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" Caption="MST" FieldName="MST" FixedStyle="Left"
                            Width="90px">
                            <DataItemTemplate>
                                <%# Eval("MST")%>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="True"/>
                            <Settings AllowHeaderFilter="True" />
                        </dx:GridViewDataTextColumn>                   
                        <dx:GridViewDataTextColumn VisibleIndex="6" Caption="Địa chỉ" 
                            FieldName="DIA_CHI" Width="150px">
                            <DataItemTemplate>
                                <%# Eval("DIA_CHI")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        
                        <%--<dx:GridViewDataTextColumn VisibleIndex="8" Caption="Giám đốc" 
                            FieldName="GIAM_DOC" Width="120px">
                            <DataItemTemplate>
                                <%# Eval("GIAM_DOC")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Điện thoại" 
                            FieldName="DIEN_THOAI" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("DIEN_THOAI")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="10" Caption="Email" FieldName="EMAIL" 
                            Width="200px">
                            <DataItemTemplate>
                                <%# Eval("EMAIL")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>

                        <dx:GridViewDataTextColumn VisibleIndex="12" Caption="Ngày ký hợp đồng" 
                            FieldName="NGAY_KY_HD" Width="110px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_KY_HD","{0:dd/MM/yyyy}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="12" Caption="Tháng bắt đầu thu" 
                            FieldName="THANG_BD_THU" Width="110px">
                            <DataItemTemplate>
                                <%# Eval("THANG_BD_THU")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="13" Caption="Nhân viên kế toán" 
                            FieldName="NV_KT" Width="150px">
                            <DataItemTemplate>
                                <%# GetUser(Eval("NV_KT"))%>
                            </DataItemTemplate>
                            <Settings AllowAutoFilter="False"/>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="66" Caption="Nhân viên kinh doanh" 
                            FieldName="NV_KD" Width="150px">
                            <DataItemTemplate>
                                <%# GetUser(Eval("NV_KD"))%>
                            </DataItemTemplate>
                            <Settings AllowAutoFilter="False"/>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="66" Caption="Nhân viên giao nhận" 
                            FieldName="NV_GN" Width="150px">
                            <DataItemTemplate>
                                <%# GetUser(Eval("NV_GN"))%>
                            </DataItemTemplate>
                            <Settings AllowAutoFilter="False"/>
                        </dx:GridViewDataTextColumn>

                        <%--<dx:GridViewDataTextColumn VisibleIndex="14" Caption="Phí dịch vụ (T1)" 
                            FieldName="PHI_DV_1" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_1", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="15" Caption="Đã thanh toán (T1)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT1_1")) 
                                    + Convert.ToInt32(Eval("DA_TT1_2")) 
                                    + Convert.ToInt32(Eval("DA_TT1_3")) 
                                    + Convert.ToInt32(Eval("DA_TT1_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="16" Caption="Ngày thanh toán (T1)" 
                            FieldName="NGAY_TT_1" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_1")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="17" Caption="Còn nợ (T1)" 
                            FieldName="CON_NO_1" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_1","{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="18" Caption="Phí dịch vụ (T2)" 
                            FieldName="PHI_DV_2" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_2", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="19" Caption="Đã thanh toán (T2)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT2_1")) 
                                    + Convert.ToInt32(Eval("DA_TT2_2")) 
                                    + Convert.ToInt32(Eval("DA_TT2_3")) 
                                    + Convert.ToInt32(Eval("DA_TT2_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="20" Caption="Ngày thanh toán (T2)" 
                            FieldName="NGAY_TT_2" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_2")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="21" Caption="Còn nợ (T2)" 
                            FieldName="CON_NO_2" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_2", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="22" Caption="Phí dịch vụ (T3)" 
                            FieldName="PHI_DV_3" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_3", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="23" Caption="Đã thanh toán (T3)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT3_1")) 
                                    + Convert.ToInt32(Eval("DA_TT3_2")) 
                                    + Convert.ToInt32(Eval("DA_TT3_3")) 
                                    + Convert.ToInt32(Eval("DA_TT3_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="24" Caption="Ngày thanh toán (T3)" 
                            FieldName="NGAY_TT_3" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_3")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="25" Caption="Còn nợ (T3)" 
                            FieldName="CON_NO_3" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_3", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="26" Caption="Phí dịch vụ (T4)" 
                            FieldName="PHI_DV_4" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_4", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="27" Caption="Đã thanh toán (4)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT4_1")) 
                                    + Convert.ToInt32(Eval("DA_TT4_2")) 
                                    + Convert.ToInt32(Eval("DA_TT4_3")) 
                                    + Convert.ToInt32(Eval("DA_TT4_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="28" Caption="Ngày thanh toán (T4)" 
                            FieldName="NGAY_TT_4" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_4")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="29" Caption="Còn nợ (T4)" 
                            FieldName="CON_NO_4" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_4", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="30" Caption="Phí dịch vụ (T5)" 
                            FieldName="PHI_DV_5" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_5", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="31" Caption="Đã thanh toán (T5)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT5_1")) 
                                    + Convert.ToInt32(Eval("DA_TT5_2")) 
                                    + Convert.ToInt32(Eval("DA_TT5_3")) 
                                    + Convert.ToInt32(Eval("DA_TT5_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="32" Caption="Ngày thanh toán (T5)" 
                            FieldName="NGAY_TT_5" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_5")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="33" Caption="Còn nợ (T5)" 
                            FieldName="CON_NO_5" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_5", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="34" Caption="Phí dịch vụ (T6)" 
                            FieldName="PHI_DV_6" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_6", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="35" Caption="Đã thanh toán (T6)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT6_1")) 
                                    + Convert.ToInt32(Eval("DA_TT6_2")) 
                                    + Convert.ToInt32(Eval("DA_TT6_3")) 
                                    + Convert.ToInt32(Eval("DA_TT6_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="36" Caption="Ngày thanh toán (T6)" 
                            FieldName="NGAY_TT_6" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_6")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="37" Caption="Còn nợ (T6)" 
                            FieldName="CON_NO_6" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_6", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="38" Caption="Phí dịch vụ (T7)" 
                            FieldName="PHI_DV_7" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_7", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="39" Caption="Đã thanh toán (T7)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT7_1")) 
                                    + Convert.ToInt32(Eval("DA_TT7_2")) 
                                    + Convert.ToInt32(Eval("DA_TT7_3")) 
                                    + Convert.ToInt32(Eval("DA_TT7_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="40" Caption="Ngày thanh toán (T7)" 
                            FieldName="NGAY_TT_7" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_7")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="41" Caption="Còn nợ (T7)" 
                            FieldName="CON_NO_7" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_7", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="42" Caption="Phí dịch vụ (T8)" 
                            FieldName="PHI_DV_8" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_8", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="43" Caption="Đã thanh toán (T8)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT8_1")) 
                                    + Convert.ToInt32(Eval("DA_TT8_2")) 
                                    + Convert.ToInt32(Eval("DA_TT8_3")) 
                                    + Convert.ToInt32(Eval("DA_TT8_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="44" Caption="Ngày thanh toán (T8)" 
                            FieldName="NGAY_TT_8" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_8")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="45" Caption="Còn nợ (T8)" 
                            FieldName="CON_NO_8" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_8", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="46" Caption="Phí dịch vụ (T9)" FieldName="PHI_DV_9" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_9", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="47" Caption="Đã thanh toán (T9)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT9_1")) 
                                    + Convert.ToInt32(Eval("DA_TT9_2")) 
                                    + Convert.ToInt32(Eval("DA_TT9_3")) 
                                    + Convert.ToInt32(Eval("DA_TT9_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="48" Caption="Ngày thanh toán (T9)" 
                            FieldName="NGAY_TT_9" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_9")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="49" Caption="Còn nợ (T9)" 
                            FieldName="CON_NO_9" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_9", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="50" Caption="Phí dịch vụ (T10)" 
                            FieldName="PHI_DV_10" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_10", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="51" Caption="Đã thanh toán (T10)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT10_1")) 
                                    + Convert.ToInt32(Eval("DA_TT10_2")) 
                                    + Convert.ToInt32(Eval("DA_TT10_3")) 
                                    + Convert.ToInt32(Eval("DA_TT10_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="52" Caption="Ngày thanh toán (T10)" 
                            FieldName="NGAY_TT_10" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_10")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="53" Caption="Còn nợ (T10)" 
                            FieldName="CON_NO_10" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_10", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="54" Caption="Phí dịch vụ (T11)" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_11", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="55" Caption="Đã thanh toán (T11)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT11_1")) 
                                    + Convert.ToInt32(Eval("DA_TT11_2")) 
                                    + Convert.ToInt32(Eval("DA_TT11_3")) 
                                    + Convert.ToInt32(Eval("DA_TT11_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="56" Caption="Ngày thanh toán (T11)" 
                            FieldName="NGAY_TT_11" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_11")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="57" Caption="Còn nợ (T11)" 
                            FieldName="CON_NO_11" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_11", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="58" Caption="Phí dịch vụ (T12)" 
                            FieldName="PHI_DV_12" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_12", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="59" Caption="Đã thanh toán (T12)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT12_1")) 
                                    + Convert.ToInt32(Eval("DA_TT12_2")) 
                                    + Convert.ToInt32(Eval("DA_TT12_3")) 
                                    + Convert.ToInt32(Eval("DA_TT12_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="60" Caption="Ngày thanh toán (T12)" 
                            FieldName="NGAY_TT_12" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_12")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="61" Caption="Còn nợ (T12)" 
                            FieldName="CON_NO_12" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_12", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="62" Caption="Phí dịch vụ (BCTC)" 
                            FieldName="PHI_DV_BCTC" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("PHI_DV_BCTC", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="63" Caption="Đã thanh toán (BCTC)" Width="110px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("DA_TT13_1")) 
                                    + Convert.ToInt32(Eval("DA_TT13_2")) 
                                    + Convert.ToInt32(Eval("DA_TT13_3")) 
                                    + Convert.ToInt32(Eval("DA_TT13_4")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="64" Caption="Ngày thanh toán (BCTC)" 
                            FieldName="NGAY_TT_BCTC" Width="130px">
                            <DataItemTemplate>
                                <%# Eval("NGAY_TT_BCTC")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="65" Caption="Còn nợ (BCTC)" 
                            FieldName="CON_NO_BCTC" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("CON_NO_BCTC", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>

                        <dx:GridViewDataTextColumn VisibleIndex="66" Caption="Tổng nợ" 
                            FieldName="TONG_NO" Width="100px">
                            <DataItemTemplate>
                                <b><%# Eval("TONG_NO", "{0:###,##0}")%></b>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="69" Caption="Ghi chú" 
                            FieldName="GHI_CHU" Width="220px">
                            <DataItemTemplate>
                                <%# Eval("GHI_CHU")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="Chưa có hồ sơ mới" />
                    <Settings ShowHorizontalScrollBar="True" showverticalscrollbar="True" 
                        verticalscrollableheight="400"/>
                    <Settings showfilterrow="True" ShowFilterRowMenu="True"/>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    <div class="pagination">
        <asp:Literal ID="ltrPage" runat="server"></asp:Literal>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
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
    function openCongNoNew(id) {
        nam = $("#CPHMain_ddlNam").attr('value');
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-cong-no.aspx?id=' + id + '&nam=' + nam + '', 'Công nợ', 'width=750px,height=550px,center=1,resize=1,scrolling=1');
    }
    var mst = new String(mst);
    function openCongNo(id, mst, nam) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-cong-no.aspx?id=' + id + '&mst=' + mst + '&nam=' + nam + '', 'Công nợ', 'width=750px,height=550px,center=1,resize=1,scrolling=1');
    }
    function openCheckError() {
        nam = $("#CPHMain_ddlNam").attr('value');
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-kiem-tra-mst-cong-no.aspx?year=' + nam + '', 'Kiểm tra lỗi hệ thống', 'width=500px,height=400px,center=1,resize=1,scrolling=1');
    }
</script>
<script>    $("#CPHMain_ddlFields :selected").text();</script>
<!--End Popup-->
<link href="/Styles/template.css" rel="stylesheet" type="text/css" />
</asp:Content>
