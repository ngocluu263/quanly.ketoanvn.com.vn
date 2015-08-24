<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="hop-dong-ke-toan.aspx.cs" Inherits="ThanhLapDN.Pages.hop_dong_ke_toan" %>
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
            Hợp đồng kế toán
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G111"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
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
                        <dx:TabPage Text="Hợp đồng kế toán">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" style="color:#333333 !important;">
                                        <tr>
                                            <td colspan="2"><asp:Label ID="lblDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <%--<asp:CheckBox ID="chkView" runat="server" Text="Theo dõi không xử lý"/>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                
                                                <p align="center" class="MsoNormal" style="margin-left:-4.5pt;text-align:center;page-break-after:avoid;mso-outline-level:2;tab-stops:center 243.0pt">
                                                    <b>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT
                                                    <country-region w:st="on"><place w:st="on">NAM</place></country-region>
                                                    <p></p>
                                                    </b>
                                                    <p>
                                                    </p>
                                                    <p align="center" class="MsoNormal" style="margin-left:-4.5pt;text-align:center;
tab-stops:center 3.25in">
                                                        Độc Lập - Tự Do - Hạnh Phúc</p>
                                                    <p align="center" class="MsoNormal" style="margin-left:-4.5pt;text-align:center;
tab-stops:center 3.25in">
                                                        * * * * * * *</p>
                                                    <p align="center" class="MsoNormal" style="margin-left:-4.5pt;text-align:center;
tab-stops:center 3.25in">
                                                        <p>
                                                            &nbsp;</p>
                                                        <p>
                                                        </p>
                                                        <p align="center" class="MsoNormal" style="margin-top:0in;margin-right:0in;
margin-bottom:6.0pt;margin-left:-4.5pt;text-align:center;page-break-after:avoid;
mso-outline-level:2">
                                                            <b><span style="font-size:16.0pt;mso-bidi-font-size:12.0pt">HỢP ĐỒNG DỊCH VỤ<p>
                                                            </p>
                                                            </span></b>
                                                            <p>
                                                            </p>
                                                            <p align="center" class="MsoNormal" 
                                                                style="margin-left:-4.5pt;text-align:center">
                                                                Số:
                                                                <span style="mso-field-code:&quot; MERGEFIELD  MerPos01  \\* MERGEFORMAT &quot;">
                                                                <span style="mso-no-proof:yes">
                                                                <asp:TextBox ID="txtMerPos01" runat="server" Width="80" class="textDoc" placeholder="dd.MM.yy"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="G111"
                                                                    ErrorMessage="Số hợp đồng phải đúng định dạng (vd: 25.08.15)" ControlToValidate="txtMerPos01"
                                                                    ValidationExpression="[0-9][0-9].[0-9][0-9].[0-9][0-9]" ForeColor="Red" Display="Dynamic">*</asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Xin nhập số hợp đồng"
                                                                    ControlToValidate="txtMerPos01" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                </span></span>/HĐTV /<span style="mso-field-code:
&quot; MERGEFIELD  MerPos02  \\* MERGEFORMAT &quot;"><span style="mso-no-proof:yes"><asp:TextBox ID="txtMerPos02" runat="server" Width="90" class="textDoc" placeholder="KL-XXX"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Xin nhập số hợp đồng"
                                                                    ControlToValidate="txtMerPos02" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span></p>
                                                            <p align="center" class="MsoNormal" 
                                                                style="margin-left:-4.5pt;text-align:center">
                                                                <b>
                                                                <p>
                                                                    &nbsp;</p>
                                                                </b>
                                                                <p>
                                                                </p>
                                                                <p class="MsoNormal" style="margin-left:31.5pt;text-align:justify;text-indent:
-.25in;mso-list:l2 level1 lfo3">
                                                                    <![if !supportLists]><span style="mso-list:
Ignore">-<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><![endif]>Căn cứ Bộ Luật Dân Sự đã ban hành ngày 14-06-2005.</p>
                                                                <p class="MsoNormal" style="margin-left:31.5pt;text-align:justify;text-indent:
-.25in;mso-list:l2 level1 lfo3">
                                                                    <![if !supportLists]><span style="mso-list:
Ignore">-<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><![endif]>Căn cứ nhu cầu và khả năng của hai đơn vị.</p>
                                                                <p class="MsoNormal" style="margin-left:-4.5pt">
                                                                    <p>
                                                                        &nbsp;</p>
                                                                    <p>
                                                                    </p>
                                                                    <p class="MsoNormal" style="mso-margin-bottom-alt:auto;margin-left:-4.5pt">
                                                                        <span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>Hôm nay, <span>
                                                                        ngày
                                                                        <span style="mso-field-code:&quot; MERGEFIELD  MerPos03  \\* MERGEFORMAT &quot;">
                                                                        <span style="mso-no-proof:yes">
                                                                            <asp:TextBox ID="txtMerPos03" runat="server" Width="30" class="textDoc" placeholder="dd"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Xin nhập ngày hợp đồng"
                                                                                ControlToValidate="txtMerPos03" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                        </span></span> tháng
                                                                        <span style="mso-field-code:
