<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-quy-trinh-nop-hs.aspx.cs" Inherits="ThanhLapDN.Pages.popup_quy_trinh_nop_hs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/Calendar/calendarStyle.css" rel="stylesheet" type="text/css" />
    <style>
        .textbox1
        {
            border-radius: 4px;
            border: 1px solid #7ec6e3;
            height: 24px;
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
         .d1
         {
            margin-top: 5px;
            font-size: 19px;
            font-weight: 700;
             }
         .d1 img 
        {
            padding-right:5px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;">
        <div style="width:25%;float:left;">
            <asp:LinkButton ID="btnSend" runat="server" onclick="btnSend_Click"><img src="../Images/hand.png" title="Send"/></asp:LinkButton>
        </div>
        <div style="width:72%;float:right;">
            <div class="d1"><img src="../Images/user_group.png" height="18" />Quản lý xử lý hồ sơ</div>
        </div>
        <div style="clear:both;"></div>
        <hr />
        <asp:RadioButtonList ID="rdbStatus" runat="server" AutoPostBack="True" 
            onselectedindexchanged="rdbStatus_SelectedIndexChanged">
            <asp:ListItem Value="1" Text="Chưa nộp"></asp:ListItem>
            <asp:ListItem Value="2" Text="Đã nộp"></asp:ListItem>
        </asp:RadioButtonList>
        <div style="width:55%;margin-top:5px;" id="iDate" runat="server">
            <div style="float:left;">
                <label>Ngày nộp: </label><uc1:pickerAndCalendar ID="pickdate_Begin" runat="server"/>
            </div>
            <div style="float:right;">
                <label>Ngày lấy: </label><uc1:pickerAndCalendar ID="pickdate_End" runat="server" />
            </div>
        </div>
        <asp:TextBox ID="txtTitleFile" runat="server" class="textbox1" placeholder="Tên file kèm (Nếu có)"></asp:TextBox>
        <asp:FileUpload ID="FileUpload1" runat="server" class="input"
                ToolTip="File đính kèm" />
        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
