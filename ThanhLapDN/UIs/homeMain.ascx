<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="homeMain.ascx.cs" Inherits="QuanLyDuAn.UIs.homeMain" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../Images/loading_bar.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:20%;left:35%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel id="UpdatePanel1" runat="server" OnLoad = "OnLoad">
<ContentTemplate>
<div class="menu">
    <div class="menu_parent" style="width:100%;">
        <table id="iSearch" runat="server" width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;border: 1px solid #aecaf0;font-size:13px;font-weight:bold;">
            <tr>
                <td><asp:Label ID="lblDangTienHanh" runat="server"></asp:Label></td>
                <td><asp:Label ID="lblDaHoanThanh" runat="server"></asp:Label></td>
                <td style="text-align:right;">
                    <asp:DropDownList ID="ddlListStatus" runat="server" CssClass="k-textbox textbox">
                        <asp:ListItem Text="Đang tiến hành" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Đã hoàn thành" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm"
                        runat="server" onclick="lbtnSearch_Click"><span class="p-i-search"></span></asp:LinkButton>
                </td>
            </tr>
        </table>

        <div class="navTask" id="iMainCreate" runat="server"><p><img src="../Images/new-page.png" width="22" height="22"/>HỒ SƠ THÀNH LẬP MỚI</p></div>
        <table width="100%" style="background-color: #0099CC;border: 1px solid #0099CC" id="layoutTable" runat="server">
            <tr>
                <td>
                    <dx:ASPxGridView ID="grv_create" runat="server" AutoGenerateColumns="False"
                        Width="100%" KeyFieldName="ID" Theme="Aqua" 
                        onbeforecolumnsortinggrouping="grv_create_BeforeColumnSortingGrouping" 
                        onpageindexchanged="grv_create_PageIndexChanged" 
                        ondatabound="grv_create_DataBound" 
                        onhtmlrowprepared="grv_create_HtmlRowPrepared">
                        <Columns>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên công ty" Width="220px" FieldName="PROF_NAME">
                                <DataItemTemplate>
                                    <%# Eval("PROF_NAME")%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày cập nhật" 
                                FieldName="PROF_DATE" Width="100px">
                                <DataItemTemplate>
                                    <%# Eval("PROF_DATE", "{0:dd/MM/yy HH:mm}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yy HH:mm">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center"/>
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày nộp HS" Width="80px" FieldName="NGAY_NHAN_HS">
                                <DataItemTemplate>
                                    <%# Eval("NGAY_NHAN_HS", "{0:dd/MM/yyyy}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày lấy HS" Width="80px" FieldName="NGAY_TRA_HS">
                                <DataItemTemplate>
                                    <%# Eval("NGAY_TRA_HS", "{0:dd/MM/yyyy}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Kinh Doanh" Width="100px" FieldName="USER_ID">
                                <DataItemTemplate>
                                    <%# GetUser(Eval("USER_ID"))%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="PROF_STATUS" 
                                Caption="Tình trạng" Width="200px">
                                <DataItemTemplate>
                                    <%# Getstatus(Eval("PROF_STATUS"),Eval("PROF_TYPE"))%>
                                    <%# getProcessStatus(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="#">
                                <DataItemTemplate>
                                    <%# getSendProcess(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>
                                    <a href="thong-tin-ho-so.aspx?id=<%# Eval("ID")%>" target="_blank" title="Thông tin hồ sơ"><img src="../Images/project.png" width="18" height="18"/></a>
                                    <%# getSkipProcess(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>               
                        </Columns>
                        <Templates>
                            <DetailRow>
                                <div class="borderChild lt">
                                <div class="dTitle">GIAI ĐOẠN</div>
                                <ul id='<%# Eval("ID")%>' style="height: 77px;overflow: hidden;">
                                <asp:Repeater ID="rptListTask" runat="server" DataSource='<%#listWorkflow(Eval("ID"))%>'>
                                    <ItemTemplate>
                                    <li>
                                        <%# Getstatus(Eval("WORK_STATUS"),1)%>
                                        <div class="items">
                                            <i class="ic"></i>
                                            Nhân viên tiếp nhận: <%# GetUser(Eval("USER_ID"))%>
                                        </div>
                                        <div class="items">
                                            <i class="ic"></i>
                                            Ngày tiếp nhận: <%# getDate(Eval("DATE"))%>
                                        </div>
                                        <%# getNote(Eval("WORK_FIELD1"))%>
                                    </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                                <div style="width:97%;text-align:right;"><a onclick="javascript:SetPayment(<%# Eval("ID")%>);" id="hplPaymentMore<%# Eval("ID")%>">Xem thêm</a></div>
                                </div>
                                <div class="borderChild rt">
                                    <div class="dTitle">FILE KÈM</div>
                                    <ul>
                                    <li><b>File thông tin:</b> <a href='<%# getfiledinhkem(DataBinder.Eval(Container.DataItem, "ID"),DataBinder.Eval(Container.DataItem, "PROF_FILE")) %>'>
                                        <%# Eval("PROF_FILE")%></a></li>
                                    <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#listFile(Eval("ID"))%>'>
                                        <ItemTemplate>
                                            <li>
                                            <a href='<%# getfiledinhkemPlus(DataBinder.Eval(Container.DataItem, "PROF_ID"),DataBinder.Eval(Container.DataItem, "ID"),DataBinder.Eval(Container.DataItem, "ATT_LINK")) %>'>
                                                <%# Eval("ATT_NAME")%></a><%# GetUserAtt(DataBinder.Eval(Container.DataItem, "ATT_USER"))%>
                                                <%# getEditFile(DataBinder.Eval(Container.DataItem, "ID"), DataBinder.Eval(Container.DataItem, "PROF_ID"))%>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </ul>
                                </div>
                            </DetailRow>
                        </Templates>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
                        <SettingsText EmptyDataRow="Chưa có hồ sơ mới" />
                        <SettingsDetail ShowDetailRow="True" />
                        <SettingsPager PageSize="10">
                        </SettingsPager>
                        <Styles>
                            <Cell>
                                <BorderLeft BorderStyle="None" />
                            </Cell>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>

        <div class="navTask" id="iMainChange" runat="server"><p><img src="../Images/change.png" width="22" height="22"/>HỒ SƠ THAY ĐỔI</p></div>
        <table width="100%" style="background-color: #0099CC;border: 1px solid #0099CC" id="Table1" runat="server">
            <tr>
                <td>
                    <dx:ASPxGridView ID="grv_change" runat="server" AutoGenerateColumns="False"
                        Width="100%" KeyFieldName="ID" Theme="Aqua" 
                        onbeforecolumnsortinggrouping="grv_change_BeforeColumnSortingGrouping" 
                        onpageindexchanged="grv_change_PageIndexChanged" 
                        ondatabound="grv_change_DataBound" 
                        onhtmlrowprepared="grv_change_HtmlRowPrepared">
                        <Columns>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên công ty" Width="220px" FieldName="PROF_NAME">
                                <DataItemTemplate>
                                    <%# Eval("PROF_NAME")%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày cập nhật" 
                                FieldName="PROF_DATE" Width="100px">
                                <DataItemTemplate>
                                    <%# Eval("PROF_DATE", "{0:dd/MM/yy HH:mm}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yy HH:mm">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày nộp HS" Width="80px" FieldName="NGAY_NHAN_HS">
                                <DataItemTemplate>
                                    <%# Eval("NGAY_NHAN_HS", "{0:dd/MM/yyyy}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày lấy HS" Width="80px" FieldName="NGAY_TRA_HS">
                                <DataItemTemplate>
                                    <%# Eval("NGAY_TRA_HS", "{0:dd/MM/yyyy}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Kinh Doanh" Width="100px" FieldName="USER_ID">
                                <DataItemTemplate>
                                    <%# GetUser(Eval("USER_ID"))%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="PROF_STATUS" Caption="Tình trạng" Width="200px">
                                <DataItemTemplate>
                                    <%# Getstatus(Eval("PROF_STATUS"),Eval("PROF_TYPE"))%>
                                    <%# getProcessStatus(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="True"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="#">
                                <DataItemTemplate>
                                    <%# getSendProcess(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>
                                    <a href="thong-tin-ho-so.aspx?id=<%# Eval("ID")%>" target="_blank" title="Thông tin hồ sơ"><img src="../Images/project.png" width="18" height="18"/></a>
                                    <%# getSkipProcess(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>               
                        </Columns>
                        <Templates>
                            <DetailRow>
                                <div class="borderChild lt">
                                <div class="dTitle">GIAI ĐOẠN</div>
                                <ul id='<%# Eval("ID")%>' style="height: 77px;overflow: hidden;">
                                <asp:Repeater ID="rptListTask" runat="server" DataSource='<%#listWorkflow(Eval("ID"))%>'>
                                    <ItemTemplate>
                                    <li>
                                        <%# Getstatus(Eval("WORK_STATUS"),2)%>
                                        <div class="items">
                                            <i class="ic"></i>
                                            Nhân viên tiếp nhận: <%# GetUser(Eval("USER_ID"))%>
                                        </div>
                                        <div class="items">
                                            <i class="ic"></i>
                                            Ngày tiếp nhận: <%# getDate(Eval("DATE"))%>
                                        </div>
                                        <%# getNote(Eval("WORK_FIELD1"))%>
                                    </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                                <div style="width:97%;text-align:right;"><a onclick="javascript:SetPayment(<%# Eval("ID")%>);" id="hplPaymentMore<%# Eval("ID")%>">Xem thêm</a></div>
                                </div>
                                <div class="borderChild rt">
                                    <div class="dTitle">FILE KÈM</div>
                                    <ul>
                                        <li><a href='<%# getfiledinhkem(DataBinder.Eval(Container.DataItem, "ID"),DataBinder.Eval(Container.DataItem, "PROF_FILE")) %>'>
                                            <%# Eval("PROF_FILE")%></a></li>
                                        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#listFile(Eval("ID"))%>'>
                                            <ItemTemplate>
                                                <li>
                                                <a href='<%# getfiledinhkemPlus(DataBinder.Eval(Container.DataItem, "PROF_ID"),DataBinder.Eval(Container.DataItem, "ID"),DataBinder.Eval(Container.DataItem, "ATT_LINK")) %>'>
                                                    <%# Eval("ATT_NAME")%></a><%# GetUserAtt(DataBinder.Eval(Container.DataItem, "ATT_USER"))%>
                                                    <%# getEditFile(DataBinder.Eval(Container.DataItem, "ID"), DataBinder.Eval(Container.DataItem, "PROF_ID"))%>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </DetailRow>
                        </Templates>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
                        <SettingsText EmptyDataRow="Chưa có hồ sơ mới" />
                        <SettingsDetail ShowDetailRow="True" />
                        <SettingsPager PageSize="10">
                        </SettingsPager>
                        <Styles>
                            <Cell>
                                <BorderLeft BorderStyle="None" />
                            </Cell>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>

        <div class="navTask" id="iMainAccounting" runat="server"><p><img src="../Images/icon_accouting.png" width="22" height="22"/>HỒ SƠ HÀNH CHÁNH</p></div>
        <table width="100%" style="background-color: #0099CC;border: 1px solid #0099CC" id="Table2" runat="server">
            <tr>
                <td>
                    <dx:ASPxGridView ID="grv_acc" runat="server" AutoGenerateColumns="False"
                        Width="100%" KeyFieldName="ID" Theme="Aqua" 
                        onbeforecolumnsortinggrouping="grv_acc_BeforeColumnSortingGrouping" 
                        onpageindexchanged="grv_acc_PageIndexChanged" 
                        ondatabound="grv_acc_DataBound" 
                        onhtmlrowprepared="grv_acc_HtmlRowPrepared">
                        <Columns>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên công ty" Width="220px" FieldName="PROF_NAME">
                                <DataItemTemplate>
                                    <%# Eval("PROF_NAME")%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày cập nhật" 
                                FieldName="PROF_DATE" Width="100px" >
                                <DataItemTemplate>
                                    <%# Eval("PROF_DATE", "{0:dd/MM/yy HH:mm}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yy HH:mm">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày nộp HS" Width="80px" FieldName="NGAY_NHAN_HS">
                                <DataItemTemplate>
                                    <%# Eval("NGAY_NHAN_HS", "{0:dd/MM/yyyy}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày lấy HS" 
                                Width="80px" FieldName="NGAY_TRA_HS" UnboundType="String">
                                <DataItemTemplate>
                                    <%# Eval("NGAY_TRA_HS", "{0:dd/MM/yyyy}")%>
                                </DataItemTemplate>
                                <PropertiesTextEdit DisplayFormatInEditMode="True" 
                                    DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" FilterMode="DisplayText"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Kinh Doanh" Width="100px" FieldName="USER_ID">
                                <DataItemTemplate>
                                    <%# GetUser(Eval("USER_ID"))%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="PROF_STATUS" Caption="Tình trạng" Width="200px">
                                <DataItemTemplate>
                                    <%# Getstatus(Eval("PROF_STATUS"),Eval("PROF_TYPE"))%>
                                    <%# getProcessStatus(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>
                                </DataItemTemplate>
                                <Settings AllowHeaderFilter="True"/>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="#">
                                <DataItemTemplate>
                                    <%# getSendProcess(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>
                                    <a href="thong-tin-ho-so.aspx?id=<%# Eval("ID")%>" target="_blank" title="Thông tin hồ sơ"><img src="../Images/project.png" width="18" height="18"/></a>
                                    <%--<%# getSkipProcess(Eval("ID"), Eval("PROF_STATUS"), Eval("PROF_TYPE"))%>--%>
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>               
                        </Columns>
                        <Templates>
                            <DetailRow>
                                <div class="borderChild lt">
                                <div class="dTitle">GIAI ĐOẠN</div>
                                <ul id='<%# Eval("ID")%>' style="height: 77px;overflow: hidden;">
                                <asp:Repeater ID="rptListTask" runat="server" DataSource='<%#listWorkflow(Eval("ID"))%>'>
                                    <ItemTemplate>
                                    <li>
                                        <%# Getstatus(Eval("WORK_STATUS"),3)%>
                                        <div class="items">
                                            <i class="ic"></i>
                                            Nhân viên tiếp nhận: <%# GetUser(Eval("USER_ID"))%>
                                        </div>
                                        <div class="items">
                                            <i class="ic"></i>
                                            Ngày tiếp nhận: <%# getDate(Eval("DATE"))%>
                                        </div>
                                        <%# getNote(Eval("WORK_FIELD1"))%>
                                    </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                                <div style="width:97%;text-align:right;"><a onclick="javascript:SetPayment(<%# Eval("ID")%>);" id="hplPaymentMore<%# Eval("ID")%>">Xem thêm</a></div>
                                </div>
                                <div class="borderChild rt">
                                    <div class="dTitle">FILE KÈM</div>
                                    <ul>
                                    <li><a href='<%# getfiledinhkem(DataBinder.Eval(Container.DataItem, "ID"),DataBinder.Eval(Container.DataItem, "PROF_FILE")) %>'>
                                        <%# Eval("PROF_FILE")%></a></li>
                                    <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#listFile(Eval("ID"))%>'>
                                        <ItemTemplate>
                                            <li>
                                            <a href='<%# getfiledinhkemPlus(DataBinder.Eval(Container.DataItem, "PROF_ID"),DataBinder.Eval(Container.DataItem, "ID"),DataBinder.Eval(Container.DataItem, "ATT_LINK")) %>'>
                                                <%# Eval("ATT_NAME")%></a><%# GetUserAtt(DataBinder.Eval(Container.DataItem, "ATT_USER"))%>
                                                <%# getEditFile(DataBinder.Eval(Container.DataItem, "ID"), DataBinder.Eval(Container.DataItem, "PROF_ID"))%>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </ul>
                                </div>
                            </DetailRow>
                        </Templates>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
                        <SettingsText EmptyDataRow="Chưa có hồ sơ mới" />
                        <SettingsDetail ShowDetailRow="True" />
                        <SettingsPager PageSize="10">
                        </SettingsPager>
                        <Styles>
                            <Cell>
                                <BorderLeft BorderStyle="None" />
                            </Cell>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
