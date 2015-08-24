<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="chi-tiet-nhan-vien.aspx.cs" Inherits="ThanhLapDN.Pages.chi_tiet_nhan_vien" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <style type="text/css">
        .td_left
        {
            width: 30%;
            padding-right: 25px;
            padding-top: 8px;
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
            Chi tiết nhân viên
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveClose" ToolTip="Lưu và đóng" CssClass="k-button"
                runat="server" OnClick="lbtnSaveClose_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-saveclose.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveNew" ToolTip="Lưu và thêm mới" CssClass="k-button"
                runat="server" OnClick="lbtnSaveNew_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save-new.png" /></asp:LinkButton>
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
                                                <span style="color: Red;">*</span>&nbsp;Địa điểm
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="ddlChiNhanh" runat="server" CssClass="k-textbox textbox">
                                                    <asp:ListItem Text="---Chọn địa điểm---" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Tp.HCM - Trụ sở chính"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Hà Nội - Chi nhánh"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Nha Trang - Chi nhánh"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Đà Nẵng - Chi nhánh"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Xin chọn chi nhánh"
                                                    ControlToValidate="ddlChiNhanh" Display="Dynamic" ForeColor="Red" ValidationGroup="G2" InitialValue="0"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Chức vụ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="Drgroup" runat="server" CssClass="k-textbox textbox"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Xin chọn chức vụ"
                                                    ControlToValidate="Drgroup" Display="Dynamic" ForeColor="Red" ValidationGroup="G2" InitialValue="0"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Họ và tên
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtname" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập họ tên"
                                                    ControlToValidate="Txtname" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Mã nhân viên
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtusername" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập mã nhân viên"
                                                    ControlToValidate="Txtusername" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Mã chấm công
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtMaCC" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Giới tính
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:RadioButtonList ID="rdoGioiTinh" runat="server" RepeatColumns="2">
                                                    <asp:ListItem Value="1" Text="Nam" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Nữ"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Ngày/tháng/năm sinh
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNgaySinh" runat="server" placeholder="dd/MM/yyyy" CssClass="k-textbox textbox" Width="150px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNgaySinh" 
                                                    ErrorMessage="Ngày sinh định dạng không đúng!" ValidationGroup="G2" Text="*"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                                                    ForeColor="Red">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Số CMND
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCMND" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Ngày cấp (CMND)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNgayCapCMND" runat="server" placeholder="dd/MM/yyyy" CssClass="k-textbox textbox" Width="150px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayCapCMND" 
                                                    ErrorMessage="Ngày sinh định dạng không đúng!" ValidationGroup="G2" Text="*"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                                                    ForeColor="Red">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Nơi cấp (CMND)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNoiCapCMND" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Dân tộc
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtDanToc" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Nguyên quán
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNguyenQuan" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Nơi đăng ký hộ khẩu thường trú
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNoiDK_HK" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Nơi ở hiện nay
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtaddress" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Email (Công ty)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtemail" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Chưa nhập địa chỉ email (công ty)"
                                                    ControlToValidate="Txtemail" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Email (Cá nhân)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtEmail_CaNhan" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Số điện thoại (Công ty)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtphone" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa nhập số điện thoại (công ty)"
                                                    ControlToValidate="Txtphone" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Số điện thoại (Cá nhân)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPhone_CaNhan" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Trình độ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTrinhDo" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>Thông tin người thân liên hệ khi có việc gấp:</td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Họ và tên (người thân)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNT_HoTen" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Số điện thoại (người thân)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNT_SDT" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Mối quan hệ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNT_MoiQuanHe" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Tài khoản truy cập">
                            <ContentCollection>
                                <dx:ContentControl ID="content02" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G3" />
                                                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Tên đăng nhập
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTenDangNhap" runat="server" class="text" CssClass="k-textbox textbox" Enabled="false" BackColor="#E6E6E6"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Mật khẩu
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtpass" runat="server" class="text" CssClass="k-textbox textbox" TextMode="Password" placeholder="******"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Nhập lại mật khẩu
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtrepass" runat="server" class="text" CssClass="k-textbox textbox" TextMode="Password" placeholder="******"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Trạng thái
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rblActive" runat="server" RepeatColumns="5">
                                                    <asp:ListItem Selected="True" Text="Kích hoạt" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Tạm khóa" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Chính sách lương và bảo hiểm">
                            <ContentCollection>
                                <dx:ContentControl ID="content03" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G3" />
                                                <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Chế độ bảo hiểm
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:RadioButtonList ID="rdoCoBH" runat="server" RepeatColumns="2"
                                                    AutoPostBack="True" OnSelectedIndexChanged="rdoCoBH_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Không" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Có"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="pHolderBH" runat="server">
                                        <tr>
                                            <td></td>
                                            <td>Các khoản cấn trừ người lao động</td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Lương bảo hiểm cố định
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtLuongBHCD" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);"></asp:TextBox>
                                                <asp:LinkButton ID="lbtnTinhPhiBH" runat="server" OnClick="lbtnTinhPhiBH_Click">Tính phí bảo hiểm</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;BHXH
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPT_BHXH" runat="server" style="width:30px;background-color:#FFE0C2;" class="text" 
                                                    CssClass="k-textbox textbox" Text="8" AutoPostBack="True" OnTextChanged="txtPT_BHXH_TextChanged"></asp:TextBox>%&nbsp;
                                                <asp:TextBox ID="txtCTBHXH" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);" style="width:350px;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;BHYT
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPT_BHYT" runat="server" style="width:30px;background-color:#FFE0C2;" class="text" 
                                                    CssClass="k-textbox textbox" Text="1.5" AutoPostBack="True" OnTextChanged="txtPT_BHYT_TextChanged"></asp:TextBox>%&nbsp;
                                                <asp:TextBox ID="txtCTBHYT" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);" style="width:350px;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;BHTN
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPT_BHTN" runat="server" style="width:30px;background-color:#FFE0C2;" class="text" 
                                                    CssClass="k-textbox textbox" Text="1" AutoPostBack="True" OnTextChanged="txtPT_BHTN_TextChanged"></asp:TextBox>%&nbsp;
                                                <asp:TextBox ID="txtCTBHTN" runat="server" class="text" CssClass="k-textbox textbox" placeholder="vnđ" onkeyup="FormatNumber(this);" style="width:350px;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        </asp:PlaceHolder>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
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