&quot; MERGEFIELD  MerPos04  \\* MERGEFORMAT &quot;"><span style="mso-no-proof:yes"><asp:TextBox ID="txtMerPos04" runat="server" Width="30" class="textDoc" placeholder="MM"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Xin nhập tháng hợp đồng"
                                                                                ControlToValidate="txtMerPos04" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span> năm
                                                                        <span style="mso-field-code:&quot; MERGEFIELD  MerPos05  \\* MERGEFORMAT &quot;">
                                                                        <span style="mso-no-proof:yes"><asp:TextBox ID="txtMerPos05" runat="server" Width="40" class="textDoc" placeholder="yyyy"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Xin nhập năm hợp đồng"
                                                                                ControlToValidate="txtMerPos05" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span></span>, Chúng tôi gồm:</p>
                                                                    <p class="MsoNormal" style="mso-margin-bottom-alt:auto;margin-left:-4.5pt">
                                                                        &nbsp;</p>
                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.5pt;line-height:130%;tab-stops:dotted 472.5pt">
                                                                        <b><u>BÊN A :</u>
                                                                        <span style="mso-field-code:&quot; MERGEFIELD  MerName  \\* MERGEFORMAT &quot;">
                                                                        <span style="mso-no-proof:yes"><asp:TextBox ID="txtMerName" runat="server" Width="500" Height="28" class="textDoc" style="text-transform:uppercase;"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Xin nhập tên công ty"
                                                                                ControlToValidate="txtMerName" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span></b></p>
                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.3pt;line-height:130%;tab-stops:35.45pt 1.0in 6.5in">
                                                                        Địa chỉ<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>:
                                                                        <span style="mso-field-code:&quot; MERGEFIELD  MerAddress  \\* MERGEFORMAT &quot;">
                                                                        <span style="mso-no-proof:yes"><asp:TextBox ID="txtMerAddress" runat="server" Width="480" class="textDoc"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Xin nhập địa chỉ"
                                                                                ControlToValidate="txtMerAddress" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span><span style="mso-tab-count:
1">&nbsp;</span></p>
                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%;tab-stops:1.0in 6.5in">
                                                                        Điện thoại<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>:<span style="mso-tab-count:1">
                                                                        <asp:TextBox ID="txtMerPhone" runat="server" Width="480px" class="textDoc"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Xin nhập số điện thoại"
                                                                                ControlToValidate="txtMerPhone" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span>
                                                                    </p>
                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:1.9pt;margin-bottom:
3.0pt;margin-left:-4.5pt;line-height:130%;tab-stops:1.0in 3.5in 6.5in">
                                                                        Mã số thuế<span style="mso-spacerun:yes">&nbsp; </span><span style="mso-tab-count:1">
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>:<span 
                                                                            style="mso-field-code:&quot; MERGEFIELD  MerTaxCode  \\* MERGEFORMAT &quot;"><span 
                                                                            style="mso-no-proof:yes"> <asp:TextBox ID="txtMerTaxCode" runat="server" 
                                                                            Width="210px" class="textDoc"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Xin nhập mã số thuế"
                                                                                ControlToValidate="txtMerTaxCode" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span><span style="mso-tab-count:
1">&nbsp;</span>Email: <span style="mso-field-code:
&quot; MERGEFIELD  MerEmail  \\* MERGEFORMAT &quot;"><span style="mso-no-proof:yes"><asp:TextBox ID="txtMerEmail" runat="server" Width="250" class="textDoc"></asp:TextBox></span></span></p>
                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.3pt;line-height:130%;tab-stops:1.0in 3.5in 6.5in">
                                                                        Đại diện<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>:<span style="mso-field-code:&quot; MERGEFIELD  MerRepresent  \\* MERGEFORMAT &quot;"><span style="mso-no-proof:yes">
                                                                            <asp:DropDownList ID="ddlDanhXung" runat="server" Width="55" Height="22" class="textDoc ">
                                                                                <asp:ListItem Value="0" Text="---" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="Ông" Text="Ông"></asp:ListItem>
                                                                                <asp:ListItem Value="Bà" Text="Bà"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Xin chọn danh xưng người đại diện"
                                                                                ControlToValidate="ddlDanhXung" Display="Dynamic" ForeColor="Red" ValidationGroup="G111" InitialValue="0"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                        <asp:TextBox ID="txtMerRepresent" runat="server" Width="150" class="textDoc"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Xin nhập người đại diện"
                                                                                ControlToValidate="txtMerRepresent" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                        </span></span>Chức vụ: <span style="mso-field-code:
