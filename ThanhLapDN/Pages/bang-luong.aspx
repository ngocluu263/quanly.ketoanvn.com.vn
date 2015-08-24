<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="bang-luong.aspx.cs" Inherits="ThanhLapDN.Pages.bang_luong" %>
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
            Danh Sách Bảng Lương
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a onClick="openCreateBL(); return false" title="Tạo bảng lương" class="k-button">
                <img alt="Thêm mới" src="../Images/addNew.png" /></a> &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
        </div>
        <div style="clear: both"></div>
    </div>
    <div id="div-search">
    <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSearch">
        <table style="width:55%;">
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
                            <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                            <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                            <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                            <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                            <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                            <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="border-left: 1px solid gray;padding-left: 5px;">
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><img alt="Tìm kiếm" src="../Images/search.png" /></asp:LinkButton>
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
                    ondatabound="ASPxGridView1_project_DataBound" 
                    onheaderfilterfillitems="ASPxGridView1_project_HeaderFilterFillItems">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" FixedStyle="Left"
                            Width="25px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="0" Caption="Chi tiết" FixedStyle="Left" Width="50px">
                            <DataItemTemplate>
                                <a href="/Pages/chi-tiet-bang-luong.aspx?id=<%# Eval("ID")%>" title="Chi tiết lương">
                                    <img src="/Images/file_edit.png" width="24" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Phòng Ban" FieldName="BL_PHONGBAN" FixedStyle="Left" Width="150px">
                            <DataItemTemplate>
                                <%# getGroupName(Eval("BL_PHONGBAN"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Họ tên nhân viên" FieldName="BL_TENNV" FixedStyle="Left" Width="170px">
                            <DataItemTemplate>
                                <%# Eval("BL_TENNV")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" Caption="Mã nhân viên" FieldName="BL_MANV" FixedStyle="Left" Width="100px">
                            <DataItemTemplate>
                                <%# Eval("BL_MANV")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" Caption="Doanh thu sản phẩm, DV" FieldName="BL_DT_SPDV" Width="110px">
                            <DataItemTemplate>
                                <%# Eval("BL_DT_SPDV", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" Caption="Lương căn bản" FieldName="BL_LUONG_CB" Width="110px">
                            <DataItemTemplate>
                                <%# Eval("BL_LUONG_CB", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Lương sản phẩm" FieldName="BL_LUONG_SP" Width="110px">
                            <DataItemTemplate>
                                <%# Eval("BL_LUONG_SP", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" Caption="Lương dự án, tư vấn" FieldName="BL_LUONG_DA_TV" Width="110px">
                            <DataItemTemplate>
                                <%# Eval("BL_LUONG_DA_TV", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewBandColumn Caption="Phụ cấp" VisibleIndex="7">
                            <Columns>               
                                <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Thưởng" Width="110px" FieldName="BL_PC_THUONG">
                                    <DataItemTemplate>
                                        <%# Eval("BL_PC_THUONG", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="2" Caption="Khác" Width="110px" FieldName="BL_PC_KHAC">
                                    <DataItemTemplate>
                                        <%# Eval("BL_PC_KHAC", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="Các khoản khấu trừ người lao động" VisibleIndex="8">
                            <Columns>               
                                <dx:GridViewDataTextColumn VisibleIndex="1" Caption="BHXH" Width="110px" FieldName="BL_BHXH">
                                    <DataItemTemplate>
                                        <%# Eval("BL_BHXH", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="2" Caption="BHYT" Width="110px" FieldName="BL_BHYT">
                                    <DataItemTemplate>
                                        <%# Eval("BL_BHYT", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" Caption="BHTN" Width="110px" FieldName="BL_BHTN">
                                    <DataItemTemplate>
                                        <%# Eval("BL_BHTN", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="4" Caption="Khác" Width="110px" FieldName="BL_KHAUTRU_KHAC">
                                    <DataItemTemplate>
                                        <%# Eval("BL_KHAUTRU_KHAC", "{0:###,##0}")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Thu nhập trước thuế" Width="110px" FieldName="BL_THUNHAP_TRUOCTHUE">
                            <DataItemTemplate>
                                <%# Eval("BL_THUNHAP_TRUOCTHUE", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="10" Caption="Tạm ứng" Width="110px" FieldName="BL_TAMUNG">
                            <DataItemTemplate>
                                <%# Eval("BL_TAMUNG", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="11" Caption="Thuế thu nhập" Width="110px" FieldName="BL_THUE_THUNHAP">
                            <DataItemTemplate>
                                <%# Eval("BL_THUE_THUNHAP", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="12" Caption="Lương thực nhận" Width="110px" FieldName="BL_LUONG_THUCNHAN">
                            <DataItemTemplate>
                                <%# Eval("BL_LUONG_THUCNHAN", "{0:###,##0}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="Bảng lương tháng này chưa được khởi tạo!" />
                    <SettingsPager PageSize="30"></SettingsPager>
                    <Settings showfilterrow="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="True"
                        showverticalscrollbar="True" verticalscrollableheight="400"/>
                    <Styles>
                        <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True"></Header>
                    </Styles>
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
        function openCreateBL() {
            _year = $("#CPHMain_ddlNam").attr('value');
            _month = $("#CPHMain_ddlThang").attr('value');
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-tao-bang-luong.aspx?year=' + _year + '&month=' + _month + '', 'Tạo bảng lương', 'width=450px,height=230px,center=1,resize=1,scrolling=1');
        }
    </script>
    <!--End Popup-->
</asp:Content>