<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="danh-sach-nhan-vien.aspx.cs" Inherits="ThanhLapDN.Pages.danh_sach_nhan_vien" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Nhân Viên
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-nhan-vien.aspx" title="Thêm dịch vụ" class="k-button">
            <img alt="Thêm mới" src="../Images/addNew.png" /></a> &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa dịch vụ" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click1"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
    <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSearch">
        <label>Từ khóa</label>
        <input class="k-textbox k-input" width="300" id="txtKeyword"
            name="txtKeyword" type="text" value="" runat="server" />
        <asp:LinkButton CssClass="k-button" ID="lbtnDeleteKeyword" ToolTip="Xóa tìm kiếm"
            runat="server" OnClick="lbtnDeleteKeyword_Click"><img alt="Tìm kiếm" src="../Images/back.png" /></asp:LinkButton>
        <label>Chức vụ</label>
        <asp:DropDownList ID="ddlChucVu" runat="server" class="k-textbox k-input">    
        </asp:DropDownList>
        <label>Địa điểm</label>
        <asp:DropDownList ID="ddlDiaDiem" runat="server" class="k-textbox k-input">
            <asp:ListItem Value="0" Text="---Tất cả địa điểm---" Selected="True"></asp:ListItem>
            <asp:ListItem Value="1" Text="Tp.HCM - Trụ sở chính"></asp:ListItem>
            <asp:ListItem Value="2" Text="Hà Nội - Chi nhánh"></asp:ListItem>
            <asp:ListItem Value="3" Text="Nha Trang - Chi nhánh"></asp:ListItem>
            <asp:ListItem Value="4" Text="Đà Nẵng - Chi nhánh"></asp:ListItem>
        </asp:DropDownList>
        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
            OnClick="lbtnSearch_Click"><img alt="Tìm kiếm" src="../Images/search.png" /></asp:LinkButton>    
    </asp:Panel>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_user" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="USER_ID" Theme="Aqua" 
                    onbeforecolumnsortinggrouping="ASPxGridView1_user_BeforeColumnSortingGrouping" 
                    onpageindexchanged="ASPxGridView1_user_PageIndexChanged" 
                    onhtmlrowprepared="ASPxGridView1_user_HtmlRowPrepared">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sửa" Width="50px">
                            <DataItemTemplate>
                                <a href="<%# getlink(Eval("USER_ID")) %>" title="Chỉnh sửa hồ sơ">
                                    <img src="/Images/file_edit.png" width="24" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center"/>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Họ và tên nhân viên" FieldName="USER_NAME" Width="160">
                            <DataItemTemplate>
                                <%# Eval("USER_NAME")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã nhân viên" FieldName="USER_UN" Width="110">
                            <DataItemTemplate>
                                <%# Eval("USER_UN")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Email" FieldName="USER_EMAIL" Width="200">
                            <DataItemTemplate>
                                <%# Eval("USER_EMAIL")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số điện thoại" FieldName="USER_PHONE" Width="110">
                            <DataItemTemplate>
                                <%# Eval("USER_PHONE")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Địa điểm" FieldName="USER_CHINHANH" Width="150">
                            <DataItemTemplate>
                                <%# getDiaDiem(Eval("USER_CHINHANH"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Chức vụ công tác" FieldName="GROUP_TYPE">
                            <DataItemTemplate>
                                <%# Getnhom(Eval("GROUP_TYPE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Trạng thái">
                            <DataItemTemplate>
                                <%# Getactive(Eval("USER_ACTIVE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <div class="ProjChildEmpl">
                                <table>
                                    <tr><td colspan="2" class="top">1. THÔNG TIN CHUNG</td></tr>
                                    <tr>
                                        <td>Giới tính:<b><%# getSex(Eval("USER_GIOITINH"))%></b></td>
                                        <td>Ngày sinh:<b><%# getDate(Eval("USER_NGAYSINH"))%></b></td>
                                    </tr>
                                    <tr>
                                        <td>Dân tộc:<b><%# Eval("USER_DANTOC")%></b></td>
                                        <td>Nguyên quán:<b><%# Eval("USER_NGUYENQUAN")%></b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Số CMND:<b><%# Eval("USER_CMND")%></b></td>
                                    </tr>
                                    <tr>
                                        <td>Ngày cấp:<b><%# getDate(Eval("USER_CMND_NGAYCAP"))%></b></td>
                                        <td>Nơi cấp:<b><%# Eval("USER_CMND_NOICAP")%></b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Nơi đăng ký hộ khẩu thường trú:<b><%# Eval("USER_NOIDK_HK")%></b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Trình độ:<b><%# Eval("USER_TRINHDO")%></b></td>
                                    </tr>
                                    <tr><td colspan="2" class="top">2. THÔNG TIN LIÊN HỆ</td></tr>
                                    <tr>
                                        <td>Email (công ty):<b><%# Eval("USER_EMAIL")%></b></td>
                                        <td>Email (cá nhân):<b><%# Eval("USER_EMAIL_CANHAN")%></b></td>
                                    </tr>
                                    <tr>
                                        <td>Điện thoại (công ty):<b><%# Eval("USER_PHONE")%></b></td>
                                        <td>Điện thoại (cá nhân):<b><%# Eval("USER_PHONE_CANHAN")%></b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Nơi ở hiện nay:<b><%# Eval("USER_ADDRESS")%></b></td>
                                    </tr>
                                    <tr>
                                        <td>Người thân:<b><%# Eval("NT_HOTEN")%></b></td>
                                        <td>Mối liên hệ:<b><%# Eval("NT_MOIQUANHE")%></b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Điện thoại (người thân):<b><%# Eval("NT_SDT")%></b></td>
                                    </tr>
                                </table>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail ShowDetailRow="True" />
                    <SettingsPager PageSize="80"></SettingsPager>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
