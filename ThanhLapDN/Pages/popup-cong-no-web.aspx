<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-cong-no-web.aspx.cs" Inherits="ThanhLapDN.Pages.popup_cong_no_web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function tongcong() {
        var txtDomain = document.getElementById("<%=txtDomain.ClientID %>");
        var txtHosting = document.getElementById("<%=txtHosting.ClientID %>");
        var txtWeb = document.getElementById("<%=txtWeb.ClientID %>");
        var txtPhanmem = document.getElementById("<%=txtPhanmem.ClientID %>");
        var txtChiphitrienkhai = document.getElementById("<%=txtChiphitrienkhai.ClientID %>");
        var txtLogo = document.getElementById("<%=txtLogo.ClientID %>");
        var txtEsell = document.getElementById("<%=txtEsell.ClientID %>");
        var txtChuphinh = document.getElementById("<%=txtChuphinh.ClientID %>");
        var txtCatalogue = document.getElementById("<%=txtCatalogue.ClientID %>");
        var txtSeotukhoa = document.getElementById("<%=txtSeotukhoa.ClientID %>");
        var txtGoogleadword = document.getElementById("<%=txtGoogleadword.ClientID %>");
        var txtVAT = document.getElementById("<%=txtVAT.ClientID %>");
        var txtTongcong = document.getElementById("<%=txtTongcong.ClientID %>");

        var domain = parseInt(txtDomain.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(domain))
            domain = 0;
        var hosting = parseInt(txtHosting.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(hosting))
            hosting = 0;
        var web = parseInt(txtWeb.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(web))
            web = 0;
        var phanmem = parseInt(txtPhanmem.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(phanmem))
            phanmem = 0;
        var chiphitrienkhai = parseInt(txtChiphitrienkhai.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(chiphitrienkhai))
            chiphitrienkhai = 0;
        var logo = parseInt(txtLogo.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(logo))
            logo = 0;
        var esell = parseInt(txtEsell.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(esell))
            esell = 0;
        var chuphinh = parseInt(txtChuphinh.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(chuphinh))
            chuphinh = 0;
        var catalogue = parseInt(txtCatalogue.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(catalogue))
            catalogue = 0;
        var seotukhoa = parseInt(txtSeotukhoa.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(seotukhoa))
            seotukhoa = 0;
        var goodleadword = parseInt(txtGoogleadword.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(goodleadword))
            goodleadword = 0;
        var vat = parseInt(txtVAT.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(vat))
            vat = 0;

        txtTongcong.value = domain + hosting + web + phanmem + chiphitrienkhai + logo + esell + chuphinh + catalogue + seotukhoa + goodleadword + vat;
        
        var num;
        num = txtTongcong.value.toString().replace(/\$|\,/g, '');
        if (isNaN(num))
            num = "";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        num = Math.floor(num / 100).toString();
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' +
        num.substring(num.length - (4 * i + 3));
        //return (((sign)?'':'-') + num); 
        txtTongcong.value = (((sign) ? '' : '-') + num);
    }
    function thanhtoan() {
        var txtTongcong = document.getElementById("<%=txtTongcong.ClientID %>");
        var txtThanhtoan = document.getElementById("<%=txtThanhtoan.ClientID %>");
        var txtCongno = document.getElementById("<%=txtCongno.ClientID %>");

        var tongcong = parseInt(txtTongcong.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(tongcong))
            tongcong = 0;
        var thanhtoan = parseInt(txtThanhtoan.value.toString().replace(/\$|\,/g, ''));
        if (isNaN(thanhtoan))
            thanhtoan = 0;

        txtCongno.value = tongcong- thanhtoan;

        var num;
        num = txtCongno.value.toString().replace(/\$|\,/g, '');
        if (isNaN(num))
            num = "";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        num = Math.floor(num / 100).toString();
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' +
        num.substring(num.length - (4 * i + 3));
        //return (((sign)?'':'-') + num); 
        txtCongno.value = (((sign) ? '' : '-') + num);
    }
    function FormatNumber(obj) {
        var strvalue;
        if (eval(obj))
            strvalue = eval(obj).value;
        else
            strvalue = objtxtSTT
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
<link href="/Calendar/calendarStyle.css" rel="stylesheet" type="text/css" />
<link href="/Styles/siteCongNo.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server" method="post" autocomplete="off">
  <table class="tCongNo">
    <tr>
      <td colspan="2" style="background:none;text-align:left;"></td>
      <td colspan="2" style="background:none;text-align:right;">
        <asp:LinkButton ID="btnSave" runat="server" onclick="btnSave_Click" ValidationGroup="G80"><img src="../Images/ICON_SAVE.jpg" title="Lưu" width="30"/></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="btnClose" runat="server" onclick="btnClose_Click"><img src="../Images/icon-32-cancel.png" title="Đóng" width="30"/></asp:LinkButton></td>
    </tr>
  </table>

  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">THÔNG TIN CHUNG</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo">
      <tr>
        <td>Số hợp đồng</td>
        <td>
            <asp:TextBox ID="txtSohopdong" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập số hợp đồng!"
                ControlToValidate="txtSohopdong" Display="None" ForeColor="Red" ValidationGroup="G80">*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td>Ngày ký hợp đồng</td>
        <td>
            <link rel="stylesheet" href="/Styles/BeatPicker.min.css" />
            <script src="/Scripts/BeatPicker.min.js"></script>
            <input type="text" data-beatpicker="true" data-beatpicker-position="['bottom','*']" data-beatpicker-module="clear" data-beatpicker-format="['DD','MM','YYYY'],separator:'/'" class="date depDate hasDatepicker fl" id="txtNgaykyhopdong" name="flights-checkin" placeholder="DD / MM / YYYY" runat="server" style="border: 1px #A9A9A9 solid; padding: 2px;" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Chưa chọn ngày ký hợp đồng!"
                ControlToValidate="txtNgaykyhopdong" Display="None" ForeColor="Red" ValidationGroup="G80">*</asp:RequiredFieldValidator>
        </td>
      </tr>      
      <tr>
        <td>Tên khách hàng</td>
        <td><asp:TextBox ID="txtTenkhachhang" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập tên khách hàng!"
                ControlToValidate="txtTenkhachhang" Display="None" ForeColor="Red" ValidationGroup="G80">*</asp:RequiredFieldValidator>
        </td>
      </tr>     
      <tr>
        <td>Thông tin liên hệ</td>
        <td><asp:TextBox ID="txtThongtinlienhe" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Chưa nhập thông tin liên hệ!"
                ControlToValidate="txtThongtinlienhe" Display="None" ForeColor="Red" ValidationGroup="G80">*</asp:RequiredFieldValidator>
        </td>
      </tr>
      </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo">     
      <tr>
        <td>Tên domain</td>
        <td><asp:TextBox ID="txtTendomain" runat="server"></asp:TextBox></td>
      </tr>
      <tr>
        <td>NVKD</td>
        <td><asp:DropDownList ID="ddlNVKD" runat="server"></asp:DropDownList></td>
      </tr>
      <tr>
        <td>NVXL</td>
        <td><asp:DropDownList ID="ddlNVXL" runat="server"></asp:DropDownList></td>
      </tr>     
      <tr>
        <td>Nội dung</td>
        <td><asp:TextBox ID="txtNoidung" runat="server" TextMode="MultiLine"></asp:TextBox></td>
      </tr>
      </table>
  </div>

  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">DOANH SỐ</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Domain</td>
        <td>
            <asp:TextBox ID="txtDomain" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>Hosting</td>
        <td>
            <asp:TextBox ID="txtHosting" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>Web</td>
        <td><asp:TextBox ID="txtWeb" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Phần mềm</td>
        <td><asp:TextBox ID="txtPhanmem" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Chi phí triển khai</td>
        <td><asp:TextBox ID="txtChiphitrienkhai" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">      
      <tr>
        <td>Logo, banner</td>
        <td><asp:TextBox ID="txtLogo" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Esell</td>
        <td><asp:TextBox ID="txtEsell" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Chụp hình</td>
        <td><asp:TextBox ID="txtChuphinh" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Catalogue</td>
        <td><asp:TextBox ID="txtCatalogue" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Seo từ khóa</td>
        <td><asp:TextBox ID="txtSeotukhoa" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Google adword</td>
        <td><asp:TextBox ID="txtGoogleadword" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
    </table>
  </div>

  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">THÔNG TIN CHUNG</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Hoa hồng KH</td>
        <td><asp:TextBox ID="txtHoahongkh" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"></asp:TextBox></td>
      </tr>
      <tr>
        <td>VAT</td>
        <td><asp:TextBox ID="txtVAT" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);tongcong();thanhtoan();" onblur="FormatNumber(this);tongcong();thanhtoan();" onchange="tongcong();thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tổng cộng</td>
        <td><asp:TextBox ID="txtTongcong" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);thanhtoan();" onblur="FormatNumber(this);thanhtoan();" onchange="thanhtoan();"></asp:TextBox></td>
      </tr>      
      <tr>
        <td>Thanh toán</td>
        <td><asp:TextBox ID="txtThanhtoan" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);thanhtoan();" onblur="FormatNumber(this);thanhtoan();" onchange="thanhtoan();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Công nợ</td>
        <td><asp:TextBox ID="txtCongno" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgaythanhtoan" runat="server"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Ngày xuất hóa đơn</td>
        <td><asp:TextBox ID="txtNgayxuathoadon" runat="server" ></asp:TextBox></td>
      </tr>
      <tr>
        <td>Ghi chú</td>
        <td><asp:TextBox ID="txtGhichu" runat="server"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tình trạng</td>
        <td>
            <asp:DropDownList ID="ddlTinhtrang" runat="server">
                <asp:ListItem Value="1" Text="Đang chờ"></asp:ListItem>
                <asp:ListItem Value="2" Text="Đang triển khai"></asp:ListItem>
                <asp:ListItem Value="3" Text="Hoàn tất"></asp:ListItem>
                <asp:ListItem Value="4" Text="Hủy"></asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
    </table>
  </div>
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    ValidationGroup="G80" ShowMessageBox="True" ShowSummary="False"/>
</form>
</body>
</html>
