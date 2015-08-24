using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;

namespace ThanhLapDN.Pages
{
    public partial class popup_quy_trinh_ket_thuc_main : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        int _id = 0;
        int _type = 0;
        SendMail sm = new SendMail();
        CongNoData _CongNoData = new CongNoData();
        ProfileData _ProjectData = new ProfileData();
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            _type = Utils.CIntDef(Request.QueryString["type"]);
            if (KiemTraCoDichVu()) 
                liMsg.Text = "=> Hồ sơ CÓ làm dịch vụ kế toán";
            else
                liMsg.Text = "=> Hồ sơ KHÔNG làm DV kế toán";
            KiemTraMST();
        }
        private void SaveStatusMain(int _status)
        {
            var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
            {
                _obj[0].PROF_STATUS = _status;
                db.SubmitChanges();
                SendEmail(Utils.CIntDef(_obj[0].USER_ID), _type, "Hồ sơ đã hoàn thành");
                SendEmailNVKeToanCongNo(_obj[0].PROF_TAXCODE, _CongNoData.GetIdKeToan(_obj[0].PROF_TAXCODE));
                SendEmailChangeCongNo(_obj[0].PROF_TAXCODE);
            }
        }
        
        private bool KiemTraMST()
        {
            var _obj = _ProjectData.GetById(_id);
            if (_obj != null)
            {
                if (_CongNoData.CheckByMST(_obj.PROF_TAXCODE))
                {
                    lblMsg.Text = "Mã số thuế <b>" + _obj.PROF_TAXCODE + "</b> đã tồn tại trong Công Nợ, những thông tin thay đổi sẽ được cập nhật vào Công Nợ";
                    return true;
                }
                else
                {
                    lblMsg.Text = "";
                    return false;
                }
            }
            else { return false; }
        }
        private bool KiemTraCoDichVu()
        {
            var _obj = _ProjectData.GetById(_id);
             if (_obj != null)
             {
                 if (_obj.HAVE_SERVICES == 3)
                 {
                     return true;
                 }
                 else { return false; }
             }
             else { return false; }
        }
        protected void btnDone_Click(object sender, EventArgs e)
        {
            SaveStatusMain(12);
            if (_type == 3)
            {
                if (KiemTraCoDichVu())
                    DuplicateCongNo();
            }//Type = 3 Insert or Update vào công nợ
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }

        private bool DuplicateCongNo()
        {
            try
            {
                var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).Single();
                if (_obj != null)
                {
                    if (KiemTraMST())
                    {
                        var i = _CongNoData.GetByMST(_obj.PROF_TAXCODE.Trim());
                        i.TEN_KH = _obj.PROF_NAME;
                        i.DIEN_THOAI = _obj.PROF_PHONE;
                        i.EMAIL = _obj.PROF_EMAIL;
                        i.DIA_CHI = _obj.PROF_ADDRESS;
                        i.MST = _obj.PROF_TAXCODE;
                        i.NV_KT = _obj.USER_KT;
                        i.NV_KD = _obj.USER_NVKD;
                        i.NV_GN = _obj.USER_GN;
                        i.NAM = Utils.CIntDef(DateTime.Now.Year);

                        _CongNoData.Update(i);
                    }
                    else
                    {
                        CONG_NO i = new CONG_NO();
                        i.TEN_KH = _obj.PROF_NAME;
                        i.DIEN_THOAI = _obj.PROF_PHONE;
                        i.EMAIL = _obj.PROF_EMAIL;
                        i.DIA_CHI = _obj.PROF_ADDRESS;
                        i.MST = _obj.PROF_TAXCODE;
                        i.NV_KT = _obj.USER_KT;
                        i.NV_KD = _obj.USER_NVKD;
                        i.NV_GN = _obj.USER_GN;
                        i.DATE = DateTime.Now;
                        i.NAM = Utils.CIntDef(DateTime.Now.Year);

                        _CongNoData.Create(i);
                    }
                }
                return true;
            }
            catch (Exception) { return false; }
        }

        #region Email
        private void SendEmail(int idUserRecive, int type, string str)
        {
            try
            {
                int idUser = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
                sm.SendEmail(idUser, idUserRecive, type, str, _ProjectData.Get_NameCompany(_id));
            }
            catch (Exception) { throw; }
        }
        private void SendEmailChangeCongNo(string _mst)
        {
            try
            {
                var listUser = db.USERs.Where(n => n.GROUP_ID == 10).ToList();
                if (listUser.Count > 0)
                {
                    int idUser = Utils.CIntDef(Session["Userid"]);
                    var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                    string _nameSend = _getUserSend[0].USER_NAME;
                    string _mailBody = string.Empty;
                    _mailBody += "<br/>Xin thông báo!";
                    _mailBody += "<br/>Hồ sơ công ty có mã số thuế <strong>" + _mst + "</strong> đã thay đổi hồ sơ thuế hoàn thành.";
                    _mailBody += "<br/>Tên nhân viên thay đổi hồ sơ: <strong>" + _nameSend + "</strong>";
                    _mailBody += "<br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để biết thêm thông tin.<br/><br/>";
                    _mailBody += "Ngày cập nhật <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo từ Kế Toán VN: Thông báo về chỉnh sửa hồ sơ", listUser[0].USER_EMAIL, "", "", _sMailBody, true, false);
                }
            }
            catch (Exception) { throw; }
        }
        private void SendEmailNVKeToanCongNo(string _mst, int _idKeToan)
        {
            try
            {
                var listUser = db.USERs.Where(n => n.USER_ID == _idKeToan).ToList();
                if (listUser.Count > 0)
                {
                    int idUser = Utils.CIntDef(Session["Userid"]);
                    var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                    string _nameSend = _getUserSend[0].USER_NAME;
                    string _mailBody = string.Empty;
                    _mailBody += "<br/>Xin thông báo!";
                    _mailBody += "<br/>Hồ sơ công ty có mã số thuế <strong>" + _mst + "</strong> đã thay đổi hồ sơ thuế hoàn thành.";
                    _mailBody += "<br/>Tên nhân viên thay đổi hồ sơ: <strong>" + _nameSend + "</strong>";
                    _mailBody += "<br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để biết thêm thông tin.<br/><br/>";
                    _mailBody += "Ngày cập nhật <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo từ Kế Toán VN: Thông báo về chỉnh sửa hồ sơ", listUser[0].USER_EMAIL, "", "", _sMailBody, true, false);
                }
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}