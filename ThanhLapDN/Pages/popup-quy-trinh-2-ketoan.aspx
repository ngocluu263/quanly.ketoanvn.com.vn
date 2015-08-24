<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-quy-trinh-2-ketoan.aspx.cs" Inherits="ThanhLapDN.Pages.popup_quy_trinh_2_ketoan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>
        .textbox1
        {
            border-radius: 4px;
            border: 1px solid #7ec6e3;
            height: 24px;
            width:97%;
            padding: 2px 0px 2px 5px;
            margin-bottom: 5px;
            }
         .input
        {
            border-radius: 4px;
            padding: 4px 12px;
            font-size: 14px;
            color: #666;
            border: 1px solid #7ec6e3;
            font-family: inherit;
            cursor:pointer;
            width: 90%;
            }
         .d1
         {
            margin-top: 8px;
            font-size: 19px;
            font-weight: 700;
             }
        .d1 img 
        {
            padding-right:5px;
            }
        fieldset
        {
            border:1px solid #CCCCCC;
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
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;">
        <div style="width:20%;float:left;">
            <asp:LinkButton ID="btnSend" runat="server" onclick="btnSend_Click" ValidationGroup="G22"><img src="../Images/hand.png" title="Send"/></asp:LinkButton>
        </div>
        <div style="width:77%;float:right;">
            <select id="ListUser" style="width:100%" runat="server" class="UserList"></select>
            <div class="d1"><img src="../Images/user_group.png" height="18" /><asp:Literal ID="liTitle" runat="server"></asp:Literal></div>
        </div>
        <div style="clear:both;"></div>
        <hr />
        <div style="width:45%;float:left;padding-right: 10px;">
            <asp:TextBox ID="txtNote" runat="server" class="textbox1" 
                placeholder="Ghi chú (Nếu có)" TextMode="MultiLine"></asp:TextBox>
            <asp:TextBox ID="txtTitleFile" runat="server" class="textbox1" placeholder="Tên file kèm (Nếu có)"></asp:TextBox>
            <asp:FileUpload ID="FileUpload1" runat="server" class="input"
                    ToolTip="File đính kèm" />
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
            <fieldset>
                <legend>Chọn:</legend>
                <asp:CheckBox ID="chk1" runat="server" Text="ĐK tài khoản ngân hàng"/><br />
                <asp:CheckBox ID="chk2" runat="server" Text="ĐK tính thuế GTGT phương pháp trực tiếp"/><br />
                <asp:CheckBox ID="chk3" runat="server" Text="ĐK tính thuế GTGT phương pháp khấu trừ"/><br />
                <asp:CheckBox ID="chk4" runat="server" Text="Đặt in hóa đơn"/>
            </fieldset>
        </div>
        <fieldset style="width:47%;float:left;">
            <legend>Khai báo thông tin:</legend>
            
            <table width="100%">
                <tr>
                    <td><label>Tên công ty</label></td>
                    <td>
                        <asp:TextBox ID="txtTenCTy" runat="server" class="textbox1" placeholder="Tên công ty"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Tên công ty không được bỏ trống!"
                            ControlToValidate="txtTenCTy" Display="None" ForeColor="Red" ValidationGroup="G22"
                            CssClass="tlp-error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><label>Mã số thuế</label></td>
                    <td>
                        <asp:TextBox ID="txtMST" runat="server" class="textbox1" placeholder="Mã số thuế"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mã số thuế không được bỏ trống!"
                            ControlToValidate="txtMST" Display="None" ForeColor="Red" ValidationGroup="G22"
                            CssClass="tlp-error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><label>Trụ sở chính</label></td>
                    <td>
                        <asp:TextBox ID="txtTruSoChinh" runat="server" class="textbox1" placeholder="Trụ sở chính"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Trụ sở chính không được bỏ trống!"
                            ControlToValidate="txtTruSoChinh" Display="None" ForeColor="Red" ValidationGroup="G22"
                            CssClass="tlp-error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><label>Tổng vốn góp</label></td>
                    <td>
                        <asp:TextBox ID="txtTongVonGop" runat="server" placeholder="Tổng vốn góp vnđ" class="text" CssClass="textbox1" 
                            onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><label>Vốn pháp định</label></td>
                    <td>
                        <asp:TextBox ID="txtVonPhapDinh" runat="server" placeholder="Vốn pháp định vnđ" class="text" CssClass="textbox1" 
                            onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><label>Số điện thoại</label></td>
                    <td>
                        <asp:TextBox ID="txtSDT" runat="server" class="textbox1" placeholder="Điện thoại liên hệ"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><label>Địa chỉ email</label></td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" class="textbox1" placeholder="Địa chỉ email"></asp:TextBox>
                    </td>
                </tr>
            </table>   
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="G22" />
        </fieldset>
    </div>
    <!--Script Employ-->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <script src="/Scripts/select2.js"></script>
    <link href="/Styles/select2.css" rel="stylesheet"/>
    <script>
        $(document).ready(function () {
            $(".UserList").select2({ placeholder: 'Người nhận' });
        });
    </script>
    <script>
        $(document).ready(function () {
            $('#chk2').click(function () {
                if ($('#chk2').attr('checked')) {
                    document.getElementById("chk3").checked = false;
                }
            });
            $('#chk3').click(function () {
                if ($('#chk3').attr('checked')) {
                    document.getElementById("chk2").checked = false;
                }
            });
        });
    </script>
    <!--End Employ-->
    </form>
</body>
</html>
