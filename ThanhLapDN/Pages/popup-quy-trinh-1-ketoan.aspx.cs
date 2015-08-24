using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.IO;
using ThanhLapDN.Data;

namespace ThanhLapDN.Pages
{
    public partial class popup_quy_trinh_1_ketoan : System.Web.UI.Page
    {
        AppketoanDataContext db = new AppketoanDataContext();
        SendMail sm = new SendMail();
        UnitData unitdata = new UnitData();
        ProfileData _ProjectData = new ProfileData();
        int _id = 0;
        int status = 0;
        int _type = 0;
        string m_pathFile = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            status = Utils.CIntDef(Request.QueryString["status"]);
            _type = Utils.CIntDef(Request.QueryString["type"]);
            if (!IsPostBack)
            {
                Load_employ();
            }
        }

        #region Employ
        private void Load_employ()
        {
            var list = db.USERs.Where(n => (n.GROUP_ID == 11 || n.GROUP_ID == 7 || n.GROUP_ID == 13));
            ListUser.DataTextField = "USER_NAME";
            ListUser.DataValueField = "USER_ID";
            ListUser.DataSource = list;
            ListUser.DataBind();
            ListUser.Multiple = true;
        }
        private void Save_Employ()
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
            }
            else
            {

            }
        }
        #endregion

        #region Funtion
        private bool CheckEmpt()
        {
            List<ListItem> EmpSelected = ListUser.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
            if (txtTitleFile.Text == "" && FileUpload1.PostedFile.FileName != null && FileUpload1.PostedFile.FileName != "")
            {
                lblMsg.Text = "Xin nhập tên file đính kèm";
                return false;
            }
            else if (EmpSelected.Count <= 0)
            {
                lblMsg.Text = "Xin chọn người nhận yêu cầu";
                return false;
            }
            else return true;
        }
        private void CreateDirectory()
        {
            m_pathFile = "/File/Profile/" + _id + "/" + _id + "/";

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }

            Session["FileManager"] = m_pathFile;
        }
        private bool SendData()
        {
            try
            {
                if (_id > 0)
                {
                    if (FileUpload1.PostedFile.FileName != null && FileUpload1.PostedFile.FileName != "")
                    {
                        if (txtTitleFile.Text != "")
                            SaveAttach();
                        else
                            return false;
                    }
                    //if (status == 1)
                    //{
                    //    Save_NVXLHSBuoc1();
                    //}
                    Save_Employ();
                    SaveStatus();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private void SaveStatus()
        {
            int _status = status + 1;
            var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
                _obj[0].PROF_STATUS = _status;
            db.SubmitChanges();
        }
        private void SaveAttach()
        {
            CreateDirectory();
            /*--------------File-------------*/
            string img = string.Empty;
            string pathfile = string.Empty;
            string fullpathfile = string.Empty;
            string path = string.Empty;
            img = FileUpload1.PostedFile.FileName;
            /*------------------------------*/
            PROFILE_ATTACH i = new PROFILE_ATTACH();
            i.ATT_LINK = img;
            i.ATT_NAME = txtTitleFile.Text;
            i.PROF_ID = _id;
            if (Utils.CIntDef(Session["Userid"], 0) > 0)
                i.ATT_USER = Utils.CIntDef(Session["Userid"]);
            db.PROFILE_ATTACHes.InsertOnSubmit(i);
            db.SubmitChanges();

            var getlink = db.PROFILE_ATTACHes.OrderByDescending(n => n.ID).Take(1).ToList();
            if (getlink.Count > 0)
            {
                if (!string.IsNullOrEmpty(FileUpload1.PostedFile.FileName))
                {
                    pathfile = Server.MapPath("/File/Profile/" + _id + "/" + Utils.CStrDef(getlink[0].ID));
                    path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + _id + "/" + Utils.CStrDef(getlink[0].ID + "/" + img);
                    fullpathfile = pathfile + "/" + img;

                    if (!Directory.Exists(pathfile))
                    {
                        Directory.CreateDirectory(pathfile);
                    }
                    FileUpload1.PostedFile.SaveAs(fullpathfile);
                }
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
            if (CheckEmpt())
            {
                SendData();
                ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
            }
        }
    }
}