&quot; MERGEFIELD  MerPosition  \\* MERGEFORMAT &quot;"><span style="mso-no-proof:yes"><asp:TextBox ID="txtMerPosition" runat="server" Width="235" class="textDoc"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Xin nhập chức vụ"
                                                                                ControlToValidate="txtMerPosition" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator></span></span></p>
                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.3pt;line-height:130%;tab-stops:1.0in dotted 3.5in">
                                                                        <b><u>BÊN B :</u><span style="mso-spacerun:yes">&nbsp; </span>CÔNG TY TNHH TƯ VẤN 
                                                                        THƯƠNG MẠI KHÁNH LINH<p>
                                                                        </p>
                                                                        </b>
                                                                        <p>
                                                                        </p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.3pt;line-height:130%">
                                                                            Địa chỉ<span style="mso-tab-count:2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: 
                                                                            232/17 Cộng Hoà, Phường 12, Quận Tân Bình, Tp HCM.</p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.3pt;line-height:130%">
                                                                            VP Hà Nội<span style="mso-tab-count:2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: 
                                                                            Phòng 1702, Tòa nhà 17T4, KĐT Trung Hòa Nhân Chính, Đ. Hoàng Đạo Thúy, Quận Thanh Xuân, Hà Nội.</p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.3pt;line-height:130%">
                                                                            VP Đà Nẵng<span style="mso-tab-count:2">&nbsp;&nbsp;&nbsp;&nbsp; </span>: 
                                                                            K21/7 Ông Ích Khiêm, P. Thanh Bình, Quận Hải Châu, Tp Đà Nẵng.</p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.3pt;line-height:130%">
                                                                            VP Nha Trang<span style="mso-tab-count:2">&nbsp; </span>: 
                                                                            Tầng 1 (Vinaconex 17), 184 Lê Hồng Phong, P. Phước Hải, Tp Nha Trang.</p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.3pt;line-height:130%">
                                                                            Điện thoại <span style="mso-tab-count:
1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: <span style="mso-bidi-font-weight:bold">08.3811 6723 – 093 30 30 678 - Fax : 08.3811 6791</span></p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.3pt;line-height:130%">
                                                                            Mã số thuế<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: 0307633556<span 
                                                                                style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
                                                                            <span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>E – mail : info@ketoanvn.com.vn</p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.3pt;line-height:130%">
                                                                            Đại diện<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>:
                                                                            <span style="mso-bidi-font-weight:bold">Bà<b> Đào Cao Bích Ngọc</b></span><span 
                                                                                style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>Chức vụ : Giám Đốc
                                                                        </p>
                                                                        <p class="MsoNormal" style="margin-top:6.0pt;margin-right:0in;margin-bottom:0in;
margin-left:-4.5pt;margin-bottom:.0001pt;line-height:20.0pt;mso-line-height-rule:
exactly">
                                                                            Hai bên cùng thống nhất ký kết hợp đồng kinh tế với các điều khoản sau:</p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%">
                                                                            <b><u>ĐIỀU 1:</u></b><span style="mso-spacerun:yes">&nbsp; </span><u>
                                                                            <span style="mso-spacerun:yes">&nbsp;</span><b>NỘI DUNG HỢP ĐỒNG</b></u></p>
                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                            <b>Bên A đồng ý giao bên B làm dịch vụ khai báo thuế và hỗ trợ nghiệp vụ kế toán 
                                                                            cho bên A bao gồm các công việc sau:<p>
                                                                            </p>
                                                                            </b>
                                                                            <p>
                                                                            </p>
                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%">
                                                                                <b>1.1 Kê khai hàng tháng/quý:<u><p>
                                                                                </p>
                                                                                </u></b>
                                                                                <p>
                                                                                </p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                    <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                        style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                    <![endif]>Lập và gửi các loại hồ sơ, giấy tờ theo yêu cầu của cơ quan thuế.</p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                    <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                        style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                    <![endif]>Hướng dẫn viết hoá đơn và lập các loại chứng từ theo quy định.</p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                    <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                        style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                    <![endif]>Lập và gửi các loại<span style="mso-spacerun:yes">&nbsp; </span>báo cáo 
                                                                                    thuế hàng tháng, quý theo qui định.</p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                    <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                        style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                    <![endif]>Hướng dẫn lập và hoàn thành phiếu thu, phiếu chi, phiếu nhập kho, 
                                                                                    phiếu xuất kho theo chứng từ gốc của đơn vị.</p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                    <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                        style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                    <![endif]>Phân loại, sắp xếp và đóng chứng từ theo tháng.</p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                    <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                        style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                    <![endif]>Hỗ trợ đơn vị hoàn chỉnh và in các loại sổ sách kế toán theo quy định 
                                                                                    hiện hành.</p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                    <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                        style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                    <![endif]>Cử người trực tiếp làm việc với cơ quan thuế khi có yêu cầu.</p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                    <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                        style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                    <![endif]>Tư vấn những vấn đề liên quan đến quy định về thuế, lao động khi đơn 
                                                                                    vị có yêu cầu.</p>
                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                    <b>1.2 Quyết<span style="mso-spacerun:yes">&nbsp; </span>toán<span 
                                                                                        style="mso-spacerun:yes">&nbsp; </span>năm</b><u>
                                                                                    <p>
                                                                                    </p>
                                                                                    </u>
                                                                                    <p>
                                                                                    </p>
                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                        <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                            style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                        <![endif]>Lập quyết toán thuế, báo cáo tài chính cuối năm gửi các cơ quan chức 
                                                                                        năng.</p>
                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                        <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                            style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                        <![endif]>Lập và gửi các loại báo cáo thống kê.</p>
                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:31.5pt;text-align:justify;text-indent:-.25in;line-height:130%;
