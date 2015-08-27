<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-cong-no.aspx.cs" Inherits="ThanhLapDN.Pages.popup_cong_no"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
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
    $(document).ready(function () {
        if ($('#rdbLoainop_0').is(':checked')) {
            $('#trdanopthue1').show();
            $('#trdanopthue2').show();
            $('#trdanopthue3').show();
            $('#trdanopthue4').show();
            $('#trdanopthue5').show();
            $('#trdanopthue6').show();
            $('#trdanopthue7').show();
            $('#trdanopthue8').show();
            $('#trdanopthue9').show();
            $('#trdanopthue10').show();
            $('#trdanopthue11').show();
            $('#trdanopthue12').show();
        }
        else {
            $('#trdanopthue1').hide();
            $('#trdanopthue2').hide();
            $('#trdanopthue3').show();
            $('#trdanopthue4').hide();
            $('#trdanopthue5').hide();
            $('#trdanopthue6').show();
            $('#trdanopthue7').hide();
            $('#trdanopthue8').hide();
            $('#trdanopthue9').show();
            $('#trdanopthue10').hide();
            $('#trdanopthue11').hide();
            $('#trdanopthue12').show();
        }

        $('#rdbLoainop_0').on('change', function () {
            if ($(this).is(':checked')) {
                $('#trdanopthue1').show();
                $('#trdanopthue2').show();
                $('#trdanopthue3').show();
                $('#trdanopthue4').show();
                $('#trdanopthue5').show();
                $('#trdanopthue6').show();
                $('#trdanopthue7').show();
                $('#trdanopthue8').show();
                $('#trdanopthue9').show();
                $('#trdanopthue10').show();
                $('#trdanopthue11').show();
                $('#trdanopthue12').show();
            }
        });
        $('#rdbLoainop_1').on('change', function () {
            if ($(this).is(':checked')) {
                $('#trdanopthue1').hide();
                $('#trdanopthue2').hide();
                $('#trdanopthue3').show();
                $('#trdanopthue4').hide();
                $('#trdanopthue5').hide();
                $('#trdanopthue6').show();
                $('#trdanopthue7').hide();
                $('#trdanopthue8').hide();
                $('#trdanopthue9').show();
                $('#trdanopthue10').hide();
                $('#trdanopthue11').hide();
                $('#trdanopthue12').show();
            }
        });
    });
    
</script>
<link href="/Calendar/calendarStyle.css" rel="stylesheet" type="text/css" />
<link href="/Styles/siteCongNo.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
  <table class="tCongNo">
    <tr>
      <td colspan="2" style="background:none;text-align:left;">
        <asp:DropDownList ID="ddlNam" runat="server" Width="80" AutoPostBack="True" 
            onselectedindexchanged="ddlNam_SelectedIndexChanged">
          <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
          <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
          <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
          <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
          <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
          <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
          <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
        </asp:DropDownList></td>
      <td colspan="2" style="background:none;text-align:right;">
        <asp:LinkButton ID="btnSave" runat="server" onclick="btnSave_Click" ValidationGroup="G90"><img src="../Images/ICON_SAVE.jpg" title="Lưu" width="30"/></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="btnClose" runat="server" onclick="btnClose_Click"><img src="../Images/icon-32-cancel.png" title="Đóng" width="30"/></asp:LinkButton></td>
    </tr>
  </table>
