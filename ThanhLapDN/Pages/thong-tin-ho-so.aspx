<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="thong-tin-ho-so.aspx.cs" Inherits="ThanhLapDN.Pages.thong_tin_ho_so" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
        .td_left
        {
            width: 30%;
            padding-right: 20px;
            font-size:13px;
        }
        .td_right
        {
            font-weight:bold;
            font-size:13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <table width="60%" cellpadding="3" cellspacing="3" style="margin:auto;">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Thông tin hồ sơ">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" style="color:#333333 !important;">
                                        <tr>
                                            <td colspan="2"><asp:Label ID="lblDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr id="idMain1" runat="server">
                                            <td colspan="2" style="text-align:center;font-size:1.3em;">
                                                <p>Thông tin hồ sơ<asp:Literal ID="liMsgType3" runat="server"></asp:Literal></p>
                                                <hr style="border:1px solid #B8B8B8;width:80%;margin:auto;padding:0;"/>
                                            </td>
                                        </tr>
                                        <tr id="idMain2" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Loại hồ sơ
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblLoai" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idMain3" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Đính kèm File
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblDinhKem" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idMain4" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tổng phí
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblTongPhi" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idMain5" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Đã thu
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblDaThu" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idMain6" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Còn lại
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblConLai" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align:center;font-size:1.3em;">
                                                <p>Chi tiết hồ sơ</p>
                                                <hr style="border:1px solid #B8B8B8;width:80%;margin:auto;padding:0;"/>
                                            </td>
                                        </tr>
                                        <tr id="idNew0" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Loại hình đăng ký
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <dx:ASPxTreeList ID="ASPxTreeList_menu" runat="server" AutoGenerateColumns="False"
                                                    Width="400px" KeyFieldName="TYPE_ID" Theme="Aqua" 
                                                    ParentFieldName="TYPE_PARENT">
                                                    <Columns>
                                                        <dx:TreeListTextColumn Caption="TYPE_ID" FieldName="TYPE_ID" VisibleIndex="0" Visible="False">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Danh sách loại hình doanh nghiệp" FieldName="TYPE_NAME" VisibleIndex="2"
                                                            Width="95%">
                                                            <DataCellTemplate>
                                                                <%# Eval("TYPE_NAME")%>
                                                            </DataCellTemplate>
                                                        </dx:TreeListTextColumn>
                                                    </Columns>
                                                    <SettingsSelection AllowSelectAll="True" Enabled="True" Recursive="True" />
                                                </dx:ASPxTreeList>
                                            </td>
                                        </tr>
                                        <tr id="idNew1" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tên công ty
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lbltenCongTy" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idNew2" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tên Giao dịch(Nếu có)
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblTenGD" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idNew3" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tên Viết Tắt(Nếu có)
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblTenVT" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idNew4" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Trụ sở chính 
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblTruSoChinh" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idNew5" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Ngành nghề kinh doanh
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblNganhNgheKD" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idNew6" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tổng số vốn góp 
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblTongVopGop" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idNew7" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Vốn pháp định
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblVonPhapDinh" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idNew8" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Điện thoại liên hệ
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblDienThoai" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idNew9" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Danh sách thành viên
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Literal ID="liLoadMemLink" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr id="idCreate1" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tên công ty
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblCTenCongTy" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idCreate3" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Mã số thuế
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblCMST" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idCreate2" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Địa chỉ công ty
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblCDiaChiCty" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr id="idCreate4" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Số điện thoại
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblCDienThoai" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idCreate5" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Nội dung cần thay đổi
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <asp:Label ID="lblCNoiDungCanDoi" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
                    </LoadingPanelImage>
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                </dx:ASPxPageControl>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
     <!--Popup-->
    <link rel="stylesheet" href="/Styles/dhtmlwindow.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/dhtmlwindow.js"></script>
    <link rel="stylesheet" href="/Styles/modal.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/modal.js"></script>
    <script type="text/javascript">
        function openDSTV1(id, status) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-thanh-vien.aspx?id=' + id + '', 'Danh sách thành viên', 'width=450px,height=500px,center=1,resize=1,scrolling=1');
        }
    </script>
    <!--End Popup-->
</asp:Content>
