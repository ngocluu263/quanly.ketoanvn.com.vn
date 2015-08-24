<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-quy-trinh-ket-thuc.aspx.cs" Inherits="ThanhLapDN.Pages.popup_quy_trinh_ket_thuc" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;">
        <div style="width:17%;float:left;">
            <img src="../Images/done.png" title="Done"/>
        </div>
        <div style="width:80%;float:right;">
            <p class="p p1">Hoàn thành hồ sơ!</p>
            <p class="p p2">Bạn có muốn làm Hồ sơ hành chánh ?</p>
        </div>
        <div style="clear:both;"></div>
        <hr />
        <asp:RadioButtonList ID="rdbStatus" runat="server">
            <asp:ListItem Value="1" Text="Không, làm hồ sơ thuế" Selected="True"></asp:ListItem>
            <asp:ListItem Value="2" Text="Có, làm hồ sơ thuế"></asp:ListItem>
            <asp:ListItem Value="3" Text="Có, làm hồ sơ thuế + dịch vụ kế toán"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="btnDone" runat="server" Text="Hoàn thành" CssClass="bDone" 
            onclick="btnDone_Click"/>
        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
