using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using vpro.functions;

namespace ThanhLapDN.Pages
{
    public partial class popup_edit_file : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        int _id = 0;
        int _id_prof = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            _id_prof = Utils.CIntDef(Request.QueryString["id_prof"]);
            if (!IsPostBack)
                Load_File();
        }

        #region Funtion
        private void DeleteAllFilesInFolder(string folderpath)
        {
            try
            {
                foreach (var f in System.IO.Directory.GetFiles(folderpath))
                    System.IO.File.Delete(f);
                foreach (var f in System.IO.Directory.GetDirectories(folderpath))
                {
                    foreach (var i in System.IO.Directory.GetFiles(f))
                        System.IO.File.Delete(i);
                    System.IO.Directory.Delete(f);
                }
                if (Directory.Exists(folderpath))
                {
                    Directory.Delete(folderpath);
                }
            }
            catch (Exception ex) { }
        }
        public void Load_filedinhkem(string file, string nameFile)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + _id_prof + "/" + _id + "/" + Utils.CStrDef(file);
            lifile.Text = "<a href='" + path + "' title='" + nameFile + "'>" + nameFile + "</a>";
        }
        #endregion

        #region Data
        private void Save_File()
        {
            /*--------------File-------------*/
            string img = string.Empty;
            string pathfile = string.Empty;
            string fullpathfile = string.Empty;
            //string path = string.Empty;
            img = FileUpload1.PostedFile.FileName;
            /*------------------------------*/
            var _obj = db.PROFILE_ATTACHes.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
            {
                _obj[0].ATT_LINK = img;
                _obj[0].ATT_NAME = txtTitleFile.Text;
                if(Utils.CIntDef(Session["Userid"],0) > 0)
                    _obj[0].ATT_USER = Utils.CIntDef(Session["Userid"]);
                db.SubmitChanges();
            }

            if (_id > 0)
            {
                if (!string.IsNullOrEmpty(FileUpload1.PostedFile.FileName))
                {
                    pathfile = Server.MapPath("/File/Profile/" + _id_prof + "/" + Utils.CStrDef(_id));
                    //path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + _id_prof + "/" + Utils.CStrDef(_id + "/" + img);
                    fullpathfile = pathfile + "/" + img;
                    DeleteAllFilesInFolder(pathfile);

                    if (!Directory.Exists(pathfile))
                    {
                        Directory.CreateDirectory(pathfile);
                    }
                    FileUpload1.PostedFile.SaveAs(fullpathfile);
                }
            }
        }
        private void Dalete_File()
        {
            string pathfile = Server.MapPath("/File/Profile/" + _id_prof + "/" + Utils.CStrDef(_id));
            DeleteAllFilesInFolder(pathfile);

            var _obj = db.PROFILE_ATTACHes.Where(n => n.ID == _id).Single();
            db.PROFILE_ATTACHes.DeleteOnSubmit(_obj);
            db.SubmitChanges();
        }
        private void Load_File()
        {
            var _obj = db.PROFILE_ATTACHes.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
            {
                txtTitleFile.Text = _obj[0].ATT_NAME;
                Load_filedinhkem(_obj[0].ATT_LINK, _obj[0].ATT_NAME);
            }
        }
        #endregion

        #region Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save_File();
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Dalete_File();
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }
        #endregion
    }
}