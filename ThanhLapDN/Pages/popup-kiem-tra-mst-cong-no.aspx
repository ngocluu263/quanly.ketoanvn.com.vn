<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-kiem-tra-mst-cong-no.aspx.cs" Inherits="ThanhLapDN.Pages.popup_kiem_tra_mst_cong_no" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>
        .p
        {
            line-height:2px;
            }
        .p1
        {
            font-size: 18px;
            font-weight: 700;
            }
        .p2
        {
            font-size: 14px;
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
            <img src="../Images/automation.png" title="Done"/>
        </div>
        <div style="width:80%;float:right;">
            <p class="p p1"><asp:Literal ID="liTitle" runat="server"></asp:Literal></p>
            <p class="p p2">Kiểm tra các lỗi phát sinh trong dữ liệu hệ thống.</p>
            <asp:Button ID="btnDone" runat="server" Text="Xử lý" CssClass="bDone" 
                OnClientClick="return confirm('Hệ thống sẽ rà soát toàn bộ dữ liệu công nợ, xin đừng dừng chương trình cho tới khi hoàn thành!');" onclick="btnDone_Click"/>
        </div>
        <div style="clear:both;"></div>
        <hr />
        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
        <br />
        <asp:TextBox ID="txtContent" runat="server" Rows="15" TextMode="MultiLine" 
            Width="98%" Enabled="False"></asp:TextBox>
    </div>
    </form>
</body>
</html>
