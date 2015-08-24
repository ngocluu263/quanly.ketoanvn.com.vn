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
    public partial class popup_quy_trinh_ket_thuc : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        ProfileData _ProjectData = new ProfileData();
        CongNoData _CongNoData = new CongNoData();
        int _id = 0;
        int _type = 0;
        SendMail sm = new SendMail();
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            _type = Utils.CIntDef(Request.QueryString["type"]);
        }

        #region Data
        private void SaveStatusMain(int _status)
        {
            var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
                _obj[0].PROF_STATUS = _status;
            db.SubmitChanges();
            SendEmail(Utils.CIntDef(_obj[0].USER_ID), _type, "Hồ sơ đã hoàn thành");
        }
        private void ProfNew()
        {
            var obj = _ProjectData.GetById(_id);
            if (obj != null)
            {
                int h_service = Utils.CIntDef(rdbStatus.SelectedValue);
                _ProjectData.InsertDuplicate(obj, 3, 1, _id, h_service);
                var getlink = db.PROFILE_NEWs.OrderByDescending(n => n.ID).Take(1).ToList();
                if (getlink.Count > 0)
                    Save_Employ(getlink[0].ID);

                SendEmailNVKeToanCongNo(obj.PROF_TAXCODE, _CongNoData.GetIdKeToan(obj.PROF_TAXCODE));
                SendEmailChangeCongNo(obj.PROF_TAXCODE);
            }
        }
        private void Save_Employ(int _idprof)
        {
            //Gửi cho toàn bộ nhân viên xử lý hồ sơ
            var list = db.USERs.Where(n => n.GROUP_ID == 11).ToList();
            if (list.Count > 0)
            {
                foreach (var i in list)
                {
                    int _i = Utils.CIntDef(i.USER_ID);
                    WORKFLOW_USER b = new WORKFLOW_USER();
                    b.USER_ID = _i;
                    b.PROF_ID = _idprof;
                    b.DATE = DateTime.Now;
                    b.WORK_STATUS = 1;
                    db.WORKFLOW_USERs.InsertOnSubmit(b);
                    db.SubmitChanges();
                    SendEmailKeToan(_i, 3, "Giai đoạn 1: Tiếp nhận hồ sơ");
                }
            }
        }
        private void SendEmailKeToan(int idUserRecive, int type, string str)
        {
            try
            {
                int idUser = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
                sm.SendEmail(idUser, idUserRecive, type, str, _ProjectData.Get_NameCompany(_id));
            }
            catch { }
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
                    _mailBody += "<br/>Hồ sơ công ty có mã số thuế <strong>" + _mst + "</strong> đã thay đổi hồ sơ sở hoàn thành.";
                    _mailBody += "<br/>Tên nhân viên thay đổi hồ sơ: <strong>" + _nameSend + "</strong>";
                    _mailBody += "<br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để biết thêm thông tin.<br/><br/>";
                    _mailBody += "Ngày cập nhật <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo từ Kế Toán VN: Thông báo về chỉnh sửa hồ sơ", listUser[0].USER_EMAIL, "", "", _sMailBody, true, false);
                }
            }
            catch { }
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
                    _mailBody += "<br/>Hồ sơ công ty có mã số thuế <strong>" + _mst + "</strong> đã thay đổi hồ sơ sở hoàn thành.";
                    _mailBody += "<br/>Tên nhân viên thay đổi hồ sơ: <strong>" + _nameSend + "</strong>";
                    _mailBody += "<br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để biết thêm thông tin.<br/><br/>";
                    _mailBody += "Ngày cập nhật <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo từ Kế Toán VN: Thông báo về chỉnh sửa hồ sơ", listUser[0].USER_EMAIL, "", "", _sMailBody, true, false);
                }
            }
            catch { }
        }
        #endregion
        protected void btnDone_Click(object sender, EventArgs e)
        {
            SaveStatusMain(12);
            if (rdbStatus.SelectedValue != "1")
            {
                ProfNew();
            }
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }
        private void SendEmail(int idUserRecive, int type, string str)
        {
            try
            {
                int idUser = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
                sm.SendEmail(idUser, idUserRecive, type, str, _ProjectData.Get_NameCompany(_id));
            }
            catch { }
        }

    }
}