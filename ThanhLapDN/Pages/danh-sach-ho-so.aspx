<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-ho-so.aspx.cs" Inherits="ThanhLapDN.Pages.danh_sach_ho_so" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Hồ sơ
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-ho-so.aspx" title="Thêm dự án" class="k-button">
                <img alt="Thêm mới" src="../Images/addNew.png" /></a> &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
        </div>
        <div style="clear: both"></div>
    </div>
    <div id="div-search">
    <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSearch">
        <table style="width:100%;">
            <tbody>
                <tr>
                    <td>
                    <div class="div-sub">
                        <label>Từ khóa</label>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbtnDeleteKeyword" />
                            </Triggers>
                            <ContentTemplate>
                            <input class="k-textbox k-input search-noidung fill-input" style="width:200px !important;" id="txtKeyword"
                                name="txtKeyword" type="text" value="" runat="server" clientidmode="Static"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:LinkButton CssClass="k-button" ID="lbtnDeleteKeyword" ToolTip="Xóa tìm kiếm"
                            runat="server" OnClick="lbtnDeleteKeyword_Click"><img alt="Tìm kiếm" src="../Images/back.png" /></asp:LinkButton>
                    </div>
                    <div class="div-sub">
                        <label>Từ ngày</label>
                        <uc1:pickerAndCalendar ID="pickdate_Begin" runat="server" />
                    </div>
                    <div class="div-sub">
                        <label>Đến ngày</label>
                        <uc1:pickerAndCalendar ID="pickdate_End" runat="server" />
                    </div>
                    <div class="div-sub">
                        <label>Loại hồ sơ</label>
                        <asp:DropDownList ID="ddlTypeProf" runat="server" class="k-textbox k-input">
                            <asp:ListItem Value="0" Text="---Tất cả hồ sơ---" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Hồ sơ thành lập mới"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Hồ sơ thay đổi"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Hồ sơ hành chánh"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="div-sub">
                        <dx:ASPxCheckBox ID="chkViewDebt" runat="server" Text="Xem hồ sơ còn nợ">
                        </dx:ASPxCheckBox>
                    </div>
                    <div class="div-sub">
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><img alt="Tìm kiếm" src="../Images/search.png" /></asp:LinkButton>
                    </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_project" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua"
                    onbeforecolumnsortinggrouping="ASPxGridView1_project_BeforeColumnSortingGrouping" 
                    onpageindexchanged="ASPxGridView1_project_PageIndexChanged" 
                    ondatabound="ASPxGridView1_project_DataBound" 
                    onheaderfilterfillitems="ASPxGridView1_project_HeaderFilterFillItems">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" 
                            Width="20px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Loại hồ sơ" FieldName="PROF_TYPE"
                            Width="130px">
                            <DataItemTemplate>
                                <%# GetType(Eval("PROF_TYPE"))%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <Settings AllowHeaderFilter="True"/>
                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên công ty" FieldName="PROF_NAME" HeaderStyle-CssClass="visible-lg" CellStyle-CssClass="visible-lg">
                            <DataItemTemplate>
                            <a href="<%# getlink(Eval("ID")) %>">
                                    <%# Eval("PROF_NAME")%></a>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nhân viên tạo" FieldName="USER_ID" HeaderStyle-CssClass="visible-lg" CellStyle-CssClass="visible-lg"
                            Width="150px">
                            <DataItemTemplate>
                                <%# GetUser(Eval("USER_ID"))%>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="True"/>
                            <HeaderStyle Font-Bold="True" />
                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày cập nhật" HeaderStyle-CssClass="visible-lg" CellStyle-CssClass="visible-lg"
                            FieldName="PROF_DATE" Width="90px">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center"/>
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                            <DataItemTemplate>
                                <%# Eval("PROF_DATE", "{0:dd/MM/yyyy}")%>
                            </DataItemTemplate>
							<PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yyyy hh:mm tt">
                            </PropertiesTextEdit>
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="File đính kèm" HeaderStyle-CssClass="visible-lg" CellStyle-CssClass="visible-lg"
                            Width="160px">
                            <DataItemTemplate>
                                <a href='<%# getfiledinhkem(DataBinder.Eval(Container.DataItem, "ID"),DataBinder.Eval(Container.DataItem, "PROF_FILE")) %>'>
                                    <%# Eval("PROF_FILE")%></a>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Còn nợ" Visible="false" HeaderStyle-CssClass="visible-lg" CellStyle-CssClass="visible-lg"
                            Width="80px">
                            <DataItemTemplate>
                                <%# String.Format("{0:###,##0}", Convert.ToInt32(Eval("PROF_COST1")) - Convert.ToInt32(Eval("PROF_COST2")))%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>                  
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tình trạng" Width="140px" FieldName="PROF_STATUS" HeaderStyle-CssClass="visible-lg" CellStyle-CssClass="visible-lg">
                            <DataItemTemplate>
                                <%# Getstatus(Eval("PROF_STATUS"),Eval("PROF_LEVEL"))%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="DV kế toán" Width="30px" HeaderStyle-CssClass="visible-lg" CellStyle-CssClass="visible-lg">
                            <DataItemTemplate>
                                <%# getDVKT(Eval("PROF_TYPE"), Eval("PROF_TAXCODE"), Eval("PROF_PARENT"))%>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center"/>
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
						<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sửa" Width="30px" HeaderStyle-CssClass="visible-lg" CellStyle-CssClass="visible-lg">
                            <DataItemTemplate>
                                <a href="<%# getlink(Eval("ID")) %>" title="Xem hồ sơ dịch vụ kế toán">
                                    <img src="/Images/file_edit.png" width="24" style="cursor:pointer;"/>
                                </a>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <div class="ProjChild">
                                <%# GetInfoChild(Eval("ID"), Eval("PROF_TYPE"),Eval("PROF_NAME"), Eval("PROF_TRANSACTION"), Eval("PROF_ATC"), Eval("PROF_ADDRESS"), Eval("PROF_TOTAL_CAPITAL"), Eval("PROF_CAPITAL"), Eval("PROF_PHONE"), Eval("PROF_TAXCODE"), Eval("PROF_NOTE"), Eval("PROF_PARENT"))%>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsText EmptyDataRow="Chưa có hồ sơ mới" />
                    <SettingsDetail ShowDetailRow="True" />
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                    <Settings showfilterrow="True" ShowFilterRowMenu="True"/>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>

    <!--Complete-->
    <script src="../Complete/Script/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <link href="../Complete/Styles/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />
    <style>
        .ui-autocomplete-category
        {
            font-weight: bold;
            padding: .2em .4em;
            margin: .8em 0 .2em;
            line-height: 1.5;
        }
        .ui-autocomplete
        {
            max-height: 500px;
            overflow: hidden;
        }
    </style>
    <script>
        $.widget("custom.catcomplete", $.ui.autocomplete, {
            _create: function () {
                this._super();
                this.widget().menu("option", "items", "> :not(.ui-autocomplete-category)");
            },
            _renderMenu: function (ul, items) {
                var that = this,
            currentCategory = "";
                $.each(items, function (index, item) {
                    var li;
                    li = that._renderItemData(ul, item);
                    if (item.label) {
                        li.attr("aria-label", item.label + " : " + item.label);
                    }
                });
            }
        });
    </script>
    <script src="../Complete/Ajax/AjaxCompleteHoSo.js" type="text/javascript"></script>
    <!--End Complete-->
    <!--Popup-->
    <link rel="stylesheet" href="/Styles/dhtmlwindow.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/dhtmlwindow.js"></script>
    <link rel="stylesheet" href="/Styles/modal.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/modal.js"></script>
    <script type="text/javascript">
        function openDSTV(id, status) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-danh-sach-thanh-vien.aspx?id=' + id + '', 'Thêm thành viên', 'width=450px,height=500px,center=1,resize=1,scrolling=1');
        }
        function openDSTV1(id, status) {
            emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-thanh-vien.aspx?id=' + id + '', 'Danh sách thành viên', 'width=450px,height=500px,center=1,resize=1,scrolling=1');
        }
    </script>
    <!--End Popup-->
</asp:Content>