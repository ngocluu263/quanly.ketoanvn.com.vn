<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-trang-thai-phu-luc-gia-han.aspx.cs" Inherits="ThanhLapDN.Pages.popup_trang_thai_phu_luc_gia_han" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>
        .p{margin:0px;}
        .p1{font-size: 18px;font-weight: 700;}
        .p2{font-size: 14px;}
        .textbox1
        {
            border-radius: 4px;
            border: 1px solid #7ec6e3;
            height: 50px;
            width:98%;
            padding: 2px 0px 2px 5px;
            margin-top:5px;
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
            width: 93%;
            }
        .bDone
        {
              margin-top: 10px;
              border: none;
              background-color: #8AC007;
              color: #fff;
              padding: 6px 26px;
              font-size: 16px;
              box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
              -moz-box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
              cursor: pointer;
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
        <div style="width:17%;float:left;">
            <img src="../Images/notification_done_mini.png" title="Done"/>
        </div>
        <div style="width:80%;float:right;">
            <p class="p p1">Cập nhật trạng thái.</p>
            <p class="p p2"><asp:Literal ID="liTitle" runat="server"></asp:Literal></p>
        </div>
        <div style="clear:both;"></div>
        <hr />
        Chọn trạng thái:
        <asp:DropDownList ID="ddlTrangThai" runat="server" style="width:100px;">
            <asp:ListItem Value="0" Text="Chưa xử lý"></asp:ListItem>
            <asp:ListItem Value="1" Text="Xử lý xong"></asp:ListItem>
            <asp:ListItem Value="2" Text="Không xử lý"></asp:ListItem>
            <asp:ListItem Value="3" Text="Đã hoàn thành"></asp:ListItem>
        </asp:DropDownList>
        <hr />
        <div id="ighichu" runat="server">
        Ghi chú:
        <asp:TextBox ID="txtGhiChu" runat="server" class="textbox1" style="width:100%;"
            TextMode="MultiLine" Rows="3"></asp:TextBox>
        <hr />
        </div>
        <asp:Button ID="btnDone" runat="server" Text="Cập nhật" CssClass="bDone" onclick="btnDone_Click"/>
        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
