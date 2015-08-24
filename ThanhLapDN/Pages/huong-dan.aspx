<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="huong-dan.aspx.cs" Inherits="ThanhLapDN.Pages.huong_dan" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
<style type="text/css">
        .td_left
        {
            width: 30%;
            padding-right: 20px;
            font-size:13px;
        }
        .td_right
        {
            font-weight:bold;
            font-size:13px;
        }
    </style>
    <table width="60%" cellpadding="3" cellspacing="3" style="margin:auto;">
        <tr>
            <td>
                <dx:aspxpagecontrol ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Hướng dẫn">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3" style="color:#333333 !important;">
                                        <tr id="idMain1" runat="server">
                                            <td colspan="2" style="text-align:center;font-size:1.3em;">
                                                <p>File hướng dẫn sử dụng</p>
                                                <hr style="border:1px solid #B8B8B8;width:80%;margin:auto;padding:0;"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Nhân viên kinh doanh
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <a href="https://docs.google.com/document/d/1OYGQ4KLdjiuXzHkyf6qd0yDmS-AclndLZHe0uIcuqLs/edit?usp=sharing" target="_blank">Dành cho nhân viên kinh doanh</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Quản lý xử lý hồ sơ
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <a href="https://docs.google.com/document/d/1iyvGNa6iXBiKrMte1VAhRB_-69J9zaIiL-M_P0onphw/edit?usp=sharing" target="_blank">Dành cho quản lý xử lý hồ sơ</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Nhân viên xử lý hồ sơ
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <a href="https://docs.google.com/document/d/1WAK96_qMHRvrmN6JC_TZSJgY5_NaiUbO-meUksjJTL0/edit?usp=sharing" target="_blank">Dành cho nhân viên xử lý hồ sơ</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Nhân viên hành chánh
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <a href="https://docs.google.com/document/d/1z3s6Q_HUQH_MWrWuE8JKbcsRdlXDNYQlxw5yodbGQFY/edit?usp=sharing" target="_blank">Dành cho nhân viên hành chánh</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Nhân viên giao nhận
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <a href="https://docs.google.com/document/d/1HqpSeDeiNX7NbW-opyTxZIVA5PUvWEdSb1XhYeZwV4g/edit?usp=sharing" target="_blank">Dành cho nhân viên giao nhận</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Nhân viên nộp hồ sơ lên sở
                                            </td>
                                            <td class="td_right" align="left" nowrap="nowrap">
                                                <a href="https://docs.google.com/document/d/1XIy9uRu5LXjUrii9cEU6LFpUqMLLFAxLimfF-xV-ltY/edit?usp=sharing" target="_blank">Dành cho nhân viên nộp hồ sơ</a>
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
                </dx:aspxpagecontrol>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
