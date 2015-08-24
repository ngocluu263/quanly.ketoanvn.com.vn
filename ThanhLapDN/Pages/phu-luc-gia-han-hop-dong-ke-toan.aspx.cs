using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;
using System.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace ThanhLapDN.Pages
{
    public partial class phu_luc_gia_han_hop_dong_ke_toan : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        MerPhuLucGiaHanData _MerPhuLucGiaHanData = new MerPhuLucGiaHanData();
        SendMail sm = new SendMail();
        int _id = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            TestPermission();
            if (!IsPostBack)
            {
                Load_Data();
            }
        }
        #region Data
        private void Load_Data()
        {
            var obj = db.MER_PHULUC_GIAHANs.Where(n => n.ID == _id).ToList();
            if (obj.Count > 0)
            {
                txtMerPos01.Text = obj[0].MER_POS01;
                txtMerPos02.Text = obj[0].MER_POS02;
                txtMerPos03.Text = obj[0].MER_POS03;
                txtMerPos04.Text = obj[0].MER_POS04;
                txtMerPos05.Text = obj[0].MER_POS05;
                txtMerPos06.Text = obj[0].MER_POS06;
                txtMerPos07.Text = obj[0].MER_POS07;
                txtMerPos08.Text = obj[0].MER_POS08;
                txtMerPos09.Text = obj[0].MER_POS09;
                txtMerPos10.Text = obj[0].MER_POS10;
                txtMerName.Text = obj[0].MER_NAME;
                txtMerAddress.Text = obj[0].MER_ADDRESS;
                txtMerPhone.Text = obj[0].MER_PHONE;
                txtMerTaxCode.Text = obj[0].MER_TAXCODE;
                txtMerEmail.Text = obj[0].MER_EMAIL;
                ddlDanhXung.SelectedValue = obj[0].MER_NX;
                txtMerRepresent.Text = obj[0].MER_REPRESENT;
                txtMerPosition.Text = obj[0].MER_POSITION;
                txtMerPosBody01.Text = obj[0].MER_POSBODY01;
                txtMerPosBody02.Text = obj[0].MER_POSBODY02;
                txtMerPosBody03.Text = obj[0].MER_POSBODY03;
                txtMerPosBody04.Text = obj[0].MER_POSBODY04;
                txtMerPosBody05.Text = obj[0].MER_POSBODY05;
                txtMerPosBody06.Text = obj[0].MER_POSBODY06;
                txtMerPosBody07.Text = obj[0].MER_POSBODY07;
                txtMerPosBody08.Text = obj[0].MER_POSBODY08;
                txtMerPosBody09.Text = obj[0].MER_POSBODY09;
                txtMerPosBody10.Text = obj[0].MER_POSBODY10;
                txtMerPosFo01.Text = obj[0].MER_POSFO01;
                txtMerPosFo02.Text = obj[0].MER_POSFO02;
                txtMerPosFo03.Text = obj[0].MER_POSFO03;
                txtMerPosFo04.Text = obj[0].MER_POSFO04;
                txtMerPosFo05.Text = obj[0].MER_POSFO05;
                txtMerPosFo06.Text = obj[0].MER_POSFO06;
                txtMerPosFo07.Text = obj[0].MER_POSFO07;
                txtMerCostTitle.Text = obj[0].MER_COST_TITLE;
                txtMerCostDetail.Text = obj[0].MER_COST_DETAIL;
            }
        }
        protected void Save_FileDoc(int _IdFile)
        {
            //Copy từ file gốc ra 1 bản 
            string _pathOrigin = Server.MapPath("/File/Template/TemplatePhuLucGiaHanHDDV.docx");
            string _pathNew = String.Format(Server.MapPath("/File/WordFile/PhuLucGiaHanDVKT/PhuLucGiaHanDVKeToan_Code_{0}.docx"), _IdFile);
            if (File.Exists(_pathNew))
                File.Delete(_pathNew);
            if (File.Exists(_pathOrigin))
            {
                File.Copy(_pathOrigin, _pathNew);

                var docText = "";
                WordprocessingDocument docR = WordprocessingDocument.Open(_pathNew, true);
                MainDocumentPart mainPartR = docR.MainDocumentPart;
                using (StreamReader sr = new StreamReader(docR.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();

                    string _MerTemp = "<w:spacing w:line=\"320\" w:lineRule=\"exact\"/><w:ind w:left=\"1134\"/><w:jc w:val=\"both\"/></w:pPr><w:r><w:t>MerCostDetail</w:t></w:r></w:p><w:p w:rsidR=\"0089720F\" w:rsidRPr=\"000F3ED5\" w:rsidRDefault=\"0089720F\" w:rsidP=\"007A595E\"><w:pPr><w:pStyle w:val=\"ListParagraph\"/><w:shd w:val=\"clear\" w:color=\"auto\" w:fill=\"FFFFFF\"/>";
                    string _MerCostDetail = "";
                    string sFirst = "<w:spacing w:line=\"320\" w:lineRule=\"exact\"/><w:ind w:left=\"1134\"/><w:jc w:val=\"both\"/></w:pPr><w:r><w:t>";
                    string sEnd = "</w:t></w:r></w:p><w:p w:rsidR=\"0089720F\" w:rsidRPr=\"000F3ED5\" w:rsidRDefault=\"0089720F\" w:rsidP=\"007A595E\"><w:pPr><w:pStyle w:val=\"ListParagraph\"/><w:shd w:val=\"clear\" w:color=\"auto\" w:fill=\"FFFFFF\"/>";
                    string[] lines = System.Text.RegularExpressions.Regex.Split(txtMerCostDetail.Text, "\r\n");
                    foreach (string line in lines)
                    {
                        if (line != "")
                            _MerCostDetail += sFirst + "" + line + "" + sEnd;
                    }

                    docText = docText.Replace("MerPos01", txtMerPos01.Text.Trim());
                    docText = docText.Replace("MerPos02", txtMerPos02.Text.Trim());
                    docText = docText.Replace("MerPos03", txtMerPos03.Text.Trim());
                    docText = docText.Replace("MerPos04", txtMerPos04.Text.Trim());
                    docText = docText.Replace("MerPos05", txtMerPos05.Text.Trim());
                    docText = docText.Replace("MerPos06", txtMerPos06.Text.Trim());
                    docText = docText.Replace("MerPos07", txtMerPos07.Text.Trim());
                    docText = docText.Replace("MerPos08", txtMerPos08.Text.Trim());
                    docText = docText.Replace("MerPos09", txtMerPos09.Text.Trim());
                    docText = docText.Replace("MerPos10", txtMerPos10.Text.Trim());
                    docText = docText.Replace("MerName", ConvertString(txtMerName.Text.ToUpper().Trim()));
                    docText = docText.Replace("MerAddress", txtMerAddress.Text.Trim());
                    docText = docText.Replace("MerPhone", txtMerPhone.Text.Trim());
                    docText = docText.Replace("MerTaxCode", txtMerTaxCode.Text.Trim());
                    docText = docText.Replace("MerEmail", txtMerEmail.Text.Trim());
                    string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                    docText = docText.Replace("MerNX", ddlDanhXung.SelectedValue);
                    docText = docText.Replace("MerRepresent", ConvertStringNDD(sNguoiDD));
                    docText = docText.Replace("MerPosition", ToFirstUpper(txtMerPosition.Text.Trim()));
                    docText = docText.Replace("MerPosBody01", txtMerPosBody01.Text.Trim());
                    docText = docText.Replace("MerPosBody02", txtMerPosBody02.Text.Trim());
                    docText = docText.Replace("MerPosBody03", txtMerPosBody03.Text.Trim());
                    docText = docText.Replace("MerPosBody04", txtMerPosBody04.Text.Trim());
                    docText = docText.Replace("MerPosBody05", txtMerPosBody05.Text.Trim());
                    docText = docText.Replace("MerPosBody06", txtMerPosBody06.Text.Trim());
                    docText = docText.Replace("MerPosBody07", txtMerPosBody07.Text.Trim());
                    docText = docText.Replace("MerPosBody08", txtMerPosBody08.Text.Trim());
                    docText = docText.Replace("MerPosBody09", txtMerPosBody09.Text.Trim());
                    docText = docText.Replace("MerPosBody10", txtMerPosBody10.Text.Trim());
                    docText = docText.Replace("MerPosFo01", txtMerPosFo01.Text.Trim());
                    docText = docText.Replace("MerPosFo02", txtMerPosFo02.Text.Trim());
                    docText = docText.Replace("MerPosFo03", txtMerPosFo03.Text.Trim());
                    docText = docText.Replace("MerPosFo04", txtMerPosFo04.Text.Trim());
                    docText = docText.Replace("MerPosFo05", txtMerPosFo05.Text.Trim());
                    docText = docText.Replace("MerPosFo06", txtMerPosFo06.Text.Trim());
                    docText = docText.Replace("MerPosFo07", txtMerPosFo07.Text.Trim());
                    docText = docText.Replace("MerCostTitle", txtMerCostTitle.Text.Trim());
                    docText = docText.Replace(_MerTemp, _MerCostDetail);

                }
                using (StreamWriter sw = new StreamWriter(docR.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                docR.Close();
            }
        }
        private void Save_Data(string strLink = "")
        {
            try
            {
                if (_id == 0)
                {
                    int _idUser = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
                    MER_PHULUC_GIAHAN i = new MER_PHULUC_GIAHAN();
                    i.USER_ID = _idUser;
                    i.MER_POS01 = txtMerPos01.Text.Trim();
                    i.MER_POS02 = txtMerPos02.Text.Trim();
                    i.MER_POS03 = txtMerPos03.Text.Trim();
                    i.MER_POS04 = txtMerPos04.Text.Trim();
                    i.MER_POS05 = txtMerPos05.Text.Trim();
                    i.MER_POS06 = txtMerPos06.Text.Trim();
                    i.MER_POS07 = txtMerPos07.Text.Trim();
                    i.MER_POS08 = txtMerPos08.Text.Trim();
                    i.MER_POS09 = txtMerPos09.Text.Trim();
                    i.MER_POS10 = txtMerPos10.Text.Trim();
                    i.MER_NAME = ConvertString(txtMerName.Text.ToUpper().Trim());
                    i.MER_ADDRESS = txtMerAddress.Text.Trim();
                    i.MER_PHONE = txtMerPhone.Text.Trim();
                    i.MER_TAXCODE = txtMerTaxCode.Text.Trim();
                    i.MER_EMAIL = txtMerEmail.Text.Trim();
                    i.MER_NX = ddlDanhXung.SelectedValue;
                    string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                    i.MER_REPRESENT = ConvertStringNDD(sNguoiDD);
                    i.MER_POSITION = ToFirstUpper(txtMerPosition.Text.Trim());
                    i.MER_POSBODY01 = txtMerPosBody01.Text.Trim();
                    i.MER_POSBODY02 = txtMerPosBody02.Text.Trim();
                    i.MER_POSBODY03 = txtMerPosBody03.Text.Trim();
                    i.MER_POSBODY04 = txtMerPosBody04.Text.Trim();
                    i.MER_POSBODY05 = txtMerPosBody05.Text.Trim();
                    i.MER_POSBODY06 = txtMerPosBody06.Text.Trim();
                    i.MER_POSBODY07 = txtMerPosBody07.Text.Trim();
                    i.MER_POSBODY08 = txtMerPosBody08.Text.Trim();
                    i.MER_POSBODY09 = txtMerPosBody09.Text.Trim();
                    i.MER_POSBODY10 = txtMerPosBody10.Text.Trim();
                    i.MER_COST_TITLE = txtMerCostTitle.Text.Trim();
                    i.MER_COST_DETAIL = txtMerCostDetail.Text.Trim();
                    i.MER_POSFO01 = txtMerPosFo01.Text.Trim();
                    i.MER_POSFO02 = txtMerPosFo02.Text.Trim();
                    i.MER_POSFO03 = txtMerPosFo03.Text.Trim();
                    i.MER_POSFO04 = txtMerPosFo04.Text.Trim();
                    i.MER_POSFO05 = txtMerPosFo05.Text.Trim();
                    i.MER_POSFO06 = txtMerPosFo06.Text.Trim();
                    i.MER_POSFO07 = txtMerPosFo07.Text.Trim();

                    i.MER_DATE = DateTime.Now;
                    i.MER_STATUS = 0;

                    _MerPhuLucGiaHanData.Create(i);
                    db.SubmitChanges();
                    var getlink = db.MER_PHULUC_GIAHANs.OrderByDescending(n => n.ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        strLink = string.IsNullOrEmpty(strLink) ? "phu-luc-gia-han-hop-dong-ke-toan.aspx?id=" + getlink[0].ID : strLink;
                        Save_FileDoc(getlink[0].ID);
                    }

                    //Gửi mail
                    string _nameUser = HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_NAME"];

                    SendEmail(24, _nameUser, ConvertString(txtMerName.Text.ToUpper().Trim()));

                    if (!string.IsNullOrEmpty(strLink))
                    {
                        string strScript = "<script>";
                        strScript += "alert('Hợp đồng dịch vụ kế toán đã cập nhật thành công!');";
                        strScript += "window.location='" + strLink + "';";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
                else
                {
                    var obj = db.MER_PHULUC_GIAHANs.Where(n => n.ID == _id).ToList();
                    foreach (var i in obj)
                    {
                        i.MER_POS01 = txtMerPos01.Text.Trim();
                        i.MER_POS02 = txtMerPos02.Text.Trim();
                        i.MER_POS03 = txtMerPos03.Text.Trim();
                        i.MER_POS04 = txtMerPos04.Text.Trim();
                        i.MER_POS05 = txtMerPos05.Text.Trim();
                        i.MER_POS06 = txtMerPos06.Text.Trim();
                        i.MER_POS07 = txtMerPos07.Text.Trim();
                        i.MER_POS08 = txtMerPos08.Text.Trim();
                        i.MER_POS09 = txtMerPos09.Text.Trim();
                        i.MER_POS10 = txtMerPos10.Text.Trim();
                        i.MER_NAME = ConvertString(txtMerName.Text.ToUpper().Trim());
                        i.MER_ADDRESS = txtMerAddress.Text.Trim();
                        i.MER_PHONE = txtMerPhone.Text.Trim();
                        i.MER_TAXCODE = txtMerTaxCode.Text.Trim();
                        i.MER_EMAIL = txtMerEmail.Text.Trim();
                        i.MER_NX = ddlDanhXung.SelectedValue;
                        string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                        i.MER_REPRESENT = ConvertStringNDD(sNguoiDD);
                        i.MER_POSITION = ToFirstUpper(txtMerPosition.Text.Trim());
                        i.MER_POSBODY01 = txtMerPosBody01.Text.Trim();
                        i.MER_POSBODY02 = txtMerPosBody02.Text.Trim();
                        i.MER_POSBODY03 = txtMerPosBody03.Text.Trim();
                        i.MER_POSBODY04 = txtMerPosBody04.Text.Trim();
                        i.MER_POSBODY05 = txtMerPosBody05.Text.Trim();
                        i.MER_POSBODY06 = txtMerPosBody06.Text.Trim();
                        i.MER_POSBODY07 = txtMerPosBody07.Text.Trim();
                        i.MER_POSBODY08 = txtMerPosBody08.Text.Trim();
                        i.MER_POSBODY09 = txtMerPosBody09.Text.Trim();
                        i.MER_POSBODY10 = txtMerPosBody10.Text.Trim();
                        i.MER_COST_TITLE = txtMerCostTitle.Text.Trim();
                        i.MER_COST_DETAIL = txtMerCostDetail.Text.Trim();
                        i.MER_POSFO01 = txtMerPosFo01.Text.Trim();
                        i.MER_POSFO02 = txtMerPosFo02.Text.Trim();
                        i.MER_POSFO03 = txtMerPosFo03.Text.Trim();
                        i.MER_POSFO04 = txtMerPosFo04.Text.Trim();
                        i.MER_POSFO05 = txtMerPosFo05.Text.Trim();
                        i.MER_POSFO06 = txtMerPosFo06.Text.Trim();
                        i.MER_POSFO07 = txtMerPosFo07.Text.Trim();
                        if (i.MER_STATUS == 2)
                        {
                            i.MER_STATUS = 0;
                            i.MER_CHI_CHU = "Đã sửa lại hợp đồng";
                        }

                        _MerPhuLucGiaHanData.Update(i);
                        db.SubmitChanges();
                    }
                    strLink = string.IsNullOrEmpty(strLink) ? "phu-luc-gia-han-hop-dong-ke-toan.aspx?id=" + _id : strLink;
                    Save_FileDoc(_id);

                    if (!string.IsNullOrEmpty(strLink))
                    {
                        string strScript = "<script>";
                        strScript += "alert('Phụ lục gia hạn dịch vụ kế toán đã cập nhật thành công!');";
                        strScript += "window.location='" + strLink + "';";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Funtion
        public void TestPermission()
        {
            try
            {
                getCookies _getCookies = new getCookies();
                _getCookies.getCookiesNew();

                if (_id != 0)
                {
                    int _idUser = Utils.CIntDef(Session["Userid"]);
                    int _idGroup = Utils.CIntDef(Session["Grouptype"], 0);
                    if (_idGroup != 1 && _idGroup != 2)
                    {
                        var obj = _MerPhuLucGiaHanData.GetById(_id);
                        if (obj != null)
                        {
                            if (Utils.CIntDef(obj.USER_ID) != _idUser)
                                Response.Redirect("danh-sach-phu-luc-gia-han-hop-dong-ke-toan.aspx");
                            else
                            {
                                if (obj.MER_STATUS == 1)
                                {
                                    string strScript = "<script>";
                                    strScript += "alert(' Hợp đồng này đã được xử lý xong, bạn không có quyền chỉnh sửa!');";
                                    strScript += "window.location='danh-sach-phu-luc-gia-han-hop-dong-ke-toan.aspx';";
                                    strScript += "</script>";
                                    Page.RegisterClientScriptBlock("strScript", strScript);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { Response.Redirect("danh-sach-phu-luc-gia-han-hop-dong-ke-toan.aspx"); }
        }
        private string ConvertString(string s)
        {
            string stemp = "";
            stemp = s.ToUpper();
            stemp = stemp.Replace("TRÁCH NHIỆM HỮU HẠN", "TNHH");
            stemp = stemp.Replace("THƯƠNG MẠI", "TM");
            stemp = stemp.Replace("DỊCH VỤ", "DV");
            stemp = stemp.Replace("MỘT THÀNH VIÊN", "MTV");
            stemp = stemp.Replace("SẢN XUẤT", "SX");
            stemp = stemp.Replace("CỔ PHẦN", "CP");
            stemp = stemp.Replace("XUẤT NHẬP KHẨU", "XNK");

            return stemp;
        }
        public static string ToFirstUpper(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ  

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
        }
        private void SendEmail(int _IdUser, string _nameUserSend, string _nameCty)
        {
            try
            {
                var _getUser = db.USERs.Where(n => n.USER_ID == _IdUser).ToList();
                if (_getUser.Count > 0)
                {
                    string _nameUser = _getUser[0].USER_NAME;
                    string _email = _getUser[0].USER_EMAIL;
                    string _mailBody = string.Empty;
                    _mailBody += "<br/>Xin chào <b>" + _nameUser + "</b>";
                    _mailBody += "<br/><br/>Nhân viên: <b>" + _nameUserSend + "</b> vừa tạo phụ lục hợp đồng gia hạn dịch vụ kế toán trên trang <i>http://quanly.ketoanvn.com.vn/</i>";
                    _mailBody += "<br/><br/>Tên công ty gia hạn : <b>" + _nameCty + "</b>";
                    _mailBody += "<br/><br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để xử lý hợp đồng này.<br/><br/>";
                    _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo: Phụ lục gia hạn hợp đồng dịch vụ kế toán", _email, "", "", _sMailBody, true, false);
                }
            }
            catch (Exception) { }
        }
        private string ConvertStringNDD(string s)
        {
            string _s = "";
            string _temp = s.Substring(0, 4);
            if (_temp.Contains("Ông"))
                _s = _temp.Replace("Ông", "") + s.Substring(4, s.Length - 4);
            else if (_temp.Contains("Bà"))
                _s = _temp.Replace("Bà", "") + s.Substring(4, s.Length - 4);
            else _s = s;
            return _s.Trim();
        }
        #endregion

        #region Event
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            lbtnSave.Enabled = true;
            Save_Data();
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-phu-luc-gia-han.aspx");
        }
        #endregion

    }
}