mso-list:l3 level1 lfo2">
                                                                                        <![if !supportLists]><span style="mso-list:Ignore">-<span 
                                                                                            style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                        <![endif]>Đăng ký các loại hồ sơ cho năm mới.</p>
                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%;tab-stops:dotted 524.5pt">
                                                                                        <i><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>Công việc ở điều 1 không bao 
                                                                                        gồm việc đăng ký BHXH &amp; BHYT, hoàn thuế, giải thể doanh nghiệp (nếu có) và mọi 
                                                                                        công việc phát sinh ngoài Tỉnh/Thành phố nơi bên A đặt trụ sở.<p>
                                                                                        </p>
                                                                                        </i>
                                                                                        <p>
                                                                                        </p>
                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%">
                                                                                            <b><u>ĐIỀU 2:</u> <u>PHÍ DỊCH VỤ VÀ PHƯƠNG THỨC THANH TOÁN:<p>
                                                                                            </p>
                                                                                            </u></b>
                                                                                            <p>
                                                                                            </p>
                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                <b>2.1 Chi phí dịch vụ</b>:<i><p>
                                                                                                </p>
                                                                                                </i>
                                                                                                <p>
                                                                                                </p>
                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.5pt;text-align:justify;line-height:130%;tab-stops:dotted 432.35pt">
                                                                                                    2.1.1 <asp:TextBox ID="txtMerCostTitle01" runat="server" style="width:85%;text-align:left;" class="textDoc" Text="Phí hàng tháng là : ……………. đồng/tháng (Nếu không phát sinh hoá đơn GTGT)"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Xin nhập chi phí dịch vụ"
                                                                                                        ControlToValidate="txtMerCostTitle01" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                                                </p>
                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                    <span style="mso-tab-count:
1">&nbsp; </span><span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span > 
                                                                                                    <asp:TextBox ID="txtMerCostDetail" runat="server" 
                                                                                                        style="width:70%;text-align:left;" class="textDoc" Text="* Từ 1 đến 10 hoá đơn:
* Từ 11 đến 20 hoá đơn:

* Thêm 20.000đ cho mỗi hóa đơn tăng thêm từ ……. hóa đơn trở lên" 
                                                                                                        Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                                                                    </span>
                                                                                                    <span style="color:
