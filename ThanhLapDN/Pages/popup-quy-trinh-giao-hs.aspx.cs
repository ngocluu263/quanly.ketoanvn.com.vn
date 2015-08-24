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
    public partial class popup_quy_trinh_giao_hs : System.Web.UI.Page
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
            lblMsg.Text = "";
            _id = Utils.CIntDef(Request.QueryString["id"]);
            status = Utils.CIntDef(Request.QueryString["status"]);
            _type = Utils.CIntDef(Request.QueryString["type"]);
            if (!IsPostBack)
            {
                LoadStatus();
                rdbStatus_SelectedIndexChanged(sender, e);
            }
        }

        #region Funtion
        private bool SaveStatus()
        {
            var _getobj = db.WORKFLOW_STATUS.Where(n => n.PROF_ID == _id && n.ST_STATUS == status).ToList();
            if (rdbStatus.SelectedValue != "2")
            {
                if (_getobj.Count > 0)
                {
                    _getobj[0].ST_STATUS1 = Utils.CIntDef(rdbStatus.SelectedValue);
                    db.SubmitChanges();
                    return true;
                }
                else
                {
                    WORKFLOW_STATUS _obj = new WORKFLOW_STATUS();
                    _obj.ST_STATUS1 = Utils.CIntDef(rdbStatus.SelectedValue);
                    _obj.ST_STATUS = status;
                    _obj.PROF_ID = _id;
                    db.WORKFLOW_STATUS.InsertOnSubmit(_obj);
                    db.SubmitChanges();
                    return true;
                }
            }
            else
            {
                if (Save_Employ())
                {
                    SaveStatusMain();
                    //Remove Status Temp
                    if (_getobj.Count > 0)
                    {
                        var _obj = db.WORKFLOW_STATUS.Single(n => n.ID == _getobj[0].ID);
                        db.WORKFLOW_STATUS.DeleteOnSubmit(_obj);
                        db.SubmitChanges();
                    }
                    return true;
                }
                else
                {
                    lblMsg.Text = "Xin chọn tên nhân viên nhận!";
                    return false;
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

        #region Employ
        private void Load_employ()
        {
            var list = db.USERs.Where(n => (n.GROUP_ID == 8 || status == 0));
            ListUser.DataTextField = "USER_NAME";
            ListUser.DataValueField = "USER_ID";
            ListUser.DataSource = list;
            ListUser.DataBind();
            ListUser.Multiple = true;
        }
        private bool Save_Employ()
        {
            List<ListItem> EmpSelected = ListUser.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
            if (EmpSelected.Count > 0)
            {
                foreach (var i in EmpSelected)
                {
                    int _i = Utils.CIntDef(i.Value);
                    WORKFLOW_USER b = new WORKFLOW_USER();
                    b.USER_ID = _i;
                    b.PROF_ID = _id;
                    b.DATE = DateTime.Now;
                    b.WORK_STATUS = status + 1;
                    b.WORK_FIELD1 = txtNote.Text;
                    db.WORKFLOW_USERs.InsertOnSubmit(b);
                    db.SubmitChanges();
                    SendEmail(Utils.CIntDef(i.Value), _type, unitdata.Namestatus(status, _type));
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveStatus())
                {
                    ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
                }
            }
            catch (Exception) { throw; }
        }

        protected void rdbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _rdb = Utils.CIntDef(rdbStatus.SelectedValue);
            if (_rdb == 2)
            {
                iUser.Visible = txtNote.Visible = true;
                liTitle.Text = "Nhân viên nộp hồ sơ lên sở";
                Load_employ();
            }
            else
            {
                iUser.Visible = txtNote.Visible = false;
            }
        }
    }
}