<!--Popup-->
<link rel="stylesheet" href="/Styles/dhtmlwindow.css" type="text/css" />
<script type="text/javascript" src="/Scripts/dhtmlwindow.js"></script>
<link rel="stylesheet" href="/Styles/modal.css" type="text/css" />
<script type="text/javascript" src="/Scripts/modal.js"></script>
<script type="text/javascript">
    function openProces(id,status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=450px,height=240px,center=1,resize=1,scrolling=1');
    }
    function openProcesHS(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-ky-hs.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=500px,height=220px,center=1,resize=1,scrolling=1');
    }
    function openProcesHS_KT(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-ky-hs-ketoan.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=500px,height=270px,center=1,resize=1,scrolling=1');
    }
    function openProcesNopHS(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-nop-hs.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=450px,height=280px,center=1,resize=1,scrolling=1');
    }
    function openProcesTraHS(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-tra-hs.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=450px,height=290px,center=1,resize=1,scrolling=1');
    }
    function openProcesTraHS_KT(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-tra-hs-ketoan.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=450px,height=330px,center=1,resize=1,scrolling=1');
    }
    function openProcesGiaoHS(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-giao-hs.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=450px,height=190px,center=1,resize=1,scrolling=1');
    }
    function openProcesKDHS(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-khac-dau.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=450px,height=190px,center=1,resize=1,scrolling=1');
    }
    function openProcesFn(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-ket-thuc.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Hoàn thành hồ sơ', 'width=350px,height=220px,center=1,resize=1,scrolling=1');
    }
    function openProcesFnMain(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-ket-thuc-main.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Hoàn thành hồ sơ', 'width=350px,height=180px,center=1,resize=1,scrolling=1');
    }
    function openProcesSkip(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-skip.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Bỏ qua quy trình', 'width=350px,height=150px,center=1,resize=1,scrolling=1');
    }
    function openProces1_KT(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-1-ketoan.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=450px,height=240px,center=1,resize=1,scrolling=1');
    }
    function openProces2_KT(id, status, type) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-quy-trinh-2-ketoan.aspx?id=' + id + '&status=' + status + '&type=' + type + '', 'Phân xử lý công việc', 'width=700px,height=430px,center=1,resize=1,scrolling=1');
    }
    function openEditFile(id, id_prof) {
        emailwindow = dhtmlmodal.open('EmailBox', 'iframe', '/Pages/popup-edit-file.aspx?id=' + id + '&id_prof=' + id_prof + '', 'Chỉnh sửa file đính kèm', 'width=500px,height=250px,center=1,resize=1,scrolling=1');
    }
</script>
<!--End Popup-->
<script type="text/javascript">
    function GetCurrentTime() {
        __doPostBack("<%=UpdatePanel1.UniqueID %>", "");
    }
</script>

<script language="javascript">
    function TogglePayment(id) {
        document.getElementById(id).style.height = "auto";
        document.getElementById('hplPaymentMore' + id).innerHTML = "<i class='iup'></i>Thu gọn";
        //document.getElementById('hplPaymentMore').style.background = "url(../vi-vn/images/arr_more.png) right -32px no-repeat";
    }
    function CollapsePayment(id) {
        document.getElementById(id).style.height = "77px";
        document.getElementById('hplPaymentMore' + id).innerHTML = "<i class='idow'></i>Xem thêm";
        //document.getElementById('hplPaymentMore').style.background = "url(../vi-vn/images/arr_more.png) right 13px no-repeat";
    }
    function SetPayment(id) {
        if (document.getElementById(id).style.height == "77px")
            TogglePayment(id);
        else
            CollapsePayment(id);
    }
</script>