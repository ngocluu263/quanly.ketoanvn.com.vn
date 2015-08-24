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
    public partial class thanh_ly_hop_dong_ke_toan : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        MerThanhLyHopDongDVData _MerThanhLyHopDongDVData = new MerThanhLyHopDongDVData();
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
            var obj = db.MER_THANHLY_HOPDONGs.Where(n => n.ID == _id).ToList();
            if (obj.Count > 0)
            {
                txtMerPos01.Text = obj[0].MER_POS01;
                txtMerPos02.Text = obj[0].MER_POS02;
                txtMerPos03.Text = obj[0].MER_POS03;
                txtMerPos04.Text = obj[0].MER_POS04;
                txtMerPos05.Text = obj[0].MER_POS05;
                txtMerName1.Text = obj[0].MER_NAME1;
                txtMerName2.Text = obj[0].MER_NAME2;
                txtMerDayNow.Text = obj[0].MER_DAYNOW;
                txtMerMonthNow.Text = obj[0].MER_MONTHNOW;
                txtMerYearNow.Text = obj[0].MER_YEARNOW;
                txtMerAddress.Text = obj[0].MER_ADDRESS;
                txtMerPhone.Text = obj[0].MER_PHONE;
                txtMerTaxCode.Text = obj[0].MER_TAXCODE;
                txtMerEmail.Text = obj[0].MER_EMAIL;
                ddlDanhXung.SelectedValue = obj[0].MER_NX;
                txtMerRepresent.Text = obj[0].MER_REPRESENT;
                txtMerPosition.Text = obj[0].MER_POSITION;
                txtMerDayEnd.Text = obj[0].MER_DAYEND;
                txtMerMonthEnd.Text = obj[0].MER_MONTHEND;
                txtMerYearEnd.Text = obj[0].MER_YEAREND;

            }
        }
        protected void Save_FileDoc(int _IdFile)
        {
            //Copy từ file gốc ra 1 bản 
            string _pathOrigin = Server.MapPath("/File/Template/TemplateThanhLyHDDV.docx");
            string _pathNew = String.Format(Server.MapPath("/File/WordFile/HopDongThanhLyDVKT/HopDongThanhLyDVKeToan_Code_{0}.docx"), _IdFile);
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

                    docText = docText.Replace("MerPos01", txtMerPos01.Text.Trim());
                    docText = docText.Replace("MerPos02", txtMerPos02.Text.Trim());
                    docText = docText.Replace("MerPos03", txtMerPos03.Text.Trim());
                    docText = docText.Replace("MerPos04", txtMerPos04.Text.Trim());
                    docText = docText.Replace("MerPos05", txtMerPos05.Text.Trim());
                    docText = docText.Replace("MerName1", ConvertStringNoUpp(txtMerName1.Text.Trim()));
                    docText = docText.Replace("MerName2", ConvertString(txtMerName2.Text.ToUpper().Trim()));
                    docText = docText.Replace("MerAddress", ToFirstUpper(txtMerAddress.Text.Trim()));
                    docText = docText.Replace("MerPhone", txtMerPhone.Text.Trim());
                    docText = docText.Replace("MerTaxCode", txtMerTaxCode.Text.Trim());
                    docText = docText.Replace("MerEmail", txtMerEmail.Text.Trim());
                    string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                    docText = docText.Replace("MerNX", ddlDanhXung.SelectedValue);
                    docText = docText.Replace("MerRepresent", ConvertStringNDD(sNguoiDD));
                    docText = docText.Replace("MerPosition", ToFirstUpper(txtMerPosition.Text.Trim()));
                    docText = docText.Replace("MerDayNow", txtMerDayNow.Text.Trim());
                    docText = docText.Replace("MerMonthNow", txtMerMonthNow.Text.Trim());
                    docText = docText.Replace("MerYearNow", txtMerYearNow.Text.Trim());
                    docText = docText.Replace("MerDayEnd", txtMerDayNow.Text.Trim());
                    docText = docText.Replace("MerMonthEnd", txtMerMonthNow.Text.Trim());
                    docText = docText.Replace("MerYearEnd", txtMerYearNow.Text.Trim());
                    
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
                    MER_THANHLY_HOPDONG i = new MER_THANHLY_HOPDONG();
                    i.USER_ID = _idUser;
                    i.MER_POS01 = txtMerPos01.Text.Trim();
                    i.MER_POS02 = txtMerPos02.Text.Trim();
                    i.MER_POS03 = txtMerPos03.Text.Trim();
                    i.MER_POS04 = txtMerPos04.Text.Trim();
                    i.MER_POS05 = txtMerPos05.Text.Trim();
                    i.MER_NAME1 = ConvertStringNoUpp(txtMerName1.Text.Trim());
                    i.MER_NAME2 = ConvertString(txtMerName2.Text.ToUpper().Trim());
                    i.MER_ADDRESS = txtMerAddress.Text.Trim();
                    i.MER_PHONE = txtMerPhone.Text.Trim();
                    i.MER_TAXCODE = txtMerTaxCode.Text.Trim();
                    i.MER_EMAIL = txtMerEmail.Text.Trim();
                    i.MER_NX = ddlDanhXung.SelectedValue;
                    string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                    i.MER_REPRESENT = ConvertStringNDD(sNguoiDD);
                    i.MER_POSITION = ToFirstUpper(txtMerPosition.Text.Trim());
                    i.MER_DAYNOW = txtMerDayNow.Text.Trim();
                    i.MER_MONTHNOW = txtMerMonthNow.Text.Trim();
                    i.MER_YEARNOW = txtMerYearNow.Text.Trim();
                    i.MER_DAYEND = txtMerDayEnd.Text.Trim();
                    i.MER_MONTHEND = txtMerMonthEnd.Text.Trim();
                    i.MER_YEAREND = txtMerYearEnd.Text.Trim();
                    i.MER_DATE = DateTime.Now;
                    i.MER_STATUS = 0;

                    _MerThanhLyHopDongDVData.Create(i);
                    db.SubmitChanges();
                    var getlink = db.MER_THANHLY_HOPDONGs.OrderByDescending(n => n.ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        strLink = string.IsNullOrEmpty(strLink) ? "thanh-ly-hop-dong-ke-toan.aspx?id=" + getlink[0].ID : strLink;
                        Save_FileDoc(getlink[0].ID);
                    }

                    //Gửi mail
                    string _nameUser = HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_NAME"];
                    string _merSo = txtMerPos01.Text.Trim() + "/HĐTV/" + txtMerPos02.Text.Trim();
                    string _merNgay = "Ngày "+txtMerPos03.Text.Trim()+" tháng "+txtMerPos04.Text.Trim() +" năm "+ txtMerPos05.Text.Trim();

                    SendEmail(24, _nameUser, _merSo, _merNgay, ConvertString(txtMerName2.Text.ToUpper().Trim()), ToFirstUpper(txtMerAddress.Text.Trim()), txtMerPhone.Text.Trim()
                        , txtMerTaxCode.Text.Trim(), txtMerEmail.Text.Trim(), ToFirstUpper(txtMerRepresent.Text.Trim()), ToFirstUpper(txtMerPosition.Text.Trim()));

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
                    var obj = db.MER_THANHLY_HOPDONGs.Where(n => n.ID == _id).ToList();
                    foreach (var i in obj)
                    {
                        i.MER_POS01 = txtMerPos01.Text.Trim();
                        i.MER_POS02 = txtMerPos02.Text.Trim();
                        i.MER_POS03 = txtMerPos03.Text.Trim();
                        i.MER_POS04 = txtMerPos04.Text.Trim();
                        i.MER_POS05 = txtMerPos05.Text.Trim();
                        i.MER_NAME1 = ConvertStringNoUpp(txtMerName1.Text.Trim());
                        i.MER_NAME2 = ConvertString(txtMerName2.Text.ToUpper().Trim());
                        i.MER_ADDRESS = txtMerAddress.Text.Trim();
                        i.MER_PHONE = txtMerPhone.Text.Trim();
                        i.MER_TAXCODE = txtMerTaxCode.Text.Trim();
                        i.MER_EMAIL = txtMerEmail.Text.Trim();
                        i.MER_NX = ddlDanhXung.SelectedValue;
                        string sNguoiDD = ToFirstUpper(txtMerRepresent.Text.Trim());
                        i.MER_REPRESENT = ConvertStringNDD(sNguoiDD);
                        i.MER_POSITION = ToFirstUpper(txtMerPosition.Text.Trim());
                        i.MER_DAYNOW = txtMerDayNow.Text.Trim();
                        i.MER_MONTHNOW = txtMerMonthNow.Text.Trim();
                        i.MER_YEARNOW = txtMerYearNow.Text.Trim();
                        i.MER_DAYEND = txtMerDayEnd.Text.Trim();
                        i.MER_MONTHEND = txtMerMonthEnd.Text.Trim();
                        i.MER_YEAREND = txtMerYearEnd.Text.Trim();
                        
                        if (i.MER_STATUS == 2)
                        {
                            i.MER_STATUS = 0;
                            i.MER_CHI_CHU = "Đã sửa lại hợp đồng";
                        }

                        _MerThanhLyHopDongDVData.Update(i);
                        db.SubmitChanges();
                    }
                    strLink = string.IsNullOrEmpty(strLink) ? "thanh-ly-hop-dong-ke-toan.aspx?id=" + _id : strLink;
                    Save_FileDoc(_id);

                    if (!string.IsNullOrEmpty(strLink))
                    {
                        string strScript = "<script>";
                        strScript += "alert('Hợp đồng thanh lý dịch vụ kế toán đã cập nhật thành công!');";
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
                    if (_idGroup != 1 && _idGroup != 2)
                    {
                        var obj = _MerThanhLyHopDongDVData.GetById(_id);
                        if (obj != null)
                        {
                            if (Utils.CIntDef(obj.USER_ID) != _idUser)
                                Response.Redirect("danh-sach-bien-ban-thanh-ly-hop-dong.aspx");
                            else
                            {
                                if (obj.MER_STATUS == 1)
                                {
                                    string strScript = "<script>";
                                    strScript += "alert(' Hợp đồng này đã được xử lý xong, bạn không có quyền chỉnh sửa!');";
                                    strScript += "window.location='danh-sach-bien-ban-thanh-ly-hop-dong.aspx';";
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
        private string ConvertStringNoUpp(string s)
        {
            string stemp = "";
            stemp = s;
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
            , string _merEmail, string _merDaiDien, string _merChucVu)
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
                    _mailBody += "<br/><br/>Nhân viên: <b>" + _nameUserSend + "</b> vừa tạo hợp đồng thanh lý dịch vụ kế toán trên trang <i>http://quanly.ketoanvn.com.vn/</i>";
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
                    _mailBody += "<br/><br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để xử lý hợp đồng này.<br/><br/>";
                    _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo: Hợp đồng thanh lý dịch vụ kế toán", _email, "", "", _sMailBody, true, false);
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
            Response.Redirect("danh-sach-bien-ban-thanh-ly-hop-dong.aspx");
        }
        #endregion

    }
}