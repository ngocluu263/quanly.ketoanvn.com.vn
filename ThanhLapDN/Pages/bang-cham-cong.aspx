<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="bang-cham-cong.aspx.cs" Inherits="ThanhLapDN.Pages.bang_cham_cong" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Bảng Chấm Công
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a onClick="openCreateCC(); return false" title="Tạo bảng chấm công" class="k-button">
                <img alt="Thêm mới" src="../Images/addNew.png" /></a>
            &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="confDelete(); return false"
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
        <label>Tháng</label>
        <asp:DropDownList ID="ddlThang" runat="server" Width="80" class="k-textbox k-input">
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
        <label>Năm</label>
        <asp:DropDownList ID="ddlNam" runat="server" Width="80" class="k-textbox k-input">
            <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
            <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
            <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
            <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
            <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
            <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
        </asp:DropDownList>
        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
            OnClick="lbtnSearch_Click"><img alt="Tìm kiếm" src="../Images/search.png" /></asp:LinkButton>    
    </asp:Panel>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <table id="CPHMain_grvChamCong_DXTitle" cellspacing="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
			        <tbody>
                        <tr>
				            <td class="dxgvTitlePanel_Aqua" style="font-size:15pt;font-weight:bold;">
                                BẢNG CHẤM CÔNG THÁNG 
                                <asp:Label ID="lblThang" runat="server"></asp:Label> NĂM <asp:Label ID="lblNam" runat="server" Text=""></asp:Label></td>
			            </tr>
		            </tbody>
                </table>
                <dx:ASPxGridView ID="grvChamCong" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="USER_ID" Theme="Aqua" 
                    onbeforecolumnsortinggrouping="grvChamCong_BeforeColumnSortingGrouping" 
                    onpageindexchanged="grvChamCong_PageIndexChanged" 
                    onhtmlrowprepared="grvChamCong_HtmlRowPrepared">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="30">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Họ và tên nhân viên" FieldName="USER_NAME">
                            <DataItemTemplate>
                                <%# Eval("USER_NAME")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã chấm công" FieldName="USER_MACC">
                            <DataItemTemplate>
                                <%# Eval("USER_MACC")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số ngày nghỉ" FieldName="USER_MACC">
                            <DataItemTemplate>
                                <%# getDayoff(Eval("USER_MACC"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số phút đi trễ" FieldName="USER_MACC">
                            <DataItemTemplate>
                                <%# getMinuLateSum(Eval("USER_MACC"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tình trạng" FieldName="USER_MACC">
                            <DataItemTemplate>
                                <%# getStatus(Eval("USER_MACC"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Styles>
                        <Header Font-Bold="True"></Header>
                    </Styles>
                    <Templates>
                        <DetailRow>
                            <table class="ProjChildCC">
                                <tr>
                                    <td>Ngày</td>
                                    <td>Thứ</td>
                                    <td>Vào</td>
                                    <td>Ra</td>
                                    <td>Số phút trể</td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# listChamCong(Eval("USER_MACC"))%>'>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("CC_NGAY","{0:dd/MM/yyyy}")%></td>
                                            <td><%# Eval("CC_THU") %></td>
                                            <td><%# getTime(Eval("CC_VAO"))%></td>
                                            <td><%# getTime(Eval("CC_RA"))%></td>
                                            <td><%# getMinuLate(Eval("CC_SOPHUTTRE"))%></td>
                                        </tr>
                                        <tr><td colspan="5" style="width:100%;border-bottom:1px dotted #E6E6E6;"></td></tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail ShowDetailRow="True" />
                    <SettingsPager PageSize="80"></SettingsPager>
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
        function openCreateCC() {
            _year = $("#CPHMain_ddlNam").attr('value');
            _month = $("#CPHMain_ddlThang").attr('value');
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-tao-bang-cham-cong.aspx?year=' + _year + '&month=' + _month + '', 'Tạo bảng lương', 'width=450px,height=230px,center=1,resize=1,scrolling=1');
        }

        function confDelete() {
            _year = document.getElementById('CPHMain_lblNam').innerText;
            _month = document.getElementById('CPHMain_lblThang').innerText;
            return confirm('Hệ thống sẽ xóa bảng chấm công tháng ' + _month + ' năm ' + _year + ' của nhân viên này?');
        }
    </script>
    <!--End Popup-->
</asp:Content>
