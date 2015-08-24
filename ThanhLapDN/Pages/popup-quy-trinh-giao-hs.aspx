<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-quy-trinh-giao-hs.aspx.cs" Inherits="ThanhLapDN.Pages.popup_quy_trinh_giao_hs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <div style="width:72%;float:right;" id="iUser" runat="server">
            <select id="ListUser" style="width:100%" runat="server" class="UserList"></select>
            <div class="d1"><img src="../Images/user_group.png" height="18" /><asp:Literal ID="liTitle" runat="server"></asp:Literal></div>
        </div>
        <div style="clear:both;"></div>
        <hr />
        <asp:RadioButtonList ID="rdbStatus" runat="server" AutoPostBack="true" 
            onselectedindexchanged="rdbStatus_SelectedIndexChanged">
            <asp:ListItem Value="1" Text="Chưa giao"></asp:ListItem>
            <asp:ListItem Value="2" Text="Đã giao"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:TextBox ID="txtNote" runat="server" class="textbox1" 
            placeholder="Ghi chú (Nếu có)" TextMode="MultiLine"></asp:TextBox>
        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    <!--Script Employ-->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <script src="/Scripts/select2.js"></script>
    <link href="/Styles/select2.css" rel="stylesheet"/>
    <script>
        $(document).ready(function () {
            $(".UserList").select2({ placeholder: 'Send' });
        });
    </script>
    <!--End Employ-->
    </form>
</body>
</html>