red"></span>
                                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.3pt;text-align:justify;line-height:130%;tab-stops:dotted 7.0in">
                                                                                                        2.1.2
                                                                                                        <asp:TextBox ID="txtMerCostTitle02" runat="server" class="textDoc" 
                                                                                                            style="width:85%;text-align:left;" 
                                                                                                            Text="Phí làm quyết toán cuối năm bằng bình quân tiền phí dịch vụ phát sinh trong năm."></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Xin nhập chi phí dịch vụ"
                                                                                                        ControlToValidate="txtMerCostTitle02" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                                                    </p>
                                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.3pt; text-align:justify;line-height:130%; tab-stops: dotted 7.0in;">
                                                                                                        2.1.3 
                                                                                                        <asp:TextBox ID="txtMerCostTitle03" runat="server" class="textDoc" 
                                                                                                            style="width:85%;text-align:left;" 
                                                                                                            Text="Phí hỗ trợ làm lại sổ sách (nếu có) bằng 70% phí dịch vụ hàng tháng."></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Xin nhập chi phí dịch vụ"
                                                                                                        ControlToValidate="txtMerCostTitle03" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                                        CssClass="tlp-error">*</asp:RequiredFieldValidator></p>
                                                                                                    <fieldset style="width:80%;padding: 5px 10px;font-size: 14px;">
                                                                                                        <legend>Phí dịch vụ</legend>
                                                                                                        <b style="color:#FF0000;">Lưu ý: Xin vui lòng nhập lại biểu phí dạng số dòng 2.1.1</b><br /><br />
                                                                                                        2.1.1 Phí hàng tháng là : <asp:TextBox ID="txtPhiHangThang" runat="server" style="width:100px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đồng/tháng (Nếu không phát sinh hoá đơn GTGT)
                                                                                                        <div style="padding-left:10px;">
                                                                                                            <p style="margin: 2px;">
                                                                                                            * Từ <asp:TextBox ID="txtTu1_1" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();"></asp:TextBox> đến 
                                                                                                                <asp:TextBox ID="txtTu1_2" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();"></asp:TextBox> hoá đơn: <asp:TextBox ID="txtPhi1" runat="server" style="width:100px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đồng/tháng <i><b>(nếu có)</b></i>
                                                                                                            </p>
                                                                                                            <p style="margin: 2px;">
                                                                                                            * Từ <asp:TextBox ID="txtTu2_1" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đến 
                                                                                                                <asp:TextBox ID="txtTu2_2" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> hoá đơn: <asp:TextBox ID="txtPhi2" runat="server" style="width:100px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đồng/tháng <i><b>(nếu có)</b></i>
                                                                                                            </p>
                                                                                                            <p style="margin: 2px;">
                                                                                                            * Từ <asp:TextBox ID="txtTu3_1" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đến 
                                                                                                                <asp:TextBox ID="txtTu3_2" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> hoá đơn: <asp:TextBox ID="txtPhi3" runat="server" style="width:100px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đồng/tháng <i><b>(nếu có)</b></i>
                                                                                                            </p>
                                                                                                            <p style="margin: 2px;">
                                                                                                            * Từ <asp:TextBox ID="txtTu4_1" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đến 
                                                                                                                <asp:TextBox ID="txtTu4_2" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> hoá đơn: <asp:TextBox ID="txtPhi4" runat="server" style="width:100px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đồng/tháng <i><b>(nếu có)</b></i>
                                                                                                            </p>
                                                                                                            <p style="margin: 2px;">
                                                                                                            * Từ <asp:TextBox ID="txtTu5_1" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đến 
                                                                                                                <asp:TextBox ID="txtTu5_2" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> hoá đơn: <asp:TextBox ID="txtPhi5" runat="server" style="width:100px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đồng/tháng <i><b>(nếu có)</b></i>
                                                                                                            </p>
                                                                                                            <p style="margin: 2px;">
                                                                                                            * Từ <asp:TextBox ID="txtTu6_1" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đến 
                                                                                                                <asp:TextBox ID="txtTu6_2" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> hoá đơn: <asp:TextBox ID="txtPhi6" runat="server" style="width:100px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> đồng/tháng <i><b>(nếu có)</b></i>
                                                                                                            </p>
                                                                                                            <p style="margin: 2px;">
                                                                                                            * Thêm <asp:TextBox ID="txtThemPhi" runat="server" style="width:100px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox>đ cho mỗi hóa đơn tăng thêm từ 
                                                                                                                <asp:TextBox ID="txtThemHd" runat="server" style="width:30px;height:22px;" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" ></asp:TextBox> hóa đơn trở lên
                                                                                                            </p>
                                                                                                        </div>
                                                                                                    </fieldset>
                                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.5pt;text-align:justify;line-height:130%;tab-stops:dotted 7.0in">
                                                                                                        <i>* </i><b><span style="mso-spacerun:yes">&nbsp;</span>Phí dịch vụ trên được tính 
                                                                                                        dựa vào số lượng phát sinh hóa đơn, chứng từ trong tháng bao gồm hóa đơn mua 
                                                                                                        vào, bán ra.</b>
                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.5pt;text-align:justify;line-height:130%;tab-stops:dotted 7.0in">
                                                                                                            <i>* </i><b><span style="mso-spacerun:yes">&nbsp;</span>Phí hỗ trợ làm lại sổ sách kế toán không bao gồm các khoản phạt chậm nộp tờ khai thuế, chậm nộp tiền thuế, các chi phí phát sinh khi phải điều chỉnh các số liệu báo cáo năm cũ (nếu có) …</b>
                                                                                                        </p>
                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;margin-left:-4.5pt;text-align:justify;line-height:130%;tab-stops:dotted 7.0in">
                                                                                                            <i>* Toàn bộ phí trên <b style="mso-bidi-font-weight:normal">chưa bao gồm thuế 
                                                                                                            GTGT</b> và sẽ được xem xét lại vào tháng 01 của năm mới hoặc khi công việc kinh 
                                                                                                            doanh của bên A có thay đổi so với lúc ký hợp đồng</i>.</p>
                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%;tab-stops:dotted 7.0in">
                                                                                                            <b>2.2 Phương thức thanh toán:<p>
                                                                                                            </p>
                                                                                                            </b>
                                                                                                            <p>
                                                                                                            </p>
                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%;tab-stops:dotted 7.0in">
                                                                                                                2.2.1 Phí dịch vụ hàng tháng thanh toán khi Bên B nhận chứng từ, hóa đơn từ Bên 
                                                                                                                A. Phí dịch vụ bắt đầu thu từ tháng
                                                                                                                <span style="mso-field-code:&quot; MERGEFIELD  MerBeginM  \\* MERGEFORMAT &quot;">
                                                                                                                <span style="mso-no-proof:yes">
                                                                                                                <asp:TextBox ID="txtMerBeginM" runat="server" class="textDoc" Width="120px" placeholder="MM/yyyy"></asp:TextBox>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Xin nhập tháng thu phí dịch vụ"
                                                                                                                ControlToValidate="txtMerBeginM" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                                                CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="G111"
                                                                                                                ErrorMessage="Tháng thu phí dịch vụ không đúng định dạng (vd: 08/2015)" ControlToValidate="txtMerBeginM"
                                                                                                                ValidationExpression="[0-3][0-9]/201[0-9]" ForeColor="Red" Display="Dynamic">*</asp:RegularExpressionValidator>
                                                                                                                </span></span>
                                                                                                            </p>
                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%;tab-stops:dotted 7.0in">
                                                                                                                2.2.2 Phí làm quyết toán cuối năm thu chung với phí tháng 12</p>
                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%">
                                                                                                                2.2.3 Phí hỗ trợ làm lại sổ sách (nếu có) sẽ được thanh toán ngay khi bên B nhận 
                                                                                                                hồ sơ chứng từ.</p>
                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                <b><u>ĐIỀU 3</u>:<u> PHƯƠNG THỨC THỰC HIỆN</u> :<span style="mso-spacerun:yes">&nbsp;&nbsp;
                                                                                                                </span>
                                                                                                                <p>
                                                                                                                </p>
                                                                                                                </b>
                                                                                                                <p>
                                                                                                                </p>
                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                    3.1 Mọi công việc được thực hiện tại văn phòng bên B.</p>
                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                    3.2 Hàng tháng từ ngày 05 đến ngày 15 tháng sau bên B sẽ thông báo cho bên A 
                                                                                                                    chuẩn bị các loại hồ sơ, hóa đơn, chứng từ và hẹn ngày đến lấy.</p>
                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                    3.3 Chậm nhất là đến ngày 20 bên A phải tập hợp và giao chứng từ cho 
                                                                                                                    bên B. Đến ngày 20 nếu bên A chưa bàn giao chứng từ thì bên A có trách 
                                                                                                                    nhiệm gửi chứng từ cho bên B tại văn phòng của bên B, qua đường bưu 
                                                                                                                    điện hoặc gửi mail cho bộ phận kế toán, hành chánh (Vui lòng xác 
                                                                                                                    nhận thông tin bằng điện thoại khi gửi bằng đường bưu điện hoặc email)</p>
                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                    3.4 Khi bên B đã nhận hồ sơ, hóa đơn chứng từ, trường hợp bên A muốn bổ sung 
                                                                                                                    thêm hóa đơn, chứng từ phát sinh thì bên A có trách nhiệm gửi cho bên B tại văn 
                                                                                                                    phòng trực tiếp hoặc qua đường bưu điện hoặc scan chụp gửi qua mail của bộ phận 
                                                                                                                    kế toán, hành chánh. <span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;</span></p>
                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                    3.5 Chậm nhất đến trước 03 ngày hết hạn nộp báo cáo bên B sẽ gửi báo cáo cho bên 
                                                                                                                    A kiểm tra, ký nộp hoặc gửi lại cho bên B nộp (Nộp bằng tài khoản online).</p>
                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                    <span style="letter-spacing:
