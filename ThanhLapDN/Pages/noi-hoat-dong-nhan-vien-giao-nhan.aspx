<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="noi-hoat-dong-nhan-vien-giao-nhan.aspx.cs" Inherits="ThanhLapDN.Pages.noi_hoat_dong_nhan_vien_giao_nhan" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <style type="text/css">
        .td_left
        {
            width: 30%;
            padding-right: 50px;
        }
        .textbox
        {
            width: 400px;
        }
        .style1
        {
            width: 30%;
            padding-right: 50px;
            height: 32px;
        }
        .style2
        {
            height: 32px;
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
            Địa bàn hoạt động của nhân viên giao nhận
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G22"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnDelete" ToolTip="Xóa" CssClass="k-button" runat="server"
                OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnClose" ToolTip="Đóng" CssClass="k-button" runat="server"
                OnClick="lbtnClose_Click"><img alt="Đóng" src="../Images/icon-32-cancel.png" /></asp:LinkButton>
        </div>
    </div>

    <table width="100%" cellpadding="3" cellspacing="3" runat="server" id="tEditText"
        style="background-color: #f4f4f4;border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px" KeyFieldName="ID"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Thông tin chung">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G22" />
                                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Tên nhân viên&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="ddlNhanVienGN" runat="server" 
                                                    CssClass="k-textbox textbox" AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddlNhanVienGN_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Xin chọn tên nhân viên!"
                                                    ControlToValidate="ddlNhanVienGN" Display="Dynamic" ForeColor="Red" ValidationGroup="G22" InitialValue="0"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Tỉnh/Thành phố&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlThanhPho" runat="server" CssClass="k-textbox textbox" 
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlThanhPho_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Xin chọn thành phố!"
                                                    ControlToValidate="ddlThanhPho" Display="Dynamic" ForeColor="Red" ValidationGroup="G22" InitialValue="0"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Quận/Huyện&nbsp;
                                            </td>
                                            <td align="left">
                                                <dx:ASPxTreeList ID="ASPxTreeList_menu" runat="server" AutoGenerateColumns="False"
                                                    Width="400px" KeyFieldName="PROP_ID" Theme="Office2003Blue">
                                                    <SettingsSelection AllowSelectAll="True" Enabled="True" Recursive="True" />
                                                    <Columns>
                                                        <dx:TreeListTextColumn Caption="PROP_ID" FieldName="PROP_ID" VisibleIndex="0" Visible="False">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Danh sách quận/huyện" FieldName="PROP_NAME" VisibleIndex="2"
                                                            Width="95%">
                                                            <DataCellTemplate>
                                                                <%# Eval("PROP_NAME")%>
                                                            </DataCellTemplate>
                                                        </dx:TreeListTextColumn>
                                                    </Columns>
                                                    <SettingsSelection Enabled="True" AllowSelectAll="True" Recursive="True"></SettingsSelection>
                                                </dx:ASPxTreeList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Phạm vi hoạt động khác&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:CheckBoxList ID="chkOtherPos" runat="server">
                                                    <asp:ListItem Value="166" Text="Bình Dương" ></asp:ListItem>
                                                    <asp:ListItem Value="268" Text="Đồng Nai" ></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
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

    <table width="60%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4; margin:auto; border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_request" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="USER_ID" Theme="Aqua" 
                    onbeforecolumnsortinggrouping="ASPxGridView1_request_BeforeColumnSortingGrouping" 
                    onpageindexchanged="ASPxGridView1_request_PageIndexChanged">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="25px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="35px">
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên nhân viên" FieldName="USER_ID">
                            <DataItemTemplate>
                                <%# GetUser(Eval("USER_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Địa bàn làm việc" Width="400px">
                            <DataItemTemplate>
                                <%# GetQuanHuyen(Eval("USER_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                    <Styles>
                        <Header HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center"></Cell>
                    </Styles>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>