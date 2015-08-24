using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;
using System.IO;

namespace ThanhLapDN.Pages
{
    public partial class popup_quy_trinh_tra_hs : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        int _id = 0;
        int status = 0;
        int _type = 0;
        string m_pathFile = "";
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
                if (_type == 2)
                    txtMST.Visible = false;
                Load_employ();
                LoadStatus();
                rdbStatus_SelectedIndexChanged(sender, e);
            }
        }

        #region Funtion
        private bool CheckEmpt()
        {
            if (rdbStatus.SelectedValue == "3")
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
            else
            {
                if (txtMST.Text == "")
                {
                    lblMsg.Text = "Xin nhập mã số thuế";
                    lblMsg.Focus();
                    return false;
                }
                else return true;
            }
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
        #endregion

        #region Data
        private void SaveStatus()
        {
            //Delete_Workflow();
            if (rdbStatus.SelectedValue == "1")
            {
                SaveStatusMain(status + 1, 1);//kết thúc giai đoạn
                Save_EmployManager(status + 1);
                //Gửi cho kinh doanh
                var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
                if (_obj.Count > 0)
                {
                    SendEmail(Utils.CIntDef(_obj[0].USER_ID), _type, "Đã cấp giấy phép");
                }
            }
            else if (rdbStatus.SelectedValue == "2")
            {
                SaveStatusMain(status + 2, 1);//Có khắc dấu
                Save_EmployManager(status + 2);
                //Gửi cho kinh doanh
                var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
                if (_obj.Count > 0)
                {
                    SendEmail(Utils.CIntDef(_obj[0].USER_ID), _type, "Đã cấp giấy phép chờ khắc dấu");
                }
            }
            else
            {
                if (FileUpload1.PostedFile.FileName != null && FileUpload1.PostedFile.FileName != "")
                {
                    SaveAttach();
                }
                Save_Employ();
                SaveStatusMain(2, 1);//Quay lại bước 2
            }
        }
        private void LoadStatus()
        {
            var list = db.WORKFLOW_STATUS.Where(n => n.PROF_ID == _id && n.ST_STATUS == status).ToList();
            if (list.Count > 0)
            {
                rdbStatus.SelectedValue = Utils.CStrDef(list[0].ST_STATUS1);
            }
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
                    b.WORK_STATUS = 2;
                    b.WORK_FIELD1 = txtNote.Text;
                    db.WORKFLOW_USERs.InsertOnSubmit(b);
                    db.SubmitChanges();
                    SendEmail(Utils.CIntDef(i.Value), _type, "Giai đoạn 2: Soạn HS");
                }
            }
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
                b.WORK_FIELD1 = txtNote.Text;
                db.WORKFLOW_USERs.InsertOnSubmit(b);
                db.SubmitChanges();
            }
        }
        private void Load_employ()
        {
            var list = db.USERs.Where(n => (n.GROUP_ID == 5));
            ListUser.DataTextField = "USER_NAME";
            ListUser.DataValueField = "USER_ID";
            ListUser.DataSource = list;
            ListUser.DataBind();
            ListUser.Multiple = true;
        }
        private bool Delete_Workflow()
        {
            try
            {
                var _obj = db.WORKFLOW_USERs.Where(n => n.PROF_ID == _id).ToList();
                db.WORKFLOW_USERs.DeleteAllOnSubmit(_obj);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void SendEmail(int idUserRecive,int type, string str)
        {
            try
            {
                int idUser = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
                sm.SendEmail(idUser, idUserRecive, type, str, _ProjectData.Get_NameCompany(_id));
            }
            catch (Exception) { throw; }
        }
        private void SaveStatusMain(int _status, int level)
        {
            var _obj = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
            {
                _obj[0].PROF_STATUS = _status;
                _obj[0].PROF_LEVEL = level;
                if (_type == 1)
                    _obj[0].PROF_TAXCODE = txtMST.Text;
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
        #endregion

        #region Event
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
            if (rdbStatus.SelectedValue == "3")
            {
                liNote.Text = "";
                ListUser.Visible = txtTitleFile.Visible = FileUpload1.Visible = iUs.Visible = true;
                txtMST.Visible = false;
            }
            else
            {
                if (rdbStatus.SelectedValue == "1")
                    liNote.Text = "<p>Hoàn tất hồ sơ và kết thúc quy trình<br/><i style='color:red;'>(Thông báo khách hàng thời gian nhận kết quả)</i></p>";
                else
                    liNote.Text = "<p>Chuyển tiếp giai đoạn, chờ đóng dấu<br/><i style='color:red;'>(Thông báo khách hàng thời gian nhận kết quả)</i></p>";
                ListUser.Visible = txtTitleFile.Visible = FileUpload1.Visible = iUs.Visible = false;
                txtMST.Visible = true;
            }
        }
        #endregion
    }
}