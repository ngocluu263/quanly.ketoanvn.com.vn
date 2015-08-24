<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="chi-tiet-bang-luong.aspx.cs" Inherits="ThanhLapDN.Pages.chi_tiet_bang_luong" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <style type="text/css">
        .td_left
        {
            width: 30%;
            padding-right: 50px;
        }
        .textbox
        {
            width: 400px;
        }
    </style>
    <script type="text/javascript">
        function FormatNumber(obj) {
            var strvalue;
            if (eval(obj))
                strvalue = eval(obj).value;
            else
                strvalue = obj;
            var num;
            num = strvalue.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                num.substring(num.length - (4 * i + 3));
            //return (((sign)?'':'-') + num); 
            eval(obj).value = (((sign) ? '' : '-') + num);
        }
    </script>
    <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            Chi tiết bảng lương
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveClose" ToolTip="Lưu và đóng" CssClass="k-button"
                runat="server" OnClick="lbtnSaveClose_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-saveclose.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnDelete" ToolTip="Xóa" CssClass="k-button" runat="server"
                OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnClose" ToolTip="Đóng" CssClass="k-button" runat="server"
                OnClick="lbtnClose_Click"><img alt="Đóng" src="../Images/icon-32-cancel.png" /></asp:LinkButton>
        </div>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Thông tin nhân viên">
                            <ContentCollection>
                                <dx:ContentControl ID="content01" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G2" />
                                                <asp:Label ID="Lberrors" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                Số ngày trong tháng&nbsp;<asp:TextBox ID="txtSoNgayTrongThang" runat="server" class="text" CssClass="k-textbox textbox" Width="50"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;Ngày công&nbsp;<asp:TextBox ID="TextBox1" runat="server" class="text" CssClass="k-textbox textbox" Width="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Tên nhân viên
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTenNV" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Mã nhân viên
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtMaNV" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Lương căn bản
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtLuongCanBan" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Thu nhập trước thuế
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtThuNhapTruocThue" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Tạm ứng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTamUng" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Thuế thu nhập
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtThueThuNhap" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>Phụ cấp</td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Thưởng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPCThuong" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Khác
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPCKhac" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>Các khoản cấn trừ người lao động</td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;BHXH
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCTBHXH" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;BHYT
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCTBHYT" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;BHTN
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCTBHTN" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Khác
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCTKhac" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Lương theo sản phẩm, dự án và tư vấn">
                            <ContentCollection>
                                <dx:ContentControl ID="content02" runat="server" SupportsDisabledAttribute="True">
                                    <table width="60%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4; margin:auto; border: 1px solid #aecaf0">
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="ASPxGridView1_request" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" KeyFieldName="ID" Theme="Aqua" 
                                                    onbeforecolumnsortinggrouping="ASPxGridView1_request_BeforeColumnSortingGrouping" 
                                                    onpageindexchanged="ASPxGridView1_request_PageIndexChanged" 
                                                    OnRowUpdating="ASPxGridView1_request_RowUpdating" 
                                                    OnRowDeleting="ASPxGridView1_request_RowDeleting">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" Width="20px">
                                                            <EditButton Text="Sửa" Visible="true"></EditButton>
                                                            <DeleteButton Text="Xóa" Visible="true"></DeleteButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="25px">
                                                            <DataItemTemplate>
                                                                <%# Container.ItemIndex + 1 %>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã số thuế" 
                                                            FieldName="MST">
                                                            <DataItemTemplate>
                                                                <%# Eval("MST")%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên đối tượng" 
                                                            FieldName="TEN_CTY">
                                                            <DataItemTemplate>
                                                                <%# Eval("TEN_CTY")%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Doanh thu" Width="90px"
                                                            FieldName="DOANH_THU">
                                                            <DataItemTemplate>
                                                                <%# Eval("DOANH_THU", "{0:###,##0}")%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tỷ lệ lương" Width="40px"
                                                            FieldName="TYLE_LUONG">
                                                            <DataItemTemplate>
                                                                <%# Eval("TYLE_LUONG")%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Lương theo sản phẩm" Width="90px"
                                                            FieldName="LUONG_SP">
                                                            <DataItemTemplate>
                                                                <%# Eval("LUONG_SP", "{0:###,##0}")%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Lương theo dự án" Width="90px"
                                                            FieldName="LUONG_DUAN_TV">
                                                            <DataItemTemplate>
                                                                <%# Eval("LUONG_DUAN_TV", "{0:###,##0}")%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="30"></SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Settings ShowTitlePanel="true"/>
                                                    <SettingsText Title="Danh sách lương sản phẩm, dự án và tư vấn"/>
                                                    <Styles>
                                                        <Header HorizontalAlign="Center" Wrap="True"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dx:ASPxGridView>
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
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px"></Border>
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
</asp:Content>