-.1pt">3.6 Bên B sẽ hoàn chỉnh toàn bộ hồ sơ, chứng từ theo thoả thuận tại Điều 1 trả lại cho bên A bằng files mềm hoặc hồ sơ cứng để lưu giữ, bảo quản chậm nhất sau hạn nộp báo cáo cuối năm 
                                                                                                                    (ngày 30/03 năm sau) 45 ngày hoặc đến ngày 30 của tháng tiếp theo tùy theo yêu 
                                                                                                                    cầu quản lý của bên A.<p>
                                                                                                                    </p>
                                                                                                                    </span>
                                                                                                                    <p>
                                                                                                                    </p>
                                                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                        3.7 Trong quá trình làm việc nếu có thông báo của cơ quan thuế bên A chuyển ngay 
                                                                                                                        cho bên B để bên B cử người đi xử lý.</p>
                                                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                        3.8 Trừ trường hợp đặc biệt mọi tư vấn hoặc trao đổi công việc giữa bên B với 
                                                                                                                        bên A được thực hiện qua email, điện thoại.
                                                                                                                    </p>
                                                                                                                    <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%">
                                                                                                                        <b><u>ĐIỀU 4:</u><span style="mso-spacerun:yes">&nbsp; </span><u>TRÁCH NHIỆM MỖI BÊN</u>:<p>
                                                                                                                        </p>
                                                                                                                        </b>
                                                                                                                        <p>
                                                                                                                        </p>
                                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:13.5pt;text-indent:-.25in;line-height:130%;mso-list:l0 level2 lfo4">
                                                                                                                            <![if !supportLists]><b style="mso-bidi-font-weight:normal">
                                                                                                                            <i style="mso-bidi-font-style:normal"><span style="mso-list:Ignore">4.1<span 
                                                                                                                                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp; </span></span></i></b>
                                                                                                                            <![endif]><b><i>Trách nhiệm của bên A </i>:</b></p>
                                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                            4.1.1 Ngay sau khi ký hợp đồng, cung cấp đầy đủ cho bên B các loại hồ sơ, tài 
                                                                                                                            liệu như (bản photo có sao y): Giấy chứng nhận ĐKKD, giấy chứng nhận đăng ký 
                                                                                                                            thuế, giấy chứng nhận góp vốn của các thành viên, đăng ký mở tài khoản ngân 
                                                                                                                            hàng, các loại hợp đồng thuê, mướn, ….. và các loại giấy tờ liên quan khác (nếu 
                                                                                                                            có).</p>
                                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                            4.1.2 Chậm nhất đến ngày 20 của tháng sau phải cung cấp cho bên B
                                                                                                                            <b style="mso-bidi-font-weight:
