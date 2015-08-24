<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="phan-quyen.aspx.cs" Inherits="ThanhLapDN.Pages.phan_quyen" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <!--Script Employ-->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <link href="/Styles/select2.css" rel="stylesheet"/>
    <script src="/Scripts/select2.js"></script>
    <script>
        $(document).ready(function () {
            $(".UserList").select2({ placeholder: 'Tên nhân viên' });
        });
    </script>
    <!--End Employ-->
    <style type="text/css">
        .divMain {width:100%;}
        .divMain .divL{width:250px;float:left;}
        .divMain .divR{float:left;}
        .divMain p {margin-bottom:10px;font-size: 14px;font-weight: 700;}
        .form-control
        {
                display: block;
                width: 100%;
                height: 100%;
                padding: 6px 12px;
                font-size: 14px;
                line-height: 1.42857143;
                color: #555;
                background-color: #fff;
                background-image: none;
                border: 1px solid #ccc;
                border-radius: 4px;
                -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
                box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
                -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
                -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
                transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            }
          .btn
          {
                display: inline-block;
                width:80%;
                height:35px;
                padding: 6px 12px;
                margin-bottom: 10px;
                font-size: 14px;
                font-weight: 400;
                line-height: 1.42857143;
                text-align: center;
                white-space: nowrap;
                vertical-align: middle;
                -ms-touch-action: manipulation;
                touch-action: manipulation;
                cursor: pointer;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
                background-image: none;
                border: 1px solid transparent;
                border-radius: 4px;
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
    <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            Phân quyền
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G20"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnClose" ToolTip="Đóng" CssClass="k-button" runat="server"
                OnClick="lbtnClose_Click"><img alt="Đóng" src="../Images/icon-32-cancel.png" /></asp:LinkButton>
        </div>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px" KeyFieldName="ID"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Cấu hình chung">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <div class="divMain">
                                    <div style="margin-bottom:10px;">
                                        <asp:DropDownList ID="ddlMenu" runat="server" CssClass="k-textbox textbox" 
                                            Width="190" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="---Chọn mục phân quyền---"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Thêm/sửa hồ sơ sở"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Danh sách hồ sơ sở"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Danh sách công nợ"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width:41%; float:left;height:200px;">
                                        <asp:DropDownList ID="multiselect" runat="server" class="form-control" size="8" multiple="multiple">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width:16%; float:left;text-align: center;height:200px;">
                                        <button type="button" id="CPHMain_ASPxPageControl2_multiselect_rightAll" class="btn btn-block"><img src="../Images/playback_next_icon.png"/></button>
		                                <button type="button" id="CPHMain_ASPxPageControl2_multiselect_rightSelected" class="btn btn-block"><img src="../Images/playback_play1.png" width="24"/></button>
		                                <button type="button" id="CPHMain_ASPxPageControl2_multiselect_leftSelected" class="btn btn-block"><img src="../Images/playback_play2.png"/ width="24"></button>
		                                <button type="button" id="CPHMain_ASPxPageControl2_multiselect_leftAll" class="btn btn-block"><img src="../Images/playback_prev_icon.png"/></button>
                                    </div>
                                    <div style="width:41%; float:left;height:200px;">
                                        <asp:DropDownList ID="multiselect_to" runat="server" class="form-control" size="8" multiple="multiple">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="text-align:center;">
                                        <asp:CheckBoxList ID="chkFuntion" runat="server" RepeatColumns="5" style="margin:auto;border-bottom: 1px dotted gray;margin-bottom: 5px;"></asp:CheckBoxList>
                                        <asp:LinkButton ID="btnDown" runat="server" OnClick="btnDown_Click"><img src="../Images/down.png"/></asp:LinkButton>
                                    </div>
                                    <div>
                                        
                                    </div>
                                </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
                    </LoadingPanelImage>
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                </dx:ASPxPageControl>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="/Scripts/multiselect/js/multiselect.js"></script>
    <script type="text/javascript" src="/Scripts/multiselect/js/multiselect.min.js"></script>
    <script type="text/javascript">
            jQuery(document).ready(function ($) {
                $('#CPHMain_ASPxPageControl2_multiselect').multiselect({
                    keepRenderingSort: true
                });
            });
    </script>
</asp:Content>