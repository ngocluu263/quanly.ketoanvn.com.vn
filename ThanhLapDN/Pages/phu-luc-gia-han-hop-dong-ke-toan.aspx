<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="phu-luc-gia-han-hop-dong-ke-toan.aspx.cs" Inherits="ThanhLapDN.Pages.phu_luc_gia_han_hop_dong_ke_toan" %>
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
    p.MsoListParagraphCxSpFirst
	{margin-top:0cm;
	margin-right:0cm;
	margin-bottom:0cm;
	margin-left:36.0pt;
	margin-bottom:.0001pt;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	}
p.MsoListParagraphCxSpLast
	{margin-top:0cm;
	margin-right:0cm;
	margin-bottom:0cm;
	margin-left:36.0pt;
	margin-bottom:.0001pt;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
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
            Phụ lục gia hạn hợp đồng
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G113"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
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
                        <dx:TabPage Text="Phụ lục gia hạn hợp đồng">
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
                                                    <b>
                                                    <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;;border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm">CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM<br /> Độc Lập – Tự Do – Hạnh Phúc</span></b><span lang="EN-US" style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;"><br /> 
                                                    ----------------------------<p></p>
                                                    </span>
                                                    <p>
                                                    </p>
                                                    <p align="right" class="MsoNormal">
                                                        <i>
                                                        <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;;border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm">Tp. HCM, ngày
                                                        <asp:TextBox ID="txtMerPos01" runat="server" class="textDoc" placeholder="dd" 
                                                            Width="30px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="txtMerPos01" CssClass="tlp-error" Display="Dynamic" 
                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                        tháng
                                                        <asp:TextBox ID="txtMerPos02" runat="server" class="textDoc" placeholder="MM" 
                                                            Width="30px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="txtMerPos02" CssClass="tlp-error" Display="Dynamic" 
                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                        năm
                                                        <asp:TextBox ID="txtMerPos03" runat="server" class="textDoc" placeholder="yyyy" 
                                                            Width="40px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="txtMerPos03" CssClass="tlp-error" Display="Dynamic" 
                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                        &nbsp;</span></i><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;"><p>
                                                        </p>
                                                        </span>
                                                        <p>
                                                        </p>
                                                        <p align="center" class="MsoNormal">
                                                            <b>
                                                            <span lang="EN-US" style="font-size:14.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm">GIA HẠN HỢP ĐỒNG</span><span lang="EN-US" style="font-size:14.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;"><p>
                                                            </p>
                                                            </span></b>
                                                            <p>
                                                            </p>
                                                            <p align="center" class="MsoNormal">
                                                                <i style="mso-bidi-font-style:
normal"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm;mso-bidi-font-weight:bold">Số </span></i><i>
                                                                <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm">01/HĐTV-KL/<asp:TextBox ID="txtMerPos04" runat="server" class="textDoc" placeholder="" 
                                                                    Width="80px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                                    ControlToValidate="txtMerPos04" CssClass="tlp-error" Display="Dynamic" 
                                                                    ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                </span></i>
                                                                <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;">
                                                                <p>
                                                                </p>
                                                                </span>
                                                                <p>
                                                                </p>
                                                                <p class="MsoNormal">
                                                                    <i>
                                                                    <span lang="EN-US" style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm">-&nbsp;&nbsp;Căn cứ theo HĐKT số
                                                                    <asp:TextBox ID="txtMerPos05" runat="server" class="textDoc" placeholder="" 
                                                                        Width="65px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                        ControlToValidate="txtMerPos05" CssClass="tlp-error" Display="Dynamic" 
                                                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                    </span></i><i style="mso-bidi-font-style:normal"><span lang="EN-US" 
                                                                        style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;">
                                                                    /HĐTV-KL/</span></i><i><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm"><asp:TextBox ID="txtMerPos06" runat="server" class="textDoc" placeholder="" Width="75px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                                        ControlToValidate="txtMerPos06" CssClass="tlp-error" Display="Dynamic" 
                                                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                    đã ký ngày
                                                                    <asp:TextBox ID="txtMerPos07" runat="server" class="textDoc" placeholder="dd" 
                                                                        Width="30px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                                        ControlToValidate="txtMerPos07" CssClass="tlp-error" Display="Dynamic" 
                                                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                    tháng
                                                                    <asp:TextBox ID="txtMerPos08" runat="server" class="textDoc" placeholder="MM" 
                                                                        Width="30px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                                        ControlToValidate="txtMerPos08" CssClass="tlp-error" Display="Dynamic" 
                                                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                    năm
                                                                    <asp:TextBox ID="txtMerPos09" runat="server" class="textDoc" placeholder="yyyy" 
                                                                        Width="40px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                                        ControlToValidate="txtMerPos09" CssClass="tlp-error" Display="Dynamic" 
                                                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                    <br />
                                                                    -&nbsp;&nbsp;Căn cứ nhu cầu thực tế 2 bên
                                                                    <asp:TextBox ID="txtMerPos10" runat="server" class="textDoc" placeholder="" 
                                                                        style="margin-top: 5px;" Width="250px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                                        ControlToValidate="txtMerPos10" CssClass="tlp-error" Display="Dynamic" 
                                                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                    </span></i><i style="mso-bidi-font-style:normal">
                                                                    <span lang="EN-US" style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-bidi-font-weight:bold"></span></i><i>
                                                                    <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm">&amp; Công Ty TNHH Tư Vấn TM Khánh Linh<p>
                                                                    </p>
                                                                    </span></i>
                                                                    <p>
                                                                    </p>
                                                                    <p class="MsoNormal">
                                                                        <span lang="EN-US" style="font-size:12.0pt;font-family:
&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;">
                                                                        <p>
                                                                            &nbsp;</p>
                                                                        </span>
                                                                        <p>
                                                                        </p>
                                                                        <p class="MsoNormal">
                                                                            <b>
                                                                            <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;;border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm">Chúng tôi gồm có:<p>
                                                                            </p>
                                                                            </span></b>
                                                                            <p>
                                                                            </p>
                                                                            <p class="MsoNormal">
                                                                                <b>
                                                                                <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;;border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm">
                                                                                <p>
                                                                                    &nbsp;</p>
                                                                                </span></b>
                                                                                <p>
                                                                                </p>
                                                                                <p class="MsoNormal">
                                                                                    <b><u>
                                                                                    <span lang="EN-US" style="font-size:12.0pt;
line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:
major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">BÊN A :</span></u><span lang="EN-US" style="font-size:12.0pt;line-height:130%;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:major-latin;
mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">
                                                                                    <asp:TextBox ID="txtMerName" runat="server" class="textDoc" Height="28px" 
                                                                                        style="text-transform:uppercase;" Width="500px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                                                        ControlToValidate="txtMerName" CssClass="tlp-error" Display="Dynamic" 
                                                                                        ErrorMessage="Xin nhập tên công ty" ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                    </span></b>
                                                                                    <span lang="EN-US" style="font-size:12.0pt;line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-ascii-theme-font:major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:
major-latin">
                                                                                    <p>
                                                                                    </p>
                                                                                    </span>
                                                                                    <p>
                                                                                    </p>
                                                                                    <p class="MsoNormal">
                                                                                        <span lang="EN-US" style="font-size:12.0pt;
line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:
major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">Địa chỉ<span style="mso-tab-count:2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>:<span style="mso-no-proof:yes">
                                                                                        <asp:TextBox ID="txtMerAddress" runat="server" class="textDoc" Width="480px"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                                                            ControlToValidate="txtMerAddress" CssClass="tlp-error" Display="Dynamic" 
                                                                                            ErrorMessage="Xin nhập địa chỉ" ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                        </span>
                                                                                        <p>
                                                                                        </p>
                                                                                        </span>
                                                                                        <p>
                                                                                        </p>
                                                                                        <p class="MsoNormal">
                                                                                            <span lang="EN-US" style="font-size:12.0pt;
line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:
major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">Điện thoại<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>:
                                                                                            <asp:TextBox ID="txtMerPhone" runat="server" class="textDoc" Width="480px"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                                                                ControlToValidate="txtMerPhone" CssClass="tlp-error" Display="Dynamic" 
                                                                                                ErrorMessage="Xin nhập số điện thoại" ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                            <p>
                                                                                            </p>
                                                                                            </span>
                                                                                            <p>
                                                                                            </p>
                                                                                            <p class="MsoNormal">
                                                                                                <span lang="EN-US" style="font-size:12.0pt;line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-ascii-theme-font:major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:
major-latin">Mã số thuế<span style="mso-spacerun:yes">&nbsp; </span><span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp; </span>:
                                                                                                <asp:TextBox ID="txtMerTaxCode" runat="server" class="textDoc" Width="210px"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                                                                                    ControlToValidate="txtMerTaxCode" CssClass="tlp-error" Display="Dynamic" 
                                                                                                    ErrorMessage="Xin nhập mã số thuế" ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                <span style="mso-tab-count:1">&nbsp; </span>Email:
                                                                                                <asp:TextBox ID="txtMerEmail" runat="server" class="textDoc" Width="250px"></asp:TextBox>
                                                                                                <p>
                                                                                                </p>
                                                                                                </span>
                                                                                                <p>
                                                                                                </p>
                                                                                                <p class="MsoNormal">
                                                                                                    <span lang="EN-US" style="font-size:12.0pt;
line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:
major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">Đại diện<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>:
                                                                                                    <asp:DropDownList ID="ddlDanhXung" runat="server" class="textDoc " 
                                                                                                        Height="22px" Width="55px">
                                                                                                        <asp:ListItem Selected="True" Text="---" Value="0"></asp:ListItem>
                                                                                                        <asp:ListItem Text="Ông" Value="Ông"></asp:ListItem>
                                                                                                        <asp:ListItem Text="Bà" Value="Bà"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                                                                                        ControlToValidate="ddlDanhXung" CssClass="tlp-error" Display="Dynamic" 
                                                                                                        ErrorMessage="Xin chọn danh xưng người đại diện" ForeColor="Red" 
                                                                                                        InitialValue="0" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                    <asp:TextBox ID="txtMerRepresent" runat="server" class="textDoc" Width="150px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                                                                                        ControlToValidate="txtMerRepresent" CssClass="tlp-error" Display="Dynamic" 
                                                                                                        ErrorMessage="Xin nhập người đại diện" ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                    <span style="mso-tab-count:1">&nbsp; </span>Chức vụ:
                                                                                                    <asp:TextBox ID="txtMerPosition" runat="server" class="textDoc" Width="235px"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                                                                                        ControlToValidate="txtMerPosition" CssClass="tlp-error" Display="Dynamic" 
                                                                                                        ErrorMessage="Xin nhập chức vụ" ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                    <p>
                                                                                                    </p>
                                                                                                    </span>
                                                                                                    <p>
                                                                                                    </p>
                                                                                                    <p class="MsoNormal">
                                                                                                        <b><u>
                                                                                                        <span lang="EN-US" style="font-size:
12.0pt;line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:
major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">BÊN B :</span></u><span lang="EN-US" style="font-size:12.0pt;line-height:130%;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:major-latin;
mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin"><span style="mso-spacerun:yes">&nbsp; </span>CÔNG TY TNHH TƯ VẤN THƯƠNG MẠI KHÁNH LINH<p>
                                                                                                        </p>
                                                                                                        </span></b>
                                                                                                        <p>
                                                                                                        </p>
                                                                                                        <p class="MsoNormal">
                                                                                                            <span lang="EN-US" style="font-size:12.0pt;line-height:
130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:major-latin;
mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">Địa chỉ<span style="mso-tab-count:2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: 232/17 Cộng Hoà, Phường 12, Quận Tân Bình, Tp HCM.<p>
                                                                                                            </p>
                                                                                                            </span>
                                                                                                            <p>
                                                                                                            </p>
                                                                                                            <p class="MsoNormal">
                                                                                                                <span lang="EN-US" style="font-size:12.0pt;line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-ascii-theme-font:major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:
major-latin">VP Hà Nội<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: Phòng 1702, Tòa nhà 17T4, KĐT Trung Hòa Nhân Chính, Đ. Hoàng Đạo Thúy, Quận Thanh Xuân, Hà Nội.<p>
                                                                                                                </p>
                                                                                                                </span>
                                                                                                                <p>
                                                                                                                </p>
                                                                                                                <p class="MsoNormal">
                                                                                                                    <span lang="EN-US" style="font-size:12.0pt;line-height:
130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:major-latin;
mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">VP Đà Nẵng<span style="mso-tab-count:1">&nbsp;&nbsp; </span>: K21/7 Ông Ích Khiêm, P. Thanh Bình, Quận Hải Châu, Tp Đà Nẵng.<p>
                                                                                                                    </p>
                                                                                                                    </span>
                                                                                                                    <p>
                                                                                                                    </p>
                                                                                                                    <p class="MsoNormal">
                                                                                                                        <span lang="EN-US" style="font-size:12.0pt;line-height:
130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:major-latin;
mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">VP Nha Trang<span style="mso-tab-count:1"> </span>: Tầng 1 (Vinaconex 17), 184 Lê Hồng Phong, P. Phước Hải, Tp Nha Trang.<p>
                                                                                                                        </p>
                                                                                                                        </span>
                                                                                                                        <p>
                                                                                                                        </p>
                                                                                                                        <p class="MsoNormal">
                                                                                                                            <span lang="EN-US" style="font-size:12.0pt;line-height:
130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:major-latin;
mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">Điện thoại <span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: <span style="mso-bidi-font-weight:bold">08.3811 6723 – 093 30 30 678 - Fax : 
                                                                                                                            08.3811 6791</span><p>
                                                                                                                            </p>
                                                                                                                            </span>
                                                                                                                            <p>
                                                                                                                            </p>
                                                                                                                            <p class="MsoNormal">
                                                                                                                                <span lang="EN-US" style="font-size:12.0pt;
line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:
major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">Mã số thuế<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: 0307633556<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                </span>E – mail : info@ketoanvn.com.vn<p>
                                                                                                                                </p>
                                                                                                                                </span>
                                                                                                                                <p>
                                                                                                                                </p>
                                                                                                                                <p class="MsoNormal">
                                                                                                                                    <span lang="EN-US" style="font-size:12.0pt;
line-height:130%;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-ascii-theme-font:
major-latin;mso-hansi-theme-font:major-latin;mso-bidi-theme-font:major-latin">Đại diện<span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>: <span style="mso-bidi-font-weight:
bold">Bà<b> Đào Cao Bích Ngọc</b></span><span style="mso-tab-count:1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>Chức vụ : Giám Đốc
                                                                                                                                    <p>
                                                                                                                                    </p>
                                                                                                                                    </span>
                                                                                                                                    <p>
                                                                                                                                    </p>
                                                                                                                                    <p class="MsoNormal">
                                                                                                                                        <span lang="EN-US" style="font-size:12.0pt;font-family:
&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;border:
none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm;
mso-bidi-font-weight:bold">Sau khi xem xét hai bên đã đi đến thống nhất thỏa thuận ký Gia Hạn HĐTV Số<span style="mso-spacerun:yes">&nbsp; </span>
                                                                                                                                        <asp:TextBox ID="txtMerPosBody01" runat="server" class="textDoc" placeholder="" 
                                                                                                                                            Width="65px"></asp:TextBox>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                                                                                                                            ControlToValidate="txtMerPosBody01" CssClass="tlp-error" Display="Dynamic" 
                                                                                                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                                                        </span>
                                                                                                                                        <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm;mso-bidi-font-style:italic">/HĐTV-KL/</span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosBody02" runat="server" class="textDoc" placeholder="" Width="65px"></asp:TextBox>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                                                                                                                            ControlToValidate="txtMerPosBody02" CssClass="tlp-error" Display="Dynamic" 
                                                                                                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                                                        về việc gia hạn hợp đồng phí dịch vụ phát sinh hàng tháng đối với hợp đồng đã ký 
                                                                                                                                        số
                                                                                                                                        <asp:TextBox ID="txtMerPosBody03" runat="server" class="textDoc" placeholder="" 
                                                                                                                                            style="margin:5px 0px;" Width="65px"></asp:TextBox>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                                                                                                                                            ControlToValidate="txtMerPosBody03" CssClass="tlp-error" Display="Dynamic" 
                                                                                                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                                                        </span>
                                                                                                                                        <span lang="EN-US" style="font-size:12.0pt;font-family:
&quot;Times New Roman&quot;,&quot;serif&quot;">/HĐTV-KL/</span><span lang="EN-US" style="font-size:
12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm;
mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosBody04" runat="server" class="textDoc" placeholder="" Width="65px"></asp:TextBox>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                                                                                                                                            ControlToValidate="txtMerPosBody04" CssClass="tlp-error" Display="Dynamic" 
                                                                                                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                                                        , ngày
                                                                                                                                        <asp:TextBox ID="txtMerPosBody05" runat="server" class="textDoc" 
                                                                                                                                            placeholder="dd" Width="30px"></asp:TextBox>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                                                                                                                                            ControlToValidate="txtMerPosBody05" CssClass="tlp-error" Display="Dynamic" 
                                                                                                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                                                        tháng
                                                                                                                                        <asp:TextBox ID="txtMerPosBody06" runat="server" class="textDoc" 
                                                                                                                                            placeholder="MM" Width="30px"></asp:TextBox>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                                                                                                                            ControlToValidate="txtMerPosBody06" CssClass="tlp-error" Display="Dynamic" 
                                                                                                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                                                        năm
                                                                                                                                        <asp:TextBox ID="txtMerPosBody07" runat="server" class="textDoc" 
                                                                                                                                            placeholder="yyyy" Width="40px"></asp:TextBox>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                                                                                                                                            ControlToValidate="txtMerPosBody07" CssClass="tlp-error" Display="Dynamic" 
                                                                                                                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator>
                                                                                                                                        cụ thể như sau:<p>
                                                                                                                                        </p>
                                                                                                                                        </span>
                                                                                                                                        <p>
                                                                                                                                        </p>
                                                                                                                                        <p class="MsoNormal">
                                                                                                                                            <![if !supportLists]>
                                                                                                                                            <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;"><span style="mso-list:Ignore">-<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span></span><![endif]>
                                                                                                                                            <span lang="EN-US" style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;"><asp:TextBox ID="txtMerCostTitle" runat="server" style="width:85%;text-align:left;margin-bottom:5px;" class="textDoc" Text="Phí hàng tháng là : ……………. đồng/tháng (Nếu không phát sinh hoá đơn GTGT)"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Xin nhập chi phí dịch vụ"
                                                                                                        ControlToValidate="txtMerCostTitle" Display="Dynamic" ForeColor="Red" ValidationGroup="G113"
                                                                                                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                                                                                                            </span>
                                                                                                                                            <p class="MsoListParagraphCxSpFirst" style="margin-left:2.0cm;mso-add-space:auto;
text-align:justify;line-height:16.0pt;mso-line-height-rule:exactly;tab-stops:
dotted 432.35pt">
                                                                                                                                                <span lang="EN-US"><asp:TextBox ID="txtMerCostDetail" runat="server" 
                                                                                                        style="width:70%;text-align:left;" class="textDoc" Text="* Từ 1 đến 10 hoá đơn:
* Từ 11 đến 20 hoá đơn:
* Thêm 20.000đ cho mỗi hóa đơn tăng thêm từ …… hóa đơn trở lên" 
                                                                                                        Rows="5" TextMode="MultiLine"></asp:TextBox></span></p>
                                                                                                                                            <p class="MsoListParagraphCxSpLast" 
                                                                                                                                                style="margin-left: 0cm; mso-add-space: auto; line-height: 14.65pt; background: white">
                                                                                                                                                <i><span lang="EN-US">Toàn bộ phí trên chưa bao gồm thuế GTGT và sẽ được xem xét 
                                                                                                                                                lại vào tháng 01 của năm mới hoặc khi công việc kinh doanh của bên A có thay đổi 
                                                                                                                                                so với lúc ký hợp đồng</span></i><span lang="EN-US" style="border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm;mso-bidi-font-weight:bold"><p>
                                                                                                                                                </p>
                                                                                                                                                </span>
                                                                                                                                                <p>
                                                                                                                                                </p>
                                                                                                                                                <p class="MsoNormal">
                                                                                                                                                    <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm;
mso-bidi-font-weight:bold">Phụ lục hợp đồng này có hiệu lực từ ngày </span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosBody08" runat="server" class="textDoc" placeholder="dd" Width="30px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
                                                                                ControlToValidate="txtMerPosBody08" CssClass="tlp-error" Display="Dynamic" 
                                                                                ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm;
mso-bidi-font-weight:bold">/</span><span lang="EN-US" style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm;
mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosBody09" runat="server" class="textDoc" placeholder="MM" Width="30px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" 
                                        ControlToValidate="txtMerPosBody09" CssClass="tlp-error" Display="Dynamic" 
                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm;mso-bidi-font-weight:bold">/</span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosBody10" runat="server" class="textDoc" placeholder="yyyy" Width="40px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" 
                                        ControlToValidate="txtMerPosBody10" CssClass="tlp-error" Display="Dynamic" 
                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm;
mso-bidi-font-weight:bold">, thời hạn1 năm kể từ ngày ký phụ lục hợp đồng này<p>
                                                                                                                                                    </p>
                                                                                                                                                    </span>
                                                                                                                                                    <p>
                                                                                                                                                    </p>
                                                                                                                                                    <p class="MsoNormal">
                                                                                                                                                        <b>
                                                                                                                                                        <span lang="EN-US" style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm">◊ Điều khoản chung :</span></b><span lang="EN-US" style="font-size:12.0pt;font-family:
&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;"><p>
                                                                                                                                                        </p>
                                                                                                                                                        </span>
                                                                                                                                                        <p>
                                                                                                                                                        </p>
                                                                                                                                                        <p class="MsoNormal">
                                                                                                                                                            <span lang="EN-US" style="font-size:12.0pt;font-family:
&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;">+ &nbsp;&nbsp;Quyền và nghĩa vụ của mỗi bên được quy định trong hợp đồng số&nbsp;<span style="border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosFo01" runat="server" class="textDoc" placeholder="" Width="65px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" 
                                        ControlToValidate="txtMerPosFo01" CssClass="tlp-error" Display="Dynamic" 
                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span></span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;">/HĐTV-KL/</span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosFo02" runat="server" class="textDoc" placeholder="" Width="65px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" 
                                        ControlToValidate="txtMerPosFo02" CssClass="tlp-error" Display="Dynamic" 
                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;"><p>
                                                                                                                                                            </p>
                                                                                                                                                            </span>
                                                                                                                                                            <p>
                                                                                                                                                            </p>
                                                                                                                                                            <p class="MsoNormal">
                                                                                                                                                                <span lang="EN-US" style="font-size:12.0pt;font-family:
&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;">+&nbsp;&nbsp; PL HĐ được lập thành 02 (hai)</span><span lang="EN-US"><a href="http://www.luatminhgia.vn/"><span style="font-size:12.0pt;font-family:
&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;color:windowtext;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm;
text-decoration:none;text-underline:none">,</span></a></span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;">&nbsp;có nội dung &amp; giá trị pháp lý như nhau, mỗi bên giữ 01 bản.<br /> +&nbsp;&nbsp; Phụ lục này là 1 phần không thể tách rời của HĐKT số <span style="border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosFo03" runat="server" class="textDoc" placeholder="" Width="65px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" 
                                        ControlToValidate="txtMerPosFo03" CssClass="tlp-error" Display="Dynamic" 
                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span></span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;">/HĐTV-KL/</span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosFo04" runat="server" class="textDoc" placeholder="" Width="75px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" 
                                        ControlToValidate="txtMerPosFo04" CssClass="tlp-error" Display="Dynamic" 
                                        ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator> </span><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;">và có giá trị kể từ ngày <span style="border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosFo05" runat="server" class="textDoc" placeholder="dd" Width="30px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" 
                                            ControlToValidate="txtMerPosFo05" CssClass="tlp-error" Display="Dynamic" 
                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span>/<span style="border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosFo06" runat="server" class="textDoc" placeholder="MM" Width="30px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" 
                                            ControlToValidate="txtMerPosFo06" CssClass="tlp-error" Display="Dynamic" 
                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span>/<span style="border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;
padding:0cm;mso-bidi-font-weight:bold"><asp:TextBox ID="txtMerPosFo07" runat="server" class="textDoc" placeholder="yyyy" Width="40px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" 
                                            ControlToValidate="txtMerPosFo07" CssClass="tlp-error" Display="Dynamic" 
                                            ForeColor="Red" ValidationGroup="G113">*</asp:RequiredFieldValidator></span><p>
                                                                                                                                                                </p>
                                                                                                                                                                </span>
                                                                                                                                                                <p>
                                                                                                                                                                </p>
                                                                                                                                                                <p class="MsoNormal">
                                                                                                                                                                    <b>
                                                                                                                                                                    <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm">
                                                                                                                                                                    <p>
                                                                                                                                                                        &nbsp;</p>
                                                                                                                                                                    </span></b>
                                                                                                                                                                    <p>
                                                                                                                                                                    </p>
                                                                                                                                                                    <p align="center" class="MsoNormal">
                                                                                                                                                                        <b>
                                                                                                                                                                        <span lang="EN-US" style="font-size:
12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;
border:none windowtext 1.0pt;mso-border-alt:none windowtext 0cm;padding:0cm">ĐẠI DIỆN BÊN A<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>ĐẠI DIỆN BÊN B</span></b><span lang="EN-US" style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;"><p>
                                                                                                                                                                        </p>
                                                                                                                                                                        </span>
                                                                                                                                                                        <p>
                                                                                                                                                                        </p>
                                                                                                                                                                        <p align="center" class="MsoNormal">
                                                                                                                                                                            <b>
                                                                                                                                                                            <span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;border:none windowtext 1.0pt;
mso-border-alt:none windowtext 0cm;padding:0cm">GIÁM ĐỐC<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>GIÁM ĐỐC</span></b><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;"><p>
                                                                                                                                                                            </p>
                                                                                                                                                                            </span>
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