<asp:PlaceHolder ID="pMain" runat="server">
  <table class="tCongNo">
    <tr>
      <td class="tt_note">Tên công ty</td>
      <td class="note">
        <asp:TextBox ID="txtTenKH" runat="server" Width="100%" BackColor="#FFCC66"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Tên công ty không được để trống!"
            ControlToValidate="txtTenKH" Display="None" ForeColor="Red" ValidationGroup="G90">*</asp:RequiredFieldValidator>
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
                ControlToValidate="txtMST" Display="None" ForeColor="Red" ValidationGroup="G90">*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td>STT</td>
        <td><asp:TextBox ID="txtSTT" runat="server" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Ngày thành lập</td>
        <td>
            <asp:TextBox ID="txtNgayThanhLap" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNgayThanhLap" 
                ErrorMessage="Ngày thành lập không đúng định dạng!" ValidationGroup="G90" Text="*"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
      </tr>
      <tr>
        <td>Địa chỉ</td>
        <td><asp:TextBox ID="txtDiaChi1" runat="server" TextMode="MultiLine"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Quản lý thuế</td>
        <td><asp:DropDownList ID="ddlCity" runat="server" Width="100" AutoPostBack="True" style="font-size:11px;height:22px;"
                    onselectedindexchanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlDist" runat="server" Width="90" style="font-size:11px;height:22px;"></asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>Người đại diện</td>
        <td><asp:TextBox ID="txtGiamDoc" runat="server"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Email</td>
        <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Điện thoại liên lạc</td>
        <td><asp:TextBox ID="txtDienThoai" runat="server"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tháng bắt đầu thu</td>
        <td><asp:TextBox ID="txtNgayThu" runat="server" ></asp:TextBox></td>
      </tr>
      <tr>
        <td>Nhân viên KD</td>
        <td><asp:DropDownList ID="ddlNVKD" runat="server"></asp:DropDownList></td>
      </tr>
      <tr>
        <td>Nhân viên GN</td>
        <td><asp:DropDownList ID="ddlNVGN" runat="server"> </asp:DropDownList></td>
      </tr>
      <tr>
        <td>NV Kế Toán</td>
        <td><asp:DropDownList ID="ddlNVKT" runat="server"> </asp:DropDownList></td>
      </tr>
      <tr>
        <td>Ngày ký HĐ</td>
        <td>
            <asp:TextBox ID="txtNgayKyHD" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNgayKyHD" 
                ErrorMessage="Ngày ký hợp đồng không đúng định dạng!" ValidationGroup="G90" Text="*" Display="None"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo">
      <tr>
        <td>Năm
          <asp:Literal ID="liNam" runat="server"></asp:Literal></td>
        <td><asp:TextBox ID="txtNoNamTruoc" runat="server" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Tình trạng</td>
        <td><asp:DropDownList ID="ddlTinhTrang" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlTinhTrang_SelectedIndexChanged">
            <asp:ListItem Value="---" Text="---" Selected="True"></asp:ListItem>
            <asp:ListItem Value="Tạm ngưng hoạt động" Text="Tạm ngưng hoạt động"></asp:ListItem>
            <asp:ListItem Value="Giải thể" Text="Giải thể"></asp:ListItem>
            <asp:ListItem Value="Ngừng dịch vụ" Text="Ngừng dịch vụ"></asp:ListItem>
            <asp:ListItem Value="Không thu phí" Text="Không thu phí"></asp:ListItem>
          </asp:DropDownList></td>
      </tr>
      <tr id="iThoigianBDKT" runat="server">
        <td><asp:Label ID="iThoiGianTieuDe" runat="server"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtNgayBatDau" runat="server" placeholder="dd/MM/yyyy" Width="91">&nbsp;</asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNgayBatDau" 
                ErrorMessage="Ngày bắt đầu không đúng định dạng!" ValidationGroup="G90" Text="*" Display="None"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
            <asp:TextBox ID="txtNgayKetThuc" runat="server" placeholder="dd/MM/yyyy" Width="91">&nbsp;</asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtNgayKetThuc" 
                ErrorMessage="Ngày kết thúc không đúng định dạng!" ValidationGroup="G90" Text="*" Display="None"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
      </tr>
      <tr>
        <td>Cố định/Không PS</td>
        <td><asp:TextBox ID="txtPhi" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Biểu phí 1</td>
        <td><asp:TextBox ID="txtBP1_SL" runat="server" placeholder="1-10" Width="60" CssClass="fee"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBP1_PHI" runat="server" placeholder="vnđ" Width="122" CssClass="fee" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Biểu phí 2</td>
        <td><asp:TextBox ID="txtBP2_SL" runat="server" placeholder="10-20" Width="60" CssClass="fee"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBP2_PHI" runat="server" placeholder="vnđ" Width="122" CssClass="fee" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Biểu phí 3</td>
        <td><asp:TextBox ID="txtBP3_SL" runat="server" placeholder="20-30" Width="60" CssClass="fee"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBP3_PHI" runat="server" placeholder="vnđ" Width="122" CssClass="fee" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Biểu phí 4</td>
        <td><asp:TextBox ID="txtBP4_SL" runat="server" placeholder="30-40" Width="60" CssClass="fee"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBP4_PHI" runat="server" placeholder="vnđ" Width="122" CssClass="fee" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Biểu phí 5</td>
        <td><asp:TextBox ID="txtBP5_SL" runat="server" placeholder="40-50" Width="60" CssClass="fee"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBP5_PHI" runat="server" placeholder="vnđ" Width="122" CssClass="fee" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Biểu phí 6</td>
        <td><asp:TextBox ID="txtBP6_SL" runat="server" placeholder="50-60" Width="60" CssClass="fee"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBP6_PHI" runat="server" placeholder="vnđ" Width="122" CssClass="fee" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Biểu phí thêm</td>
        <td><asp:TextBox ID="txtPhiPhatSinh" runat="server" placeholder="vnđ" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Loại chữ ký số</td>
        <td>
            <asp:DropDownList ID="ddlLoaiCKS" runat="server">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>Ngày hết hạn CKS</td>
        <td>
            <asp:TextBox ID="txtNgayHetHanCKS" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNgayHetHanCKS" 
                ErrorMessage="Ngày hết hạn chữ ký số không đúng định dạng!" ValidationGroup="G90" Text="*" Display="None"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
      </tr>
      <tr>
        <td>Có giữ CKS</td>
        <td><asp:CheckBox ID="chkCoGiuCKS" runat="server" Text="Có giữ" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkCoGiuCKS_CheckedChanged"/></td>
      </tr>
      <tr id="iNgayLayCKS" runat="server">
        <td>Ngày lấy CKS</td>
        <td>
            <asp:TextBox ID="txtNgayLayCKS" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtNgayLayCKS" 
                ErrorMessage="Ngày lấy chữ ký số không đúng định dạng!" ValidationGroup="G90" Text="*" Display="None"
                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
        <tr>
          <td class="tt_note">Ghi chú</td>
          <td class="note"><asp:TextBox ID="txtGhiChuMain" runat="server" TextMode="MultiLine" Width="100%" Rows="4"></asp:TextBox></td>
        </tr>
        <tr>
          <td class="tt_note">Loại nộp</td>
          <td class="note">
            <asp:RadioButtonList ID="rdbLoainop" runat="server" CssClass="danopthue" RepeatDirection="Horizontal">
                <asp:ListItem Text="Tháng" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Quý" Value="2"></asp:ListItem>
            </asp:RadioButtonList>
          </td>
        </tr>
      </table>
