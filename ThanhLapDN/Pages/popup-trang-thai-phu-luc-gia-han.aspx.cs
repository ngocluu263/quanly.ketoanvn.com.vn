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
    public partial class popup_trang_thai_phu_luc_gia_han : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        SendMail sm = new SendMail();
        int id = 0;
        int status = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            status = Utils.CIntDef(Request.QueryString["status"], 0);
            if (!IsPostBack)
            {
                Load_Status();
            }
        }

        private void Load_Status()
        {
            if (id > 0)
            {
                var obj = db.MER_PHULUC_GIAHANs.Where(n => n.ID == Utils.CIntDef(id, 0)).Single();
                if (obj != null)
                {
                    liTitle.Text = obj.MER_NAME;
                    txtGhiChu.Text = obj.MER_CHI_CHU;
                }
            }
            ddlTrangThai.SelectedValue = Utils.CStrDef(status);
        }
        private void SendEmailNV(int _IdUser, string _nameCom,string _content)
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
                    _mailBody += "<br/><br/>Phụ lục gia hạn hợp đồng dịch vụ kế toán của bạn trên trang <i>http://quanly.ketoanvn.com.vn/</i> đã bị hủy!";
                    _mailBody += "<br/>Tên công ty : <strong>" + _nameCom + "</strong>";
                    _mailBody += "<br/>Nội dung hủy: <strong>" + _content + "</strong>";
                    _mailBody += "<br/><br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> chỉnh sửa lại hợp đồng.<br/><br/>";
                    _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo: Phụ lục gia hạn hợp đồng dịch vụ kế toán", _email, "", "", _sMailBody, true, false);
                }
            }
            catch (Exception) { }
        }
        protected void btnDone_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                var obj = db.MER_PHULUC_GIAHANs.Where(n => n.ID == Utils.CIntDef(id, 0)).Single();
                if (obj != null)
                {
                    int _trangThai = Utils.CIntDef(ddlTrangThai.SelectedValue, 0);
                    obj.MER_STATUS = _trangThai;

                    if (_trangThai == 2)
                    {
                        obj.MER_CHI_CHU = txtGhiChu.Text;
                        //Gửi mail cho nhân viên kinh doanh nếu hồ sơ bị hủy
                        SendEmailNV(Utils.CIntDef(obj.USER_ID), obj.MER_NAME, txtGhiChu.Text);
                    }
                    else obj.MER_CHI_CHU = "";

                    if (_trangThai == 3 && obj.MER_NGAY_HT == null)
                        obj.MER_NGAY_HT = DateTime.Now;
                    db.SubmitChanges();

                    
                }
            }
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }
    }
}