<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-quy-trinh-skip.aspx.cs" Inherits="ThanhLapDN.Pages.popup_quy_trinh_skip" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;">
        <div style="width:17%;float:left;">
            <img src="../Images/done.png" title="Done"/>
        </div>
        <div style="width:80%;float:right;">
            <p class="p p1">Chọn quy trình tiếp theo!</p>
            <asp:DropDownList ID="ddlProccess" runat="server" CssClass="input" DataTextField="Text" DataValueField="Value">
            </asp:DropDownList>
        </div>
        <div style="clear:both;"></div>
        <hr />
        <asp:Button ID="btnDone" runat="server" Text="Xác nhận" 
            CssClass="bDone" onclick="btnDone_Click" />
        <asp:Button ID="btnClose" runat="server" Text="Đóng" CssClass="bDone" 
            style="background-color: #808080;" onclick="btnClose_Click"/>
        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>