normal"><u>đầy đủ các loại hóa đơn, chứng từ mua hàng, bán hàng, chứng từ giao dịch với ngân hàng, hợp đồng kinh tế và các loại thông tin khác nếu có (bản chính hoặc bản photo).<p>
                                                                                                                            </p>
                                                                                                                            </u></b>
                                                                                                                            <p>
                                                                                                                            </p>
                                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                4.1.3 Thông báo cho bên B biết trước 01 tuần nếu có những thay đổi trong hoạt 
                                                                                                                                động kinh doanh của đơn vị mình như: Thay đổi nội dung trên giấy phép, thay đổi 
                                                                                                                                mặt hàng kinh doanh chính, mở tài khoản ngân hàng mới,……</p>
                                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                4.1.4 Chịu trách nhiệm hoàn toàn về tính pháp lý của các loại hồ sơ, chứng từ do 
                                                                                                                                bên A cung cấp cũng như mọi vấn đề<span style="mso-spacerun:yes">&nbsp; </span>liên 
                                                                                                                                quan đến hoạt động<span style="mso-spacerun:yes">&nbsp; </span>kinh doanh của đơn vị 
                                                                                                                                mình.</p>
                                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                4.1.5 Chịu trách nhiệm kiểm tra, lưu giữ, bảo quản và ký đầy đủ vào các loại báo 
                                                                                                                                cáo, hồ sơ, chứng từ, …do bên B in ra và gửi cho bên A.</p>
                                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                4.1.6 Thực hiện đầy đủ các nghĩa vụ đối với Nhà nước theo quy định</p>
                                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                4.1.7 Bên A chỉ giao hồ sơ chứng từ cho người có giấy giới thiệu của bên B. Khi 
                                                                                                                                giao nhận phải liệt kê hoặc kiểm đếm đầy đủ theo mẫu giao nhận của bên B.</p>
                                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                4.1.8 Trong thời hạn còn hợp đồng, nếu bên A tự ý yêu cầu các cơ quan đến kiểm 
                                                                                                                                tra hoặc sau khi đã chấm dứt hợp đồng mà nhờ bên B giải trình kiểm tra thì phải 
                                                                                                                                tính thêm phí giải trình cho bên B.
                                                                                                                            </p>
                                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;text-indent:0in;line-height:130%;
mso-list:l1 level3 lfo1">
                                                                                                                                <![if !supportLists]><span style="mso-list:Ignore">4.1.9<span 
                                                                                                                                    style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>
                                                                                                                                <![endif]>Bên A tạo điều kiện thuận lợi cho bên B hoàn thành tốt công việc</p>
                                                                                                                            <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%">
                                                                                                                                <b><i>4.2 Trách nhiệm của bên B </i>:<p>
                                                                                                                                </p>
                                                                                                                                </b>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                    4.2.1 Dựa trên những tài liệu, hồ sơ, chứng từ, thông tin do bên A cung cấp thực 
                                                                                                                                    hiện đầy đủ và đúng qui định các nội dung trong điều 1 của hợp đồng này kể từ 
                                                                                                                                    ngày hợp đồng được ký kết cho đến khi chấm dứt.
                                                                                                                                </p>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                    4.2.2 Kiểm tra, phát hiện và báo cho bên A biết những sai sót trong các loại hồ 
                                                                                                                                    sơ chứng từ để bổ sung, sửa chữa kịp thời.Thường xuyên gửi thông báo và tư vấn 
                                                                                                                                    cho bên A những thay đổi liên quan đến việc kê khai, quyết toán thuế.</p>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                    4.2.3 Hàng tháng/quý phải thông cho bên A biết những khoản thuế mà bên A phải 
                                                                                                                                    nộp cùng với thời gian gửi báo cáo thuế cho bên A.</p>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                    4.2.4 Chịu trách nhiệm bảo mật toàn bộ số liệu về doanh số và các hoạt động kinh 
                                                                                                                                    doanh của bên A trong và sau khi thực hiện hợp đồng. Bảo quản hóa đơn chứng từ 
                                                                                                                                    do bên A cung cấp trong thời gian thực hiện công việc cho đến khi giao lại cho 
                                                                                                                                    bên A.</p>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                    4.2.5 Chịu trách nhiệm giải trình với các cơ quan liên quan đến công việc do bên 
                                                                                                                                    B thực hiện và bồi thường<span style="mso-spacerun:yes">&nbsp; </span>những sai sót 
                                                                                                                                    do thao tác nghiệp vụ dẫn đến thiệt hại cho bên A.
                                                                                                                                </p>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                    4.2.6 Khi chấm dứt hợp đồng phải bàn giao đầy đủ với người có trách nhiệm của 
                                                                                                                                    bên A.<span style="mso-spacerun:yes">&nbsp;&nbsp; </span>
                                                                                                                                </p>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                    4.2.7 Bên B không can thiệp vào nội bộ, không chịu trách nhiệm về những hoạt 
                                                                                                                                    động kinh doanh cũng như các nội dung, tính pháp lý đã ghi trong hoá đơn, chứng 
                                                                                                                                    từ của bên A. Bên B chỉ chịu trách nhiệm về những sai sót do chính bên B làm và 
                                                                                                                                    là người trực tiếp giải trình với các cơ quan liên quan .</p>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                    4.2.8 Ngưng ngay hợp đồng hoặc không bàn giao sổ sách chứng từ nếu bên A không 
                                                                                                                                    thanh toán phí đúng hạn theo điều 2 mà không có lý do chính đáng bằng văn bản 
                                                                                                                                    gửi cho bên B.</p>
                                                                                                                                <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;line-height:130%">
                                                                                                                                    <b><u>ĐIỀU 5:</u> <u>ĐIỀU KHOẢN CHUNG</u>:</b><p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                        5.1 Hợp đồng này có hiệu lực thực hiện trong thời hạn
                                                                                                                                        <span style="mso-field-code:&quot; MERGEFIELD  MerDeadlineInt  \\* MERGEFORMAT &quot;">
                                                                                                                                        <span style="mso-no-proof:yes">
                                                                                                                                        <asp:TextBox ID="txtMerDeadlineInt" runat="server" class="textDoc" 
                                                                                                                                            placeholder="số" Width="40px"></asp:TextBox>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Xin nhập thời hạn hợp đồng (ghi bằng số)"
                                                                                                                                            ControlToValidate="txtMerDeadlineInt" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                                                                            CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                                                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="G111" Operator="DataTypeCheck" Type="Integer"
                                                                                                                                            ControlToValidate="txtMerDeadlineInt" ErrorMessage="Thời hạn hợp đồng ghi bằng số" Text="*"/>
                                                                                                                                        </span></span>(<span 
                                                                                                                                            style="mso-field-code:&quot; MERGEFIELD  MerDeadlineString  \\* MERGEFORMAT &quot;"><span 
                                                                                                                                            style="mso-no-proof:yes"><asp:TextBox ID="txtMerDeadlineString" 
                                                                                                                                            runat="server" class="textDoc" placeholder="ghi bằng chữ" Width="100px"></asp:TextBox>
                                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Xin nhập thời hạn hợp đồng (ghi bằng chữ)"
                                                                                                                                            ControlToValidate="txtMerDeadlineString" Display="Dynamic" ForeColor="Red" ValidationGroup="G111"
                                                                                                                                            CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                                                                                        </span></span>) tháng kể từ ngày ký. Khi <span lang="VI" 
                                                                                                                                            style="mso-ansi-language:VI;mso-no-proof:yes">đến ngày hết hạn </span>
                                                                                                                                        <span style="mso-no-proof:yes">h</span><span lang="VI" style="mso-ansi-language:VI;
