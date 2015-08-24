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
    public partial class hop_dong_ke_toan : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        MerHopDongDVData _MerHopDongDVData = new MerHopDongDVData();
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
            var obj = db.MER_HOPDONG_DVs.Where(n => n.ID == _id).ToList();
            if (obj.Count > 0)
            {
                txtMerPos01.Text = obj[0].MER_POS01;
                txtMerPos02.Text = obj[0].MER_POS02;
                txtMerPos03.Text = obj[0].MER_POS03;
                txtMerPos04.Text = obj[0].MER_POS04;
                txtMerPos05.Text = obj[0].MER_POS05;
                txtMerName.Text = obj[0].MER_NAME;
                txtMerAddress.Text = obj[0].MER_ADDRESS;
                txtMerPhone.Text = obj[0].MER_PHONE;
                txtMerTaxCode.Text = obj[0].MER_TAXCODE;
                txtMerEmail.Text = obj[0].MER_EMAIL;
                ddlDanhXung.SelectedValue = obj[0].MER_NX;
                txtMerRepresent.Text = obj[0].MER_REPRESENT;
                txtMerPosition.Text = obj[0].MER_POSITION;
                txtMerCostTitle01.Text = obj[0].MER_COST_TITLE01;
                txtMerCostTitle02.Text = obj[0].MER_COST_TITLE02;
                txtMerCostTitle03.Text = obj[0].MER_COST_TITLE03;
                txtMerCostDetail.Text = obj[0].MER_COST_DETAIL;
                txtMerBeginM.Text = obj[0].MER_BEGIN_M;
                txtMerDeadlineInt.Text = obj[0].MER_DEADLINE_INT;
                txtMerDeadlineString.Text = obj[0].MER_DEADLINE_STRING;

                //Biểu phí
                txtPhiHangThang.Text = obj[0].PHI_HANGTHANG == null ? "" : obj[0].PHI_HANGTHANG.Value.ToString("###,##0").Replace(".", ",");
                txtTu1_1.Text = Utils.CStrDef(obj[0].PHI_TU1_1);
                txtTu1_2.Text = Utils.CStrDef(obj[0].PHI_TU1_2);
                txtPhi1.Text = obj[0].PHI_HD1 == null ? "" : obj[0].PHI_HD1.Value.ToString("###,##0").Replace(".",",");
                txtTu2_1.Text = Utils.CStrDef(obj[0].PHI_TU2_1);
                txtTu2_2.Text = Utils.CStrDef(obj[0].PHI_TU2_2);
                txtPhi2.Text = obj[0].PHI_HD2 == null ? "" : obj[0].PHI_HD2.Value.ToString("###,##0").Replace(".",",");
                txtTu3_1.Text = Utils.CStrDef(obj[0].PHI_TU3_1);
                txtTu3_2.Text = Utils.CStrDef(obj[0].PHI_TU3_2);
                txtPhi3.Text = obj[0].PHI_HD3 == null ? "" : obj[0].PHI_HD3.Value.ToString("###,##0").Replace(".",",");
                txtTu4_1.Text = Utils.CStrDef(obj[0].PHI_TU4_1);
                txtTu4_2.Text = Utils.CStrDef(obj[0].PHI_TU4_2);
                txtPhi4.Text = obj[0].PHI_HD4 == null ? "" : obj[0].PHI_HD4.Value.ToString("###,##0").Replace(".",",");
                txtTu5_1.Text = Utils.CStrDef(obj[0].PHI_TU5_1);
                txtTu5_2.Text = Utils.CStrDef(obj[0].PHI_TU5_2);
                txtPhi5.Text = obj[0].PHI_HD5 == null ? "" : obj[0].PHI_HD5.Value.ToString("###,##0").Replace(".",",");
                txtTu6_1.Text = Utils.CStrDef(obj[0].PHI_TU6_1);
                txtTu6_2.Text = Utils.CStrDef(obj[0].PHI_TU6_2);
                txtPhi6.Text = obj[0].PHI_HD6 == null ? "" : obj[0].PHI_HD6.Value.ToString("###,##0").Replace(".",",");
                txtThemPhi.Text = obj[0].PHI_THEMPHI == null ? "" : obj[0].PHI_THEMPHI.Value.ToString("###,##0").Replace(".",",");
                txtThemHd.Text = Utils.CStrDef(obj[0].PHI_THEMHD);
            }
        }
        protected void Save_FileDoc(int _IdFile)
        {
            //Copy từ file gốc ra 1 bản 
            string _pathOrigin = Server.MapPath("/File/Template/TemplateHDDV.docx");
            string _pathNew = String.Format(Server.MapPath("/File/WordFile/HopDongDVKT/HopDongDVKeToan_Code_{0}.docx"), _IdFile);
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

                    string _MerTemp = "<w:spacing w:before=\"60\" w:after=\"60\" w:line=\"312\" w:lineRule=\"auto\"/><w:ind w:left=\"720\"/><w:jc w:val=\"both\"/></w:pPr><w:r><w:t>MerCostDetail</w:t></w:r></w:p><w:p w:rsidR=\"006B5F13\" w:rsidRPr=\"00D80530\" w:rsidRDefault=\"006B5F13\" w:rsidP=\"006B5F13\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:leader=\"dot\" w:pos=\"10080\"/></w:tabs>";
                    string _MerCostDetail = "";
                    string sFirst = "<w:spacing w:before=\"60\" w:after=\"60\" w:line=\"312\" w:lineRule=\"auto\"/><w:ind w:left=\"720\"/><w:jc w:val=\"both\"/></w:pPr><w:r><w:t>";
                    string sEnd = "</w:t></w:r></w:p><w:p w:rsidR=\"006B5F13\" w:rsidRPr=\"00D80530\" w:rsidRDefault=\"006B5F13\" w:rsidP=\"006B5F13\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:leader=\"dot\" w:pos=\"10080\"/></w:tabs>";
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
                    docText = docText.Replace("MerName", ConvertString(txtMerName.Text.ToUpper().Trim()));
                    docText = docText.Replace("MerAddress", txtMerAddress.Text.Trim());
                    docText = docText.Replace("MerPhone", txtMerPhone.Text.Trim());
                    docText = docText.Replace("MerTaxCode", txtMerTaxCode.Text.Trim());
                    docText = docText.Replace("MerEmail", txtMerEmail.Text.Trim());
                    string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                    docText = docText.Replace("MerNX", ddlDanhXung.SelectedValue);
                    docText = docText.Replace("MerRepresent", ConvertStringNDD(sNguoiDD));
                    docText = docText.Replace("MerPosition", ToFirstUpper(txtMerPosition.Text.Trim()));
                    docText = docText.Replace("MerCostTitle01", txtMerCostTitle01.Text.Trim());
                    docText = docText.Replace("MerCostTitle02", txtMerCostTitle02.Text.Trim());
                    docText = docText.Replace("MerCostTitle03", txtMerCostTitle03.Text.Trim());
                    docText = docText.Replace(_MerTemp, _MerCostDetail);
                    docText = docText.Replace("MerBeginM", txtMerBeginM.Text.Trim());
                    docText = docText.Replace("MerDeadline01", txtMerDeadlineInt.Text.Trim());
                    docText = docText.Replace("MerDeadline02", txtMerDeadlineString.Text.ToLower().Trim());
                    
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
                    MER_HOPDONG_DV i = new MER_HOPDONG_DV();
                    i.USER_ID = _idUser;
                    i.MER_POS01 = txtMerPos01.Text.Trim();
                    i.MER_POS02 = txtMerPos02.Text.Trim();
                    i.MER_POS03 = txtMerPos03.Text.Trim();
                    i.MER_POS04 = txtMerPos04.Text.Trim();
                    i.MER_POS05 = txtMerPos05.Text.Trim();
                    i.MER_NAME = ConvertString(txtMerName.Text.ToUpper().Trim());
                    i.MER_ADDRESS = txtMerAddress.Text.Trim();
                    i.MER_PHONE = txtMerPhone.Text.Trim();
                    i.MER_TAXCODE = txtMerTaxCode.Text.Trim();
                    i.MER_EMAIL = txtMerEmail.Text.Trim();
                    i.MER_NX = ddlDanhXung.SelectedValue;
                    string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                    i.MER_REPRESENT = ConvertStringNDD(sNguoiDD);
                    i.MER_POSITION = ToFirstUpper(txtMerPosition.Text.Trim());
                    i.MER_COST_TITLE01 = txtMerCostTitle01.Text.Trim();
                    i.MER_COST_TITLE02 = txtMerCostTitle02.Text.Trim();
                    i.MER_COST_TITLE03 = txtMerCostTitle03.Text.Trim();
                    i.MER_COST_DETAIL = txtMerCostDetail.Text.Trim();
                    i.MER_BEGIN_M = txtMerBeginM.Text.Trim();
                    i.MER_DEADLINE_INT = txtMerDeadlineInt.Text.Trim();
                    i.MER_DEADLINE_STRING = txtMerDeadlineString.Text.ToLower().Trim();
                    i.MER_DATE = DateTime.Now;
                    i.MER_STATUS = 0;

                    //Biểu phí
                    i.PHI_HANGTHANG = Utils.CIntDef(txtPhiHangThang.Text.Replace(",", ""));
                    i.PHI_TU1_1 = Utils.CIntDef(txtTu1_1.Text.Replace(",", ""));
                    i.PHI_TU1_2 = Utils.CIntDef(txtTu1_2.Text.Replace(",", ""));
                    i.PHI_HD1 = Utils.CIntDef(txtPhi1.Text.Replace(",", ""));
                    i.PHI_TU2_1 = Utils.CIntDef(txtTu2_1.Text.Replace(",", ""));
                    i.PHI_TU2_2 = Utils.CIntDef(txtTu2_2.Text.Replace(",", ""));
                    i.PHI_HD2 = Utils.CIntDef(txtPhi2.Text.Replace(",", ""));
                    i.PHI_TU3_1 = Utils.CIntDef(txtTu3_1.Text.Replace(",", ""));
                    i.PHI_TU3_2 = Utils.CIntDef(txtTu3_2.Text.Replace(",", ""));
                    i.PHI_HD3 = Utils.CIntDef(txtPhi3.Text.Replace(",", ""));
                    i.PHI_TU4_1 = Utils.CIntDef(txtTu4_1.Text.Replace(",", ""));
                    i.PHI_TU4_2 = Utils.CIntDef(txtTu4_2.Text.Replace(",", ""));
                    i.PHI_HD4 = Utils.CIntDef(txtPhi4.Text.Replace(",", ""));
                    i.PHI_TU5_1 = Utils.CIntDef(txtTu5_1.Text.Replace(",", ""));
                    i.PHI_TU5_2 = Utils.CIntDef(txtTu5_2.Text.Replace(",", ""));
                    i.PHI_HD5 = Utils.CIntDef(txtPhi5.Text.Replace(",", ""));
                    i.PHI_TU6_1 = Utils.CIntDef(txtTu6_1.Text.Replace(",", ""));
                    i.PHI_TU6_2 = Utils.CIntDef(txtTu6_2.Text.Replace(",", ""));
                    i.PHI_HD6 = Utils.CIntDef(txtPhi6.Text.Replace(",", ""));
                    i.PHI_THEMPHI = Utils.CIntDef(txtThemPhi.Text.Replace(",", ""));
                    i.PHI_THEMHD = Utils.CIntDef(txtThemHd.Text.Replace(",", ""));

                    _MerHopDongDVData.Create(i);
                    db.SubmitChanges();
                    var getlink = db.MER_HOPDONG_DVs.OrderByDescending(n => n.ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        strLink = string.IsNullOrEmpty(strLink) ? "hop-dong-ke-toan.aspx?id=" + getlink[0].ID : strLink;
                        Save_FileDoc(getlink[0].ID);
                    }

                    //Gửi mail
                    string _nameUser = HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_NAME"];
                    string _merSo = txtMerPos01.Text.Trim() + "/HĐTV/" + txtMerPos02.Text.Trim();
                    string _merNgay = "Ngày "+txtMerPos03.Text.Trim()+" tháng "+txtMerPos04.Text.Trim() +" năm "+ txtMerPos05.Text.Trim();

                    string _merPhiTemp = "";
                    string[] lines = System.Text.RegularExpressions.Regex.Split(txtMerCostDetail.Text, "\r\n");
                    foreach (string line in lines)
                    {
                        if (line != "")
                            _merPhiTemp += line + "<br />" ;
                    }
                    string _merPhi = 
                        "-" + txtMerCostTitle01.Text.Trim() + "<br />"  +
                        _merPhiTemp +
                        "-" + txtMerCostTitle02.Text.Trim() + "<br />" +
                        "-" + txtMerCostTitle03.Text.Trim();
                    SendEmail(24, _nameUser, _merSo, _merNgay, ConvertString(txtMerName.Text.ToUpper().Trim()), ToFirstUpper(txtMerAddress.Text.Trim()), txtMerPhone.Text.Trim()
                        , txtMerTaxCode.Text.Trim(), txtMerEmail.Text.Trim(), ToFirstUpper(txtMerRepresent.Text.Trim()), ToFirstUpper(txtMerPosition.Text.Trim())
                        , _merPhi, txtMerBeginM.Text.Trim(), txtMerDeadlineInt.Text.Trim() + " (" + txtMerDeadlineString.Text.Trim() + ")");

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
                    var obj = db.MER_HOPDONG_DVs.Where(n => n.ID == _id).ToList();
                    foreach (var i in obj)
                    {
                        i.MER_POS01 = txtMerPos01.Text.Trim();
                        i.MER_POS02 = txtMerPos02.Text.Trim();
                        i.MER_POS03 = txtMerPos03.Text.Trim();
                        i.MER_POS04 = txtMerPos04.Text.Trim();
                        i.MER_POS05 = txtMerPos05.Text.Trim();
                        i.MER_NAME = ConvertString(txtMerName.Text.ToUpper().Trim());
                        i.MER_ADDRESS = txtMerAddress.Text.Trim();
                        i.MER_PHONE = txtMerPhone.Text.Trim();
                        i.MER_TAXCODE = txtMerTaxCode.Text.Trim();
                        i.MER_EMAIL = txtMerEmail.Text.Trim();
                        i.MER_NX = ddlDanhXung.SelectedValue;
                        string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                        i.MER_REPRESENT = ConvertStringNDD(sNguoiDD);
                        i.MER_POSITION = ToFirstUpper(txtMerPosition.Text.Trim());
                        i.MER_COST_TITLE01 = txtMerCostTitle01.Text.Trim();
                        i.MER_COST_TITLE02 = txtMerCostTitle02.Text.Trim();
                        i.MER_COST_TITLE03 = txtMerCostTitle03.Text.Trim();
                        i.MER_COST_DETAIL = txtMerCostDetail.Text.Trim();
                        i.MER_BEGIN_M = txtMerBeginM.Text.Trim();
                        i.MER_DEADLINE_INT = txtMerDeadlineInt.Text.Trim();
                        i.MER_DEADLINE_STRING = txtMerDeadlineString.Text.ToLower().Trim();
                        if (i.MER_STATUS == 2)
                        {
                            i.MER_STATUS = 0;
                            i.MER_CHI_CHU = "Đã sửa lại hợp đồng";
                        }

                        //Biểu phí
                        i.PHI_HANGTHANG = Utils.CIntDef(txtPhiHangThang.Text.Replace(",", ""));
                        i.PHI_TU1_1 = Utils.CIntDef(txtTu1_1.Text.Replace(",", ""));
                        i.PHI_TU1_2 = Utils.CIntDef(txtTu1_2.Text.Replace(",", ""));
                        i.PHI_HD1 = Utils.CIntDef(txtPhi1.Text.Replace(",", ""));
                        i.PHI_TU2_1 = Utils.CIntDef(txtTu2_1.Text.Replace(",", ""));
                        i.PHI_TU2_2 = Utils.CIntDef(txtTu2_2.Text.Replace(",", ""));
                        i.PHI_HD2 = Utils.CIntDef(txtPhi2.Text.Replace(",", ""));
                        i.PHI_TU3_1 = Utils.CIntDef(txtTu3_1.Text.Replace(",", ""));
                        i.PHI_TU3_2 = Utils.CIntDef(txtTu3_2.Text.Replace(",", ""));
                        i.PHI_HD3 = Utils.CIntDef(txtPhi3.Text.Replace(",", ""));
                        i.PHI_TU4_1 = Utils.CIntDef(txtTu4_1.Text.Replace(",", ""));
                        i.PHI_TU4_2 = Utils.CIntDef(txtTu4_2.Text.Replace(",", ""));
                        i.PHI_HD4 = Utils.CIntDef(txtPhi4.Text.Replace(",", ""));
                        i.PHI_TU5_1 = Utils.CIntDef(txtTu5_1.Text.Replace(",", ""));
                        i.PHI_TU5_2 = Utils.CIntDef(txtTu5_2.Text.Replace(",", ""));
                        i.PHI_HD5 = Utils.CIntDef(txtPhi5.Text.Replace(",", ""));
                        i.PHI_TU6_1 = Utils.CIntDef(txtTu6_1.Text.Replace(",", ""));
                        i.PHI_TU6_2 = Utils.CIntDef(txtTu6_2.Text.Replace(",", ""));
                        i.PHI_HD6 = Utils.CIntDef(txtPhi6.Text.Replace(",", ""));
                        i.PHI_THEMPHI = Utils.CIntDef(txtThemPhi.Text.Replace(",", ""));
                        i.PHI_THEMHD = Utils.CIntDef(txtThemHd.Text.Replace(",", ""));

                        _MerHopDongDVData.Update(i);
                        db.SubmitChanges();
                    }
                    strLink = string.IsNullOrEmpty(strLink) ? "hop-dong-ke-toan.aspx?id=" + _id : strLink;
                    Save_FileDoc(_id);

                    if (!string.IsNullOrEmpty(strLink))
                    {
                        string strScript = "<script>";
                        strScript += "alert('Hợp đồng dịch vụ kế toán đã cập nhật thành công!');";
                        strScript += "window.location='" + strLink + "';";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
            }
            catch (Exception){throw;}
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
                    if (_idGroup != 1 && _idGroup != 2 && _idGroup != 7)
                    {
                        var obj = _MerHopDongDVData.GetById(_id);
                        if (obj != null)
                        {
                            if (Utils.CIntDef(obj.USER_ID) != _idUser)
                                Response.Redirect("danh-sach-hop-dong-ke-toan.aspx");
                            else
                            {
                                if (obj.MER_STATUS == 1)
                                {
                                    string strScript = "<script>";
                                    strScript += "alert(' Hợp đồng này đã được xử lý xong, bạn không có quyền chỉnh sửa!');";
                                    strScript += "window.location='danh-sach-hop-dong-ke-toan.aspx';";
                                    strScript += "</script>";
                                    Page.RegisterClientScriptBlock("strScript", strScript);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { Response.Redirect("danh-sach-hop-dong-ke-toan.aspx"); }
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
        private void SendEmail(int _IdUser, string _nameUserSend,string _merSo,string _merNgay, string _nameCom, string _merDiaChi, string _merDienThoai, string _merMST
            , string _merEmail, string _merDaiDien, string _merChucVu, string _merPhi, string _merThangThu, string _merHieuLucHD)
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
                    _mailBody += "<br/><br/>Nhân viên: <b>" + _nameUserSend + "</b> vừa tạo hợp đồng dịch vụ kế toán trên trang <i>http://quanly.ketoanvn.com.vn/</i>";
                    _mailBody += "<br/><br/><u><b>Thông tin trong hợp đồng bao gồm</b></u>";
                    _mailBody += "<br/>Số : <strong>" + _merSo + "</strong>";
                    _mailBody += "<br/>Ngày hợp đồng: <strong>" + _merNgay + "</strong>";
                    _mailBody += "<br/>Tên công ty : <strong>" + _nameCom + "</strong>";
                    _mailBody += "<br/>Địa chỉ: <strong>" + _merDiaChi + "</strong>";
                    _mailBody += "<br/>Số điện thoại: <strong>" + _merDienThoai + "</strong>";
                    _mailBody += "<br/>Mã số thuế: <strong>" + _merMST + "</strong>";
                    _mailBody += "<br/>Email: <strong>" + _merEmail + "</strong>";
                    _mailBody += "<br/>Đại diện: <strong>" + _merDaiDien + "</strong>";
                    _mailBody += "<br/>Chức vụ: <strong>" + _merChucVu + "</strong>";
                    _mailBody += "<br/>Phí dịch vụ: ";
                    _mailBody += "<br/><strong>" + _merPhi + "</strong>";
                    _mailBody += "<br/>Tháng bắt đầu thu phí: <strong>" + _merThangThu + "</strong>";
                    _mailBody += "<br/>Hiệu lực hợp đồng: <strong>" + _merHieuLucHD + "</strong>";
                    _mailBody += "<br/><br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để xử lý hợp đồng này.<br/><br/>";
                    _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo: Hợp đồng dịch vụ kế toán", _email, "", "", _sMailBody, true, false);
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
            Response.Redirect("danh-sach-hop-dong-ke-toan.aspx");
        }
        #endregion

    }
}