<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup-edit-file.aspx.cs" Inherits="ThanhLapDN.Pages.popup_edit_file" %>

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
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;">
        <div style="width:17%;float:left;">
            <asp:LinkButton ID="btnSave" runat="server" onclick="btnSave_Click"><img src="../Images/ICON_SAVE.jpg" title="Cập nhật file"/></asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="btnDelete" runat="server" onclick="btnDelete_Click"><img src="../Images/ICON_DELETE_ALL.jpg" title="Xóa file"/></asp:LinkButton>
        </div>
        <div style="width:80%;float:right;">
            
        </div>
        <div style="clear:both;"></div>
        <hr />
        <label>Tên file: </label><asp:TextBox ID="txtTitleFile" runat="server" class="textbox1" placeholder="Tên file đính kèm"></asp:TextBox>
        <label>File upload: </label><asp:FileUpload ID="FileUpload1" runat="server" class="input" ToolTip="File đính kèm" style="margin-bottom:10px;"/>
        <label>File đã tải lên: </label><asp:Literal ID="lifile" runat="server"></asp:Literal>
        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>