mso-no-proof:yes">ợp </span><span style="mso-no-proof:yes">đ</span><span lang="VI" style="mso-ansi-language:VI;mso-no-proof:yes">ồng, nếu </span><span style="mso-no-proof:yes">c</span><span lang="VI" style="mso-ansi-language:VI;
mso-no-proof:yes">ác Bên không có đề nghị bằng văn bản về việc thanh lý và chấm dứt hợp đồng thì </span><span style="mso-no-proof:yes">h</span><span lang="VI" style="mso-ansi-language:VI;mso-no-proof:yes">ợp đồng sẽ 
                                                                                                                                        tự động gia hạn thêm những năm tiếp theo.</span><b></b><p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                            5.2 Trong quá trình thực hiện hợp đồng nếu có phát sinh thì hai bên cùng thỏa 
                                                                                                                                            thuận và tiến hành làm thêm phụ lục hợp đồng.</p>
                                                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                            5.3 Hai bên cam kết thực hiện nghiêm chỉnh các điều khoản ghi trong hợp đồng. 
                                                                                                                                            Trong trường hợp khó khăn sẽ cùng nhau giải quyết trên tinh thần hợp tác. Trong 
                                                                                                                                            trường hợp nếu không giải quyết được bất đồng thì sẽ đưa ra Tòa án theo thẩm 
                                                                                                                                            quyền.</p>
                                                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                            5.4 Nếu một trong hai bên đơn phương chấm dứt hợp đồng thì trước khi chấm dứt 
                                                                                                                                            phải thông báo bằng văn bản cho bên kia biết trước 01 tháng, sau đó thực hiện 
                                                                                                                                            thanh lý hợp đồng.</p>
                                                                                                                                        <p class="MsoNormal" style="margin-top:3.0pt;margin-right:0in;margin-bottom:3.0pt;
margin-left:-4.5pt;text-align:justify;line-height:130%">
                                                                                                                                            5.5 Hợp đồng được lập thành 02 (hai) bản, mỗi bên giữ 01 (một) bản có giá trị 
                                                                                                                                            pháp lý như nhau .</p>
                                                                                                                                        <p>
                                                                                                                                            &nbsp;</p>
                                                                                                                                        <p class="MsoNormal" style="margin-left:-4.5pt">
                                                                                                                                            <span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>Đại diện bên 
                                                                                                                                            A<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp; 
                                                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>Đại diện bên B<span 
                                                                                                                                                style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
                                                                                                                                        </p>
                                                                                                                                        <p class="MsoNormal" style="margin-left:-4.5pt">
                                                                                                                                            <span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>GIÁM ĐỐC<span 
                                                                                                                                                style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                            </span>GIÁM ĐỐC</p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                    </p>
                                                                                                                                </p>
                                                                                                                            </p>
                                                                                                                        </p>
                                                                                                                    </p>
                                                                                                                </p>
                                                                                                            </p>
                                                                                                        </p>
                                                                                                    </p>
                                                                                                </p>
                                                                                            </p>
                                                                                        </p>
                                                                                    </p>
                                                                                </p>
                                                                            </p>
                                                                        </p>
                                                                    </p>
                                                                </p>
                                                            </p>
                                                        </p>
                                                    </p>
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
        ShowSummary="False" ValidationGroup="G111" />
</asp:Content>
