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
    public partial class popup_quy_trinh_ky_hs_ketoan : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        int _id = 0;
        int status = 0;
        int _type = 0;
        SendMail sm = new SendMail();
        UnitData unitdata = new UnitData();
        ProfileData _ProjectData = new ProfileData();
        public DateTime fromDate = new DateTime();
        public DateTime toDate = new DateTime();
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            status = Utils.CIntDef(Request.QueryString["status"]);
            _type = Utils.CIntDef(Request.QueryString["type"]);
            if (!IsPostBack)
            {
                LoadStatus();
                //Load_employ();
                rdbStatus_SelectedIndexChanged(sender, e);
            }
        }

        #region Funtion
        private bool CheckEmpt()
        {
            fromDate = pickdate_Begin.returnDate;
            toDate = pickdate_End.returnDate;
            if (rdbStatus.SelectedValue == "2")
            {
                if (fromDate.Date < DateTime.Now.AddDays(-30))
                {
                    lblMsg.Text = "Ngày nộp phải lớn hơn ngày hiện tại";
                    return false;
                }
                else if (toDate.Date < DateTime.Now.AddDays(-30))
                {
                    lblMsg.Text = "Ngày lấy phải lớn hơn ngày hiện tại";
                    return false;
                }
                else return true;
            }
            else return true;
        }
        private void SaveStatus()
        {
            var _getobj = db.WORKFLOW_STATUS.Where(n => n.PROF_ID == _id && n.ST_STATUS == status).ToList();
            if (rdbStatus.SelectedValue != "2")
            {
                if (_getobj.Count > 0)
                {
                    _getobj[0].ST_STATUS1 = Utils.CIntDef(rdbStatus.SelectedValue);
                    db.SubmitChanges();
                }
                else
                {
                    WORKFLOW_STATUS _obj = new WORKFLOW_STATUS();
                    _obj.ST_STATUS1 = Utils.CIntDef(rdbStatus.SelectedValue);
                    _obj.ST_STATUS = status;
                    _obj.PROF_ID = _id;
                    db.WORKFLOW_STATUS.InsertOnSubmit(_obj);
                    db.SubmitChanges();
                }
            }
            else
            {
                Save_Employ();
                SaveStatusMain();
                //Remove Status Temp
                if (_getobj.Count > 0)
                {
                    var _obj = db.WORKFLOW_STATUS.Single(n => n.ID == _getobj[0].ID);
                    db.WORKFLOW_STATUS.DeleteOnSubmit(_obj);
                    db.SubmitChanges();
                }
            }
        }
        private bool LoadStatus()
        {
            var list = db.WORKFLOW_STATUS.Where(n => n.PROF_ID == _id && n.ST_STATUS == status).ToList();
            if (list.Count > 0)
            {
                rdbStatus.SelectedValue = Utils.CStrDef(list[0].ST_STATUS1);
                return true;
            }
            else return false;
        }
        private void Save_Employ()
        {
            //Gửi về cho nhân viên xử lý hồ sơ - hành chánh
            var _getUser = db.USERs.Where(n => n.GROUP_ID == 11).ToList();
            for (int i = 0; i < _getUser.Count; i++)
            {
                WORKFLOW_USER b = new WORKFLOW_USER();
                b.USER_ID = _getUser[i].USER_ID;
                b.PROF_ID = _id;
                b.DATE = DateTime.Now;
                b.WORK_STATUS = status + 1;
                db.WORKFLOW_USERs.InsertOnSubmit(b);
                db.SubmitChanges();

                SendEmail(Utils.CIntDef(_getUser[i].USER_ID), _type, unitdata.Namestatus(status, _type));
            }
        }
        private void SendEmail(int idUserRecive, int type, string str)
        {
            try
            {
                int idUser = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
                sm.SendEmail(idUser, idUserRecive, type, str, _ProjectData.Get_NameCompany(_id));
            }
            catch (Exception) { throw; }
        }
        private void SaveStatusMain()
        {
            int _status = status + 1;
            var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
            {
                _obj[0].PROF_STATUS = _status;
                _obj[0].NGAY_NHAN_HS = pickdate_Begin.returnDate;
                _obj[0].NGAY_TRA_HS = pickdate_End.returnDate;
            }
            db.SubmitChanges();
        }
        #endregion

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckEmpt())
                {
                    SaveStatus();
                    ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
                }
            }
            catch (Exception) { throw; }
        }

        protected void rdbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            liNote.Text = "1/ Đã nhận: thông báo nhân viên đã nhận hồ sơ.<br/>2/ Đã giao khách: đã giao hồ sơ cho khách ký.<br />3/ Đã ký xong: khách đã ký và nộp hồ sơ lên thuế.";
            if (rdbStatus.SelectedValue != "2")
            {
                txtNote.Visible = iUs.Visible = ListUser.Visible = iDate.Visible = false;
            }
            else
            {
                liNote.Text = "<b>Hồ sơ khách đã ký và đã nộp lên thuế!</b><br/><i>Xin vui lòng chọn ngày nộp và ngày trả kết quả bên dưới.</i>";
                txtNote.Visible = iDate.Visible = true;
                iUs.Visible = ListUser.Visible = false;
                pickdate_Begin.returnDate = Convert.ToDateTime("01/01/2000");
                fromDate = pickdate_Begin.returnDate;
                pickdate_End.returnDate = Convert.ToDateTime("01/01/2000");
                toDate = pickdate_End.returnDate;
            }
        }
    }
}