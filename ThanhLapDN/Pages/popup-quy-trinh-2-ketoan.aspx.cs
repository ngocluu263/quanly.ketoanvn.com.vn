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
    public partial class popup_quy_trinh_2_ketoan : System.Web.UI.Page
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
                Load_Data();
            }
        }

        #region Employ
        private void Load_employ()
        {
            var list = db.USERs.Where(n => (n.GROUP_ID == getGUserRecevice(status) || status == 0));
            ListUser.DataTextField = "USER_NAME";
            ListUser.DataValueField = "USER_ID";
            ListUser.DataSource = list;
            ListUser.DataBind();
            ListUser.Multiple = true;
        }
        private void Load_employById(int _idGroup, DropDownList _ddl, string _text)
        {
            var list = db.USERs.Where(n => (n.GROUP_ID == _idGroup));
            _ddl.DataTextField = "USER_NAME";
            _ddl.DataValueField = "USER_ID";
            _ddl.DataSource = list;
            _ddl.DataBind();
            ListItem l = new ListItem(_text, "0", true);
            _ddl.Items.Insert(0, l);
            _ddl.SelectedValue = "0";
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
        public int getGUserRecevice(int _status)
        {
            int type = 0;
            switch (_status)
            {
                case 2: type = 7;
                    liTitle.Text = "Nhân viên hành chánh";
                    break;//Nhân viên hành chánh
            }
            return type;
        }
        #endregion

        #region Data
        private void Load_Data()
        {
            var obj = _ProjectData.GetById(_id);
            if (obj != null)
            {
                txtTenCTy.Text = obj.PROF_NAME;
                txtMST.Text = obj.PROF_TAXCODE;
                txtTruSoChinh.Text = obj.PROF_ADDRESS;
                txtTongVonGop.Text = obj.PROF_TOTAL_CAPITAL.Value.ToString("###,##0");
                txtVonPhapDinh.Text = obj.PROF_CAPITAL.Value.ToString("###,##0");
                txtSDT.Text = obj.PROF_PHONE;
                txtEmail.Text = obj.PROF_EMAIL;
            }
        }
        private bool Update_Data()
        {
            try
            {
                var i = _ProjectData.GetById(_id);
                if (i != null)
                {
                    i.PROF_NAME = txtTenCTy.Text;
                    i.PROF_TAXCODE = txtMST.Text;
                    i.PROF_ADDRESS = txtTruSoChinh.Text;
                    i.PROF_TOTAL_CAPITAL = Utils.CDecDef(txtTongVonGop.Text.Replace(",", ""), 0);
                    i.PROF_CAPITAL = Utils.CDecDef(txtVonPhapDinh.Text.Replace(",", ""), 0);
                    i.PROF_PHONE = txtSDT.Text;
                    i.PROF_EMAIL = txtEmail.Text;

                    _ProjectData.Update(i);
                }
                return true;
            }
            catch (Exception) { return false; }
        }
        #endregion

        #region Funtion
        private int CheckedBox(CheckBox _chk)
        {
            if (_chk.Checked)
                return 1;
            else
                return 0;
        }
        private int SelectedUser(DropDownList _ddl)
        {
            return Utils.CIntDef(_ddl.SelectedValue);
        }
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
            {
                _obj[0].PROF_STATUS = _status;
                _obj[0].HAVE_DK_TKNH = CheckedBox(chk1);
                _obj[0].HAVE_DK_GTGT_TT = CheckedBox(chk2);
                _obj[0].HAVE_DK_GTGT_KT = CheckedBox(chk3);
                _obj[0].HAVE_HD = CheckedBox(chk4);
            }
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
                if (Update_Data())
                {
                    SendData();
                    ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
                }
                else { lblMsg.Text = "Lỗi phát sinh trong quá trình cập nhật!"; }
            }
        }
    }
}