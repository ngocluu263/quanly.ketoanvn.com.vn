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
    public partial class popup_quy_trinh_khac_dau : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        int _id = 0;
        int status = 0;
        int _type = 0;
        SendMail sm = new SendMail();
        UnitData unitdata = new UnitData();
        ProfileData _ProjectData = new ProfileData();
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            status = Utils.CIntDef(Request.QueryString["status"]);
            _type = Utils.CIntDef(Request.QueryString["type"]);
            if (!IsPostBack)
                LoadStatus();
        }

        #region Funtion
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
                Save_EmployManager(status + 1);
                SaveStatusMain();
                //Gửi cho kinh doanh
                var _getuser = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
                if (_getuser.Count > 0)
                {
                    SendEmail(Utils.CIntDef(_getuser[0].USER_ID), _type, "Hồ sơ đã nhận dấu");
                }
                //Remove Status Temp
                if (_getobj.Count > 0)
                {
                    var _obj = db.WORKFLOW_STATUS.Single(n => n.ID == _getobj[0].ID);
                    db.WORKFLOW_STATUS.DeleteOnSubmit(_obj);
                    db.SubmitChanges();
                }
            }
        }
        private void SaveStatusMain()
        {
            int _status = status + 1;
            var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
                _obj[0].PROF_STATUS = _status;
            db.SubmitChanges();
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
        private void Save_EmployManager(int st)
        {
            int _id_user = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
            int _id_group = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_GROUP_ID"]);
            if (_id_group == 4)
            {
                WORKFLOW_USER b = new WORKFLOW_USER();
                b.USER_ID = _id_user;
                b.PROF_ID = _id;
                b.DATE = DateTime.Now;
                b.WORK_STATUS = st;
                db.WORKFLOW_USERs.InsertOnSubmit(b);
                db.SubmitChanges();
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
        #endregion

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SaveStatus();
                ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
            }
            catch (Exception) { throw; }
        }
    }
}