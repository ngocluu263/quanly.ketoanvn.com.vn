<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="chi-tiet-ho-so.aspx.cs" Inherits="ThanhLapDN.Pages.chi_tiet_ho_so" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>

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
        .input-parent
        {
            float:left;
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
        function GetNumberTongPhi(obj) {
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
                num = num.substring(0, num.length - (4 * i + 3)) + num.substring(num.length - (4 * i + 3));

            var num1 = $('#CPHMain_ASPxPageControl2_txtDaThu').val().replace(',', '');
            num1 = num1.toString().replace(/\$|\,/g, '');
            if (isNaN(num1))
                num1 = "";
            sign = (num1 == (num1 = Math.abs(num1)));
            num1 = Math.floor(num1 * 100 + 0.50000000001);
            num1 = Math.floor(num1 / 100).toString();
            for (var i = 0; i < Math.floor((num1.length - (1 + i)) / 3); i++)
                num1 = num1.substring(0, num1.length - (4 * i + 3)) + num1.substring(num1.length - (4 * i + 3));
            $price1 = parseInt(num);
            $price2 = parseInt(num1);
            var num2 = $price1 - $price2;
            for (var i = 0; i < Math.floor(($price3.length - (1 + i)) / 3); i++)
                $price3 = $price3.substring(0, $price3.length - (4 * i + 3)) + ',' + $price3.substring($price3.length - (4 * i + 3));
            $(".lblConLai").html(num2);
        }
        function GetNumberDaThu(obj) {
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
                num = num.substring(0, num.length - (4 * i + 3)) + num.substring(num.length - (4 * i + 3));

            var num1 = $('#CPHMain_ASPxPageControl2_txtTongPhi').val().replace(',', '');
            num1 = num1.toString().replace(/\$|\,/g, '');
            if (isNaN(num1))
                num1 = "";
            sign = (num1 == (num1 = Math.abs(num1)));
            num1 = Math.floor(num1 * 100 + 0.50000000001);
            num1 = Math.floor(num1 / 100).toString();
            for (var i = 0; i < Math.floor((num1.length - (1 + i)) / 3); i++)
                num1 = num1.substring(0, num1.length - (4 * i + 3)) + num1.substring(num1.length - (4 * i + 3));
            $price1 = parseInt(num1);
            $price2 = parseInt(num);
            $price3 = $price1 - $price2;
            for (var i = 0; i < Math.floor(($price3.length - (1 + i)) / 3); i++)
                $price3 = $price3.substring(0, $price3.length - (4 * i + 3)) + ',' + $price3.substring($price3.length - (4 * i + 3));
            $(".lblConLai").html($price3);
        }
    </script>
    <link rel="stylesheet" href="/Styles/BeatPicker.min.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/BeatPicker.min.js"></script>
    <script>
        function DisplayItems() {

            var div = document.getElementById('items');
            if (div) {
                div.style.display = "block";
            }
        }
        function HideItems() {
            var div = document.getElementById('items');
            if (div) {
                div.style.display = "none";
            }
        }
        function ToggleVisibility() {

            var div = document.getElementById('items');
            if (div) {
                if (div.style.display == "block") {
                    HideItems();
                }
                else {
                    DisplayItems();
                }
            }
        }
        function SelectValue(obj) {
            var input = document.getElementById('ContentPlaceHolder1_Appointment1_txtFromTime');
            if (input) {
                input.value = obj.getAttribute("value");
            }
            HideItems();
        }

        function DisplayItems1() {

            var div = document.getElementById('items1');
            if (div) {
                div.style.display = "block";
            }
        }
        function HideItems1() {
            var div = document.getElementById('items1');
            if (div) {
                div.style.display = "none";
            }
        }
        function ToggleVisibility1() {

            var div = document.getElementById('items1');
            if (div) {
                if (div.style.display == "block") {
                    HideItems1();
                }
                else {
                    DisplayItems1();
                }
            }
        }
        function SelectValue1(obj) {
            var input = document.getElementById('ContentPlaceHolder1_Appointment1_txtToTime');
            if (input) {
                input.value = obj.getAttribute("value");
            }
            HideItems1();
        }
    </script>
    <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            CHI TIẾT HỒ SƠ
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
    <table width="100%" cellpadding="3" cellspacing="3">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Thông tin chung">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" id="iTop" runat="server">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel id="UpdatePanel1" runat="server" OnLoad = "OnLoad">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Font-Size="14px"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G2" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                    ErrorMessage="Email Định Dạng Chưa Đúng" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ForeColor="Red" ValidationGroup="G2"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtChEmail"
                                                    ErrorMessage="Email Định Dạng Chưa Đúng" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ForeColor="Red" ValidationGroup="G2"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr id="idNameUser" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Tên nhân viên tạo&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="ddlNameUser" runat="server" CssClass="k-textbox textbox">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="idActive" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Loại hồ sơ&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="k-textbox textbox" 
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="-----Select-----" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Hồ sơ thành lập công ty" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Hồ sơ thay đổi" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Hồ sơ hành chánh" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Đính kèm File
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:FileUpload ID="fileUpload" runat="server" CssClass="k-textbox textbox" /><br />
                                                <asp:Label ID="lblLoadNameFile" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Tổng phí&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTongPhi" runat="server" placeholder="vnđ" class="text" CssClass="k-textbox textbox" 
                                                onkeyup="GetNumberTongPhi(this);FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" Text="0"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa nhập tổng phí"
                                                    ControlToValidate="txtTongPhi" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Đã thu&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtDaThu" runat="server" placeholder="vnđ" class="text" CssClass="k-textbox textbox" style="width:200px;float:left;"
                                                onkeyup="GetNumberDaThu(this);FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" Text="0"></asp:TextBox>
                                                <input type="text" data-beatpicker="true" placeholder="Ngày thu tiền" data-beatpicker-position="['left','*']" data-beatpicker-module="clear" data-beatpicker-format="['DD','MM','YYYY'],separator:'/'" class="k-textbox textbox" 
                                                    id="txtFromDate" name="flights-checkin" style="background: #FFFFFF url(/Images/calendar_icon.jpg) 98% center no-repeat !important;width:190px;margin-left:10px;float:left;" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Chưa nhập phí đã thu"
                                                    ControlToValidate="txtDaThu" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Tổng phí thu phải lớn hơn phí đã thu"
                                                    ControlToCompare="txtTongPhi" ControlToValidate="txtDaThu" ForeColor="Red" 
                                                    Operator="LessThanEqual" ValidationGroup="G2" Type="Currency">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Còn lại&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:Label ID="lblConLai" runat="server" class="lblConLai"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="idLoaiHinh" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Loại hình đăng ký&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <dx:ASPxTreeList ID="ASPxTreeList_menu" runat="server" AutoGenerateColumns="False"
                                                    Width="400px" KeyFieldName="TYPE_ID" Theme="Aqua" 
                                                    ParentFieldName="TYPE_PARENT">
                                                    <Columns>
                                                        <dx:TreeListTextColumn Caption="TYPE_ID" FieldName="TYPE_ID" VisibleIndex="0" Visible="False">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Danh sách loại hình đăng ký" FieldName="TYPE_NAME" VisibleIndex="2"
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
                                    </table>
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" id="iCreate" runat="server">
                                        <tr>
                                            <td colspan="2" style="text-align:center;font-size:1.4em;">
                                                <p>Hồ sơ mới</p>
                                                <hr style="border:1px solid gray;width:80%;margin:auto;padding:0;"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Tên công ty&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTenCty" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập tên công ty"
                                                    ControlToValidate="txtTenCty" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Tên Giao dịch(Nếu có)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTenGiaoDich" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Tên Viết Tắt(Nếu có)
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTenVietTat" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Trụ sở chính&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTruSoChinh" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập địa chỉ trụ sở chính"
                                                    ControlToValidate="txtTruSoChinh" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Ngành nghề kinh doanh
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="ddlNganhKD" runat="server" CssClass="k-textbox textbox">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Chưa chọn nghành nghề kinh doanh"
                                                    ControlToValidate="ddlNganhKD" Display="Dynamic" ForeColor="Red" ValidationGroup="G2" InitialValue="0"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Tổng số vốn góp&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTongSoVonGop" runat="server" placeholder="vnđ" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Chưa nhập tổng số vốn góp"
                                                    ControlToValidate="txtTongSoVonGop" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Vốn pháp định&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtVonPhapDinh" runat="server" placeholder="vnđ" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Chưa nhập vốn pháp định"
                                                    ControlToValidate="txtVonPhapDinh" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Điện thoại liên hệ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtDienThoai" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Chưa nhập điện thoại liên hệ"
                                                    ControlToValidate="txtDienThoai" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Email&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtEmail" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Danh sách thành viên
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:Literal ID="liLoadMemLink" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" id="iChange" runat="server">
                                        <tr>
                                            <td colspan="2" style="text-align:center;font-size:1.4em;">
                                                <p>Hồ sơ thay đổi</p>
                                                <hr style="border:1px solid gray;width:80%;margin:auto;padding:0;"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Tên công ty&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtChTenCty" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Chưa nhập tên công ty"
                                                    ControlToValidate="txtChTenCty" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Địa chỉ công ty&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtChDiaChi" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Chưa nhập địa chỉ công ty"
                                                    ControlToValidate="txtChDiaChi" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Mã số thuế&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtChMST" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Chưa nhập mã số thuế"
                                                    ControlToValidate="txtChMST" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Số điện thoại&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtChDienThoai" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Chưa nhập mã số thuế"
                                                    ControlToValidate="txtChDienThoai" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Email&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtChEmail" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Nội dung cần thay đổi&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtChContent" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Chưa nhập nội dung thay đổi"
                                                    ControlToValidate="txtChContent" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
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
    <!--Popup-->
    <link rel="stylesheet" href="/Styles/dhtmlwindow.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/dhtmlwindow.js"></script>
    <link rel="stylesheet" href="/Styles/modal.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/modal.js"></script>
    <script type="text/javascript">
        function openDSTV(id, status) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-danh-sach-thanh-vien.aspx?id=' + id + '', 'Thêm thành viên', 'width=450px,height=500px,center=1,resize=1,scrolling=1');
        }
        function openDSTV1(id, status) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-thanh-vien.aspx?id=' + id + '', 'Danh sách thành viên', 'width=450px,height=500px,center=1,resize=1,scrolling=1');
        }
    </script>
    <!--End Popup-->
    <script type="text/javascript">
        function GetCurrentTime() {
            __doPostBack("<%=UpdatePanel1.UniqueID %>", "");
        }
    </script>
</asp:Content>