</asp:PlaceHolder>
  <!--Month 1-->
<asp:PlaceHolder ID="pMonth1" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 1</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV01" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL01" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL01_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD01" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD01_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT01" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT01_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT01_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT01_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT01_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo01" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">    
    <tr id="trdanopthue1">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue1" runat="server" CssClass="danopthue" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu01" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="2" class="tBottom"><asp:LinkButton ID="btnSaveT1" runat="server" CssClass="link" onclick="btnSaveT1_Click">Lưu tháng 1</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 2-->
<asp:PlaceHolder ID="pMonth2" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 2</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV02" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL02" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL02_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD02" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD02_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT02" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT02_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT02_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT02_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT02_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo02" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue2">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue2" runat="server" CssClass="danopthue" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu02" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT2" runat="server" CssClass="link" onclick="btnSaveT2_Click">Lưu tháng 2</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 3-->
<asp:PlaceHolder ID="pMonth3" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 3</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV03" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL03" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL03_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD03" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD03_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT03" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT03_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT03_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT03_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT03_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo03" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue3">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue3" runat="server" CssClass="danopthue" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu03" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT3" runat="server" CssClass="link" onclick="btnSaveT3_Click">Lưu tháng 3</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 4-->
<asp:PlaceHolder ID="pMonth4" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 4</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV04" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL04" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL04_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD04" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD04_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT04" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT04_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT04_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT04_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT04_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo04" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue4">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue4" runat="server" CssClass="danopthue" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu04" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT4" runat="server" CssClass="link" onclick="btnSaveT4_Click">Lưu tháng 4</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
<!--Month 5-->
<asp:PlaceHolder ID="pMonth5" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 5</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV05" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL05" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL05_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD05" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD05_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT05" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT05_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT05_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT05_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT05_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo05" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue5">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue5" runat="server" CssClass="danopthue" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu05" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT5" runat="server" CssClass="link" onclick="btnSaveT5_Click">Lưu tháng 5</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 6-->
<asp:PlaceHolder ID="pMonth6" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 6</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV06" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL06" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL06_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD06" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD06_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT06" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT06_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT06_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT06_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT06_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo06" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue6">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue6" CssClass="danopthue" runat="server" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu06" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT6" runat="server" CssClass="link" onclick="btnSaveT6_Click">Lưu tháng 6</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 7-->
<asp:PlaceHolder ID="pMonth7" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 7</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV07" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL07" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL07_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD07" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD07_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT07" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT07_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT07_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT07_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT07_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo07" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue7">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue7" CssClass="danopthue" runat="server" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu07" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT7" runat="server" CssClass="link" onclick="btnSaveT7_Click">Lưu tháng 7</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 8-->
<asp:PlaceHolder ID="pMonth8" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 8</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV08" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL08" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL08_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD08" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD08_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT08" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT08_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT08_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT08_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT08_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo08" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue8">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue8" CssClass="danopthue" runat="server" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu08" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT8" runat="server" CssClass="link" onclick="btnSaveT8_Click">Lưu tháng 8</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 9-->
<asp:PlaceHolder ID="pMonth9" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 9</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV09" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL09" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL09_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD09" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD09_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT09" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT09_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT09_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT09_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT09_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo09" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue9">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue9" CssClass="danopthue" runat="server" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu09" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT9" runat="server" CssClass="link" onclick="btnSaveT9_Click">Lưu tháng 9</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 10-->
<asp:PlaceHolder ID="pMonth10" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 10</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV10" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL10" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL10_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD10" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD10_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT10" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT10_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT10_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT10_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT10_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo10" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue10">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue10" CssClass="danopthue" runat="server" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu10" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT10" runat="server" CssClass="link" onclick="btnSaveT10_Click">Lưu tháng 10</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 11-->
<asp:PlaceHolder ID="pMonth11" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 11</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV11" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL11" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL11_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD11" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD11_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT11" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT11_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT11_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT11_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT11_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo11" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue11">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue11" CssClass="danopthue" runat="server" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu11" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT11" runat="server" CssClass="link" onclick="btnSaveT11_Click">Lưu tháng 11</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Month 12-->
<asp:PlaceHolder ID="pMonth12" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td class="tcenter tt_monh">Tháng 12</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV12" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL12" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL12_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD12" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD12_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT12" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT12_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT12_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT12_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT12_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo12" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr id="trdanopthue12">
      <td class="tt_note">Đã nộp thuế</td>
      <td class="note"><asp:CheckBox ID="chkDanopthue12" CssClass="danopthue" runat="server" Text="Đã nộp thuế" Font-Size="13px" /></td>
    </tr>
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu12" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT12" runat="server" CssClass="link" onclick="btnSaveT12_Click">Lưu tháng 12</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--Bao cao tai chinh-->
<asp:PlaceHolder ID="pMonth13" runat="server">
  <table class="tCongNo t_month">
    <tr>
      <td colspan="4" class="tcenter">Báo cáo tài chánh</td>
    </tr>
  </table>
  <div class="col_Congno">
    <table class="tCongNo t_month">
      <tr>
        <td>Phí dịch vụ</td>
        <td><asp:TextBox ID="txtPhiDV13" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>S/L theo BP</td>
        <td><asp:TextBox ID="txtSL13" runat="server" class="text" Width="35"
                    onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" 
                    onchange="getno();" AutoPostBack="True" ontextchanged="txtSL13_TextChanged"></asp:TextBox>
          <asp:CheckBox ID="chkBPCD13" runat="server" Text="Cố định/Không PS" Font-Size="13px"
                    AutoPostBack="True" oncheckedchanged="chkBPCD13_CheckedChanged"/></td>
      </tr>
      <tr>
        <td>Ngày thanh toán</td>
        <td><asp:TextBox ID="txtNgayTT13" runat="server"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <div class="col_Congno r_col">
    <table class="tCongNo t_month">
      <tr>
        <td>TT lần 1</td>
        <td><asp:TextBox ID="txtDaTT13_1" runat="server" Width="85" placeholder="vnđ - lần 1" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td> TT lần 2</td>
        <td><asp:TextBox ID="txtDaTT13_2" runat="server" Width="85" placeholder="vnđ - lần 2" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td> TT lần 3</td>
        <td><asp:TextBox ID="txtDaTT13_3" runat="server" Width="85" placeholder="vnđ - lần 3" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox>
        <td>TT lần 4</td>
        <td><asp:TextBox ID="txtDaTT13_4" runat="server" Width="85" placeholder="vnđ - lần 4" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
      <tr>
        <td>Còn nợ</td>
        <td class="cols_td" colspan="3"><asp:TextBox ID="txtConNo13" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();"></asp:TextBox></td>
      </tr>
    </table>
  </div>
  <table class="tCongNo t_month">
    <tr>
      <td class="tt_note">Ghi chú</td>
      <td class="note"><asp:TextBox ID="txtGhiChu13" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    </tr>
    <tr>
      <td colspan="6" class="tBottom"><asp:LinkButton ID="btnSaveT13" runat="server" CssClass="link" onclick="btnSaveT13_Click">Lưu BCTC</asp:LinkButton></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <!--All Debt-->
<asp:PlaceHolder ID="pDebt" runat="server">
  <table class="tCongNo t_alldebt" style="margin-bottom:20px;">
    <tr>
      <td colspan="4" class="tcenter">TỔNG NỢ</td>
    </tr>
    <tr>
      <td colspan="4" style="text-align:center;background:none;">
        <asp:TextBox ID="txtTongNo" runat="server" placeholder="vnđ" class="text" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" style="text-align:center;font-weight:700;"></asp:TextBox></td>
    </tr>
  </table>
</asp:PlaceHolder>
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
      ValidationGroup="G90" ShowMessageBox="True" ShowSummary="False"/>
</form>
</body>
</html>
