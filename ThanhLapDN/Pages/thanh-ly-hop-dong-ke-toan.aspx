<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="thanh-ly-hop-dong-ke-toan.aspx.cs" Inherits="ThanhLapDN.Pages.thanh_ly_hop_dong_ke_toan" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .textDoc
        {
            border: none;
            text-align: center;
            background-color: #CCFFCC;
            font-size: 16px;
            font-family: Times New Roman;
            }
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
        p.MsoNormal
	    {margin-bottom:.0001pt;
	    font-size:12.0pt;
	    font-family:"Times New Roman","serif";
	         margin-left: 0in;
             margin-right: 0in;
             margin-top: 0in;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            Thanh lý hợp đồng kế toán
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G112"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            <%--&nbsp;<asp:LinkButton ID="lbtnSaveClose" ToolTip="Lưu và đóng" CssClass="k-button"
                runat="server" OnClick="lbtnSaveClose_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-saveclose.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveNew" ToolTip="Lưu và thêm mới" CssClass="k-button"
                runat="server" OnClick="lbtnSaveNew_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save-new.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnDelete" ToolTip="Xóa" CssClass="k-button" runat="server"
                OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>--%>
            &nbsp;<asp:LinkButton ID="lbtnClose" ToolTip="Đóng" CssClass="k-button" runat="server"
                OnClick="lbtnClose_Click"><img alt="Đóng" src="../Images/icon-32-cancel.png" /></asp:LinkButton>
        </div>
    </div>
    <table width="60%" cellpadding="3" cellspacing="3" style="margin:auto;">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Thanh lý hợp đồng kế toán">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" style="color:#333333 !important;">
                                        <tr>
                                            <td colspan="2"><asp:Label ID="lblDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="td_left">
                                                
                                            </td>
                                            <td class="td_right">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                
                                                <p align="center" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                    <span lang="NL" style="mso-ansi-language:
NL">CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM<p></p>
                                                    </span></b>
                                                </p>
                                                <p align="center" class="MsoNormal">
                                                    <span lang="EN-US">Độc lập - Tự do - Hạnh phúc</span></p>
                                                <p align="center" class="MsoNormal">
                                                    <span lang="EN-US">----o0o-----</span></p>
                                                <p align="center" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal"><span lang="EN-US">
                                                    <p>&nbsp;</p>
                                                    </span></b>
                                                </p>
                                                <p align="center" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal"><span lang="EN-US" 
                                                        style="font-size:18.0pt">BIÊN BẢN THANH LÝ HỢP ĐỒNG<span 
                                                        style="mso-spacerun:yes">&nbsp; </span>
                                                    <p></p>
                                                    </span></b>
                                                </p>
                                                <p align="center" class="MsoNormal">
                                                    <span lang="EN-US">
                                                    <p>&nbsp;</p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">-
                                                    <span style="letter-spacing:
-.1pt">Căn cứ hợp đồng số <asp:TextBox ID="txtMerPos01" runat="server" Width="80" class="textDoc" placeholder="dd.MM.yy"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="G112"
                                                                    ErrorMessage="Số hợp đồng phải đúng định dạng (vd: 25.08.15)" ControlToValidate="txtMerPos01"
                                                                    ValidationExpression="[0-9][0-9].[0-9][0-9].[0-9][0-9]" ForeColor="Red" Display="Dynamic">*</asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Xin nhập số hợp đồng"
                                                                    ControlToValidate="txtMerPos01" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span><span style="font-size:13.0pt;
line-height:130%;mso-ansi-language:VI">/HĐTV-KL/</span><span style="font-size:
13.0pt;line-height:130%;letter-spacing:-.1pt"> <span lang="EN-US"><asp:TextBox ID="txtMerPos02" runat="server" Width="90" class="textDoc" placeholder="KL-XXX"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Xin nhập số hợp đồng"
                                                                    ControlToValidate="txtMerPos02" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span><span style="font-size:13.0pt;line-height:130%;letter-spacing:-.1pt;mso-ansi-language:
VI"> ký ngày </span><span lang="EN-US" style="font-size:13.0pt;line-height:130%;letter-spacing:-.1pt">
    <asp:TextBox ID="txtMerPos03" runat="server" Width="30" class="textDoc" placeholder="dd"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Xin nhập ngày ký hợp đồng"
        ControlToValidate="txtMerPos03" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
        CssClass="tlp-error">*</asp:RequiredFieldValidator>
</span><span style="font-size:13.0pt;line-height:
130%;letter-spacing:-.1pt;mso-ansi-language:VI">/</span><span style="font-size:13.0pt;line-height:130%;letter-spacing:-.1pt"> <span lang="EN-US">
<asp:TextBox ID="txtMerPos04" runat="server" Width="30" class="textDoc" placeholder="MM"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Xin nhập tháng ký hợp đồng"
    ControlToValidate="txtMerPos04" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
    CssClass="tlp-error">*</asp:RequiredFieldValidator>
</span></span><span style="font-size:13.0pt;line-height:130%;letter-spacing:-.1pt;mso-ansi-language:
VI">/</span><span style="font-size:13.0pt;line-height:130%;letter-spacing:-.1pt"> <span lang="EN-US">
<asp:TextBox ID="txtMerPos05" runat="server" Width="40" class="textDoc" placeholder="yyyy"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Xin nhập năm ký hợp đồng"
    ControlToValidate="txtMerPos05" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
    CssClass="tlp-error">*</asp:RequiredFieldValidator>
</span></span><span style="font-size:13.0pt;
line-height:130%;letter-spacing:-.1pt;mso-ansi-language:VI"> giữa Công ty TNHH TV TM Khánh Linh và Công Ty </span>
                                                    <span lang="EN-US" style="font-size:13.0pt;
line-height:130%;letter-spacing:-.1pt"><asp:TextBox ID="txtMerName1" runat="server" Width="350" Height="24" class="textDoc" style="text-align:left;"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Xin nhập tên công ty"
                                                                                ControlToValidate="txtMerName1" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span><span style="font-size:
13.0pt;line-height:130%;letter-spacing:-.1pt;mso-ansi-language:VI"> về việc thực hiện dịch vụ kế toán thuế</span><span style="font-size:13.0pt;line-height:
130%;mso-ansi-language:VI"><p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">- Căn cứ nhu cầu và 
                                                    khả năng của 2 đơn vị<p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">Hôm nay, ngày 
                                                    <asp:TextBox ID="txtMerDayNow" runat="server" Width="30" class="textDoc" placeholder="dd"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Xin nhập ngày hiện tại"
    ControlToValidate="txtMerDayNow" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
    CssClass="tlp-error">*</asp:RequiredFieldValidator> tháng <asp:TextBox ID="txtMerMonthNow" runat="server" Width="30" class="textDoc" placeholder="MM"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Xin nhập tháng hiện tại"
    ControlToValidate="txtMerMonthNow" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
    CssClass="tlp-error">*</asp:RequiredFieldValidator> năm <asp:TextBox ID="txtMerYearNow" runat="server" Width="40" class="textDoc" placeholder="yyyy"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Xin nhập năm hiện tại"
    ControlToValidate="txtMerYearNow" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
    CssClass="tlp-error">*</asp:RequiredFieldValidator>, chúng tôi gồm:<p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <b><u><span lang="EN-US" style="font-size:13.0pt;line-height:130%">BÊN A :</span></u><span 
                                                        lang="EN-US" style="font-size:13.0pt;line-height:130%"><span 
                                                        style="mso-spacerun:yes">&nbsp; </span></span></b>
                                                    <b style="mso-bidi-font-weight:
normal"><span lang="EN-US" style="font-size:13.0pt;line-height:130%;letter-spacing:
-.1pt"><asp:TextBox ID="txtMerName2" runat="server" Width="500" Height="28" class="textDoc" style="text-transform:uppercase;"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Xin nhập tên công ty"
                                                                                ControlToValidate="txtMerName2" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span></b><span lang="EN-US" style="font-size:13.0pt;line-height:
130%"><p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;
line-height:130%">Địa chỉ<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: <asp:TextBox ID="txtMerAddress" runat="server" Width="480" class="textDoc"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Xin nhập địa chỉ"
                                                                                ControlToValidate="txtMerAddress" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator><p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;
line-height:130%">Điện thoại : <asp:TextBox ID="txtMerPhone" runat="server" Width="480px" class="textDoc"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Xin nhập số điện thoại"
                                                                                ControlToValidate="txtMerPhone" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span><p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="FR" style="font-size:13.0pt;line-height:130%;mso-ansi-language:FR">
                                                    Mã số thuế: <asp:TextBox ID="txtMerTaxCode" runat="server" 
                                                                            Width="210px" class="textDoc"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Xin nhập mã số thuế"
                                                                                ControlToValidate="txtMerTaxCode" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator> <span style="mso-tab-count:1">
                                                    </span>Email : <asp:TextBox ID="txtMerEmail" runat="server" Width="250" class="textDoc"></asp:TextBox><p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="FR" style="font-size:13.0pt;line-height:130%;mso-ansi-language:FR">
                                                    Đại diện<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp; </span>:<b style="mso-bidi-font-weight:
normal"> </b>
                                                                            <asp:DropDownList ID="ddlDanhXung" runat="server" Width="55" Height="22" class="textDoc ">
                                                                                <asp:ListItem Value="0" Text="---" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="Ông" Text="Ông"></asp:ListItem>
                                                                                <asp:ListItem Value="Bà" Text="Bà"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Xin chọn danh xưng người đại diện"
                                                                                ControlToValidate="ddlDanhXung" Display="Dynamic" ForeColor="Red" ValidationGroup="G112" InitialValue="0"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator>
<asp:TextBox ID="txtMerRepresent" runat="server" Width="150" class="textDoc"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Xin nhập người đại diện"
                                                                                ControlToValidate="txtMerRepresent" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator><b style="mso-bidi-font-weight:normal"><span style="mso-tab-count:1">
                                                    </span></b>Chức vụ : <asp:TextBox ID="txtMerPosition" runat="server" Width="235" class="textDoc"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Xin nhập chức vụ"
                                                                                ControlToValidate="txtMerPosition" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator><p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <b><u><span lang="EN-US" style="font-size:13.0pt;line-height:130%">BÊN B :</span></u><span 
                                                        lang="EN-US" style="font-size:13.0pt;line-height:130%"><span 
                                                        style="mso-spacerun:yes">&nbsp; </span>CÔNG TY TNHH TƯ VẤN THƯƠNG MẠI KHÁNH LINH<p></p>
                                                    </span></b>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">Địa chỉ<span 
                                                        style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: 232/17 Cộng Hoà, Phường 12, Quận Tân 
                                                    Bình, Tp HCM.<p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">Điện thoại :
                                                    <span style="mso-bidi-font-weight:
bold">08.3811 6723 – 093 30 30 678<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>Fax : 08.3811 6791</span><p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">Mã số thuế: 
                                                    0307633556<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </span>Email : info@ketoanvn.com.vn<p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">Đại diện<span 
                                                        style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp; </span>: <b>Bà Đào Cao Bích Ngọc</b><span 
                                                        style="mso-tab-count:1">&nbsp;&nbsp; </span><span style="mso-spacerun:yes">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>Chức vụ : Giám Đốc
                                                    <p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">Sau khi xem xét 
                                                    tình hình thực hiện hợp đồng và đối chiếu công nợ, hai bên cùng đồng ý thanh lý 
                                                    hợp đồng với các điều sau:<p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:
normal"><u><span lang="EN-US" style="font-size:13.0pt;line-height:130%">Điều 1</span></u></b><b style="mso-bidi-font-weight:normal"><span 
                                                        lang="EN-US" style="font-size:13.0pt;
line-height:130%">:</span></b><span lang="EN-US" style="font-size:13.0pt;
line-height:130%"> Bên A đồng ý chấm dứt hợp đồng đã ký kết với bên B kể từ ngày <asp:TextBox ID="txtMerDayEnd" runat="server" Width="30" class="textDoc" placeholder="dd"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Xin nhập ngày kết thúc HĐ"
    ControlToValidate="txtMerDayEnd" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
    CssClass="tlp-error">*</asp:RequiredFieldValidator>/<asp:TextBox ID="txtMerMonthEnd" runat="server" Width="30" class="textDoc" placeholder="MM"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Xin nhập tháng kết thúc HĐ"
    ControlToValidate="txtMerMonthEnd" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
    CssClass="tlp-error">*</asp:RequiredFieldValidator>/<asp:TextBox ID="txtMerYearEnd" runat="server" Width="40" class="textDoc" placeholder="yyyy"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Xin nhập năm kết thúc HĐ"
    ControlToValidate="txtMerYearEnd" Display="Dynamic" ForeColor="Red" ValidationGroup="G112"
    CssClass="tlp-error">*</asp:RequiredFieldValidator><p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:
normal"><u><span lang="EN-US" style="font-size:13.0pt;line-height:130%">Điều 2</span></u></b><b style="mso-bidi-font-weight:normal"><span 
                                                        lang="EN-US" style="font-size:13.0pt;
line-height:130%">:</span></b><span lang="EN-US" style="font-size:13.0pt;
line-height:130%"> <span style="letter-spacing:-.3pt">Bên A đã thanh toán cho bên B đầy đủ chi phí dịch vụ tính đến lúc thanh lý hợp 
                                                    đồng
                                                    <p></p>
                                                    </span></span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:
normal"><u><span lang="EN-US" style="font-size:13.0pt;line-height:130%">Điều 3</span></u></b><b style="mso-bidi-font-weight:normal"><span 
                                                        lang="EN-US" style="font-size:13.0pt;
line-height:130%">:</span></b><span lang="EN-US" style="font-size:13.0pt;
line-height:130%"> Bên B có trách nhiệm bàn giao đầy đủ các chứng từ liên quan cho bên A để báo cáo quyết toán cơ quan quản lý.<p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt;line-height:130%">Biên bản thanh lý 
                                                    được lập thành 2 bản, mỗi bên giữ 1 bản, có giá trị ngang nhau<p></p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt">
                                                    <p>&nbsp;</p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal"><span lang="EN-US" 
                                                        style="font-size:13.0pt"><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>ĐẠI 
                                                    DIỆN BÊN A<span style="mso-tab-count:
4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>ĐẠI DIỆN BÊN 
                                                    B<p></p>
                                                    </span></b>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt">
                                                    <p>&nbsp;</p>
                                                    </span>
                                                </p>
                                                <p class="MsoNormal">
                                                    <span lang="EN-US" style="font-size:13.0pt"><span style="mso-spacerun:yes">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
                                                    <p></p>
                                                    </span>
                                                </p>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;</td>
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="G112" />
</asp:Content>
