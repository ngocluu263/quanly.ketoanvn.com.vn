<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-cong-no-cks.aspx.cs" Inherits="ThanhLapDN.Pages.popup_cong_no_cks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
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
  <table class="tCongNo">
    <tr>
      <td class="tt_note">Tên công ty</td>
      <td class="note">
        <asp:TextBox ID="txtTenKH" runat="server" Width="100%" BackColor="#FFCC66"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Tên công ty không được để trống!"
            ControlToValidate="txtTenKH" Display="None" ForeColor="Red" ValidationGroup="G80">*</asp:RequiredFieldValidator>
      </td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo">
      <tr>
        <td>MST</td>
        <td>
            <asp:TextBox ID="txtMST" runat="server" BackColor="#FFCC66" Font-Bold="true"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mã số thuế không được để trống!"
                ControlToValidate="txtMST" Display="None" ForeColor="Red" ValidationGroup="G80">*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td>STT</td>
        <td><asp:TextBox ID="txtSTT" runat="server" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tình trạng</td>
        <td>
            <asp:DropDownList ID="ddlTinhTrang" runat="server">
            <asp:ListItem Value="0" Text="--- Chọn ---" Selected="True"></asp:ListItem>
            <asp:ListItem Value="Đại lý ra TB" Text="Đại lý ra TB"></asp:ListItem>
            <asp:ListItem Value="Hủy" Text="Hủy"></asp:ListItem>
            <asp:ListItem Value="Chuyển đối soát tháng sau" Text="Chuyển đối soát tháng sau"></asp:ListItem>
            <asp:ListItem Value="Đang xử lý" Text="Đang xử lý"></asp:ListItem>
            <asp:ListItem Value="Hoàn tất" Text="Hoàn tất"></asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>Loại hợp đồng</td>
        <td>
            <asp:DropDownList ID="ddlLoaiHD" runat="server">
            <asp:ListItem Value="0" Text="--- Chọn ---" Selected="True"></asp:ListItem>
            <asp:ListItem Value="2 bên" Text="2 bên"></asp:ListItem>
            <asp:ListItem Value="3 bên" Text="3 bên"></asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>
      </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo">
      <tr>
        <td>NVKD</td>
        <td><asp:DropDownList ID="ddlNVKD" runat="server"></asp:DropDownList></td>
      </tr>
      <tr>
        <td>Ngày nhận TB</td>
        <td>
            <asp:TextBox ID="txtNgayNhanTB" runat="server" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNgayNhanTB" 
                ErrorMessage="Ngày nhận TB không đúng định dạng!" ValidationGroup="G80" Text="*"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
      </tr>
      <tr>
        <td>Ghi chú</td>
        <td><asp:TextBox ID="txtGhiChu" runat="server" TextMode="MultiLine" ></asp:TextBox></td>
      </tr>
      </table>
  </div>
  <!--DỊCH VỤ CHỮ KÝ SỐ-->
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">DỊCH VỤ</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td colspan="4" class="tsubRow">Chữ ký số</td>
      </tr>
      <tr>
        <td>Nhà cung cấp</td>
        <td>
            <asp:DropDownList ID="ddlNhaCungCap" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlNhaCungCap_SelectedIndexChanged"></asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>Sản phẩm</td>
        <td>
            <asp:DropDownList ID="ddlSanPham" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlSanPham_SelectedIndexChanged"></asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>Phí DV</td>
        <td><asp:TextBox ID="txtCKS_PhiDV" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Giá TB Token</td>
        <td><asp:TextBox ID="txtCKS_GiaTbToken" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>VAT</td>
        <td><asp:TextBox ID="txtCKS_VAT" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tổng cộng</td>
        <td><asp:TextBox ID="txtCKS_TongCong" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" style="background-color:#E6FFE6;"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tỷ lệ hoa hồng</td>
        <td><asp:TextBox ID="txtCKS_HoaHong_TyLe" runat="server" placeholder="%" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Hoa hồng KL</td>
        <td><asp:TextBox ID="txtCKS_HoaHong_DL" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>VAT hoa hồng</td>
        <td><asp:TextBox ID="txtCKS_HoaHong_VAT" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tổng hoa hồng</td>
        <td><asp:TextBox ID="txtCKS_HoaHong" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" style="background-color:#E6FFE6;"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tổng thanh toán <br/>nhà cung cấp</td>
        <td><asp:TextBox ID="txtCKS_TongTTNhaCC" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" style="background-color:#B8E6B8;"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Ngày hết hạn</td>
        <td>
            <asp:TextBox ID="txtNgayHetHanTB" runat="server" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayHetHanTB" 
                ErrorMessage="Ngày hết hạn TB không đúng định dạng!" ValidationGroup="G80" Text="*"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td colspan="4" class="tsubRow">Phần mềm</td>
      </tr>
      <tr>
        <td>Nhà cung cấp</td>
        <td>
            <asp:DropDownList ID="ddlNhaCungCap1" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlNhaCungCap1_SelectedIndexChanged"></asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>Sản phẩm</td>
        <td>
            <asp:DropDownList ID="ddlSanPham1" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlSanPham1_SelectedIndexChanged"></asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>Phí DV</td>
        <td><asp:TextBox ID="txtPM_PhiDV" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Giá TB Token</td>
        <td><asp:TextBox ID="txtPM_GiaTbToken" runat="server" placeholder="vnđ" 
                onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                onchange="getno();" Enabled="False"></asp:TextBox></td>
      </tr>
      <tr>
        <td>VAT</td>
        <td><asp:TextBox ID="txtPM_VAT" runat="server" placeholder="vnđ" 
                onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                onchange="getno();" Enabled="False"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tổng cộng</td>
        <td><asp:TextBox ID="txtPM_TongCong" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" style="background-color:#E6FFE6;"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tỷ lệ hoa hồng</td>
        <td><asp:TextBox ID="txtPM_HoaHong_TyLe" runat="server" placeholder="%" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Hoa hồng KL</td>
        <td><asp:TextBox ID="txtPM_HoaHong_DL" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>VAT hoa hồng</td>
        <td><asp:TextBox ID="txtPM_HoaHong_VAT" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tổng hoa hồng</td>
        <td><asp:TextBox ID="txtPM_HoaHong" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" style="background-color:#E6FFE6;"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tổng thanh toán <br/>nhà cung cấp</td>
        <td><asp:TextBox ID="txtPM_TongTTNhaCC" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" style="background-color:#B8E6B8;"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Ngày hết hạn</td>
        <td>
            <asp:TextBox ID="txtNgayHetHanTB1" runat="server" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNgayHetHanTB1" 
                ErrorMessage="Ngày hết hạn TB không đúng định dạng!" ValidationGroup="G80" Text="*"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
      </tr>
    </table>
  </div>

  <!--PHẦN MỀM KHAI THUẾ QUA MẠNG-->

  <!--THU TIỀN KHÁCH HÀNG-->
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">THU TIỀN KHÁCH HÀNG</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td colspan="4" class="tsubRow">NVKD Trích Hoa Hồng</td>
      </tr>
      <tr>
        <td>Chữ ký số</td>
        <td><asp:TextBox ID="txtTTHoaHong_CKS" runat="server" Width="85" 
                AutoPostBack="True" ontextchanged="txtTTHoaHong_CKS_TextChanged"></asp:TextBox></td>
        <td>Phần mềm</td>
        <td><asp:TextBox ID="txtTTHoaHong_PM" runat="server" Width="85" AutoPostBack="True" 
                ontextchanged="txtTTHoaHong_PM_TextChanged"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td colspan="4" class="tsubRow">Đã thu tiền</td>
      </tr>
      <tr>
        <td>Token + Chữ ký số</td>
        <td><asp:TextBox ID="txtTT_TokenCKS" runat="server" AutoPostBack="True" 
                ontextchanged="txtTT_TokenCKS_TextChanged"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Phần mềm</td>
        <td><asp:TextBox ID="txtTT_PM" runat="server" AutoPostBack="True" 
                ontextchanged="txtTT_PM_TextChanged"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Ngày thu</td>
        <td><asp:TextBox ID="txtTT_NgayThu" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_alldebt">
        <tr>
          <td colspan="4" class="tcenter">CÒN NỢ</td>
        </tr>
        <tr>
          <td colspan="4" style="text-align:center;background:none;">
            <asp:TextBox ID="txtTT_ConNo" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" style="text-align:center;font-weight:700;background-color:#FFCCCC;"></asp:TextBox></td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
      ValidationGroup="G80" ShowMessageBox="True" ShowSummary="False"/>
</form>
</body>
</html>
