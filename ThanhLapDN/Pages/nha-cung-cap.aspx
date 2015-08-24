<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="nha-cung-cap.aspx.cs" Inherits="ThanhLapDN.Pages.nha_cung_cap" %>
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
            Nhà cung cấp chữ ký số - phần mềm
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G20"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveClose" ToolTip="Lưu và đóng" CssClass="k-button"
                runat="server" OnClick="lbtnSaveClose_Click" ValidationGroup="G20"><img alt="Lưu thông tin" src="../Images/icon-32-saveclose.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveNew" ToolTip="Lưu và thêm mới" CssClass="k-button"
                runat="server" OnClick="lbtnSaveNew_Click" ValidationGroup="G20"><img alt="Lưu thông tin" src="../Images/icon-32-save-new.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnDelete" ToolTip="Xóa" CssClass="k-button" runat="server"
                OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
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
                        <dx:TabPage Text="Thông tin chung">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G20" />
                                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Loại&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:RadioButtonList ID="rdoType" runat="server" RepeatColumns="2">
                                                    <asp:ListItem Value="0" Text="Chữ ký số" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Phần mềm"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>Mã nhà cung cấp&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtMaNCC" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập mã nhà cung cấp!"
                                                    ControlToValidate="txtMaNCC" Display="Dynamic" ForeColor="Red" ValidationGroup="G20"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tên nhà cung cấp&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtTenNCC" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tỷ lệ hoa hồng&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtHoaHong" runat="server" style="width:100px;" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" Text="0"></asp:TextBox>%
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1" align="right" valign="top" nowrap="nowrap">
                                                Thứ tự&nbsp;
                                            </td>
                                            <td align="left" nowrap="nowrap" class="style2">
                                                <asp:TextBox ID="txtThuTu" runat="server" style="width:100px;" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();" onchange="getno();" Text="0"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Hiển thị
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rblActive" runat="server" RepeatColumns="5">
                                                    <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
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
                    Width="100%" KeyFieldName="ID" Theme="Aqua" 
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
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Loại" Width="100px">
                            <DataItemTemplate>
                                <%# GetType(Eval("NCC_TYPE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã nhà cung cấp" 
                            FieldName="NCC_MA">
                            <DataItemTemplate>
                                <%# Eval("NCC_MA")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tỷ lệ hoa hồng" Width="100px"
                            FieldName="NCC_HOA_HONG">
                            <DataItemTemplate>
                                <%# Eval("NCC_HOA_HONG")%>%
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tình trạng" Width="80px">
                            <DataItemTemplate>
                                <%# Getactive(Eval("NCC_ACTIVE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Thứ tự" Width="60px">
                            <DataItemTemplate>
                                <%# Eval("NCC_ORDER")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sửa" Width="30px">
                            <DataItemTemplate>
                                <a href="nha-cung-cap.aspx?id=<%# Eval("ID") %>" title="Sửa hồ sơ">
                                    <img src="/Images/file_edit.png" width="24" style="cursor:pointer;"/>
                                </a>
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