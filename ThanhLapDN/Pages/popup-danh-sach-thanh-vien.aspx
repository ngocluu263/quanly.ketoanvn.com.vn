<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-danh-sach-thanh-vien.aspx.cs" Inherits="ThanhLapDN.Pages.popup_danh_sach_thanh_vien" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>
        .tbox
        {
            border-radius: 4px;
            border: 1px solid #7ec6e3;
            height: 18px;
            width:98%;
            padding: 2px 0px 2px 5px;
            margin-top:2px;
            margin-bottom: 2px;
            }
        .tdl
        {
            width:150px;
            text-align:right;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;"> 
        <asp:LinkButton ID="btnSave" runat="server" onclick="btnSave_Click"><img src="../Images/icon-32-save.png" title="Lưu"/></asp:LinkButton>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        <asp:RadioButtonList ID="rdbType" runat="server" RepeatColumns="2" style="margin:auto;"
            AutoPostBack="True" onselectedindexchanged="rdbType_SelectedIndexChanged">
            <asp:ListItem Value="0" Text="Người đại diện"></asp:ListItem>
            <asp:ListItem Value="1" Text="Tên thành viên" Selected="True"></asp:ListItem>
        </asp:RadioButtonList>
        <table>
            <tr>
                <td class="tdl">Họ và tên</td>
                <td>
                    <asp:TextBox ID="txtHovaTen" runat="server" class="tbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Xin nhập họ và tên"
                        ControlToValidate="txtHovaTen" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdl">Ngày sinh</td>
                <td>
                    <uc1:pickerAndCalendar ID="pickdate_deli" runat="server" />
                    <asp:Label ID="lblBirthday" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdl">Dân tộc</td>
                <td>
                    <asp:TextBox ID="txtDanToc" runat="server" class="tbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Xin nhập dân tộc"
                        ControlToValidate="txtDanToc" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdl">Giới tính</td>
                <td>
                    <asp:RadioButtonList ID="rdbGioiTinh" runat="server" RepeatColumns="2">
                        <asp:ListItem Value="1" Text="Nam" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="0" Text="Nữ"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="tdl">Quốc tịch</td>
                <td>
                    <asp:TextBox ID="txtQuocTich" runat="server" class="tbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Xin nhập quốc tịch"
                        ControlToValidate="txtQuocTich" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdl">CMND</td>
                <td>
                    <asp:TextBox ID="txtCMND" runat="server" class="tbox" AutoPostBack="True" 
                        ontextchanged="txtCMND_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Xin nhập CMND"
                        ControlToValidate="txtCMND" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdl">Ngày cấp (CMND)</td>
                <td>
                    <uc1:pickerAndCalendar ID="pickdate_CMND" runat="server" />
                    <asp:Label ID="lblNgayCMND" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdl">Nơi cấp (CMND)</td>
                <td>
                    <asp:TextBox ID="txtNoiCapCMND" runat="server" class="tbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Xin nhập nơi cấp CMND"
                        ControlToValidate="txtNoiCapCMND" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdl">Nơi thường trú hộ khẩu</td>
                <td>
                    <asp:TextBox ID="txtHKTT" runat="server" class="tbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Xin nhập Hộ khẩu thường trú"
                        ControlToValidate="txtHKTT" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdl">Hiện tạm trú tại</td>
                <td>
                    <asp:TextBox ID="txtTamTru" runat="server" class="tbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Xin nhập địa chỉ"
                        ControlToValidate="txtTamTru" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdl">Vốn góp</td>
                <td>
                    <asp:TextBox ID="txtVonGop" class="tbox" runat="server" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Xin nhập vốn góp"
                        ControlToValidate="txtVonGop" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdl">Tỷ lệ %</td>
                <td>
                    <asp:TextBox ID="txtTyLe" class="tbox" runat="server" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Xin nhập tỷ lệ"
                        ControlToValidate="txtTyLe" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="iChucDanh" runat="server">
                <td class="tdl">Chức danh ĐDPL</td>
                <td>
                    <asp:TextBox ID="txtChucDanh" runat="server" class="tbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Xin nhập chức danh đại diện PL"
                        ControlToValidate="txtChucDanh" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="iNguoiGiuChucDanh" runat="server">
                <td class="tdl">Người giữ chức danh còn lại</td>
                <td>
                    <asp:TextBox ID="txtNguoiGiuChucConLai" runat="server" class="tbox"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
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
    </form>
</body>
</html>

