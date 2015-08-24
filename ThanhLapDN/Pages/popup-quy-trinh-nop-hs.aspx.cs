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
    public partial class popup_quy_trinh_nop_hs : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        int _id = 0;
        int status = 0;
        int _type = 0;
        string m_pathFile = "";
        SendMail sm = new SendMail();
        UnitData unitdata = new UnitData();
        ProfileData _ProjectData = new ProfileData();
        public DateTime fromDate = new DateTime();
        public DateTime toDate = new DateTime();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
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
        private bool CheckEmpt()
        {
            fromDate = pickdate_Begin.returnDate;
            toDate = pickdate_End.returnDate;
            if (rdbStatus.SelectedValue == "2")
            {
                if (txtTitleFile.Text == "" && FileUpload1.PostedFile.FileName != null && FileUpload1.PostedFile.FileName != "")
                {
                    lblMsg.Text = "Xin nhập tên file đính kèm";
                    return false;
                }
                else if (fromDate.Date < DateTime.Now.AddDays(-30))
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
                if (FileUpload1.PostedFile.FileName != null && FileUpload1.PostedFile.FileName != "")
                {
                    SaveAttach();
                }
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
            //Gửi về quản lý
            var _getUser = db.USERs.Where(n => n.GROUP_ID == 4).ToList();
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
            if (rdbStatus.SelectedValue == "2")
            {
                txtTitleFile.Visible = FileUpload1.Visible = iDate.Visible = true;
                pickdate_Begin.returnDate = Convert.ToDateTime("01/01/2000");
                fromDate = pickdate_Begin.returnDate;
                pickdate_End.returnDate = Convert.ToDateTime("01/01/2000");
                toDate = pickdate_End.returnDate;
            }
            else
            {
                txtTitleFile.Visible = FileUpload1.Visible = iDate.Visible = false;
            }
        }
    }
}